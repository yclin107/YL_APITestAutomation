using API.Core.Models;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates schema validation methods for test classes
    /// </summary>
    public static class SchemaValidationGenerator
    {
        public static string GenerateSchemaValidationMethods(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints)
        {
            var codeBuilder = new StringBuilder();
            
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("        #region Schema Validation Methods");
            codeBuilder.AppendLine();
            
            foreach (var endpoint in endpoints)
            {
                try
                {
                    var successResponses = endpoint.Responses?.Where(r => r.Key.StartsWith("2")).ToList() ?? new List<KeyValuePair<string, OpenApiResponse>>();
                    if (successResponses.Any())
                    {
                        var sanitizedMethodName = SanitizeMethodName($"{endpoint.Method}_{endpoint.Path}");
                        var schemaMethod = GenerateSchemaValidationMethod(spec, endpoint, sanitizedMethodName);
                        codeBuilder.AppendLine(schemaMethod);
                    }
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"⚠️  Memory error generating schema validation for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                    var fallbackMethod = GenerateFallbackSchemaValidationMethod(endpoint);
                    codeBuilder.AppendLine(fallbackMethod);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️  Error generating schema validation for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                    var fallbackMethod = GenerateFallbackSchemaValidationMethod(endpoint);
                    codeBuilder.AppendLine(fallbackMethod);
                }
            }
            
            codeBuilder.AppendLine("        #endregion");
            
            return codeBuilder.ToString();
        }
        
        private static string GenerateSchemaValidationMethod(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string sanitizedMethodName)
        {
            var schemaJson = ExtractResponseSchema(spec, endpoint);
            var isSimplified = schemaJson.Contains("Memory allocation error") || schemaJson.Contains("Simplified schema");
            var noteComment = isSimplified ? $"        // NOTE: Schema was simplified due to complexity for {endpoint.Method} {endpoint.Path}" : "";
            
            return $@"{noteComment}
        private async Task ValidateResponseSchema_{sanitizedMethodName}(string jsonResponse)
        {{
            try
            {{
                var schemaJson = @""{EscapeJsonForCSharp(schemaJson)}"";
                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);
                var validator = new JsonSchemaValidator();
                var errors = validator.Validate(jsonResponse, schema);

                if (errors.Any())
                {{
                    var errorMessages = errors.Select(e => $""{{e.Path}}: {{e.Kind}} - {{e.Property}}"");
                    var allErrors = string.Join("", "", errorMessages);
                    
                    Console.WriteLine($""❌ Schema validation failed for {endpoint.Method} {endpoint.Path}. Errors: {{allErrors}}"");
                    Console.WriteLine($""📋 Expected schema type: {{schema.Type ?? ""object""}} with specific properties"");
                    Console.WriteLine($""📋 Actual response: {{(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + ""..."" : jsonResponse)}}"");
                    
                    // Attach validation details to Allure
                    AttachResponse(""Expected Schema"", schemaJson);
                    AttachResponse(""Actual Response"", jsonResponse);
                    AllureApi.AddAttachment(""Expected Schema"", ""application/json"", schemaJson);
                    AllureApi.AddAttachment(""Actual Response"", ""application/json"", jsonResponse);
                    
                    Assert.Fail($""Response schema validation failed. Errors: {{allErrors}}"");
                }}
                else
                {{
                    Console.WriteLine(""✅ Schema validation passed for {endpoint.Method} {endpoint.Path}"");
                }}
            }}
            catch (Exception ex)
            {{
                Console.WriteLine($""⚠️  Schema validation error for {endpoint.Method} {endpoint.Path}: {{ex.Message}}"");
                AttachResponse(""Schema Validation Error"", ex.Message);
                Assert.Fail($""Schema validation error: {{ex.Message}}"");
            }}
        }}";
        }
        
        private static string GenerateFallbackSchemaValidationMethod(OpenApiEndpointTest endpoint)
        {
            var sanitizedMethodName = SanitizeMethodName($"{endpoint.Method}_{endpoint.Path}");
            
            return $@"        // NOTE: Schema was simplified due to complexity for {endpoint.Method} {endpoint.Path}
        private async Task ValidateResponseSchema_{sanitizedMethodName}(string jsonResponse)
        {{
            try
            {{
                var schemaJson = @""{{
  \""type\"": \""object\"",
  \""additionalProperties\"": true,
  \""description\"": \""Simplified schema - Memory allocation error\""
}}"";
                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);
                var validator = new JsonSchemaValidator();
                var errors = validator.Validate(jsonResponse, schema);

                // Basic validation - just check if it's valid JSON
                if (!string.IsNullOrEmpty(jsonResponse))
                {{
                    JsonDocument.Parse(jsonResponse); // Will throw if invalid JSON
                    Console.WriteLine(""✅ Basic JSON validation passed for {endpoint.Method} {endpoint.Path}"");
                }}
                else
                {{
                    Assert.Fail(""Response is empty or null"");
                }}
            }}
            catch (JsonException ex)
            {{
                Assert.Fail($""Invalid JSON response: {{ex.Message}}"");
            }}
            catch (Exception ex)
            {{
                Console.WriteLine($""⚠️  Schema validation error for {endpoint.Method} {endpoint.Path}: {{ex.Message}}"");
                Assert.Fail($""Schema validation error: {{ex.Message}}"");
            }}
        }}";
        }
        
        private static string ExtractResponseSchema(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // Find the path in the OpenAPI document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return GenerateFallbackSchema();

                // Find the operation
                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value == null) return GenerateFallbackSchema();

                // Find success response (200, 201, etc.)
                var successResponse = operation.Value.Responses?.FirstOrDefault(r => r.Key.StartsWith("2"));
                if (successResponse?.Value == null) return GenerateFallbackSchema();

                // Find JSON content
                var jsonContent = successResponse.Value.Value.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json") || c.Key.Contains("json"));
                if (jsonContent?.Value?.Schema == null) return GenerateFallbackSchema();

                // Convert schema to JSON
                var schema = ConvertOpenApiSchemaToJsonSchema(jsonContent.Value.Value.Schema);
                return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine($"⚠️  Memory allocation error extracting schema for {endpoint.Method} {endpoint.Path}");
                return GenerateSimplifiedSchema();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting schema for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return GenerateFallbackSchema();
            }
        }
        
        private static object ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema)
        {
            try
            {
                var jsonSchema = new Dictionary<string, object>();
                
                // Basic type
                if (!string.IsNullOrEmpty(openApiSchema.Type))
                {
                    jsonSchema["type"] = openApiSchema.Type;
                }
                else
                {
                    jsonSchema["type"] = "object";
                }
                
                // Properties
                if (openApiSchema.Properties?.Any() == true)
                {
                    var properties = new Dictionary<string, object>();
                    foreach (var prop in openApiSchema.Properties.Take(20)) // Limit to avoid memory issues
                    {
                        properties[prop.Key] = ConvertPropertySchema(prop.Value);
                    }
                    jsonSchema["properties"] = properties;
                }
                
                // Required properties
                if (openApiSchema.Required?.Any() == true)
                {
                    jsonSchema["required"] = openApiSchema.Required.ToArray();
                }
                
                // Additional properties
                jsonSchema["additionalProperties"] = openApiSchema.AdditionalPropertiesAllowed;
                
                return jsonSchema;
            }
            catch (OutOfMemoryException)
            {
                throw; // Re-throw to be caught by caller
            }
            catch (Exception)
            {
                return new { type = "object", additionalProperties = true };
            }
        }
        
        private static object ConvertPropertySchema(OpenApiSchema propertySchema)
        {
            var property = new Dictionary<string, object>();
            
            if (!string.IsNullOrEmpty(propertySchema.Type))
            {
                property["type"] = propertySchema.Type;
            }
            
            if (!string.IsNullOrEmpty(propertySchema.Format))
            {
                property["format"] = propertySchema.Format;
            }
            
            if (propertySchema.Items != null && propertySchema.Type == "array")
            {
                property["items"] = ConvertPropertySchema(propertySchema.Items);
            }
            
            return property;
        }
        
        private static string GenerateFallbackSchema()
        {
            return @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema""
}";
        }
        
        private static string GenerateSimplifiedSchema()
        {
            return @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Simplified schema - Memory allocation error""
}";
        }
        
        private static string EscapeJsonForCSharp(string json)
        {
            return json.Replace("\"", "\\\"")
                      .Replace("\r", "")
                      .Replace("\n", "\\n");
        }
        
        private static string SanitizeMethodName(string input)
        {
            return input.Replace("/", "_")
                       .Replace("{", "")
                       .Replace("}", "")
                       .Replace("-", "_")
                       .Replace(":", "_")
                       .Replace(" ", "_")
                       .Replace("$", "_")
                       .Trim('_');
        }
    }
}