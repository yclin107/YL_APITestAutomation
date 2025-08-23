using System.Diagnostics;
using APITestAutomationTest.Services;

namespace APITestAutomation.Services.OpenAPI
{
    public class InteractiveMenuWithArrows
    {
        private readonly OpenApiTestManager _manager;
        private readonly ProfileManager _profileManager;

        public InteractiveMenuWithArrows()
        {
            _manager = new OpenApiTestManager();
            _profileManager = new ProfileManager();
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                var mainOptions = new[]
                {
                    "🧪 Run Tests",
                    "📊 Generate Allure Report", 
                    "🤖 Auto Generate Tests",
                    "👤 Profile Management",
                    "🚪 Exit"
                };

                var choice = ShowMenuWithArrows("Elite Test Generator - API Test Automation", mainOptions);

                switch (choice)
                {
                    case 0: // Run Tests
                        await HandleRunTests();
                        break;
                    case 1: // Generate Report
                        await HandleGenerateReport();
                        break;
                    case 2: // Auto Generate Tests
                        await HandleAutoGenerateTests();
                        break;
                    case 3: // Profile Management
                        await HandleProfileManagement();
                        break;
                    case 4: // Exit
                        Console.WriteLine("Goodbye!");
                        return;
                }
            }
        }

        private int ShowMenuWithArrows(string title, string[] options)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                
                // Show header
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║{title.PadLeft((64 + title.Length) / 2).PadRight(62)}║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.WriteLine();

                // Show options
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($" ► {options[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"   {options[i]}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Use ↑↓ arrows to navigate, Enter to select");

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == options.Length - 1 ? 0 : selectedIndex + 1;
                        break;
                }
            } while (key != ConsoleKey.Enter);

            return selectedIndex;
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
                Console.WriteLine($"📁 Expected location: {Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "APITestAutomationTest", "Profiles"))}");
                PauseForUser();
                return;
            }

            var selectedProfileIndex = ShowMenuWithArrows("Select Profile", profiles.ToArray());
            var selectedProfile = profiles[selectedProfileIndex];
            
            Console.Clear();
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
                        Arguments = $"test --logger:allure" + 
                                   (threads > 1 ? $" --parallel {threads}" : "") +
                                   (string.IsNullOrEmpty(filter) ? "" : $" --filter \"{filter}\""),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        WorkingDirectory = GetTestProjectPath()
                    }
                };

                process.StartInfo.Environment["TEST_PROFILE"] = selectedProfile;
                
                Console.WriteLine($"🚀 Running tests with profile: {selectedProfile}");
                if (threads > 1) Console.WriteLine($"⚡ Parallel threads: {threads}");
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

            var allureResultsPath = Path.Combine(GetSolutionRoot(), "allure-results");
            
            if (!Directory.Exists(allureResultsPath))
            {
                Console.WriteLine("❌ No allure-results directory found. Please run tests first.");
                PauseForUser();
                return;
            }

            try
            {
                Console.WriteLine("🚀 Opening Allure report in new terminal...");
                
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/c start cmd /k \"allure serve allure-results\"",
                        UseShellExecute = true,
                        WorkingDirectory = GetSolutionRoot(),
                        CreateNoWindow = false
                    }
                };

                process.Start();
                Console.WriteLine("📊 New terminal opened with Allure report server.");
                Console.WriteLine("The report should open in your browser automatically.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generating report: {ex.Message}");
                Console.WriteLine("Make sure Allure is installed: npm install -g allure-commandline");
            }
            
            PauseForUser();
        }

        private async Task HandleAutoGenerateTests()
        {
            var options = new[]
            {
                "🚀 Generate Tests from OpenAPI Specification",
                "🔍 Detect Changes in Specification",
                "👁️ Preview Tests (without generating)",
                "📁 Show Available Specification Files",
                "🔙 Back to Main Menu"
            };

            while (true)
            {
                var choice = ShowMenuWithArrows("Auto Generate Tests", options);

                switch (choice)
                {
                    case 0:
                        await HandleGenerateFlow();
                        break;
                    case 1:
                        await HandleDetectFlow();
                        break;
                    case 2:
                        await HandlePreviewFlow();
                        break;
                    case 3:
                        ShowSpecificationFiles();
                        break;
                    case 4:
                        return;
                }
            }
        }

        private async Task HandleProfileManagement()
        {
            var options = new[]
            {
                "📋 Show Available Profiles",
                "🔒 Encrypt All Profiles", 
                "🔓 Decrypt All Profiles",
                "🔙 Back to Main Menu"
            };

            while (true)
            {
                var choice = ShowMenuWithArrows("Profile Management", options);

                switch (choice)
                {
                    case 0:
                        await ShowProfiles();
                        break;
                    case 1:
                        await EncryptProfiles();
                        break;
                    case 2:
                        await DecryptProfiles();
                        break;
                    case 3:
                        return;
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
                Console.WriteLine($"📁 Expected location: {Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "APITestAutomationTest", "Profiles"))}");
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

        // Copy all the other methods from InteractiveMenu.cs
        private async Task HandleGenerateFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Tests ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            try
            {
                Console.WriteLine("Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath);

                Console.WriteLine("Detecting changes...");
                var changes = await _manager.DetectChangesAsync(specPath);

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

            try
            {
                Console.WriteLine("Detecting changes...");
                var changes = await _manager.DetectChangesAsync(specPath);

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

            try
            {
                Console.WriteLine("Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath);

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

        private string ReadPassword()
        {
            var password = new System.Text.StringBuilder();
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

        private string GetSpecificationsDirectory()
        {
            var currentDir = AppContext.BaseDirectory;
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

        private string GetSolutionRoot()
        {
            var currentDir = AppContext.BaseDirectory;
            var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            return Path.GetFullPath(solutionRoot);
        }

        private string GetSpecificationPath()
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

            var fileNames = files.Select(Path.GetFileName).ToArray();
            var selectedIndex = ShowMenuWithArrows("Select Specification File", fileNames);
            
            return files[selectedIndex];
        }

        private void PauseForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}