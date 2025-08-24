using API.Core.Services.OpenAPI;

namespace API.Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Show interactive menu
                var menu = new InteractiveMenuWithArrows();
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