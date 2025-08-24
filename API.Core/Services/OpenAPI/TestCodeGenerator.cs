using Microsoft.OpenApi.Models;
using System.Text;
using System.Net;
using API.Core.Models;
using System.Text.Json;

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

        private static void GenerateSchemaValidationMethods(StringBuilder sb, List<OpenApiEndpointTest> endpoints, OpenApiTestSpec spec)
        {
            sb.AppendLine("        #region Schema Validation Methods");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                var methodName = SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path);
                var schemaKey = $"{endpoint.Method}:{endpoint.Path}";
                
                // Try to get schema from successful responses (200, 201, etc.)
                var successResponse = endpoint.Responses.FirstOrDefault(r => 
                    r.Key.StartsWith("2") && r.Value.Content?.Any() == true);
                
                if (successResponse.Value != null)
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
            Console.WriteLine($"🔧 Generating schema validation method for: {methodName}");
            
            sb.AppendLine($"        private async Task ValidateResponseSchema_{methodName}(string jsonResponse)");
            sb.AppendLine("        {");
            
            // Generate schema JSON from OpenAPI response
            var schemaJson = GenerateSchemaFromResponse(response, spec, schemaKey);
            
            var isRealSchema = !schemaJson.Contains("\"description\": \"Generic response schema\"") && 
                              !schemaJson.Contains("\"description\": \"Fallback response schema\"");
            
            Console.WriteLine($"📋 Final schema for {methodName}: {(isRealSchema ? "REAL SCHEMA" : "GENERIC (no real schema)")}");
            
            if (isRealSchema)
            {
                // Generate real schema validation
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine($"                var schemaJson = @\"{EscapeJsonForString(schemaJson)}\";");
                sb.AppendLine("                var schema = await JsonSchema.FromJsonAsync(schemaJson);");
                sb.AppendLine("                var validator = new JsonSchemaValidator();");
                sb.AppendLine("                var errors = validator.Validate(jsonResponse, schema);");
                sb.AppendLine();
                sb.AppendLine("                if (errors.Any())");
                sb.AppendLine("                {");
                sb.AppendLine("                    var errorMessages = errors.Select(e => $\"{e.Path}: {e.Kind} - {e.Property}\");");
                sb.AppendLine("                    var allErrors = string.Join(\", \", errorMessages);");
                sb.AppendLine("                    Assert.Fail($\"Response schema validation failed. Errors: {allErrors}\");");
                sb.AppendLine("                }");
                sb.AppendLine("                else");
                sb.AppendLine("                {");
                sb.AppendLine("                    Console.WriteLine($\"✅ Schema validation passed for {methodName}\");");
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (Exception ex)");
                sb.AppendLine("            {");
                sb.AppendLine("                Assert.Fail($\"Schema validation error: {ex.Message}\");");
                sb.AppendLine("            }");
            }
            else
            {
                // Generate basic validation as fallback
                sb.AppendLine("            // Basic validation - ensure response is valid JSON");
                sb.AppendLine("            // No schema found in OpenAPI specification for this endpoint");
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
                sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
                sb.AppendLine("                Console.WriteLine($\"⚠️  Basic validation only for {methodName} - no schema in OpenAPI spec\");");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (JsonException ex)");
                sb.AppendLine("            {");
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
            sb.AppendLine("            // Basic validation - ensure response is valid JSON");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (JsonException ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.Fail($\"Response is not valid JSON: {ex.Message}\");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string GenerateSchemaFromResponse(OpenApiResponse response, OpenApiTestSpec spec, string schemaKey)
        {
            Console.WriteLine($"🔍 DEBUG: Generating schema from response for {schemaKey}...");
            Console.WriteLine($"📋 Response content types: {string.Join(", ", response.Content?.Keys ?? Array.Empty<string>())}");
            
            try
            {
                if (response.Content == null || !response.Content.Any())
                {
                    Console.WriteLine($"❌ No content found in response");
                    return CreateFallbackSchema("No content in response");
                }

                // Try multiple JSON content type variations
                var jsonContent = response.Content.FirstOrDefault(c => 
                    c.Key.Equals("application/json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                Console.WriteLine($"🔍 JSON Content found: {jsonContent.Key != null}");
                
                if (jsonContent.Key == null)
                {
                    Console.WriteLine($"❌ No JSON content type found. Available: {string.Join(", ", response.Content.Keys)}");
                    return CreateFallbackSchema("No JSON content type");
                }
                
                var mediaType = jsonContent.Value;
                if (mediaType?.Schema == null)
                {
                    Console.WriteLine($"❌ No schema found in media type");
                    return CreateFallbackSchema("No schema in media type");
                }

                var schema = mediaType.Schema;
                Console.WriteLine($"✅ Schema found! Type: {schema.Type ?? "undefined"}");
                Console.WriteLine($"📝 Properties count: {schema.Properties?.Count ?? 0}");
                Console.WriteLine($"🔗 Reference: {schema.Reference?.Id ?? "none"}");
                
                // Handle schema references
                if (schema.Reference != null)
                {
                    Console.WriteLine($"🔗 Resolving schema reference: {schema.Reference.Id}");
                    var resolvedSchema = ResolveSchemaReference(schema.Reference, spec);
                    if (resolvedSchema != null)
                    {
                        schema = resolvedSchema;
                        Console.WriteLine($"✅ Reference resolved! Type: {schema.Type ?? "undefined"}");
                    }
                }
                
                var convertedSchema = ConvertOpenApiSchemaToJsonSchema(schema);
                Console.WriteLine($"🎯 Generated schema length: {convertedSchema.Length} characters");
                
                // Validate that we have a meaningful schema
                if (convertedSchema.Contains("\"type\"") && 
                    (convertedSchema.Contains("\"properties\"") || 
                     convertedSchema.Contains("\"items\"") || 
                     schema.Type == "string" || 
                     schema.Type == "number" || 
                     schema.Type == "integer" || 
                     schema.Type == "boolean"))
                {
                    Console.WriteLine($"✅ Real schema generated successfully");
                    return convertedSchema;
                }
                else
                {
                    Console.WriteLine($"⚠️  Schema seems empty or invalid, using fallback");
                    return CreateFallbackSchema("Schema validation failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generating schema: {ex.Message}");
                return CreateFallbackSchema($"Error: {ex.Message}");
            }
        }
        
        private static OpenApiSchema? ResolveSchemaReference(OpenApiReference reference, OpenApiTestSpec spec)
        {
            try
            {
                if (spec.Document?.Components?.Schemas == null)
                {
                    Console.WriteLine($"❌ No components/schemas found in document");
                    return null;
                }
                
                var schemaName = reference.Id;
                if (spec.Document.Components.Schemas.TryGetValue(schemaName, out var referencedSchema))
                {
                    Console.WriteLine($"✅ Found referenced schema: {schemaName}");
                    return referencedSchema;
                }
                else
                {
                    Console.WriteLine($"❌ Referenced schema not found: {schemaName}");
                    Console.WriteLine($"📋 Available schemas: {string.Join(", ", spec.Document.Components.Schemas.Keys)}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error resolving schema reference: {ex.Message}");
                return null;
            }
        }
        
        private static string CreateFallbackSchema(string reason)
        {
            Console.WriteLine($"⚠️  Creating fallback schema: {reason}");
            return @"{
                ""type"": ""object"",
                ""additionalProperties"": true,
                ""description"": ""Fallback response schema""
            }";
        }
                    
                    var openApiSchema = jsonContent.Value.Value.Schema;
                    var convertedSchema = ConvertOpenApiSchemaToJsonSchema(openApiSchema);
                    Console.WriteLine($"🎯 Generated schema: {convertedSchema.Substring(0, Math.Min(200, convertedSchema.Length))}...");
                    return convertedSchema;
                }
                else
                {
        }

        private static string ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema)
        {
            try
            {
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
                    schema["properties"] = properties;
                }
                
                // Handle array items
                if (openApiSchema.Items != null)
                {
                    var itemSchema = new Dictionary<string, object>
                    {
                        ["type"] = ConvertOpenApiTypeToJsonSchemaType(openApiSchema.Items.Type ?? "object")
                    };
                    
                    if (openApiSchema.Items.Properties?.Any() == true)
                    {
                        itemSchema["type"] = "object";
                        var itemProperties = new Dictionary<string, object>();
                        foreach (var prop in openApiSchema.Items.Properties)
                        {
                            itemProperties[prop.Key] = new Dictionary<string, object>
                            {
                                ["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Type ?? "string")
                            };
                        }
                        itemSchema["properties"] = itemProperties;
                    }
                    
                    schema["items"] = itemSchema;
                }
                
                // Handle required fields
                if (openApiSchema.Required?.Any() == true)
                {
                    schema["required"] = openApiSchema.Required.ToArray();
                }
                
                // Set additional properties
                schema["additionalProperties"] = openApiSchema.AdditionalPropertiesAllowed;
                
                return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI schema: {ex.Message}");
                return @"{
                    ""type"": ""object"",
                    ""additionalProperties"": true,
                    ""description"": ""Fallback schema due to conversion error""
                }";
            }
        }
        
        private static string ConvertOpenApiTypeToJsonSchemaType(string openApiType)
        {
            return openApiType?.ToLower() switch
            {
                "integer" => "integer",
                "number" => "number", 
                "string" => "string",
                "boolean" => "boolean",
                "array" => "array",
                "object" => "object",
                _ => "string"
            };
        }

        private static string EscapeJsonForString(string json)
        {
            return json.Replace("\"", "\\\"")
                      .Replace("\r", "")
                      .Replace("\n", "\\n")
                      .Replace("\t", "\\t");
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
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Generates test values for different parameter types");
            sb.AppendLine("        /// Used to populate required parameters in API calls");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        private object GetTestValue(string type)");
            sb.AppendLine("        {");
            sb.AppendLine("            return type?.ToLower() switch");
            sb.AppendLine("            {");
            sb.AppendLine("                \"string\" => \"test-string-value\",");
            sb.AppendLine("                \"integer\" => 123,");
            sb.AppendLine("                \"number\" => 123.45,");
            sb.AppendLine("                \"boolean\" => true,");
            sb.AppendLine("                \"array\" => new[] { \"test-item\" },");
            sb.AppendLine("                \"uuid\" => Guid.NewGuid().ToString(),");
            sb.AppendLine("                \"date\" => DateTime.Now.ToString(\"yyyy-MM-dd\"),");
            sb.AppendLine("                \"date-time\" => DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\"),");
            sb.AppendLine("                _ => \"default-test-value\"");
            sb.AppendLine("            };");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Generates a generic test request body for POST/PUT operations");
            sb.AppendLine("        /// Override this method to provide endpoint-specific request bodies");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        private object GenerateTestRequestBody()");
            sb.AppendLine("        {");
            sb.AppendLine("            return new { ");
            sb.AppendLine("                testProperty = \"testValue\", ");
            sb.AppendLine("                id = Guid.NewGuid().ToString(),");
            sb.AppendLine("                timestamp = DateTime.UtcNow");
            sb.AppendLine("            };");
            sb.AppendLine("        }");
            
            return sb.ToString();
        }
    }
}