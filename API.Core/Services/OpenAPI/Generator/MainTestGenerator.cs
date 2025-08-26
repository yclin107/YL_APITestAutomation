using Microsoft.OpenApi.Models;
using System.Text;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class MainTestGenerator
    {
        public string GenerateMainTestClass(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tenant, string userId, string className, string tag)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using Allure.Net.Commons;");
            sb.AppendLine("using Allure.NUnit.Attributes;");
            sb.AppendLine("using API.TestBase.Source.Methods;");
            sb.AppendLine();
            sb.AppendLine("namespace API.TestBase.Tests.Component");
            sb.AppendLine("{");
            sb.AppendLine("    [TestFixture]");
            sb.AppendLine("    [Parallelizable(ParallelScope.All)]");
            sb.AppendLine($"    [AllureFeature(\"{tag}\")]");
            sb.AppendLine($"    public class {className} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine($"        private {GetMethodClassName(className)} _methods;");
            sb.AppendLine("        private TestContext _context;");
            sb.AppendLine("        private string _token;");
            sb.AppendLine();
            sb.AppendLine("        [SetUp]");
            sb.AppendLine("        public void Setup()");
            sb.AppendLine("        {");
            sb.AppendLine("            _context = GetTestContext();");
            sb.AppendLine("            _token = GetAuthToken(_context);");
            sb.AppendLine($"            _methods = new {GetMethodClassName(className)}(GetBaseUrl(), _token, _context);");
            sb.AppendLine($"            InitContext(\"{tag}\");");
            sb.AppendLine("        }");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                GeneratePositiveTest(sb, endpoint, className);
                GenerateUnauthorizedTest(sb, endpoint, className);
                GenerateMissingParametersTest(sb, endpoint, className);
                GenerateSchemaValidationTest(sb, endpoint, className);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void GeneratePositiveTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Positive");
            var methodName = GenerateMethodName(endpoint.Method, endpoint.Path);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"Generated\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        [AllureSubSuite(\"{endpoint.Tags.FirstOrDefault() ?? "General"}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Positive Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var result = await _methods.{methodName}(");
            
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Response\", result.Response);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(result.Response));");
            sb.AppendLine();
            sb.AppendLine("                Assert.That(result.Success, Is.True, $\"Request should succeed. Errors: {string.Join(\", \", result.ValidationErrors)}\");");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(result.Response), Is.False, \"Response should not be empty\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateUnauthorizedTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Unauthorized");
            var methodName = GenerateMethodName(endpoint.Method, endpoint.Path);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"Generated\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        [AllureSubSuite(\"{endpoint.Tags.FirstOrDefault() ?? "General"}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Unauthorized Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                // Create methods instance without token");
            sb.AppendLine($"                var unauthorizedMethods = new {GetMethodClassName(className)}(GetBaseUrl(), \"\", _context);");
            sb.AppendLine();
            sb.AppendLine($"                var result = await unauthorizedMethods.{methodName}(");
            
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Unauthorized Response\", result.Response);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(result.Response));");
            sb.AppendLine();
            sb.AppendLine("                Assert.That(result.Response, Does.Contain(\"401\").Or.Contain(\"Unauthorized\"), \"Should return 401 Unauthorized\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateMissingParametersTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var requiredParams = endpoint.Parameters.Where(p => p.Required).ToList();
            if (!requiredParams.Any()) return;

            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Missing_Parameters");
            var methodName = GenerateMethodName(endpoint.Method, endpoint.Path);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"Generated\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        [AllureSubSuite(\"{endpoint.Tags.FirstOrDefault() ?? "General"}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Missing Parameters Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var result = await _methods.{methodName}(");
            sb.AppendLine("                    // Intentionally omitting required parameters");
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Missing Parameters Response\", result.Response);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(result.Response));");
            sb.AppendLine();
            sb.AppendLine("                Assert.That(result.Response, Does.Contain(\"400\").Or.Contain(\"Bad Request\"), \"Should return 400 Bad Request for missing parameters\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateSchemaValidationTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Schema_Validation");
            var methodName = GenerateMethodName(endpoint.Method, endpoint.Path);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"Generated\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        [AllureSubSuite(\"{endpoint.Tags.FirstOrDefault() ?? "General"}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Schema Validation Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var result = await _methods.{methodName}(");
            
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Schema Validation Response\", result.Response);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(result.Response));");
            sb.AppendLine();
            sb.AppendLine("                if (result.ValidationErrors.Any())");
            sb.AppendLine("                {");
            sb.AppendLine("                    AllureApi.AddAttachment(\"Validation Errors\", \"text/plain\", System.Text.Encoding.UTF8.GetBytes(string.Join(\"\\n\", result.ValidationErrors)));");
            sb.AppendLine("                }");
            sb.AppendLine();
            sb.AppendLine("                Assert.That(result.ValidationErrors, Is.Empty, $\"Schema validation should pass. Errors: {string.Join(\", \", result.ValidationErrors)}\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private string GenerateTestName(string method, string path, string testType)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}_{testType}_Test";
        }

        private string GenerateMethodName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"Execute_{method.ToUpper()}_{pathPart}";
        }

        private List<string> GetDefaultParameters(OpenApiEndpointTest endpoint)
        {
            var parameters = new List<string>();

            // Add request body parameter for POST/PUT/PATCH
            if (HasRequestBody(endpoint))
            {
                parameters.Add("requestBody: null");
            }

            // Add header parameters (except special ones)
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                {
                    parameters.Add($"{GetParameterName(param.Name)}: GetTestValue(\"{param.Schema?.Type ?? "string"}\")");
                }
            }

            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                parameters.Add($"{GetParameterName(param.Name)}: GetTestValue(\"{param.Schema?.Type ?? "string"}\")");
            }

            // Add path parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                parameters.Add($"{GetParameterName(param.Name)}: GetTestValue(\"{param.Schema?.Type ?? "string"}\")");
            }

            return parameters;
        }

        private bool HasRequestBody(OpenApiEndpointTest endpoint)
        {
            return endpoint.Method.ToUpper() == "POST" || 
                   endpoint.Method.ToUpper() == "PUT" || 
                   endpoint.Method.ToUpper() == "PATCH";
        }

        private string GetParameterName(string name)
        {
            return name.Replace("-", "").Replace(".", "").ToLower();
        }

        private string GetMethodClassName(string baseClassName)
        {
            return $"{baseClassName}_Methods";
        }
    }
}