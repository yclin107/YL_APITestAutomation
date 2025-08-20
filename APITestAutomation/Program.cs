using APITestAutomation.Services.OpenAPI;

namespace APITestAutomation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cli = new OpenApiTestCLI();
            await cli.RunAsync(args);
        }
    }
}