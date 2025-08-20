using APITestAutomation.Models.OpenAPI;
using Microsoft.OpenApi.Models;
using System.Text;

namespace APITestAutomation.Services.OpenAPI
{
    public class TestCodeGenerator
    {
        public static string GenerateTestClass(OpenApiTestSpec spec, string tenant, string userId, string className)
        {
            var sb = new StringBuilder();
            
            // Generate using statements
            sb.AppendLine("using Allure.Net.Commons;");
            sb.AppendLine("using Allure.NUnit.Attributes;");
            sb.AppendLine("using APITestAutomation.Helpers;");
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using static RestAssured.Dsl;");
            sb.AppendLine("using Newtonsoft.Json.Schema;");
            sb.AppendLine("using Newtonsoft.Json.Linq;");
            sb.AppendLine();

            // Generate namespace and class
            sb.AppendLine("namespace APITestAutomationTest.Generated");
            sb.AppendLine("{");
            sb.AppendLine("    [TestFixture]");
            sb.AppendLine($"    [AllureFeature(\"{spec.Document.Info?.Title ?? "Generated API Tests"}\")]");
            sb.AppendLine($"    public class {className} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine($"        private readonly string _baseUrl = \"{spec.BaseUrl}\";");
            sb.AppendLine($"        private readonly string _tenant = \"{tenant}\";");
            sb.AppendLine($"        private readonly string _userId = \"{userId}\";");
            sb.AppendLine();

            // Generate setup method
            sb.AppendLine("        [SetUp]");
            sb.AppendLine("        public void Setup()");
            sb.AppendLine("        {");
            sb.AppendLine($"            InitContext(_tenant, _userId, \"{spec.Document.Info?.Title ?? "Generated API Tests"}\");");
            sb.AppendLine("        }");
            sb.AppendLine();

            // Generate test methods for each endpoint
            foreach (var endpoint in spec.EndpointTests.Values)
            {
                GenerateEndpointTests(sb, endpoint, spec);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateEndpointTests(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            var methodName = SanitizeMethodName(endpoint.OperationId);
            
            // Generate positive test
            GeneratePositiveTest(sb, endpoint, methodName);
            
            // Generate negative tests
            GenerateUnauthorizedTest(sb, endpoint, methodName);
            
            if (endpoint.Parameters.Any(p => p.Required))
            {
                GenerateMissingRequiredParametersTest(sb, endpoint, methodName);
            }
            
            // Generate schema validation test if response has schema
            if (endpoint.Responses.ContainsKey("200") || endpoint.Responses.ContainsKey("201"))
            {
                GenerateSchemaValidationTest(sb, endpoint, methodName);
            }
        }

        private static void GeneratePositiveTest(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine("        [Category(\"Generated\")]");
            sb.AppendLine($"        public void {methodName}_PositiveTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var user = ConfigSetup.GetUser(_tenant, _userId);");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = APITestAutomationServices.Authentications.TokenService.PPSProformaToken(_tenant, user);");
            }
            
            sb.AppendLine();
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path}\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", _tenant)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", _tenant)");
            }
            
