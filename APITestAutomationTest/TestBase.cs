using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace APITestAutomationTest
{
    [AllureNUnit]
    [TestFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        [AllureBefore]
        public static void CleanupResultDirectory() =>
            AllureLifecycle.Instance.CleanupResultDirectory();

        protected string _tenat = "";
        protected string _userId = "";

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
}
