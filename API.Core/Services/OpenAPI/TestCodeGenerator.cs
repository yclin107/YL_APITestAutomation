using API.Core.Models;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Main orchestrator for test code generation
    /// </summary>
    public static class TestCodeGenerator
    {
        public static string GenerateTestClassByTag(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, 
            string tenant, string userId, string className, string tag)
        {
            var codeBuilder = new StringBuilder();
            
            // Generate class header
            codeBuilder.AppendLine(TestClassGenerator.GenerateClassHeader(className, tag));
            
            // Generate test methods for each endpoint
            foreach (var endpoint in endpoints)
            {
                try
                {
                    var testMethods = TestMethodGenerator.GenerateTestMethodsForEndpoint(spec, endpoint, tenant, userId);
                    codeBuilder.AppendLine(testMethods);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"⚠️  Memory error processing endpoint {endpoint.Method} {endpoint.Path}: {ex.Message}");
                    var fallbackMethods = TestMethodGenerator.GenerateFallbackTestMethods(endpoint, tenant, userId);
                    codeBuilder.AppendLine(fallbackMethods);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️  Error processing endpoint {endpoint.Method} {endpoint.Path}: {ex.Message}");
                    var fallbackMethods = TestMethodGenerator.GenerateFallbackTestMethods(endpoint, tenant, userId);
                    codeBuilder.AppendLine(fallbackMethods);
                }
            }
            
            // Generate schema validation methods
            codeBuilder.AppendLine(SchemaValidationGenerator.GenerateSchemaValidationMethods(spec, endpoints));
            
            // Generate class footer
            codeBuilder.AppendLine(TestClassGenerator.GenerateClassFooter());
            
            return codeBuilder.ToString();
        }
    }
}