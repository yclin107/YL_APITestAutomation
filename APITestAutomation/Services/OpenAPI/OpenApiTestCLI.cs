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
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: generate <spec-path> [tenant] [user-id] [base-url]");
                Console.WriteLine("If tenant and user-id are not provided, you'll be prompted to select them.");
                return;
            }

            var specPath = args[1];
            var tenant = args.Length > 2 ? args[2] : await PromptForTenant();
            var userId = args.Length > 3 ? args[3] : await PromptForUser(tenant);
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
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: preview <spec-path> [tenant] [user-id] [base-url]");
                return;
            }

            var specPath = args[1];
            var tenant = args.Length > 2 ? args[2] : await PromptForTenant();
            var userId = args.Length > 3 ? args[3] : await PromptForUser(tenant);
            var baseUrl = args.Length > 4 ? args[4] : null;

            Console.WriteLine("Loading OpenAPI specification...");
            var spec = await _manager.LoadSpecificationAsync(specPath, baseUrl);
            
            Console.WriteLine("Generating preview...");
            var preview = await _manager.GenerateTestsAsync(spec, tenant, userId, false);
            Console.WriteLine(preview);
        }

        private async Task<string> PromptForTenant()
        {
            Console.WriteLine("\nAvailable tenants:");
            Console.WriteLine("1. ptpd68r3nke7q5pnutzaaw (dev)");
            Console.WriteLine("2. q7v1n2oexe2yohe1ttb9yq (test)");
            Console.Write("Select tenant (1-2): ");
            
            var choice = Console.ReadLine();
            return choice switch
            {
                "1" => "ptpd68r3nke7q5pnutzaaw",
                "2" => "q7v1n2oexe2yohe1ttb9yq",
                _ => "ptpd68r3nke7q5pnutzaaw" // default
            };
        }

        private async Task<string> PromptForUser(string tenant)
        {
            Console.WriteLine($"\nAvailable users for {tenant}:");
            Console.WriteLine("1. PPSAutoTestUser0");
            Console.WriteLine("2. PPSAutoTestUser1");
            Console.WriteLine("3. PPSAutoTestUser2");
            Console.Write("Select user (1-3): ");
            
            var choice = Console.ReadLine();
            return choice switch
            {
                "1" => "PPSAutoTestUser0",
                "2" => "PPSAutoTestUser1", 
                "3" => "PPSAutoTestUser2",
                _ => "PPSAutoTestUser0" // default
            };
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

Available Tenants and Users:
  - ptpd68r3nke7q5pnutzaaw: PPSAutoTestUser0, PPSAutoTestUser1, PPSAutoTestUser2
  - q7v1n2oexe2yohe1ttb9yq: PPSAutoTestUser0, PPSAutoTestUser1, PPSAutoTestUser2

You can also run without specifying tenant/user and you'll be prompted to select them:
  generate swagger.json
  preview openapi.json
");
        }
    }
}