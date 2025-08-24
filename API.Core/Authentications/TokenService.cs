using API.Core.Helpers;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static RestAssured.Dsl;


namespace API.Core.Authentications
{
    public static class TokenService
    {
        public static async Task<string> GetROPCToken(string tenant, ConfigSetup.UserConfig user)
        {
            var key = APITestAutomation.Helpers.TokenCache.BuildROPCTokenKey(tenant, user.LoginId);
            var cachedToken = APITestAutomation.Helpers.TokenCache.Get(key);
            if (!string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }
            var config = ConfigSetup.GetTenantConfig(tenant);
            var authority = $"https://login.microsoftonline.com/{config.TenantId}";

            var publicClientApp = PublicClientApplicationBuilder
                .Create(config.AppId)
                .WithAuthority(authority)
                .Build();

            var result = await publicClientApp.AcquireTokenByUsernamePassword(
                config.OAuthScope,
                user.LoginId,
                user.PasswordEnvVar
            ).ExecuteAsync();

            APITestAutomation.Helpers.TokenCache.Set(key, result.AccessToken);
            return result.AccessToken;

        }

        public static string PPSProformaToken(string tenant, ConfigSetup.UserConfig user)
        {
            var key = APITestAutomation.Helpers.TokenCache.BuildPPSProformaKey(tenant, user.LoginId);
            var cachedToken = APITestAutomation.Helpers.TokenCache.Get(key);
            if (!string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            var config = ConfigSetup.GetTenantConfig(tenant);

            var token = $"https://login.microsoftonline.com/{config.TenantId}/oauth2/v2.0/token";
            var ropcToken =  GetROPCToken(tenant, user).Result;
            string bodyParams = $"grant_type=password&username={user.LoginId}&password={user.PasswordEnvVar}&client_id={config.AppId}&acode={ropcToken}&scope={config.PPSScope}";

            var response = Given()
                 .ContentType("application/x-www-form-urlencoded")
                 .Body(bodyParams)
                 .Post(token)
                 .Then()
                 .Extract().Response();

            var raw = response.Content.ReadAsStringAsync().Result;
            var tokenResult = JObject.Parse(raw)["access_token"]?.ToString() ?? throw new Exception("No access token found in response");
            APITestAutomation.Helpers.TokenCache.Set(key, tokenResult);

            return tokenResult ?? throw new Exception("No token received");
        }
    }
}
