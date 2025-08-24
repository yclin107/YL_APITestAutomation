using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Core.Services.OpenAPI
{
    public class TestProtectionManager
    {
        private readonly string _protectionPath;

        public TestProtectionManager()
        {
            _protectionPath = Path.Combine(AppContext.BaseDirectory, "Config", "OpenAPI", "test-protection.json");
            Directory.CreateDirectory(Path.GetDirectoryName(_protectionPath)!);
        }

        public async Task<bool> IsTestModifiedAsync(string filePath, string originalContent)
        {
            var protection = await LoadProtectionDataAsync();
            var fileKey = GetFileKey(filePath);

            if (!protection.ContainsKey(fileKey))
            {
                // First time generating this file
                await SaveOriginalHashAsync(filePath, originalContent);
                return false;
            }

            var currentHash = ComputeHash(originalContent);
            var storedData = protection[fileKey];

            // If current content matches our stored original hash, it hasn't been modified
            return currentHash != storedData.OriginalHash;
        }

        public bool ShouldOverwriteModifiedTest(string filePath)
        {
            Console.WriteLine($"⚠️  The file '{Path.GetFileName(filePath)}' has been manually modified.");
            Console.WriteLine("Your custom changes will be lost if you continue.");
            Console.WriteLine();
            Console.Write("Do you want to overwrite your changes? (y/n): ");
            
            var response = Console.ReadLine()?.ToLower();
            var shouldOverwrite = response == "y" || response == "yes";

            if (shouldOverwrite)
            {
                Console.WriteLine("✅ File will be overwritten with new generated content.");
            }
            else
            {
                Console.WriteLine("🛡️  File will be preserved. Skipping generation for this file.");
            }

            return shouldOverwrite;
        }

        public async Task SaveOriginalHashAsync(string filePath, string content)
        {
            var protection = await LoadProtectionDataAsync();
            var fileKey = GetFileKey(filePath);

            protection[fileKey] = new TestProtectionData
            {
                OriginalHash = ComputeHash(content),
                LastGenerated = DateTime.UtcNow,
                FilePath = filePath
            };

            await SaveProtectionDataAsync(protection);
        }

        public void CreateBackup(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var backupDir = Path.Combine(Path.GetDirectoryName(filePath)!, "Backups");
            Directory.CreateDirectory(backupDir);

            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupPath = Path.Combine(backupDir, $"{fileName}_backup_{timestamp}{extension}");

            File.Copy(filePath, backupPath);
            Console.WriteLine($"📋 Backup created: {backupPath}");
        }

        private async Task<Dictionary<string, TestProtectionData>> LoadProtectionDataAsync()
        {
            if (!File.Exists(_protectionPath))
                return new Dictionary<string, TestProtectionData>();

            try
            {
                var json = await File.ReadAllTextAsync(_protectionPath);
                return JsonSerializer.Deserialize<Dictionary<string, TestProtectionData>>(json) 
                       ?? new Dictionary<string, TestProtectionData>();
            }
            catch
            {
                return new Dictionary<string, TestProtectionData>();
            }
        }

        private async Task SaveProtectionDataAsync(Dictionary<string, TestProtectionData> protection)
        {
            var json = JsonSerializer.Serialize(protection, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_protectionPath, json);
        }

        private string GetFileKey(string filePath)
        {
            return Path.GetRelativePath(AppContext.BaseDirectory, filePath).Replace('\\', '/');
        }

        private string ComputeHash(string content)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(content);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private class TestProtectionData
        {
            public string OriginalHash { get; set; } = string.Empty;
            public DateTime LastGenerated { get; set; }
            public string FilePath { get; set; } = string.Empty;
        }
    }
}