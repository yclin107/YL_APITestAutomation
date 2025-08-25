using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Core.Helpers
{
    public class ProfileManager
    {
        private readonly string _profilesPath;

        public ProfileManager()
        {
            // Point to the test project's Config/Profiles folder
            var currentDir = AppContext.BaseDirectory;
            var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Config", "Profiles");
            _profilesPath = Path.GetFullPath(_profilesPath);
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

            // Parse as raw JSON to preserve exact structure
            var options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            
            return JsonSerializer.Deserialize<TenantProfile>(content, options);
        }

        public async Task SaveProfileAsync(TenantProfile profile, string team, string environment, string tenantId, string? masterPassword = null)
        {
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var content = JsonSerializer.Serialize(profile, options);
            
            if (!string.IsNullOrEmpty(masterPassword))
            {
                content = EncryptContent(content, masterPassword);
            }

            var dirPath = Path.Combine(_profilesPath, team, environment);
            Directory.CreateDirectory(dirPath);
            
            var filePath = Path.Combine(dirPath, $"{tenantId}.json");
            await File.WriteAllTextAsync(filePath, content);
        }

        public async Task EncryptProfileAsync(string team, string environment, string tenantId, string masterPassword)
        {
            var filePath = Path.Combine(_profilesPath, team, environment, $"{tenantId}.json");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Profile file not found: {filePath}");

            var currentContent = await File.ReadAllTextAsync(filePath);
            
            // If already encrypted, skip
            if (IsEncrypted(currentContent))
            {
                Console.WriteLine($"⏭️  Profile {team}/{environment}/{tenantId} is already encrypted");
                return;
            }

            // Encrypt the current content exactly as it is
            var encryptedContent = EncryptContent(currentContent, masterPassword);
            await File.WriteAllTextAsync(filePath, encryptedContent);
        }

        public async Task DecryptProfileAsync(string team, string environment, string tenantId, string masterPassword)
        {
            var filePath = Path.Combine(_profilesPath, team, environment, $"{tenantId}.json");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Profile file not found: {filePath}");

            var currentContent = await File.ReadAllTextAsync(filePath);
            
            // If not encrypted, skip
            if (!IsEncrypted(currentContent))
            {
                Console.WriteLine($"⏭️  Profile {team}/{environment}/{tenantId} is already decrypted");
                return;
            }

            // Decrypt and save the exact original content
            var decryptedContent = DecryptContent(currentContent, masterPassword);
            await File.WriteAllTextAsync(filePath, decryptedContent);
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

        public UserProfile GetRandomUser(TenantProfile profile)
        {
            if (!profile.Users.Any())
                throw new InvalidOperationException("No users available in profile");

            var random = new Random();
            var users = profile.Users.Values.ToList();
            return users[random.Next(users.Count)];
        }

        public List<UserProfile> GetUsersForParallelExecution(TenantProfile profile, int threadCount)
        {
            var users = profile.Users.Values.ToList();
            if (users.Count < threadCount)
            {
                // If we don't have enough users, repeat users
                var result = new List<UserProfile>();
                for (int i = 0; i < threadCount; i++)
                {
                    result.Add(users[i % users.Count]);
                }
                return result;
            }

            return users.Take(threadCount).ToList();
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
                        try
                        {
                            await EncryptProfileAsync(teamName, envName, tenantId, masterPassword);
                            Console.WriteLine($"✅ Encrypted profile: {teamName}/{envName}/{tenantId}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to encrypt {teamName}/{envName}/{tenantId}: {ex.Message}");
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
                            await DecryptProfileAsync(teamName, envName, tenantId, masterPassword);
                            Console.WriteLine($"✅ Decrypted profile: {teamName}/{envName}/{tenantId}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to decrypt {teamName}/{envName}/{tenantId}: {ex.Message}");
                        }
                    }
                }
            }
        }

        private bool IsEncrypted(string content)
        {
            try
            {
                JsonDocument.Parse(content);
                return false; // Valid JSON, not encrypted
            }
            catch
            {
                return true; // Not valid JSON, likely encrypted
            }
        }

        private string EncryptContent(string content, string password)
        {
            // Validate that content is valid JSON before encrypting
            try
            {
                JsonDocument.Parse(content);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Content is not valid JSON: {ex.Message}");
            }

            using var aes = Aes.Create();
            var key = GenerateKey(password);
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var encryptedBytes = encryptor.TransformFinalBlock(contentBytes, 0, contentBytes.Length);

            var result = new byte[aes.IV.Length + encryptedBytes.Length];
            Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
            Array.Copy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        private string DecryptContent(string encryptedContent, string password)
        {
            byte[] encryptedBytes;
            try
            {
                encryptedBytes = Convert.FromBase64String(encryptedContent);
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException($"Invalid encrypted content format: {ex.Message}");
            }
            
            using var aes = Aes.Create();
            var key = GenerateKey(password);
            aes.Key = key;

            var iv = new byte[aes.IV.Length];
            Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            var encrypted = new byte[encryptedBytes.Length - iv.Length];
            Array.Copy(encryptedBytes, iv.Length, encrypted, 0, encrypted.Length);

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
            
            var decryptedContent = Encoding.UTF8.GetString(decryptedBytes);
            
            // Validate that decrypted content is valid JSON
            try
            {
                JsonDocument.Parse(decryptedContent);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Decrypted content is not valid JSON: {ex.Message}");
            }
            
            return decryptedContent;
        }

        private byte[] GenerateKey(string password)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public class TenantProfile
    {
        public string Elite3EClientId { get; set; } = string.Empty;
        public string PPSClientId { get; set; } = string.Empty;
        public string Elite3EApiUrl { get; set; } = string.Empty;
        public string ProformaApiUrl { get; set; } = string.Empty;
        public string[] OAuthScope { get; set; } = Array.Empty<string>();
        public string PPSScope { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string InstanceId { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string AuthorizationEndpoint { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public Dictionary<string, UserProfile> Users { get; set; } = new();
    }

    public class UserProfile
    {
        public string UserId => LoginId; // Use LoginId as UserId
        public string Username => $"{FirstName}{LastName}"; // Generate Username from names
        public string LoginId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DefaultTimekeeperIndex { get; set; } = string.Empty;
        public string DefaultTimekeeperNumber { get; set; } = string.Empty;
    }
}