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
                var schemaClass = _schemaGenerator.GenerateSchemaClass(spec, endpoints, $"{className}_Schemas");
                var requestBodyClass = _requestBodyGenerator.GenerateRequestBodyClass(spec, endpoints, $"{className}_RequestBodies");
                var methodClass = _testMethodGenerator.GenerateTestMethods(spec, endpoints, $"{className}_Methods");

                // Save all files
                SaveGeneratedFile("Tests/Component", $"{className}.cs", mainTest);
                SaveGeneratedFile("Source/Endpoints", $"{className}_Endpoints.cs", endpointClass);
                SaveGeneratedFile("Source/Schemas", $"{className}_Schemas.cs", schemaClass);
                SaveGeneratedFile("Source/RequestBodies", $"{className}_RequestBodies.cs", requestBodyClass);
                SaveGeneratedFile("Source/Methods", $"{className}_Methods.cs", methodClass);

                Console.WriteLine($"✅ Generated modular test structure for {className}");
                return mainTest; // Return main test for compatibility
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generating modular tests for {className}: {ex.Message}");
                return GenerateFallbackTest(className, tag, ex.Message);
            }
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
                    GenerateSchemaValidationMethod(sb, methodName, schemaKey, successResponse.Value, spec);
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
                sb.AppendLine("                Console.WriteLine($\"❌ Invalid JSON response: {ex.Message}\");");
            sb.AppendLine("                Assert.Fail($\"Response is not valid JSON: {ex.Message}\");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string GenerateSchemaFromResponse(OpenApiResponse response, OpenApiTestSpec spec, string schemaKey)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            if (response == null)
            {
                return CreateFallbackSchema("Response is null", endpointInfo);
            }
            
            try
            {
                if (response.Content == null || !response.Content.Any())
                {
                    return CreateFallbackSchema("No content in response", endpointInfo);
                }

                // Try multiple JSON content type variations
                var jsonContent = response.Content.FirstOrDefault(c => 
                    c.Key.Equals("application/json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (jsonContent.Key == null)
                {
                    return CreateFallbackSchema("No JSON content type", endpointInfo);
                }
                
                var mediaType = jsonContent.Value;
                if (mediaType?.Schema == null)
                {
                    return CreateFallbackSchema("No schema in media type", endpointInfo);
                }

                var schema = mediaType.Schema;
                
                // Handle schema references
                if (schema.Reference != null)
                {
                    var resolvedSchema = ResolveSchemaReference(schema.Reference, spec);
                    if (resolvedSchema != null)
                    {
                        schema = resolvedSchema;
                    }
                }
                
                var convertedSchema = ConvertOpenApiSchemaToJsonSchema(schema, schemaKey);
                
                // Validate that we have a meaningful schema
                if (convertedSchema.Contains("\"type\"") && 
                    (convertedSchema.Contains("\"properties\"") || 
                     convertedSchema.Contains("\"items\"") || 
                     schema.Type == "string" || 
                     schema.Type == "number" || 
                     schema.Type == "integer" || 
                     schema.Type == "boolean"))
                {
                    return convertedSchema;
                }
                else
                {
                    return CreateFallbackSchema("Schema validation failed", endpointInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Memory error processing endpoint {endpointInfo}: {ex.Message}");
                return CreateFallbackSchema($"Memory allocation error - schema too large", endpointInfo);
            }
        }
        
        private static OpenApiSchema? ResolveSchemaReference(OpenApiReference reference, OpenApiTestSpec spec)
        {
            try
            {
                if (spec.Document?.Components?.Schemas == null)
                {
                    return null;
                }
                
                var schemaName = reference.Id;
                if (spec.Document.Components.Schemas.TryGetValue(schemaName, out var referencedSchema))
                {
                    return referencedSchema;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        private static string CreateFallbackSchema(string reason, string endpointInfo = "")
        {
            var schema = new
            {
                type = "object",
                additionalProperties = true,
                description = $"Fallback response schema - {reason}",
                _note = !string.IsNullOrEmpty(endpointInfo) ? $"Endpoint: {endpointInfo}" : null
            };
            
            return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
        
        private static string ExtractEndpointFromSchemaKey(string schemaKey)
        {
            try
            {
                var parts = schemaKey.Split(':');
                return parts.Length >= 2 ? $"{parts[0]} {parts[1]}" : schemaKey;
            }
            catch
            {
                return schemaKey;
            }
        }

        private static string ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema, string schemaKey)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            try
            {
                // Check if schema is too complex (might cause memory issues)
                var complexityScore = CalculateSchemaComplexity(openApiSchema);
                if (complexityScore > 1000)
                {
                    Console.WriteLine($"⚠️  Simplifying complex schema for endpoint {endpointInfo} (complexity: {complexityScore})");
                    return CreateSimplifiedSchema(openApiSchema, schemaKey, endpointInfo);
                }
                
                var schema = new Dictionary<string, object>();
                
                // Handle different schema types
                if (!string.IsNullOrEmpty(openApiSchema.Type))
                {
                    schema["type"] = openApiSchema.Type;
                }
                else if (openApiSchema.Properties?.Any() == true)
                {
                    schema["type"] = "object";
                }
                else if (openApiSchema.Items != null)
                {
                    schema["type"] = "array";
                }
                else
                {
                    schema["type"] = "object";
                }
                
                // Handle object properties
                if (openApiSchema.Properties?.Any() == true)
                {
                    var properties = new Dictionary<string, object>();
                    foreach (var prop in openApiSchema.Properties)
                    {
                        var propSchema = new Dictionary<string, object>();
                        
                        // Set property type
                        propSchema["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Type ?? "string");
                        
                        // Add format if specified
                        if (!string.IsNullOrEmpty(prop.Value.Format))
                        {
                            propSchema["format"] = prop.Value.Format;
                        }
                        
                        // Add description if available
                        if (!string.IsNullOrEmpty(prop.Value.Description))
                        {
                            propSchema["description"] = prop.Value.Description;
                        }
                        
                        // Handle nested objects
                        if (prop.Value.Properties?.Any() == true)
                        {
                            propSchema["type"] = "object";
                            var nestedProps = new Dictionary<string, object>();
                            foreach (var nestedProp in prop.Value.Properties)
                            {
                                nestedProps[nestedProp.Key] = new Dictionary<string, object>
                                {
                                    ["type"] = ConvertOpenApiTypeToJsonSchemaType(nestedProp.Value.Type ?? "string")
                                };
                            }
                            propSchema["properties"] = nestedProps;
                        }
                        
                        // Handle arrays
                        if (prop.Value.Items != null)
                        {
                            propSchema["type"] = "array";
                            propSchema["items"] = new Dictionary<string, object>
                            {
                                ["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Items.Type ?? "string")
                            };
                        }
                        
                        properties[prop.Key] = propSchema;
                    }
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