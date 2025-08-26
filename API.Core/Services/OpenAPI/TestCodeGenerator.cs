using Microsoft.OpenApi.Models;
using System.Text;
using System.Net;
using API.Core.Models;
using API.Core.Services.OpenAPI.Generator;
using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;

namespace API.Core.Services.OpenAPI
{
    public class TestCodeGenerator
    {
        private static readonly EndpointGenerator _endpointGenerator = new();
        private static readonly SchemaGenerator _schemaGenerator = new();
        private static readonly RequestBodyGenerator _requestBodyGenerator = new();
        private static readonly TestMethodGenerator _testMethodGenerator = new();
        private static readonly MainTestGenerator _mainTestGenerator = new();

        private static readonly SchemaValidator _schemaValidator = new();

        public static string GenerateTestClassByTag(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tenant, string userId, string className, string tag)
        {
            try
            {
                Console.WriteLine($"🔧 Generating modular test structure for {className}...");
                
                // Generate all components
                var mainTest = _mainTestGenerator.GenerateMainTestClass(spec, endpoints, tenant, userId, className, tag);
                var endpointClass = _endpointGenerator.GenerateEndpointClass(spec, endpoints, $"{className}_Endpoints");
                var schemaFiles = _schemaGenerator.GenerateSchemaFiles(spec, endpoints, className);
                var requestBodyFiles = _requestBodyGenerator.GenerateRequestBodyFiles(spec, endpoints, className);

                // Save main test file (without subfolder)
                SaveGeneratedFile("Tests/Component", $"{className}.cs", mainTest);
                SaveGeneratedFile("Source/Endpoints", $"{className}_Endpoints.cs", endpointClass);
                
                // Save schema JSON files
                foreach (var schemaFile in schemaFiles)
                {
                    SaveGeneratedFile("Source/Schemas", schemaFile.Key, schemaFile.Value);
                }
                
                // Save request body JSON files
                foreach (var requestBodyFile in requestBodyFiles)
                {
                    SaveGeneratedFile("Source/RequestBodies", requestBodyFile.Key, requestBodyFile.Value);
                }

                Console.WriteLine($"✅ Generated modular test structure for {className}");
                return mainTest; // Return main test for compatibility
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generating modular tests for {className}: {ex.Message}");
                return GenerateFallbackTest(className, tag, ex.Message);
            }
        }

        private static void SaveGeneratedFile(string folder, string fileName, string content)
        {
            try
            {
                var currentDir = AppContext.BaseDirectory;
                var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
                var targetPath = Path.Combine(solutionRoot, "API.TestBase", folder);
                targetPath = Path.GetFullPath(targetPath);
                
                Directory.CreateDirectory(targetPath);
                
                var filePath = Path.Combine(targetPath, fileName);
                File.WriteAllText(filePath, content);
                
                Console.WriteLine($"📁 Saved: {folder}/{fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving {fileName}: {ex.Message}");
            }
        }

        private static string GenerateFallbackTest(string className, string tag, string error)
        {
            return $@"
using Allure.NUnit.Attributes;

namespace API.TestBase.Tests.Component
{{
    [TestFixture]
    [AllureFeature(""{tag}"")]
    public class {className} : TestBase
    {{
        [Test]
        public void Fallback_Test()
        {{
            Assert.Fail(""Test generation failed: {error}"");
        }}
    }}
}}";
        }
    }
}