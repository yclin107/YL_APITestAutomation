using System.Text.Json;


namespace API.Core.Helpers
{
    public static class ConfigSetup
    {
        private static readonly string Env = Environment.GetEnvironmentVariable("TEST_ENV")?.ToLower() ?? "dev";
        private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "Config", Env);
        private static Lazy<Dictionary<string, TenantConfig>> TenantConfigs = new(() => LoadTenantConfigs());
        private static Lazy<Dictionary<string, TenantUsers>> TenatUsers = new(() => LoadTenantUsers());

        public static UserConfig GetUser(string tenant, string userId)
        {
            if (!TenatUsers.Value.TryGetValue(tenant, out var tenantUsers))
            {
                throw new Exception($"Tenant '{tenant}' not found in env '{Env}'");
            }

            if (!tenantUsers.Users.TryGetValue(userId, out var user))
            {
                throw new Exception($"User '{userId}' not found for tenant '{tenant}' in env '{Env}'");
            }

            var password = !string.IsNullOrWhiteSpace(user.PasswordEnvVar)
                ? Environment.GetEnvironmentVariable(user.PasswordEnvVar)
                : null;

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Password not found for user '{userId}' in tenant '{tenant}' in env '{Env}'");
            }

            return new UserConfig
            {
                Username = user.Username,
                LoginId = user.LoginId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordEnvVar = password,
                TempPassword = user.TempPassword,
                DefaultTimekeeperIndex = user.DefaultTimekeeperIndex,
                DefaultTimekeeperNumber = user.DefaultTimekeeperNumber
            };
        }

        public static TenantConfig GetTenantConfig(string tenant)
        {
            if (!TenantConfigs.Value.TryGetValue(tenant, out var tenatConfig))
            {
                throw new Exception($"Tenant '{tenant}' not found in env '{Env}'");
            }

            return new TenantConfig
            {
                Elite3EClientId = tenatConfig.Elite3EClientId,
                PPSClientId = tenatConfig.PPSClientId,
                Elite3EApiUrl = tenatConfig.Elite3EApiUrl,
                ProformaApiUrl = tenatConfig.ProformaApiUrl,
                OAuthScope = tenatConfig.OAuthScope ?? Array.Empty<string>(),
                TenantId = tenatConfig.TenantId,
                AppId = tenatConfig.AppId,
                PPSScope = tenatConfig.PPSScope,
                RedirectUri = tenatConfig.RedirectUri
            };
        }

        private static Dictionary<string, TenantConfig> LoadTenantConfigs()
        {
            if (!Directory.Exists(ConfigPath))
            {
                throw new DirectoryNotFoundException($"Missing config folder: {ConfigPath}");
            }

            var tenantFiles = Directory.GetFiles(ConfigPath, "*.config.json");
            var tenants = new Dictionary<string, TenantConfig>(StringComparer.OrdinalIgnoreCase);

            foreach (var file in tenantFiles)
            {
                var tenantName = Path.GetFileNameWithoutExtension(file).Split('.')[0];
                var json = File.ReadAllText(file);
                var tenantConfig = JsonSerializer.Deserialize<TenantConfig>(json);
                if (tenantConfig != null)
                {
                    tenants[tenantName] = tenantConfig;
                }
            }
            return tenants;
        }

        private static Dictionary<string, TenantUsers> LoadTenantUsers()
        {
            if (!Directory.Exists(ConfigPath))
            {
                throw new DirectoryNotFoundException($"Missing config folder: {ConfigPath}");
            }

            var tenantUserFiles = Directory.GetFiles(ConfigPath, "*.users.json");
            var tenantUsers = new Dictionary<string, TenantUsers>(StringComparer.OrdinalIgnoreCase);

            foreach (var file in tenantUserFiles)
            {
                var tenantName = Path.GetFileNameWithoutExtension(file).Split('.')[0];
                var json = File.ReadAllText(file);
                var tenantUserlist = JsonSerializer.Deserialize<TenantUsers>(json);
                if (tenantUserlist != null)
                {
                    tenantUsers[tenantName] = tenantUserlist;
                }
            }
            return tenantUsers;
        }

        // DTOs

        public class TenantUsers
        {
            public Dictionary<string, UserConfig> Users { get; set; } = new();
        }

        public class UserConfig
        {
            public string Username { get; set; } = string.Empty;
            public string LoginId { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string PasswordEnvVar { get; set; } = string.Empty;
            public string TempPassword { get; set; } = string.Empty;
            public string DefaultTimekeeperIndex { get; set; } = string.Empty;
            public string DefaultTimekeeperNumber { get; set; } = string.Empty;
        }

        public class TenantConfig
        {
            public string Elite3EClientId { get; set; } = string.Empty;
            public string PPSClientId { get; set; } = string.Empty;
            public string Elite3EApiUrl { get; set; } = string.Empty;
            public string ProformaApiUrl { get; set; } = string.Empty;
            public string[] OAuthScope { get; set; } = [];
            public string TenantId { get; set; } = string.Empty;
            public string AppId { get; set; } = string.Empty;
            public string RedirectUri { get; set; } = string.Empty;
            public string PPSScope { get; set; } = string.Empty;
        }
    }
}
