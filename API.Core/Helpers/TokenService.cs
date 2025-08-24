using API.Core.Models.Token;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static RestAssured.Dsl;
using TokenCache = API.Core.Models.Token.TokenCache;
using API.Core.Helpers;

namespace API.Core.Helpers
{
    public static class TokenService
    {
        public static async Task<string> GetROPCToken(string tenant, UserConfig user)
        {
            var key = TokenCache.BuildROPCTokenKey(tenant, user.LoginId);
            var cachedToken = TokenCache.Get(key);
            if (!string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }
            
            // Get profile from environment
            var profileManager = new ProfileManager();
            var profile = await GetProfileFromEnvironment(profileManager);
            if (profile == null)
                throw new InvalidOperationException("No profile loaded");

            var authority = $"https://login.microsoftonline.com/{profile.TenantId}";

            var publicClientApp = PublicClientApplicationBuilder
                .Create(profile.AppId)
                .WithAuthority(authority)
                .Build();

            var result = await publicClientApp.AcquireTokenByUsernamePassword(
                profile.OAuthScope,
                user.LoginId,
                user.Password
            ).ExecuteAsync();

            TokenCache.Set(key, result.AccessToken);
            return result.AccessToken;

        }

        public static async Task<string> PPSProformaToken(string tenant, UserConfig user)
        {
            var key = TokenCache.BuildPPSProformaKey(tenant, user.LoginId);
            var cachedToken = TokenCache.Get(key);
            if (!string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            
            // Get profile from environment
            var profileManager = new ProfileManager();
            var profile = await GetProfileFromEnvironment(profileManager);
            if (profile == null)
                throw new InvalidOperationException("No profile loaded");

            var token = $"https://login.microsoftonline.com/{profile.TenantId}/oauth2/v2.0/token";
            var ropcToken = await GetROPCToken(tenant, user);
            string bodyParams = $"grant_type=password&username={user.LoginId}&password={user.Password}&client_id={profile.AppId}&acode={ropcToken}&scope={profile.PPSScope}";

            var response = Given()
                 .ContentType("application/x-www-form-urlencoded")
                 .Body(bodyParams)
                 .Post(token)
                 .Then()
                 .Extract().Response();

            var raw = response.Content.ReadAsStringAsync().Result;
            var tokenResult = JObject.Parse(raw)["access_token"]?.ToString() ?? throw new Exception("No access token found in response");
            TokenCache.Set(key, tokenResult);

            return tokenResult ?? throw new Exception("No token received");
        }
        
        private static async Task<TenantProfile?> GetProfileFromEnvironment(ProfileManager profileManager)
        {
            var profileManager = new ProfileManager();
            var profilePath = Environment.GetEnvironmentVariable("TEST_PROFILE");
            var masterPassword = Environment.GetEnvironmentVariable("MASTER_PASSWORD");
            
            if (string.IsNullOrEmpty(profilePath))
                return null;
                
            var parts = profilePath.Split('/');
            if (parts.Length != 3)
                return null;
                
            return await profileManager.LoadProfileAsync(parts[0], parts[1], parts[2], masterPassword);
        }
    }
    
    // UserConfig class for TokenService
    public class UserConfig
    {
        public string LoginId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DefaultTimekeeperIndex { get; set; } = string.Empty;
        public string DefaultTimekeeperNumber { get; set; } = string.Empty;
    }
}
