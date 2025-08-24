using System.Diagnostics;
using API.Core.Helpers;
using API.Core.Services.AzureDevOps;

namespace API.Core.Services.OpenAPI
{
    public class InteractiveMenuWithArrows
    {
        private readonly OpenApiTestManager _manager;
        private readonly ProfileManager _profileManager;
        private readonly AzureDevOpsService _azureDevOpsService;

        public InteractiveMenuWithArrows()
        {
            _manager = new OpenApiTestManager();
            _profileManager = new ProfileManager();
            _azureDevOpsService = new AzureDevOpsService();
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
                    "🔗 Azure DevOps Integration"
                };

                var choice = ShowMenuWithArrows("Elite Test Generator - API Test Automation", mainOptions);

                if (choice == -1) // Esc pressed on main menu
                    return;

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
                    case 4: // Azure DevOps Integration
                        await HandleAzureDevOpsIntegration();
                        break;
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
                Console.WriteLine("Use ↑↓ arrows to navigate, Enter to select, Esc to go back");

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex == 0 ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex == options.Length - 1 ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Escape:
                        return -1; // Indicate back/exit
                }
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

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
                Console.WriteLine($"📁 Expected location: {Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "API.TestBase", "Config", "Profiles"))}");
                PauseForUser();
                return;
            }

            var selectedProfileIndex = ShowMenuWithArrows("Select Profile", profiles.ToArray());
            if (selectedProfileIndex == -1) return; // Esc pressed
            
            var selectedProfile = profiles[selectedProfileIndex];
            
            Console.Clear();
            Console.Write("Enter number of parallel threads (default: 1): ");
            var threadsInput = Console.ReadLine()?.Trim();
            var threads = string.IsNullOrEmpty(threadsInput) ? 1 : int.Parse(threadsInput);

            Console.Write("Enter test filter (optional, e.g., Category=Generated): ");
            var filter = Console.ReadLine()?.Trim();

            try
            {
                // Clean allure-results before running tests
                var allureResultsPath = Path.Combine(GetSolutionRoot(), "allure-results");
                if (Directory.Exists(allureResultsPath))
                {
                    Directory.Delete(allureResultsPath, true);
                    Directory.CreateDirectory(allureResultsPath);
                }

                var arguments = $"test --project \"{GetTestProjectPath()}\" --logger \"allure;LogLevel=trace\"";
                
                if (threads > 1)
                {
                    arguments += $" --maxcpucount:{threads}";
                }
                
                if (!string.IsNullOrEmpty(filter))
                {
                    arguments += $" --filter \"{filter}\"";
                }

                if (threads == 1)
                {
                    // Single thread execution
                    await RunSingleThreadTests(selectedProfile, filter, allureResultsPath);
                }
                else
                {
                    // Multi-thread execution - run each thread in separate terminal
                    await RunMultiThreadTests(selectedProfile, filter, threads, allureResultsPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error running tests: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task RunSingleThreadTests(string selectedProfile, string filter, string allureResultsPath)
        {
            var batchContent = $@"@echo off
cd /d ""{GetSolutionRoot()}""
set TEST_PROFILE={selectedProfile}
echo 🚀 Running tests with profile: {selectedProfile}
echo 📊 Results will be saved to: {allureResultsPath}
echo.
dotnet test ""{(string.IsNullOrEmpty(filter) ? "" : $" --filter \"{filter}\"")}
echo.
echo ✅ Tests completed! Press any key to close...
pause > nul";

            var batchPath = Path.Combine(Path.GetTempPath(), "run_tests_single.bat");
            await File.WriteAllTextAsync(batchPath, batchContent);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c start \"API Tests - Single Thread\" cmd /k \"{batchPath}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false
                }
            };

            process.Start();
            Console.WriteLine("🚀 Opening tests in new terminal window...");
        }

        private async Task RunMultiThreadTests(string selectedProfile, string filter, int threads, string allureResultsPath)
        {
            Console.WriteLine($"🚀 Starting {threads} parallel test threads...");
            Console.WriteLine($"📊 Each thread will save results to: {allureResultsPath}");
            Console.WriteLine($"👥 Each thread will use a different user from the profile");
            Console.WriteLine();

            var processes = new List<Process>();
            
            // Load profile to check available users
            try
            {
                var parts = selectedProfile.Split('/');
                var profileManager = new ProfileManager();
                var masterPassword = Environment.GetEnvironmentVariable("MASTER_PASSWORD");
                var profile = await profileManager.LoadProfileAsync(parts[0], parts[1], parts[2], masterPassword);
                
                if (profile == null)
                {
                    Console.WriteLine("❌ Could not load profile for user validation");
                    return;
                }
                
                var availableUsers = profile.Users.Values.ToList();
                Console.WriteLine($"👥 Profile has {availableUsers.Count} users available");
                
                if (threads > availableUsers.Count)
                {
                    Console.WriteLine($"⚠️  Warning: {threads} threads requested but only {availableUsers.Count} users available");
                    Console.WriteLine($"   Users will be reused in round-robin fashion");
                }
                
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Could not validate users: {ex.Message}");
            }

            for (int i = 1; i <= threads; i++)
            {
                // Each process will get a different user via round-robin assignment
                var threadFilter = string.IsNullOrEmpty(filter) ? "" : filter;
                
                var threadId = $"Thread{i}";
                
                var batchContent = $@"@echo off
cd /d ""{GetSolutionRoot()}""
set TEST_PROFILE={selectedProfile}
set ALLURE_RESULTS_DIRECTORY={allureResultsPath}
set THREAD_ID={i}
set ALLURE_RESULTS_DIRECTORY={allureResultsPath}
title API Tests - {threadId}
echo 🧵 {threadId}: Running tests with profile: {selectedProfile} 
echo 📊 {threadId}: Results will be saved to: {allureResultsPath}
echo ⚡ {threadId}: Thread {i} of {threads}
echo 👤 {threadId}: User will be assigned automatically via round-robin
echo.
dotnet test API.TestBase --settings NUnit.runsettings --logger ""allure;LogLevel=trace""{(string.IsNullOrEmpty(threadFilter) ? "" : $" --filter \"{threadFilter}\"")}
echo.
echo ✅ {threadId}: Tests completed! Press any key to close...
pause > nul";

                var batchPath = Path.Combine(Path.GetTempPath(), $"run_tests_thread_{i}.bat");
                await File.WriteAllTextAsync(batchPath, batchContent);

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = $"/c start \"API Tests - Thread {i}\" cmd /k \"{batchPath}\"",
                        UseShellExecute = true,
                        CreateNoWindow = false
                    }
                };

                process.Start();
                processes.Add(process);
                
                Console.WriteLine($"🧵 Thread {i}: Terminal opened");
                
                // Small delay between starting threads to avoid conflicts
                await Task.Delay(2000); // Increased delay for better user assignment
            }

            Console.WriteLine();
            Console.WriteLine($"✅ All {threads} test threads started!");
            Console.WriteLine("Each thread is running in its own terminal window with different users.");
            Console.WriteLine("Check each terminal to see which user is assigned to each thread.");
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
                "📁 Show Available Specification Files"
            };

            while (true)
            {
                var choice = ShowMenuWithArrows("Auto Generate Tests", options);

                if (choice == -1) // Esc pressed
                    return;

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
                }
            }
        }

        private async Task HandleProfileManagement()
        {
            var options = new[]
            {
                "📋 Show Available Profiles",
                "🔒 Encrypt All Profiles", 
                "🔓 Decrypt All Profiles"
            };

            while (true)
            {
                var choice = ShowMenuWithArrows("Profile Management", options);

                if (choice == -1) // Esc pressed
                    return;

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
                Console.WriteLine($"📁 Expected location: {Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "API.TestBase", "Config", "Profiles"))}");
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
                //Console.WriteLine("✅ All profiles encrypted successfully!");
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
                //Console.WriteLine("✅ All profiles decrypted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error decrypting profiles: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task HandleAzureDevOpsIntegration()
        {
            var options = new[]
            {
                "🔄 Sync OpenAPI Spec to Azure DevOps",
                "⚙️ Configure Azure DevOps Settings",
                "📋 View Sync History",
                "🧪 Test Azure DevOps Connection"
            };

            while (true)
            {
                var choice = ShowMenuWithArrows("Azure DevOps Integration", options);

                if (choice == -1) // Esc pressed
                    return;

                switch (choice)
                {
                    case 0:
                        await HandleSyncToAzureDevOps();
                        break;
                    case 1:
                        await HandleConfigureAzureDevOps();
                        break;
                    case 2:
                        await HandleViewSyncHistory();
                        break;
                    case 3:
                        await HandleTestAzureDevOpsConnection();
                        break;
                }
            }
        }

        private async Task HandleSyncToAzureDevOps()
        {
            Console.Clear();
            Console.WriteLine("=== Sync to Azure DevOps ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            try
            {
                Console.WriteLine("📖 Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath);

                Console.WriteLine($"🔍 Found {spec.EndpointTests.Count} endpoints to sync");
                Console.WriteLine($"📋 API: {spec.Document.Info?.Title ?? "Unknown"}");
                Console.WriteLine($"📌 Version: {spec.Document.Info?.Version ?? "Unknown"}");
                Console.WriteLine();

                Console.Write("Do you want to proceed with the sync? (y/n): ");
                var response = Console.ReadLine()?.ToLower();

                if (response != "y" && response != "yes")
                {
                    Console.WriteLine("❌ Sync cancelled.");
                    PauseForUser();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("🚀 Starting Azure DevOps synchronization...");
                
                var result = await _azureDevOpsService.SyncOpenApiSpecAsync(spec);

                Console.WriteLine();
                Console.WriteLine("✅ Synchronization completed!");
                Console.WriteLine();
                Console.WriteLine("📊 RESULTS:");
                Console.WriteLine($"   📖 Stories: {result.CreatedStories} created, {result.UpdatedStories} updated, {result.DeletedStories} deleted");
                Console.WriteLine($"   🧪 Test Cases: {result.CreatedTestCases} created, {result.UpdatedTestCases} updated, {result.DeletedTestCases} deleted");
                
                if (result.SyncedItems.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("🔗 Synced Work Items:");
                    foreach (var item in result.SyncedItems.Take(10)) // Show first 10
                    {
                        Console.WriteLine($"   {GetActionEmoji(item.Action)} {item.Type}: {item.Title} (ID: {item.Id})");
                    }
                    
                    if (result.SyncedItems.Count > 10)
                    {
                        Console.WriteLine($"   ... and {result.SyncedItems.Count - 10} more items");
                    }
                }

                if (result.Errors.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("❌ Errors:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"   • {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Sync failed: {ex.Message}");
            }

            PauseForUser();
        }

        private async Task HandleConfigureAzureDevOps()
        {
            Console.Clear();
            Console.WriteLine("=== Configure Azure DevOps Settings ===");
            Console.WriteLine();
            
            var configPath = Path.Combine(AppContext.BaseDirectory, "Config", "AzureDevOps", "devops-config.json");
            
            Console.WriteLine($"📁 Configuration file: {configPath}");
            Console.WriteLine();
            
            if (File.Exists(configPath))
            {
                Console.WriteLine("✅ Configuration file exists");
                Console.WriteLine("📝 You can edit the configuration manually or recreate it");
                Console.WriteLine();
                Console.WriteLine("Required settings:");
                Console.WriteLine("  • Organization URL (e.g., https://dev.azure.com/YourOrg)");
                Console.WriteLine("  • Project Name");
                Console.WriteLine("  • Personal Access Token (PAT)");
                Console.WriteLine("  • Area Path");
                Console.WriteLine("  • Iteration Path");
                Console.WriteLine();
                
                var options = new[] { "📝 Open config file location", "🔄 Recreate default config" };
                var choice = ShowMenuWithArrows("Configuration Options", options);
                
                if (choice == 0)
                {
                    try
                    {
                        var process = new System.Diagnostics.Process
                        {
                            StartInfo = new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "explorer",
                                Arguments = Path.GetDirectoryName(configPath),
                                UseShellExecute = true
                            }
                        };
                        process.Start();
                        Console.WriteLine("📂 Opened configuration folder");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Could not open folder: {ex.Message}");
                    }
                }
                else if (choice == 1)
                {
                    File.Delete(configPath);
                    // This will recreate the default config
                    var _ = new API.Core.Services.AzureDevOps.AzureDevOpsService();
                    Console.WriteLine("✅ Default configuration recreated");
                }
            }
            else
            {
                Console.WriteLine("❌ Configuration file not found");
                Console.WriteLine("Creating default configuration...");
                
                // This will create the default config
                var _ = new API.Core.Services.AzureDevOps.AzureDevOpsService();
                Console.WriteLine("✅ Default configuration created");
            }

            PauseForUser();
        }

        private async Task HandleViewSyncHistory()
        {
            Console.Clear();
            Console.WriteLine("=== Sync History ===");
            Console.WriteLine();
            Console.WriteLine("📋 This feature will show previous synchronization results");
            Console.WriteLine("🚧 Coming soon in future version");
            PauseForUser();
        }

        private async Task HandleTestAzureDevOpsConnection()
        {
            Console.Clear();
            Console.WriteLine("=== Test Azure DevOps Connection ===");
            Console.WriteLine();
            
            try
            {
                Console.WriteLine("🔌 Testing connection to Azure DevOps...");
                
                // This will test the connection by trying to create the service
                var service = new API.Core.Services.AzureDevOps.AzureDevOpsService();
                
                Console.WriteLine("✅ Connection test completed");
                Console.WriteLine("📝 Check the configuration if you encounter issues during sync");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection test failed: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("💡 Common issues:");
                Console.WriteLine("  • Invalid Personal Access Token (PAT)");
                Console.WriteLine("  • Incorrect Organization URL");
                Console.WriteLine("  • Network connectivity issues");
                Console.WriteLine("  • Missing permissions on Azure DevOps project");
            }

            PauseForUser();
        }

        private string GetActionEmoji(string action)
        {
            return action switch
            {
                "Created" => "✅",
                "Updated" => "📝",
                "Deleted" => "🗑️",
                _ => "📋"
            };
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
            var specificationsPath = Path.Combine(projectRoot, "API.TestBase", "Config", "OpenAPI");
            return Path.GetFullPath(specificationsPath);
        }

        private string GetTestProjectPath()
        {
            var currentDir = AppContext.BaseDirectory;
            var projectRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            var testProjectPath = Path.Combine(projectRoot, "API.TestBase");
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
            
            if (selectedIndex == -1) return string.Empty; // Esc pressed
            
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