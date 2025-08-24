using Microsoft.OpenApi.Models;
using System.Text;
using System.Net;
using API.Core.Models;

namespace API.Core.Services.OpenAPI
{
    public class TestCodeGenerator
    {
        private static readonly SchemaValidator _schemaValidator = new();

        public static string GenerateTestClassByTag(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tenant, string userId, string className, string tag)
        {
            var sb = new StringBuilder();
            
            // Sanitize class name and tag
            var sanitizedClassName = SanitizeIdentifier(className);
            var sanitizedTag = SanitizeIdentifier(tag);
            
            // Generate using statements following the existing pattern
            sb.AppendLine("using Allure.Net.Commons;");
            sb.AppendLine("using Allure.NUnit.Attributes;");
            sb.AppendLine("using API.Core.Helpers;");
            sb.AppendLine("using System.Net;");
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using static RestAssured.Dsl;");
            sb.AppendLine("using Newtonsoft.Json.Schema;");
            sb.AppendLine("using Newtonsoft.Json.Linq;");
            sb.AppendLine("using NJsonSchema;");
            sb.AppendLine("using NJsonSchema.Validation;");
            sb.AppendLine();

            // Generate namespace and class following the existing pattern
            sb.AppendLine($"namespace API.TestBase.Tests.Generated.{sanitizedTag}");
            sb.AppendLine("{");
            sb.AppendLine("    [TestFixture]");
            sb.AppendLine($"    [AllureFeature(\"{sanitizedTag} API Tests\")]");
            sb.AppendLine($"    public class {sanitizedClassName} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine();

            // Generate test methods for each endpoint
            foreach (var endpoint in endpoints)
            {
                GenerateEndpointTestsWithAllurePattern(sb, endpoint, spec, sanitizedTag, true);
            }

            // Generate schema validation methods
            GenerateSchemaValidationMethods(sb, endpoints, spec);
            
            // Add helper methods  
            sb.AppendLine(GenerateHelperMethods());
            
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateEndpointTestsWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string sanitizedTag, bool forceUnauthorizedTest = false)
        {
            var methodName = SanitizeIdentifier(endpoint.OperationId);
            
            // Generate positive test following the existing pattern
            GeneratePositiveTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            
            // Generate unauthorized test for all endpoints (as requested)
            if (endpoint.RequiresAuth || forceUnauthorizedTest)
            {
                GenerateUnauthorizedTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            }
            
            if (endpoint.Parameters.Any(p => p.Required))
            {
                GenerateMissingRequiredParametersTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            }
            
            // Generate schema validation test if response has schema
            if (endpoint.Responses.ContainsKey("200") || endpoint.Responses.ContainsKey("201"))
            {
                GenerateSchemaValidationTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            }
        }

        private static void GeneratePositiveTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_PositiveTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            // Generate request body if needed
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("            var requestBody = GenerateTestRequestBody();");
                sb.AppendLine();
                sb.AppendLine("            AllureApi.Step(\"Generate & Attach Request Body\", () =>");
                sb.AppendLine("            {");
                sb.AppendLine("                string requestBodyJson = serializeToJson(requestBody);");
                sb.AppendLine($"                AttachResponse(\"{methodName}Request\", requestBodyJson);");
                sb.AppendLine("            });");
                sb.AppendLine();
            }

