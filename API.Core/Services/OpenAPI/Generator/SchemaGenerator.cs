using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class SchemaGenerator
    {
        public Dictionary<string, string> GenerateSchemaFiles(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string className)
        {
            var schemaFiles = new Dictionary<string, string>();

            foreach (var endpoint in endpoints)
            {
                var schemaJson = ExtractResponseSchema(endpoint, spec);
                var fileName = GenerateSchemaFileName(endpoint.Method, endpoint.Path);
                schemaFiles[fileName] = schemaJson;
            }

            return schemaFiles;
        }

        private string GenerateSchemaFileName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}_Response.json";
        }

        private string ExtractResponseSchema(OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            try
            {
                // Find the actual OpenAPI operation in the document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return GenerateFallbackSchema();

                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value == null) return GenerateFallbackSchema();

                // Look for success responses (200, 201, etc.)
                var successResponse = operation.Value.Responses.FirstOrDefault(r => r.Key.StartsWith("2"));
                if (successResponse.Value == null) return GenerateFallbackSchema();

                // Get JSON content
                var jsonContent = successResponse.Value.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json") || c.Key.Contains("json"));

                if (jsonContent.HasValue && jsonContent.Value.Value?.Schema != null)
                {
                    return ConvertOpenApiSchemaToJsonSchema(jsonContent.Value.Value.Schema);
                }

                return GenerateFallbackSchema();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting schema for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return GenerateFallbackSchema();
            }
        }

        private string ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema)
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

                return JsonSerializer.Serialize(jsonSchemaObject, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting schema: {ex.Message}");
                return GenerateFallbackSchema();
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

        private string GenerateFallbackSchema()
        {
            var fallbackSchema = new
            {
                type = "object",
                additionalProperties = true,
                description = "Fallback schema - allows any object structure"
            };

            return JsonSerializer.Serialize(fallbackSchema, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private string EscapeJsonForString(string json)
        {
            return json.Replace("\"", "\\\"")
                      .Replace("\r", "")
                      .Replace("\n", "\\n");
        }
    }
}