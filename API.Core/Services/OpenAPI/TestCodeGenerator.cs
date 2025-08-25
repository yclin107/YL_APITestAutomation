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
                Console.WriteLine($"⚠️  Schema conversion error for endpoint {endpointInfo}: {ex.Message}");
                return CreateFallbackSchema($"Conversion error: {ex.Message}", endpointInfo);
            }
        }
        
        private static int CalculateSchemaComplexity(OpenApiSchema schema)
        {
            var complexity = 0;
            complexity += schema.Properties?.Count ?? 0;
            complexity += schema.Required?.Count ?? 0;
            
            // Add complexity for nested objects
            if (schema.Properties != null)
            {
                foreach (var prop in schema.Properties)
                {
                    if (prop.Value.Properties?.Any() == true)
                        complexity += prop.Value.Properties.Count * 2;
                    if (prop.Value.Items?.Properties?.Any() == true)
                        complexity += prop.Value.Items.Properties.Count;
                }
            }
            
            return complexity;
        }
        
        private static string CreateSimplifiedSchema(OpenApiSchema openApiSchema, string schemaKey, string endpointInfo)
        {
            var simplified = new
            {
                type = openApiSchema.Type ?? "object",
                additionalProperties = true,
                description = $"Simplified schema for {endpointInfo} - original was too complex for detailed validation",
                _note = openApiSchema.Properties?.Any() == true ? 
                    $"Original schema had {openApiSchema.Properties.Count} properties - simplified for performance" : 
                    "Schema was simplified due to complexity"
            };
            
            return JsonSerializer.Serialize(simplified, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
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

        private static string FormatJsonForCSharp(string json)
        {
            // Clean and format JSON for C# verbatim string
            try
            {
                // Parse and reformat to ensure valid JSON
                var jsonDoc = JsonDocument.Parse(json);
                var formattedJson = JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                
                // Escape only quotes for verbatim string
                return formattedJson.Replace("\"", "\"\"");
            }
            catch
            {
                // Fallback: just escape quotes
                return json.Replace("\"", "\"\"");
            }
        }

        private static void GenerateEndpointTestsWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string sanitizedTag, bool forceUnauthorizedTest = false)
        {
            var methodName = SanitizeIdentifier(endpoint.OperationId);
            
            // Generate positive test following the existing pattern
            GeneratePositiveTestWithAllurePattern(sb, endpoint, spec, methodName, sanitizedTag);
            
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
                GenerateSchemaValidationTestWithAllurePattern(sb, endpoint, spec, methodName, sanitizedTag);
            }
        }

        private static void GeneratePositiveTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string methodName, string tag)
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

            // Generate request body from OpenAPI spec if needed
            if (endpoint.RequestBody != null)
            {
                var requestBodyJson = GenerateRequestBodyFromOpenApi(endpoint, spec);
                sb.AppendLine($"            var requestBody = {requestBodyJson};");
                sb.AppendLine();
                sb.AppendLine("            AllureApi.Step(\"Attach Request Body\", () =>");
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
            }
            
            // Add headers from OpenAPI spec
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name.Equals("X-3E-UserId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-UserId\", context.UserId)");
                }
                else if (param.Name.Equals("X-3E-InstanceId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
                }
                else if (param.Name.Equals("x-3e-tenantid", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                }
                else if (!param.Name.Equals("authorization", StringComparison.OrdinalIgnoreCase))
                {
                    var headerValue = param.Required ? 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")" : 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")";
                    sb.AppendLine($"                    .Header(\"{param.Name}\", {headerValue})");
                }
            }
            
            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                if (param.Required)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
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
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
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

        private static void GenerateSchemaValidationTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string methodName, string tag)
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

            // Generate request body from OpenAPI spec if needed
            if (endpoint.RequestBody != null)
            {
                var requestBodyJson = GenerateRequestBodyFromOpenApi(endpoint, spec);
                sb.AppendLine($"            var requestBody = {requestBodyJson};");
                sb.AppendLine();
            }

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} for schema validation\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
            }
            
            // Add headers from OpenAPI spec
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name.Equals("X-3E-UserId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-UserId\", context.UserId)");
                }
                else if (param.Name.Equals("X-3E-InstanceId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
                }
                else if (param.Name.Equals("x-3e-tenantid", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                }
                else if (!param.Name.Equals("authorization", StringComparison.OrdinalIgnoreCase))
                {
                    var headerValue = param.Required ? 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")" : 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")";
                    sb.AppendLine($"                    .Header(\"{param.Name}\", {headerValue})");
                }
            }
            
            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                if (param.Required)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
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
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", Encoding.UTF8.GetBytes(rawJson));");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Validate response schema\", () =>");
            sb.AppendLine("            {");
            //sb.AppendLine("                if (response.Extract().Response().IsSuccessStatusCode)");
            //sb.AppendLine("                {");
            sb.AppendLine($"               ValidateResponseSchema_{SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path)}(rawJson).Wait();");
            //sb.AppendLine("                }");
            //sb.AppendLine("                else");
            //sb.AppendLine("                {");
            //sb.AppendLine("                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, \"Response should not be empty even for error responses\");");
            //sb.AppendLine("                    Console.WriteLine($\"⚠️  Non-success response received: {response.Extract().Response().StatusCode}\");");
            //sb.AppendLine("                }");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string GenerateRequestBodyFromOpenApi(OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            if (endpoint.RequestBody == null)
                return "null";
                if (endpoint.RequestBody?.Content == null)
                {
                    return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
                }

                // Try to find JSON content type
                var jsonContent = endpoint.RequestBody.Content.FirstOrDefault(c => 
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (jsonContent.Key == null || jsonContent.Value?.Schema == null)
                {
                    return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
                }

                var schema = jsonContent.Value.Schema;
                
                // Handle schema references
                if (schema.Reference != null)
            return GenerateRequestBodyFromSchema(endpoint, spec);
        }

        private static string GenerateRequestBodyFromSchema(OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            try
            {
                // Find the request body content - prefer application/json
                var requestBody = endpoint.RequestBody;
                if (requestBody?.Content == null)
                    return "null";

                // Try different content types in order of preference
                var contentTypes = new[] { "application/json", "text/json", "application/*+json" };
                OpenApiMediaType? mediaType = null;
                
                foreach (var contentType in contentTypes)
                {
                    if (requestBody.Content.TryGetValue(contentType, out mediaType))
                        break;
                }

                if (mediaType?.Schema == null)
                    return "null";

                // Check if it's a reference
                if (!string.IsNullOrEmpty(mediaType.Schema.Reference?.Id))
                {
                    var refId = mediaType.Schema.Reference.Id;
                    var referencedSchema = spec.Document.Components?.Schemas?.GetValueOrDefault(refId);
                    if (referencedSchema != null)
                    {
                        return GenerateObjectFromSchema(referencedSchema);
                    }
                }
                
                // Generate from direct schema
                return GenerateObjectFromSchema(mediaType.Schema);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Could not generate request body from schema: {ex.Message}");
                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
        }

        private static string GenerateObjectFromSchema(OpenApiSchema schema)
        {
            if (schema?.Properties == null || !schema.Properties.Any())
                return "new { testProperty = \"testValue\" }";

            var properties = new List<string>();
            
            foreach (var prop in schema.Properties)
            {
                var propName = prop.Key;
                var propSchema = prop.Value;
                var value = GenerateValueFromSchema(propSchema);
                properties.Add($"{propName} = {value}");
            }

            return $"new {{ {string.Join(", ", properties)} }}";
        }

        private static string GenerateValueFromSchema(OpenApiSchema schema)
        {
            return schema.Type?.ToLower() switch
            {
                "string" => schema.Format?.ToLower() switch
                {
                    "date" => "DateTime.Now.ToString(\"yyyy-MM-dd\")",
                    "date-time" => "DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\")",
                    "uuid" => "Guid.NewGuid().ToString()",
                    _ => $"\"{GetDefaultStringValue(schema)}\""
                },
                "integer" => "123",
                "number" => "123.45",
                "boolean" => "true",
                "array" => "new[] { \"test-item\" }",
                _ => "\"test-value\""
            };
        }

        private static string GetDefaultStringValue(OpenApiSchema schema)
        {
            // Use example if available
            if (schema.Example != null)
            {
                return schema.Example.ToString() ?? "test-string";
            }
            
            // Generate based on property name patterns
            return "test-string";
        }

        private static string GenerateRequestBodyFromSchema(OpenApiSchema schema)
        {
            var properties = new List<string>();

            if (schema.Properties?.Any() == true)
            {
                foreach (var prop in schema.Properties.Take(10)) // Limit to 10 properties
                {
                    var value = GenerateValueFromSchema(prop.Value, prop.Key);
                    properties.Add($"{prop.Key} = {value}");
                }
            }
            else
            {
                // Fallback for schemas without properties
                properties.Add("testProperty = \"testValue\"");
                properties.Add("id = Guid.NewGuid().ToString()");
            }

            return $"new {{ {string.Join(", ", properties)} }}";
        }

        private static string GenerateValueFromSchema(OpenApiSchema schema, string propertyName)
        {
            var type = schema.Type?.ToLower();
            
            return type switch
            {
                "string" => schema.Format?.ToLower() switch
                {
                    "date" => "DateTime.Now.ToString(\"yyyy-MM-dd\")",
                    "date-time" => "DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\")",
                    "uuid" => "Guid.NewGuid().ToString()",
                    _ => $"\"test-{propertyName.ToLower()}\""
                },
                "integer" => "123",
                "number" => "123.45",
                "boolean" => "true",
                "array" => "new[] { \"test-item\" }",
                _ => $"\"test-{propertyName.ToLower()}\""
            };
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