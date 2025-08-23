using APITestAutomation.Services.OpenAPI;
using APITestAutomation.Services;
using System.Diagnostics;
using System.Text;

namespace APITestAutomation.Services.OpenAPI
{
    public class InteractiveMenu
    {
        private readonly OpenApiTestManager _manager;
        private readonly ProfileManager _profileManager;

        public InteractiveMenu()
        {
            _manager = new OpenApiTestManager();
            _profileManager = new ProfileManager();
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                ShowHeader();
                ShowMainMenu();

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        await HandleRunTests();
                        break;
                    case "2":
                        await HandleGenerateReport();
                        break;
                    case "3":
                        await HandleAutoGenerateTests();
                        break;
                    case "4":
                        await HandleProfileManagement();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowHeader()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    Elite Test Generator                      ║");
            Console.WriteLine("║                     API Test Automation                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine();
            Console.WriteLine("1. 🧪 Run Tests");
            Console.WriteLine("2. 📊 Generate Allure Report");
            Console.WriteLine("3. 🤖 Auto Generate Tests");
            Console.WriteLine("4. 👤 Profile Management");
            Console.WriteLine("5. 🚪 Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-5): ");
        }

        private async Task HandleRunTests()
        {
            Console.Clear();
            Console.WriteLine("=== Run Tests ===");
            Console.WriteLine();

            var profiles = await _profileManager.GetAvailableProfilesAsync();
            if (!profiles.Any())
            {
                Console.WriteLine("❌ No profiles found. Please create a profile first.");
                PauseForUser();
                return;
            }

            Console.WriteLine("Available profiles:");
            for (int i = 0; i < profiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {profiles[i]}");
            }

            Console.Write($"Select profile (1-{profiles.Count}): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > profiles.Count)
            {
                Console.WriteLine("❌ Invalid selection.");
                PauseForUser();
                return;
            }

            var selectedProfile = profiles[choice - 1];
            
            Console.Write("Enter number of parallel threads (default: 1): ");
            var threadsInput = Console.ReadLine()?.Trim();
            var threads = string.IsNullOrEmpty(threadsInput) ? 1 : int.Parse(threadsInput);

            Console.Write("Enter test filter (optional, e.g., Category=Generated): ");
            var filter = Console.ReadLine()?.Trim();

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"test --logger:allure --parallel {threads}" + 
                                   (string.IsNullOrEmpty(filter) ? "" : $" --filter \"{filter}\""),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        WorkingDirectory = GetTestProjectPath()
                    }
                };

                process.StartInfo.Environment["TEST_PROFILE"] = selectedProfile;
                
                Console.WriteLine($"🚀 Running tests with profile: {selectedProfile}");
                Console.WriteLine($"⚡ Parallel threads: {threads}");
                Console.WriteLine();

                process.Start();
                
                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                
                await process.WaitForExitAsync();

                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Errors:");
                    Console.WriteLine(error);
                }

                Console.WriteLine($"✅ Tests completed with exit code: {process.ExitCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error running tests: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task HandleGenerateReport()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Allure Report ===");
            Console.WriteLine();

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "allure",
                        Arguments = "serve allure-results",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                Console.WriteLine("🚀 Starting Allure report server...");
                process.Start();
                
                Console.WriteLine("📊 Allure report should open in your browser automatically.");
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
                
