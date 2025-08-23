using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using APITestAutomation.Models.ProfileModels;
using APITestAutomation.Services;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Collections.Concurrent;

namespace APITestAutomationTest
{
    [AllureNUnit]
    [TestFixture]
    public class TestBase
    {
        private static readonly ProfileManager _profileManager = new();
        private static readonly ConcurrentDictionary<int, TestContext> _threadContexts = new();
        private static TenantProfile? _currentProfile;
        private static readonly object _profileLock = new object();

        [OneTimeSetUp]
        [AllureBefore]
        public static void CleanupResultDirectory()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
            InitializeTestProfile();
        }

        private static void InitializeTestProfile()
        {
            lock (_profileLock)
            {
                if (_currentProfile != null) return;

                var profileName = Environment.GetEnvironmentVariable("TEST_PROFILE") ?? "default";
                var masterPassword = Environment.GetEnvironmentVariable("MASTER_PASSWORD");

                try
                {
                    _currentProfile = _profileManager.LoadProfileAsync(profileName, masterPassword).Result;
                    if (_currentProfile == null)
                    {
                        throw new InvalidOperationException($"Profile '{profileName}' not found. Available profiles: {string.Join(", ", _profileManager.GetAvailableProfilesAsync().Result)}");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to load profile '{profileName}': {ex.Message}");
                }
            }
        }

        protected string _tenat = "";
        protected string _userId = "";

        protected TestContext GetTestContext()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            
            return _threadContexts.GetOrAdd(threadId, _ =>
            {
                if (_currentProfile == null)
                    throw new InvalidOperationException("No profile loaded");

                var user = _profileManager.GetRandomUser(_currentProfile);
                return new TestContext
                {
                    TenantId = _currentProfile.TenantId,
                    UserId = user.UserId,
                    User = user,
                    Profile = _currentProfile
                };
            });
        }

        protected string GetBaseUrl()
        {
            return _currentProfile?.BaseUrl ?? throw new InvalidOperationException("No profile loaded");
        }

        protected string GetAuthToken(TestContext context)
        {
            // This would integrate with your existing token service
            // For now, returning a placeholder - you can integrate with your existing TokenService
            return APITestAutomationServices.Authentications.TokenService.PPSProformaToken(
                context.TenantId, 
                new APITestAutomation.Helpers.ConfigSetup.UserConfig
                {
                    LoginId = context.User.Username,
                    FirstName = context.User.FirstName,
                    LastName = context.User.LastName,
                    PasswordEnvVar = context.User.Password,
                    DefaultTimekeeperIndex = context.User.DefaultTimekeeperIndex,
                    DefaultTimekeeperNumber = context.User.DefaultTimekeeperNumber
                });
        }

        protected void InitContext(string tenat, string userId, string? feature = null)
        {
            _tenat = tenat;
            _userId = userId;

            AllureLifecycle.Instance.UpdateTestCase(tc =>
            {
                tc.labels.Add(Label.Suite(feature));
                tc.labels.Add(Label.Tag($"user: {_userId}"));
                tc.labels.Add(Label.Tag($"tenant: {_tenat}"));
            });
        }

        protected void AttachResponse(string title, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {              

                byte[] compressed;
                using (var ms = new MemoryStream())
                {
                    using (var gzip = new GZipStream(ms, CompressionLevel.Optimal))
                    using (var writer = new StreamWriter(gzip, Encoding.UTF8))
                        writer.Write(content);
                    compressed = ms.ToArray();
                }
                AllureApi.AddAttachment(title, "application/gzip", compressed);
            }
        }

        protected static string serializeToJson<T>(T value)
        {
            string json = JsonSerializer.Serialize(value, new JsonSerializerOptions
            { 
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                WriteIndented = true
            });         
            return json;
        }

        protected static readonly JsonSerializerOptions CachedJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public class TestContext
    {
        public string TenantId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public UserProfile User { get; set; } = new();
        public TenantProfile Profile { get; set; } = new();
    }
}
