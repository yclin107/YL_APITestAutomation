using APITestAutomation.Services.OpenAPI;

namespace APITestAutomation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0 || (args.Length == 1 && args[0] == "menu"))
            {
                // Show interactive menu
                var menu = new InteractiveMenu();
                await menu.ShowMenuAsync();
            }
            else
            {
                // Use CLI mode
                var cli = new OpenApiTestCLI();
                await cli.RunAsync(args);
            }
        }
    }
}