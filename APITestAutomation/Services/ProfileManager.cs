using APITestAutomation.Models.ProfileModels;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace APITestAutomation.Services
{
    public class ProfileManager
    {
        private readonly string _profilesPath;
        private readonly string _configPath;

        public ProfileManager()
        {
            // Point to the test project profiles directory
            var currentDir = AppContext.BaseDirectory;
            var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            _profilesPath = Path.Combine(solutionRoot, "APITestAutomationTest", "Profiles");
            _configPath = Path.Combine(AppContext.BaseDirectory, "Config", "profiles-config.json");
            
            // Ensure the profiles directory exists
            var fullProfilesPath = Path.GetFullPath(_profilesPath);
            Directory.CreateDirectory(fullProfilesPath);
            Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);
        }

        public async Task<List<string>> GetAvailableProfilesAsync()
        {
            var profiles = new List<string>();
            
            if (!Directory.Exists(_profilesPath))
                return profiles;

            foreach (var teamDir in Directory.GetDirectories(_profilesPath))
            {
                var teamName = Path.GetFileName(teamDir);
                foreach (var envDir in Directory.GetDirectories(teamDir))
                {
                    var envName = Path.GetFileName(envDir);
                    foreach (var file in Directory.GetFiles(envDir, "*.json"))
                    {
                        var tenantId = Path.GetFileNameWithoutExtension(file);
                        profiles.Add($"{teamName}/{envName}/{tenantId}");
                    }
                }
            }
            
            return profiles;
        }

        public async Task<TenantProfile?> LoadProfileAsync(string team, string environment, string tenantId, string? masterPassword = null)
        {
            var filePath = Path.Combine(_profilesPath, team, environment, $"{tenantId}.json");
            if (!File.Exists(filePath))
                return null;

            var content = await File.ReadAllTextAsync(filePath);
            
            // Check if file is encrypted
            if (IsEncrypted(content))
            {
                if (string.IsNullOrEmpty(masterPassword))
                    throw new InvalidOperationException("Profile is encrypted but no master password provided");
                
                content = DecryptContent(content, masterPassword);
            }

            return JsonSerializer.Deserialize<TenantProfile>(content);
        }

        public async Task SaveProfileAsync(TenantProfile profile, string team, string environment, string tenantId, string? masterPassword = null)
        {
            var content = JsonSerializer.Serialize(profile, new JsonSerializerOptions { WriteIndented = true });
            
            if (!string.IsNullOrEmpty(masterPassword))
            {
                content = EncryptContent(content, masterPassword);
            }

            var dirPath = Path.Combine(_profilesPath, team, environment);
            Directory.CreateDirectory(dirPath);
            
            var filePath = Path.Combine(dirPath, $"{tenantId}.json");
            await File.WriteAllTextAsync(filePath, content);
        }

        public async Task EncryptAllProfilesAsync(string masterPassword)
        {
            if (!Directory.Exists(_profilesPath))
                return;
            
            foreach (var teamDir in Directory.GetDirectories(_profilesPath))
            {
                var teamName = Path.GetFileName(teamDir);
                foreach (var envDir in Directory.GetDirectories(teamDir))
                {
                    var envName = Path.GetFileName(envDir);
                    foreach (var file in Directory.GetFiles(envDir, "*.json"))
                    {
                        var tenantId = Path.GetFileNameWithoutExtension(file);
                        var profile = await LoadProfileAsync(teamName, envName, tenantId);
                        if (profile != null)
                        {
                            await SaveProfileAsync(profile, teamName, envName, tenantId, masterPassword);
                            Console.WriteLine($"✅ Encrypted profile: {teamName}/{envName}/{tenantId}");
                        }
                    }
                }
            }
        }

        public async Task DecryptAllProfilesAsync(string masterPassword)
        {
            if (!Directory.Exists(_profilesPath))
                return;
            
            foreach (var teamDir in Directory.GetDirectories(_profilesPath))
            {
                var teamName = Path.GetFileName(teamDir);
                foreach (var envDir in Directory.GetDirectories(teamDir))
                {
                    var envName = Path.GetFileName(envDir);
                    foreach (var file in Directory.GetFiles(envDir, "*.json"))
                    {
                        var tenantId = Path.GetFileNameWithoutExtension(file);
                        try
                        {
                            var profile = await LoadProfileAsync(teamName, envName, tenantId, masterPassword);
                            if (profile != null)
                            {
                                await SaveProfileAsync(profile, teamName, envName, tenantId); // Save without encryption
                                Console.WriteLine($"✅ Decrypted profile: {teamName}/{envName}/{tenantId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to decrypt {teamName}/{envName}/{tenantId}: {ex.Message}");
                        }
                    }
                    }
                }
            }
        }

        public UserProfile GetRandomUser(TenantProfile profile)
        {
            if (!profile.Users.Any())
                throw new InvalidOperationException("No users available in profile");

            var random = new Random();
            return profile.Users[random.Next(profile.Users.Count)];
        }

        public List<UserProfile> GetUsersForParallelExecution(TenantProfile profile, int threadCount)
        {
            if (profile.Users.Count < threadCount)
            {
                // If we don't have enough users, repeat users
                var users = new List<UserProfile>();
                for (int i = 0; i < threadCount; i++)
                {
                    users.Add(profile.Users[i % profile.Users.Count]);
                }
                return users;
            }

            return profile.Users.Take(threadCount).ToList();
        }

        private bool IsEncrypted(string content)
        {
            // Simple check - encrypted content won't be valid JSON
            try
            {
                JsonSerializer.Deserialize<object>(content);
                return false; // Valid JSON, not encrypted
            }
            catch
            {
                return true; // Not valid JSON, likely encrypted
            }
        }

        private string EncryptContent(string content, string password)
        {
            using var aes = Aes.Create();
            var key = GenerateKey(password);
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var encryptedBytes = encryptor.TransformFinalBlock(contentBytes, 0, contentBytes.Length);

            // Combine IV and encrypted data
            var result = new byte[aes.IV.Length + encryptedBytes.Length];
            Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
            Array.Copy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        private string DecryptContent(string encryptedContent, string password)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedContent);
            
            using var aes = Aes.Create();
            var key = GenerateKey(password);
            aes.Key = key;

            // Extract IV
            var iv = new byte[aes.IV.Length];
            Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            // Extract encrypted data
            var encrypted = new byte[encryptedBytes.Length - iv.Length];
            Array.Copy(encryptedBytes, iv.Length, encrypted, 0, encrypted.Length);

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
            
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private byte[] GenerateKey(string password)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}