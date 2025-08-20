using APITestAutomation.Services.OpenAPI;

namespace APITestAutomation.Services.OpenAPI
{
    public class OpenApiTestCLI
    {
        private readonly OpenApiTestManager _manager;

        public OpenApiTestCLI()
        {
            _manager = new OpenApiTestManager();
        }

        public async Task RunAsync(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            var command = args[0].ToLower();

            try
            {
                switch (command)
                {
                    case "generate":
                        await HandleGenerateCommand(args);
                        break;
                    case "detect":
                        await HandleDetectCommand(args);
                        break;
                    case "preview":
                        await HandlePreviewCommand(args);
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        ShowHelp();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task HandleGenerateCommand(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: generate <spec-path> <tenant> <user-id> [base-url]");
                return;
            }

            var specPath = args[1];
            var tenant = args[2];
            var userId = args[3];
            var baseUrl = args.Length > 4 ? args[4] : null;

            Console.WriteLine("Loading OpenAPI specification...");
            var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);
            
            Console.WriteLine("Detecting changes...");
            var changes = await _manager.DetectChangesAsync(specPath, baseUrl);
            
            if (changes.Any())
            {
                Console.WriteLine(_manager.FormatChangeSummary(changes));
                Console.WriteLine("Do you want to apply these changes? (y/n)");
                var response = Console.ReadLine()?.ToLower();
                
                if (response != "y" && response != "yes")
                {
                    Console.WriteLine("Operation cancelled.");
                    return;
                }
            }
            
            Console.WriteLine("Generating tests...");
            var result = await _manager.GenerateTestsAsync(spec, tenant, userId, true);
            Console.WriteLine(result);
            
            if (changes.Any())
            {
                var reportPath = await _manager.GenerateChangeReportAsync(changes);
                Console.WriteLine($"Change report generated: {reportPath}");
            }
        }

        private async Task HandleDetectCommand(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: detect <spec-path> [base-url]");
                return;
            }

            var specPath = args[1];
            var baseUrl = args.Length > 2 ? args[2] : null;

            Console.WriteLine("Detecting changes...");
            var changes = await _manager.DetectChangesAsync(specPath, baseUrl);
            
            Console.WriteLine(_manager.FormatChangeSummary(changes));
        }

        private async Task HandlePreviewCommand(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: preview <spec-path> <tenant> <user-id> [base-url]");
                return;
            }

            var specPath = args[1];
            var tenant = args[2];
            var userId = args[3];
            var baseUrl = args.Length > 4 ? args[4] : null;

            Console.WriteLine("Loading OpenAPI specification...");
            var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);
            
            Console.WriteLine("Generating preview...");
            var preview = await _manager.GenerateTestsAsync(spec, tenant, userId, false);
            Console.WriteLine(preview);
        }

        private void ShowHelp()
        {
            Console.WriteLine(@"
OpenAPI Test Generator CLI

Commands:
  generate <spec-path> <tenant> <user-id> [base-url]  - Generate tests from OpenAPI spec
  detect <spec-path> [base-url]                       - Detect changes in OpenAPI spec
  preview <spec-path> <tenant> <user-id> [base-url]  - Preview what tests would be generated

Examples:
  generate swagger.json ptpd68r3nke7q5pnutzaaw PPSAutoTestUser0
  detect api-spec.yaml https://api.example.com
  preview openapi.json ptpd68r3nke7q5pnutzaaw PPSAutoTestUser1 https://api.example.com
");
        }
    }
}