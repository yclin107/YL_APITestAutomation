using Microsoft.OpenApi.Models;
using System.Text;
using System.Net;
using API.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;

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
            sb.AppendLine("using System.Text;");
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

        private static void GenerateSchemaValidationMethods(StringBuilder sb, List<OpenApiEndpointTest> endpoints, OpenApiTestSpec spec)
        {
            sb.AppendLine("        #region Schema Validation Methods");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                var methodName = SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path);
                var schemaKey = $"{endpoint.Method}:{endpoint.Path}";
                
                // Try to get schema from successful responses (200, 201, etc.)
                var successResponse = endpoint.Responses.FirstOrDefault(r => r.Key.StartsWith("2"));
                
                if (!string.IsNullOrEmpty(successResponse.Key) && successResponse.Value != null)
                {
                    var propertySchema = schema.Properties.ContainsKey(property.Key) 
                        ? schema.Properties[property.Key] 
                        : null;
                        
                }
                else
                {
                    // Generate a basic validation method if no schema is available
                    GenerateBasicValidationMethod(sb, methodName);
                }
            }

            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateSchemaValidationMethod(StringBuilder sb, string methodName, string schemaKey, OpenApiResponse response, OpenApiTestSpec spec)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            sb.AppendLine($"        private async Task ValidateResponseSchema_{methodName}(string jsonResponse)");
            sb.AppendLine("        {");
            sb.AppendLine("            AllureApi.AddAttachment(\"Actual Response\", \"application/json\", Encoding.UTF8.GetBytes(jsonResponse));");
            sb.AppendLine();
            
            // Generate schema JSON from OpenAPI response
            string schemaJson;
            try
            {
                schemaJson = GenerateSchemaFromResponse(response, spec, schemaKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Memory error generating schema for {endpointInfo}: {ex.Message}");
                schemaJson = CreateFallbackSchema("Memory allocation error - schema too large", endpointInfo);
            }
            
            var isRealSchema = !schemaJson.Contains("\"description\": \"Generic response schema\"") && 
                              !schemaJson.Contains("\"description\": \"Fallback response schema\"");

            if (isRealSchema)
            {
                // Generate real schema validation
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine($"                var schemaJson = @\"{FormatJsonForCSharp(schemaJson)}\";");
                sb.AppendLine();
                sb.AppendLine("                AllureApi.AddAttachment(\"Expected Schema\", \"application/json\", Encoding.UTF8.GetBytes(schemaJson));");
                sb.AppendLine();
                sb.AppendLine("                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);");
                sb.AppendLine("                var validator = new JsonSchemaValidator();");
                sb.AppendLine("                var errors = validator.Validate(jsonResponse, schema);");
                sb.AppendLine();
                sb.AppendLine("                if (errors.Any())");
                sb.AppendLine("                {");
                sb.AppendLine("                    var errorMessages = errors.Select(e => $\"{e.Path}: {e.Kind} - {e.Property}\");");
                sb.AppendLine("                    var allErrors = string.Join(\", \", errorMessages);");
                //sb.AppendLine("                    Console.WriteLine($\"📋 Expected schema type: object with specific properties\");");
                //sb.AppendLine("                    Console.WriteLine($\"📋 Actual response: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
                sb.AppendLine("                    Assert.Fail($\"Response schema validation failed. Errors: {allErrors}\");");
                sb.AppendLine("                }");
                sb.AppendLine("                else");
                sb.AppendLine("                {");
                //sb.AppendLine($"                    Console.WriteLine(\"✅ Schema validation passed\");");
                if (schemaJson.Contains("too complex") || schemaJson.Contains("Simplified schema") || schemaJson.Contains("Memory allocation error"))
                {
                    sb.AppendLine("                    // NOTE: Schema was simplified due to complexity - consider manual validation for critical fields");
                }
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (Exception ex)");
                sb.AppendLine("            {");
                //sb.AppendLine("                Console.WriteLine($\"❌ Schema validation exception: {ex.Message}\");");
                sb.AppendLine("                Assert.Fail($\"Schema validation error: {ex.Message}\");");
                sb.AppendLine("            }");
            }
            else
            {
                // Generate basic validation as fallback
                sb.AppendLine("            // Basic validation - ensure response is valid JSON");
                sb.AppendLine($"            // NOTE: Schema was simplified due to complexity");
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine("                // Validate JSON structure");
                sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
                sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
                sb.AppendLine("                Console.WriteLine($\"📋 Actual response: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
                sb.AppendLine($"                Console.WriteLine(\"⚠️  Basic validation - schema was simplified\");");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (JsonException ex)");
                sb.AppendLine("            {");
                sb.AppendLine("                Console.WriteLine($\"❌ Invalid JSON: {ex.Message}\");");
                sb.AppendLine("                Assert.Fail($\"Response is not valid JSON: {ex.Message}\");");
                sb.AppendLine("            }");
            }
            
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateBasicValidationMethod(StringBuilder sb, string methodName)
        {
            sb.AppendLine($"        private void ValidateResponseSchema_{methodName}(string jsonResponse)");
            sb.AppendLine("        {");
            sb.AppendLine("            AllureApi.AddAttachment(\"Actual Response\", \"application/json\", Encoding.UTF8.GetBytes(jsonResponse));");
            sb.AppendLine();
            sb.AppendLine("            // Basic validation - ensure response is valid JSON");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
                sb.AppendLine("                Console.WriteLine($\"📋 Response content: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (JsonException ex)");
            sb.AppendLine("            {");
                