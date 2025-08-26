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
            sb.AppendLine("using API.TestBase.Source.Endpoints;");
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using static RestAssured.Dsl;");
            sb.AppendLine();
            sb.AppendLine("namespace API.TestBase.Tests.Component");
            sb.AppendLine("{");
            sb.AppendLine("    [TestFixture]");
            sb.AppendLine("    [Parallelizable(ParallelScope.All)]");
            sb.AppendLine($"    [AllureFeature(\"{tag}\")]");
            sb.AppendLine($"    public class {className} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine($"        private {GetEndpointClassName(className)} _endpoints;");
            sb.AppendLine("        private TestContext _context;");
            sb.AppendLine("        private string _token;");
            sb.AppendLine();
            sb.AppendLine("        [SetUp]");
            sb.AppendLine("        public void Setup()");
            sb.AppendLine("        {");
            sb.AppendLine("            _context = GetTestContext();");
            sb.AppendLine("            _token = GetAuthToken(_context);");
            sb.AppendLine($"            _endpoints = new {GetEndpointClassName(className)}(GetBaseUrl(), _token, _context);");
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
            var endpointMethodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            var requestBodyJson = GetRequestBodyJsonPath(endpoint, className);
            var schemaJson = GetSchemaJsonPath(endpoint, className);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"{endpoint.Tags.FirstOrDefault() ?? "Generated"}\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Positive Test\", async () =>");
            sb.AppendLine("            {");
            
            // Load request body if needed
            if (HasRequestBody(endpoint))
            {
                sb.AppendLine($"                var requestBodyJson = await File.ReadAllTextAsync(\"{requestBodyJson}\");");
                sb.AppendLine("                var requestBody = JsonSerializer.Deserialize<object>(requestBodyJson);");
            }
            
            sb.AppendLine($"                var response = _endpoints.{endpointMethodName}(");
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                var rawResponse = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("                var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Response\", rawResponse);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(rawResponse));");
            sb.AppendLine();
            sb.AppendLine("                // Validate success status");
            sb.AppendLine("                Assert.That((int)statusCode, Is.InRange(200, 299), \"Should return success status code\");");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(rawResponse), Is.False, \"Response should not be empty\");");
            sb.AppendLine();
            sb.AppendLine("                // Schema validation");
            sb.AppendLine($"                var schemaJson = await File.ReadAllTextAsync(\"{schemaJson}\");");
            sb.AppendLine("                var validationErrors = await ValidateJsonSchema(rawResponse, schemaJson);");
            sb.AppendLine("                Assert.That(validationErrors, Is.Empty, $\"Schema validation should pass. Errors: {string.Join(\", \", validationErrors)}\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateUnauthorizedTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Unauthorized");
            var endpointMethodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            var requestBodyJson = GetRequestBodyJsonPath(endpoint, className);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"{endpoint.Tags.FirstOrDefault() ?? "Generated"}\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Unauthorized Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                // Create endpoints instance without token");
            sb.AppendLine($"                var unauthorizedEndpoints = new {GetEndpointClassName(className)}(GetBaseUrl(), \"\", _context);");
            
            // Load request body if needed
            if (HasRequestBody(endpoint))
            {
                sb.AppendLine($"                var requestBodyJson = await File.ReadAllTextAsync(\"{requestBodyJson}\");");
                sb.AppendLine("                var requestBody = JsonSerializer.Deserialize<object>(requestBodyJson);");
            }
            sb.AppendLine();
            sb.AppendLine($"                var response = unauthorizedEndpoints.{endpointMethodName}(");
            
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                var rawResponse = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("                var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Unauthorized Response\", rawResponse);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(rawResponse));");
            sb.AppendLine();
            sb.AppendLine("                Assert.That((int)statusCode, Is.EqualTo(401), \"Should return 401 Unauthorized\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateMissingParametersTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var requiredParams = endpoint.Parameters.Where(p => p.Required).ToList();
            if (!requiredParams.Any()) return;

            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Missing_Parameters");
            var endpointMethodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"{endpoint.Tags.FirstOrDefault() ?? "Generated"}\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            await AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Missing Parameters Test\", async () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var response = _endpoints.{endpointMethodName}(");
            sb.AppendLine("                    // Intentionally omitting required parameters");
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                var rawResponse = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("                var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Missing Parameters Response\", rawResponse);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(rawResponse));");
            sb.AppendLine();
            sb.AppendLine("                Assert.That((int)statusCode, Is.EqualTo(400), \"Should return 400 Bad Request for missing parameters\");");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void GenerateSchemaValidationTest(StringBuilder sb, OpenApiEndpointTest endpoint, string className)
        {
            var testName = GenerateTestName(endpoint.Method, endpoint.Path, "Schema_Validation");
            var endpointMethodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            var requestBodyJson = GetRequestBodyJsonPath(endpoint, className);
            var schemaJson = GetSchemaJsonPath(endpoint, className);

            sb.AppendLine($"        [Test]");
            sb.AppendLine($"        [Category(\"{endpoint.Tags.FirstOrDefault() ?? "Generated"}\")]");
            sb.AppendLine($"        [AllureTag(\"{endpoint.Method.ToUpper()}\")]");
            sb.AppendLine($"        public async Task {testName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            AllureApi.Step(\"Execute {endpoint.Method.ToUpper()} {endpoint.Path} - Schema Validation Test\", async () =>");
            sb.AppendLine("            {");
            
            // Load request body if needed
            if (HasRequestBody(endpoint))
            {
                sb.AppendLine($"                var requestBodyJson = await File.ReadAllTextAsync(\"{requestBodyJson}\");");
                sb.AppendLine("                var requestBody = JsonSerializer.Deserialize<object>(requestBodyJson);");
            }
            
            sb.AppendLine($"                var response = _endpoints.{endpointMethodName}(");
            
            // Add default parameters
            var parameters = GetDefaultParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", parameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                var rawResponse = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("                var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine("                AttachResponse(\"Schema Validation Response\", rawResponse);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", System.Text.Encoding.UTF8.GetBytes(rawResponse));");
            sb.AppendLine();
            sb.AppendLine("                // Schema validation for success responses");
            sb.AppendLine("                if ((int)statusCode >= 200 && (int)statusCode < 300)");
            sb.AppendLine("                {");
            sb.AppendLine($"                    var schemaJson = await File.ReadAllTextAsync(\"{schemaJson}\");");
            sb.AppendLine("                    var validationErrors = await ValidateJsonSchema(rawResponse, schemaJson);");
            sb.AppendLine("                    if (validationErrors.Any())");
            sb.AppendLine("                    {");
            sb.AppendLine("                        AllureApi.AddAttachment(\"Validation Errors\", \"text/plain\", System.Text.Encoding.UTF8.GetBytes(string.Join(\"\\n\", validationErrors)));");
            sb.AppendLine("                    }");
            sb.AppendLine("                    Assert.That(validationErrors, Is.Empty, $\"Schema validation should pass. Errors: {string.Join(\", \", validationErrors)}\");");
            sb.AppendLine("                }");
            sb.AppendLine();
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

        private string GenerateEndpointMethodName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}";
        }

        private List<string> GetDefaultParameters(OpenApiEndpointTest endpoint)
        {
            var parameters = new List<string>();

            // Add request body parameter for POST/PUT/PATCH
            if (HasRequestBody(endpoint))
            {
                parameters.Add("requestBody");
            }

            // Add header parameters (except special ones)
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                {
                    parameters.Add($"{GetParameterName(param.Name)}");
                }
            }

            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                parameters.Add($"{GetParameterName(param.Name)}");
            }

            // Add path parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                parameters.Add($"{GetParameterName(param.Name)}");
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

        private string GetEndpointClassName(string baseClassName)
        {
            return $"{baseClassName}_Endpoints";
        }
        
        private string GetRequestBodyJsonPath(OpenApiEndpointTest endpoint, string className)
        {
            var methodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            return $"Source/RequestBodies/{methodName}.json";
        }
        
        private string GetSchemaJsonPath(OpenApiEndpointTest endpoint, string className)
        {
            var methodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            var tag = endpoint.Tags.FirstOrDefault() ?? "General";
            return $"Source/Schemas/{tag}/{methodName}_Response.json";
        }
        
        private string GetCSharpType(string openApiType)
        {
            return openApiType?.ToLower() switch
            {
                "integer" => "int?",
                "number" => "double?",
                "boolean" => "bool?",
                "array" => "object[]",
                _ => "string"
            };
        }
    }
}