            // Generate the main API call
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path}\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.TenantId)");
            }
            
            // Add parameters
            foreach (var param in endpoint.Parameters.Where(p => p.Required))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
                else if (param.In == ParameterLocation.Header && param.Name.ToLower() != "authorization")
                {
                    sb.AppendLine($"                    .Header(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            // Add request body if needed
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(requestBody)");
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
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
            
            // Extract and attach response
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("            var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine($"            AllureApi.Step(\"Get & Attach {methodName}Response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                AttachResponse(\"{methodName}Response\", rawJson);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            // Assertions following the existing pattern
            sb.AppendLine($"            AllureApi.Step(\"Assert {methodName} response is successful\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.Multiple(() =>");
            sb.AppendLine("                {");
            sb.AppendLine("                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, \"Request should be successful\");");
            sb.AppendLine("                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, \"Response should not be empty\");");
            sb.AppendLine("                });");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateUnauthorizedTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_UnauthorizedTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} without authorization\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            // Add non-auth parameters
            foreach (var param in endpoint.Parameters.Where(p => p.Required && p.In != ParameterLocation.Header))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(401);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Assert unauthorized response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateMissingRequiredParametersTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_MissingRequiredParametersTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} with missing required parameters\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.TenantId)");
            }
            
            // Handle path parameters in URL (still need these for the URL to be valid)
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(400);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Assert bad request response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateSchemaValidationTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_SchemaValidationTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            // Generate request body if needed
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("            var requestBody = GenerateTestRequestBody();");
                sb.AppendLine();
            }

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} for schema validation\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.TenantId)");
            }
            
            // Add required parameters with test values
            foreach (var param in endpoint.Parameters.Where(p => p.Required))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
                else if (param.In == ParameterLocation.Header && param.Name.ToLower() != "authorization")
                {
                    sb.AppendLine($"                    .Header(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(requestBody)");
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then();");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine();
            sb.AppendLine($"            AllureApi.Step(\"Get & Attach Schema Validation Response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                AttachResponse(\"{methodName}SchemaValidationResponse\", rawJson);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Validate response schema\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                if (response.Extract().Response().IsSuccessStatusCode)");
            sb.AppendLine("                {");
            sb.AppendLine($"                    ValidateResponseSchema_{SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path)}(rawJson);");
            sb.AppendLine("                }");
            sb.AppendLine("                else");
            sb.AppendLine("                {");
            sb.AppendLine("                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, \"Response should not be empty even for error responses\");");
            sb.AppendLine("                }");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string SanitizeIdentifier(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return "Unknown";
                
            // Remove or replace invalid characters for C# identifiers
            var sanitized = identifier
                .Replace("-", "_")
                .Replace(" ", "_")
                .Replace(".", "_")
                .Replace("/", "_")
                .Replace("\\", "_")
                .Replace("(", "_")
                .Replace(")", "_")
                .Replace("[", "_")
                .Replace("]", "_")
                .Replace("{", "_")
                .Replace("}", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace(":", "_")
                .Replace(";", "_")
                .Replace(",", "_")
                .Replace("?", "_")
                .Replace("!", "_")
                .Replace("@", "_")
                .Replace("#", "_")
                .Replace("$", "_")
                .Replace("%", "_")
                .Replace("^", "_")
                .Replace("&", "_")
                .Replace("*", "_")
                .Replace("+", "_")
                .Replace("=", "_")
                .Replace("|", "_")
                .Replace("~", "_")
                .Replace("`", "_")
                .Replace("'", "_")
                .Replace("\"", "_");

            // Remove consecutive underscores
            while (sanitized.Contains("__"))
            {
                sanitized = sanitized.Replace("__", "_");
            }

            // Remove leading/trailing underscores
            sanitized = sanitized.Trim('_');

            // Ensure it starts with a letter or underscore (valid C# identifier)
            if (!char.IsLetter(sanitized[0]) && sanitized[0] != '_')
            {
                sanitized = "_" + sanitized;
            }

            // Ensure it's not empty
            if (string.IsNullOrEmpty(sanitized))
            {
                sanitized = "Unknown";
            }

            return sanitized;
        }

        private static string GenerateHelperMethods()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine();
            sb.AppendLine("        private object GetTestValue(string type)");
            sb.AppendLine("        {");
            sb.AppendLine("            return type?.ToLower() switch");
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
            sb.AppendLine("            return new { testProperty = \"testValue\", id = Guid.NewGuid() };");
            sb.AppendLine("        }");
            
            return sb.ToString();
        }
    }
}