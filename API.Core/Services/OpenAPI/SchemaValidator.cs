using Microsoft.OpenApi.Models;
using NJsonSchema;
using NJsonSchema.Validation;
using System.Text.Json;

namespace API.Core.Services.OpenAPI
{
    public class SchemaValidator
    {
        private readonly Dictionary<string, JsonSchema> _schemaCache = new();

        public SchemaValidator()
        {
        }

        public async Task<JsonSchema?> GenerateSchemaFromOpenApiAsync(OpenApiSchema openApiSchema, string schemaKey)
        {
            if (_schemaCache.ContainsKey(schemaKey))
            {
                return _schemaCache[schemaKey];
            }

            try
            {
                var jsonSchema = await ConvertOpenApiSchemaToJsonSchemaAsync(openApiSchema);
                if (jsonSchema != null)
                {
                    _schemaCache[schemaKey] = jsonSchema;
                }
                return jsonSchema;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Failed to generate schema for {schemaKey}: {ex.Message}");
                return null;
            }
        }

        public async Task<ValidationResult> ValidateResponseAsync(string jsonResponse, JsonSchema schema)
        {
            try
            {
                var validator = new JsonSchemaValidator();
                var errors = validator.Validate(jsonResponse, schema);
                
                return new ValidationResult
                {
                    IsValid = !errors.Any(),
                    Errors = errors.Select(e => $"Path: {e.Path}, Error: {e.Kind} - {e.Property}").ToList(),
                    Schema = schema.ToJson(),
                    Response = jsonResponse
                };
            }
            catch (Exception ex)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Errors = new List<string> { $"Validation failed: {ex.Message}" },
                    Schema = schema.ToJson(),
                    Response = jsonResponse
                };
            }
        }

        public string GenerateSchemaValidationCode(string endpointKey, string schemaJson)
        {
            var sanitizedKey = SanitizeVariableName(endpointKey);
            
            return $@"
        private static readonly string {sanitizedKey}Schema = @""{EscapeJsonForString(schemaJson)}"";

        private void ValidateResponseSchema_{sanitizedKey}(string jsonResponse)
        {{
            var schema = NJsonSchema.JsonSchema.FromJsonAsync({sanitizedKey}Schema).Result;
            var validator = new NJsonSchema.Validation.JsonSchemaValidator();
            var errors = validator.Validate(jsonResponse, schema);
            
            Assert.That(errors.Count, Is.EqualTo(0), 
                $""Response schema validation failed. Errors: {{string.Join("", "", errors.Select(e => $""{{e.Path}}: {{e.Kind}} - {{e.Property}}""))}}"");
        }}";
        }

        private async Task<JsonSchema?> ConvertOpenApiSchemaToJsonSchemaAsync(OpenApiSchema openApiSchema)
        {
            try
            {
                var jsonSchemaObject = new
                {
                    type = openApiSchema.Type ?? "object",
                    properties = ConvertProperties(openApiSchema.Properties),
                    required = openApiSchema.Required?.ToArray() ?? Array.Empty<string>(),
                    additionalProperties = openApiSchema.AdditionalPropertiesAllowed
                };

                var jsonString = JsonSerializer.Serialize(jsonSchemaObject, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return await JsonSchema.FromJsonAsync(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI schema: {ex.Message}");
                return await CreateFallbackSchemaAsync();
            }
        }

        private Dictionary<string, object> ConvertProperties(IDictionary<string, OpenApiSchema> properties)
        {
            var result = new Dictionary<string, object>();
            
            foreach (var prop in properties)
            {
                var propertySchema = new Dictionary<string, object>();
                
                if (!string.IsNullOrEmpty(prop.Value.Type))
                {
                    propertySchema["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Type);
                }
                
                if (!string.IsNullOrEmpty(prop.Value.Format))
                {
                    propertySchema["format"] = prop.Value.Format;
                }
                
                if (!string.IsNullOrEmpty(prop.Value.Description))
                {
                    propertySchema["description"] = prop.Value.Description;
                }

                if (prop.Value.Properties?.Any() == true)
                {
                    propertySchema["type"] = "object";
                    propertySchema["properties"] = ConvertProperties(prop.Value.Properties);
                }

                if (prop.Value.Items != null)
                {
                    propertySchema["type"] = "array";
                    var itemSchema = new Dictionary<string, object>();
                    if (!string.IsNullOrEmpty(prop.Value.Items.Type))
                    {
                        itemSchema["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Items.Type);
                    }
                    propertySchema["items"] = itemSchema;
                }

                result[prop.Key] = propertySchema;
            }
            
            return result;
        }

        private string ConvertOpenApiTypeToJsonSchemaType(string openApiType)
        {
            return openApiType.ToLower() switch
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

        private async Task<JsonSchema> CreateFallbackSchemaAsync()
        {
            var fallbackSchema = @"{
                ""type"": ""object"",
                ""additionalProperties"": true,
                ""description"": ""Fallback schema - allows any object structure""
            }";
            
            return await JsonSchema.FromJsonAsync(fallbackSchema);
        }

        private string SanitizeVariableName(string input)
        {
            return input.Replace("/", "_")
                       .Replace("{", "")
                       .Replace("}", "")
                       .Replace("-", "_")
                       .Replace(":", "_")
                       .Replace(" ", "_")
                       .Trim('_');
        }

        private string EscapeJsonForString(string json)
        {
            return json.Replace("\"", "\\\"")
                      .Replace("\r", "")
                      .Replace("\n", "\\n");
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new();
        public string Schema { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}