            // Add parameters
            foreach (var param in endpoint.Parameters.Where(p => p.Required))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
                else if (param.In == ParameterLocation.Header)
                {
                    sb.AppendLine($"                    .Header(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
                else if (param.In == ParameterLocation.Path)
                {
                    // Path parameters are handled in the URL template
                    // We'll replace them in the URL
                }
            }
            
            // Add request body if needed
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(GenerateTestRequestBody())");
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.ToLower().Substring(0, 1).ToUpper()}{endpoint.Method.ToLower().Substring(1)}($\"{{_baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            
            // Add expected status codes
            var successCodes = endpoint.Responses.Keys.Where(k => k.StartsWith("2")).ToList();
            if (successCodes.Any())
            {
                var primaryCode = successCodes.Contains("200") ? "200" : successCodes.First();
                sb.AppendLine($"                    .StatusCode({primaryCode});");
            }
            else
            {
                sb.AppendLine("                    .StatusCode(200);");
            }
            
            sb.AppendLine("            });");
            sb.AppendLine();
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("            AttachResponse(\"Response\", rawJson);");
            sb.AppendLine();
            sb.AppendLine("            Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, \"Request should be successful\");");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateUnauthorizedTest(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName)
        {
            if (!endpoint.RequiresAuth) return;
            
            sb.AppendLine("        [Test]");
            sb.AppendLine("        [Category(\"Generated\")]");
            sb.AppendLine($"        public void {methodName}_UnauthorizedTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} without authorization\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            sb.AppendLine("                    .When()");
            
            // Handle path parameters in URL for unauthorized test
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine($"                    .{endpoint.Method.ToLower().Substring(0, 1).ToUpper()}{endpoint.Method.ToLower().Substring(1)}($\"{{_baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(401);");
            sb.AppendLine("            });");
            sb.AppendLine();
            sb.AppendLine("            Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateMissingRequiredParametersTest(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine("        [Category(\"Generated\")]");
            sb.AppendLine($"        public void {methodName}_MissingRequiredParametersTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var user = ConfigSetup.GetUser(_tenant, _userId);");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = APITestAutomationServices.Authentications.TokenService.PPSProformaToken(_tenant, user);");
            }
            
            sb.AppendLine();
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} with missing required parameters\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", _tenant)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", _tenant)");
            }
            
            sb.AppendLine("                    .When()");
            
            // Handle path parameters in URL for missing parameters test
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine($"                    .{endpoint.Method.ToLower().Substring(0, 1).ToUpper()}{endpoint.Method.ToLower().Substring(1)}($\"{{_baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(400);");
            sb.AppendLine("            });");
            sb.AppendLine();
            sb.AppendLine("            Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateSchemaValidationTest(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine("        [Category(\"Generated\")]");
            sb.AppendLine($"        public void {methodName}_SchemaValidationTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var user = ConfigSetup.GetUser(_tenant, _userId);");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = APITestAutomationServices.Authentications.TokenService.PPSProformaToken(_tenant, user);");
            }
            
            sb.AppendLine();
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} for schema validation\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", _tenant)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", _tenant)");
            }
            
            // Add required parameters with test values
            foreach (var param in endpoint.Parameters.Where(p => p.Required))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
                else if (param.In == ParameterLocation.Header)
                {
                    sb.AppendLine($"                    .Header(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(GenerateTestRequestBody())");
            }
            
            // Handle path parameters in URL for schema validation test
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.ToLower().Substring(0, 1).ToUpper()}{endpoint.Method.ToLower().Substring(1)}($\"{{_baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then();");
            sb.AppendLine("            });");
            sb.AppendLine();
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("            AttachResponse(\"Schema Validation Response\", rawJson);");
            sb.AppendLine();
            sb.AppendLine("            AllureApi.Step(\"Validate response schema\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(rawJson), Is.False, \"Response should not be empty\");");
            sb.AppendLine("                // TODO: Add specific schema validation based on OpenAPI spec");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string SanitizeMethodName(string operationId)
        {
            return operationId.Replace("-", "_").Replace(" ", "_");
        }

        public static string GenerateHelperMethods()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("        private object GetTestValue(string type)");
            sb.AppendLine("        {");
            sb.AppendLine("            return type.ToLower() switch");
            sb.AppendLine("            {");
            sb.AppendLine("                \"string\" => \"test-value\",");
            sb.AppendLine("                \"integer\" => 123,");
            sb.AppendLine("                \"number\" => 123.45,");
            sb.AppendLine("                \"boolean\" => true,");
            sb.AppendLine("                \"array\" => new[] { \"test\" },");
            sb.AppendLine("                \"uuid\" => Guid.NewGuid().ToString(),");
            sb.AppendLine("                \"date\" => DateTime.Now.ToString(\"yyyy-MM-dd\"),");
            sb.AppendLine("                \"date-time\" => DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\"),");
            sb.AppendLine("                _ => \"test-value\"");
            sb.AppendLine("            };");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        private object GenerateTestRequestBody()");
            sb.AppendLine("        {");
            sb.AppendLine("            return new { testProperty = \"testValue\" };");
            sb.AppendLine("        }");
            
            return sb.ToString();
        }
    }
}