                if (!process.HasExited)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generating report: {ex.Message}");
                Console.WriteLine("Make sure Allure is installed: npm install -g allure-commandline");
                PauseForUser();
            }
        }

        private async Task HandleAutoGenerateTests()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Auto Generate Tests ===");
                Console.WriteLine();
                Console.WriteLine("1. 🚀 Generate Tests from OpenAPI Specification");
                Console.WriteLine("2. 🔍 Detect Changes in Specification");
                Console.WriteLine("3. 👁️  Preview Tests (without generating)");
                Console.WriteLine("4. 📁 Show Available Specification Files");
                Console.WriteLine("5. 🔙 Back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice (1-5): ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        await HandleGenerateFlow();
                        break;
                    case "2":
                        await HandleDetectFlow();
                        break;
                    case "3":
                        await HandlePreviewFlow();
                        break;
                    case "4":
                        ShowSpecificationFiles();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task HandleProfileManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Profile Management ===");
                Console.WriteLine();
                Console.WriteLine("1. 📋 Show Available Profiles");
                Console.WriteLine("2. 🔒 Encrypt All Profiles");
                Console.WriteLine("3. 🔓 Decrypt All Profiles");
                Console.WriteLine("4. 🔙 Back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice (1-4): ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        await ShowProfiles();
                        break;
                    case "2":
                        await EncryptProfiles();
                        break;
                    case "3":
                        await DecryptProfiles();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ShowProfiles()
        {
            Console.Clear();
            Console.WriteLine("=== Available Profiles ===");
            Console.WriteLine();

            var profiles = await _profileManager.GetAvailableProfilesAsync();
            if (!profiles.Any())
            {
                Console.WriteLine("No profiles found.");
            }
            else
            {
                Console.WriteLine("Found profiles:");
                Console.WriteLine();
                for (int i = 0; i < profiles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {profiles[i]}");
                }
            }

            PauseForUser();
        }

        private async Task EncryptProfiles()
        {
            Console.Clear();
            Console.WriteLine("=== Encrypt Profiles ===");
            Console.WriteLine();

            Console.Write("Enter master password: ");
            var password = ReadPassword();
            Console.WriteLine();

            try
            {
                await _profileManager.EncryptAllProfilesAsync(password);
                Console.WriteLine("✅ All profiles encrypted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error encrypting profiles: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task DecryptProfiles()
        {
            Console.Clear();
            Console.WriteLine("=== Decrypt Profiles ===");
            Console.WriteLine();

            Console.Write("Enter master password: ");
            var password = ReadPassword();
            Console.WriteLine();

            try
            {
                await _profileManager.DecryptAllProfilesAsync(password);
                Console.WriteLine("✅ All profiles decrypted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error decrypting profiles: {ex.Message}");
            }

            PauseForUser();
        }

        private string ReadPassword()
        {
            var password = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            return password.ToString();
        }

        private async Task HandleGenerateFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Tests ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            var baseUrl = GetBaseUrl();

            try
            {
                Console.WriteLine("Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);

                Console.WriteLine("Detecting changes...");
                var changes = await _manager.DetectChangesAsync(specPath, baseUrl);

                if (changes.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine(_manager.FormatChangeSummary(changes));
                    Console.WriteLine();
                    Console.Write("Do you want to apply these changes? (y/n): ");
                    var response = Console.ReadLine()?.ToLower();

                    if (response != "y" && response != "yes")
                    {
                        Console.WriteLine("Operation cancelled.");
                        PauseForUser();
                        return;
                    }
                }

                Console.WriteLine("Generating tests...");
                var result = await _manager.GenerateTestsAsync(spec, "default", "default", true);
                Console.WriteLine();
                Console.WriteLine("✅ " + result);

                if (changes.Any())
                {
                    var reportPath = await _manager.GenerateChangeReportAsync(changes);
                    Console.WriteLine($"📊 Change report generated: {reportPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task HandleDetectFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Detect Changes ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            var baseUrl = GetBaseUrl();

            try
            {
                Console.WriteLine("Detecting changes...");
                var changes = await _manager.DetectChangesAsync(specPath, baseUrl);

                Console.WriteLine();
                Console.WriteLine(_manager.FormatChangeSummary(changes));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task HandlePreviewFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Preview Tests ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            var baseUrl = GetBaseUrl();

            try
            {
                Console.WriteLine("Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);

                Console.WriteLine("=== TEST GENERATION PREVIEW ===");
                Console.WriteLine($"API: {spec.Document.Info?.Title ?? "Unknown"}");
                Console.WriteLine($"Version: {spec.Document.Info?.Version ?? "Unknown"}");
                Console.WriteLine($"Base URL: {spec.BaseUrl}");
                Console.WriteLine();

                var endpointsByTag = spec.EndpointTests.Values
                    .GroupBy(e => e.Tags.FirstOrDefault() ?? "General")
                    .ToDictionary(g => g.Key, g => g.ToList());

                var totalEndpoints = 0;
                var totalTests = 0;

                foreach (var tagGroup in endpointsByTag)
                {
                    var tag = tagGroup.Key;
                    var endpoints = tagGroup.Value;
                    var testsPerEndpoint = 4; // Positive, Unauthorized, Missing params, Schema validation
                    var tagTests = endpoints.Count * testsPerEndpoint;

                    Console.WriteLine($"📁 {tag}: {endpoints.Count} endpoints → {tagTests} tests");
                    totalEndpoints += endpoints.Count;
                    totalTests += tagTests;
                }

                Console.WriteLine();
                Console.WriteLine($"📊 SUMMARY:");
                Console.WriteLine($"   Total Endpoints: {totalEndpoints}");
                Console.WriteLine($"   Total Tests: {totalTests}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            PauseForUser();
        }

        private void ShowSpecificationFiles()
        {
            Console.Clear();
            Console.WriteLine("=== Available Specification Files ===");
            Console.WriteLine();

            var specDir = GetSpecificationsDirectory();
            if (!Directory.Exists(specDir))
            {
                Console.WriteLine("No specifications directory found.");
                PauseForUser();
                return;
            }

            var files = Directory.GetFiles(specDir, "*.*")
                .Where(f => f.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || 
                           f.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) ||
                           f.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!files.Any())
            {
                Console.WriteLine("No OpenAPI specification files found in Specifications folder.");
            }
            else
            {
                Console.WriteLine("Found specification files:");
                Console.WriteLine();
                for (int i = 0; i < files.Count; i++)
                {
                    var fileName = Path.GetFileName(files[i]);
                    var fileInfo = new FileInfo(files[i]);
                    Console.WriteLine($"{i + 1}. {fileName} ({fileInfo.Length} bytes, modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm})");
                }
            }

            PauseForUser();
        }

        private string GetSpecificationsDirectory()
        {
            // Always point to APITestAutomation/Specifications from the current project
            var currentDir = AppContext.BaseDirectory;
            // Navigate to the APITestAutomation project root, then to Specifications
            var projectRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            var specificationsPath = Path.Combine(projectRoot, "APITestAutomation", "Specifications");
            return Path.GetFullPath(specificationsPath);
        }

        private string GetTestProjectPath()
        {
            var currentDir = AppContext.BaseDirectory;
            var projectRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            var testProjectPath = Path.Combine(projectRoot, "APITestAutomationTest");
            return Path.GetFullPath(testProjectPath);
        }

        private string GetLastUsedSpecPath()
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, "Config", "OpenAPI", "last-used-spec.txt");
            if (File.Exists(configPath))
            {
                return File.ReadAllText(configPath).Trim();
            }
            return string.Empty;
        }

        private void SaveLastUsedSpecPath(string specPath)
        {
            var configDir = Path.Combine(AppContext.BaseDirectory, "Config", "OpenAPI");
            Directory.CreateDirectory(configDir);
            var configPath = Path.Combine(configDir, "last-used-spec.txt");
            File.WriteAllText(configPath, specPath);
        }

        private string GetSpecificationPath()
        {
            var lastUsed = GetLastUsedSpecPath();
            if (!string.IsNullOrEmpty(lastUsed) && File.Exists(lastUsed))
            {
                Console.WriteLine($"📄 Last used specification: {Path.GetFileName(lastUsed)}");
                Console.WriteLine($"📂 Path: {lastUsed}");
                Console.Write($"Press Enter to continue with '{Path.GetFileName(lastUsed)}' or select a different file (b): ");
            }
            else
            {
                Console.Write("No previous specification found. Press Enter to browse available files: ");
            }
            
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                if (!string.IsNullOrEmpty(lastUsed) && File.Exists(lastUsed))
                {
                    Console.WriteLine($"✅ Using: {Path.GetFileName(lastUsed)}");
                    return lastUsed;
                }
                return BrowseSpecificationFiles();
            }

            if (input.ToLower() == "b")
            {
                return BrowseSpecificationFiles();
            }

            if (!File.Exists(input))
            {
                Console.WriteLine($"❌ File not found: {input}");
                PauseForUser();
                return string.Empty;
            }

            SaveLastUsedSpecPath(input);
            return input;
        }

        private string BrowseSpecificationFiles()
        {
            var specDir = GetSpecificationsDirectory();
            if (!Directory.Exists(specDir))
            {
                Console.WriteLine("❌ Specifications directory not found.");
                return string.Empty;
            }

            var files = Directory.GetFiles(specDir, "*.*")
                .Where(f => f.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || 
                           f.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) ||
                           f.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!files.Any())
            {
                Console.WriteLine("❌ No OpenAPI specification files found.");
                return string.Empty;
            }

            Console.WriteLine();
            Console.WriteLine("Available files:");
            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write($"Select file (1-{files.Count}): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= files.Count)
            {
                var selectedPath = files[choice - 1];
                SaveLastUsedSpecPath(selectedPath);
                return selectedPath;
            }

            Console.WriteLine("❌ Invalid selection.");
            return string.Empty;
        }

        private string? GetBaseUrl()
        {
            Console.Write("Enter base URL (optional, press Enter to use from spec): ");
            var input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? null : input;
        }

        private void PauseForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}