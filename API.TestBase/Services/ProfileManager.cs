using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Core.Helpers;

namespace API.TestBase.Services
{
    public class ProfileManager
    {
        private readonly string _profilesPath;

        public ProfileManager()
        {
            // Point to the Config/Profiles folder in API.TestBase
            var currentDir = AppContext.BaseDirectory;
            var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Config", "Profiles");
            _profilesPath = Path.GetFullPath(_profilesPath);
        }

        public async Task<TenantProfile?> LoadProfileAsync(string team, string environment, string tenantId, string? masterPassword = null)
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            return await coreProfileManager.LoadProfileAsync(team, environment, tenantId, masterPassword);
        }

        public async Task<List<string>> GetAvailableProfilesAsync()
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            return await coreProfileManager.GetAvailableProfilesAsync();
        }

        public UserProfile GetRandomUser(TenantProfile profile)
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            return coreProfileManager.GetRandomUser(profile);
        }

        public List<UserProfile> GetUsersForParallelExecution(TenantProfile profile, int threadCount)
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            return coreProfileManager.GetUsersForParallelExecution(profile, threadCount);
        }

        public async Task EncryptAllProfilesAsync(string masterPassword)
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            await coreProfileManager.EncryptAllProfilesAsync(masterPassword);
        }

        public async Task DecryptAllProfilesAsync(string masterPassword)
        {
            var coreProfileManager = new API.Core.Helpers.ProfileManager();
            await coreProfileManager.DecryptAllProfilesAsync(masterPassword);
        }
    }
}