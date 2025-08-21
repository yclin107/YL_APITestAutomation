using APITestAutomation.Services.OpenAPI;

namespace APITestAutomation.Services.OpenAPI
{
    public class InteractiveMenu
    {
        private readonly OpenApiTestManager _manager;

        public InteractiveMenu()
        {
            _manager = new OpenApiTestManager();
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
            Console.WriteLine("║                    OpenAPI Test Generator                    ║");
            Console.WriteLine("║                     API Test Automation                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine();
            Console.WriteLine("1. 🚀 Generate Tests from OpenAPI Specification");
            Console.WriteLine("2. 🔍 Detect Changes in Specification");
            Console.WriteLine("3. 👁️  Preview Tests (without generating)");
            Console.WriteLine("4. 📁 Show Available Specification Files");
            Console.WriteLine("5. 🚪 Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-5): ");
        }

        private async Task HandleGenerateFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Tests ===");
            Console.WriteLine();

            var specPath = GetSpecificationPath();
            if (string.IsNullOrEmpty(specPath)) return;

            var tenant = GetTenant();
            if (string.IsNullOrEmpty(tenant)) return;

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return;

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
                var result = await _manager.GenerateTestsAsync(spec, tenant, userId, true);
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

            var tenant = GetTenant();
            if (string.IsNullOrEmpty(tenant)) return;

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId)) return;

            var baseUrl = GetBaseUrl();

            try
            {
                Console.WriteLine("Loading OpenAPI specification...");
                var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);

                Console.WriteLine("Generating preview...");
                var preview = await _manager.GenerateTestsAsync(spec, tenant, userId, false);
                Console.WriteLine();
                Console.WriteLine(preview);
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

            var specDir = Path.Combine(AppContext.BaseDirectory, "Specifications");
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

        private string GetSpecificationPath()
        {
            Console.Write("Enter OpenAPI specification file path (or press Enter to browse): ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return BrowseSpecificationFiles();
            }

            if (!File.Exists(input))
            {
                Console.WriteLine($"❌ File not found: {input}");
                PauseForUser();
                return string.Empty;
            }

            return input;
        }

        private string BrowseSpecificationFiles()
        {
            var specDir = Path.Combine(AppContext.BaseDirectory, "Specifications");
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
                return files[choice - 1];
            }

            Console.WriteLine("❌ Invalid selection.");
            return string.Empty;
        }

        private string GetTenant()
        {
            Console.Write("Enter tenant (default: ptpd68r3nke7q5pnutzaaw): ");
            var input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? "ptpd68r3nke7q5pnutzaaw" : input;
        }

        private string GetUserId()
        {
            Console.Write("Enter user ID (default: PPSAutoTestUser0): ");
            var input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? "PPSAutoTestUser0" : input;
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