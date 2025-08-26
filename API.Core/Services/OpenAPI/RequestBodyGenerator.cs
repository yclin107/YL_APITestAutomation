using API.Core.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates request body code from OpenAPI specifications
    /// </summary>
    public static class RequestBodyGenerator
    {
        public static string GenerateRequestBodyFromOpenApi(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // Try to get actual request body from the OpenAPI document
                var actualRequestBody = GetActualRequestBodyFromDocument(spec, endpoint);
                if (!string.IsNullOrEmpty(actualRequestBody))
                {
                    return $"var requestBody = {actualRequestBody};";
                }
                
                // Fallback to generating from endpoint info
                if (endpoint.RequestBody != null)
                {
                    return GenerateRequestBodyFromSchema(endpoint);
                }
                
                return "var requestBody = new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() };";
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine($"⚠️  Memory error generating request body for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return "var requestBody = new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() };";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating request body for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return "var requestBody = new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() };";
            }
        }
        
        private static string GetActualRequestBodyFromDocument(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // Find the path in the OpenAPI document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return null;

                // Find the operation (GET, POST, etc.)
                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value?.RequestBody == null) return null;

                // Look for application/json content
                var jsonContent = operation.Value.RequestBody.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json") || c.Key.Contains("json"));
                if (jsonContent?.Value == null) return null;

                // First, try to get from examples
                if (jsonContent.Value.Value.Examples?.Any() == true)
                {
                    var example = jsonContent.Value.Value.Examples.First().Value;
                    if (example.Value != null)
                    {
                        return ConvertOpenApiValueToCSharp(example.Value);
                    }
                }

                // Then try the single example
                if (jsonContent.Value.Value.Example != null)
                {
                    return ConvertOpenApiValueToCSharp(jsonContent.Value.Value.Example);
                }

                // Finally, try to generate from schema
                if (jsonContent.Value.Value.Schema != null)
                {
                    return GenerateRequestBodyFromSchema(jsonContent.Value.Value.Schema);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting request body from document: {ex.Message}");
                return null;
            }
        }
        
        private static string ConvertOpenApiValueToCSharp(IOpenApiAny openApiValue)
        {
            try
            {
                switch (openApiValue)
                {
                    case OpenApiObject obj:
                        var properties = new List<string>();
                        foreach (var prop in obj)
                        {
                            var value = ConvertOpenApiValueToCSharp(prop.Value);
                            properties.Add($"{prop.Key} = {value}");
                        }
                        return $"new {{ {string.Join(", ", properties)} }}";
                        
                    case OpenApiString str:
                        return $"\"{str.Value}\"";
                        
                    case OpenApiInteger integer:
                        return integer.Value.ToString();
                        
                    case OpenApiDouble doubleVal:
                        return doubleVal.Value.ToString();
                        
                    case OpenApiBoolean boolean:
                        return boolean.Value.ToString().ToLower();
                        
                    case OpenApiArray array:
                        var items = array.Select(ConvertOpenApiValueToCSharp);
                        return $"new[] {{ {string.Join(", ", items)} }}";
                        
                    default:
                        // Try to parse as JSON if it's a string representation
                        var jsonString = openApiValue.ToString();
                        if (!string.IsNullOrEmpty(jsonString) && (jsonString.StartsWith("{") || jsonString.StartsWith("[")))
                        {
                            try
                            {
                                var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);
                                return ConvertJsonElementToCSharp(jsonElement);
                            }
                            catch
                            {
                                return $"\"{jsonString}\"";
                            }
                        }
                        return $"\"{jsonString}\"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI value: {ex.Message}");
                return "\"fallback-value\"";
            }
        }
        
        private static string ConvertJsonElementToCSharp(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var properties = new List<string>();
                    foreach (var prop in element.EnumerateObject())
                    {
                        var value = ConvertJsonElementToCSharp(prop.Value);
                        properties.Add($"{prop.Name} = {value}");
                    }
                    return $"new {{ {string.Join(", ", properties)} }}";
                    
                case JsonValueKind.Array:
                    var items = element.EnumerateArray().Select(ConvertJsonElementToCSharp);
                    return $"new[] {{ {string.Join(", ", items)} }}";
                    
                case JsonValueKind.String:
                    return $"\"{element.GetString()}\"";
                    
                case JsonValueKind.Number:
                    return element.GetDecimal().ToString();
                    
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return element.GetBoolean().ToString().ToLower();
                    
                case JsonValueKind.Null:
                    return "null";
                    
                default:
                    return $"\"{element.ToString()}\"";
            }
        }
        
        private static string GenerateRequestBodyFromSchema(OpenApiEndpointTest endpoint)
        {
            // This is a fallback when we can't extract from the document
            var properties = new List<string>();
            properties.Add("id = Guid.NewGuid().ToString()");
            
            // Add some common properties based on endpoint path
            var path = endpoint.Path.ToLower();
            if (path.Contains("proforma"))
            {
                properties.Add("proformaId = Guid.NewGuid().ToString()");
            }
            if (path.Contains("user"))
            {
                properties.Add("userId = Guid.NewGuid().ToString()");
            }
            if (path.Contains("client"))
            {
                properties.Add("clientId = Guid.NewGuid().ToString()");
            }
            
            return $"new {{ {string.Join(", ", properties)} }}";
        }
        
        private static string GenerateRequestBodyFromSchema(OpenApiSchema schema)
        {
            try
            {
                var properties = new List<string>();
                
                if (schema.Properties?.Any() == true)
                {
                    foreach (var prop in schema.Properties.Take(10)) // Limit to avoid memory issues
                    {
                        var value = GenerateValueFromSchema(prop.Key, prop.Value);
                        properties.Add($"{prop.Key} = {value}");
                    }
                }
                else
                {
                    // Fallback
                    properties.Add("testProperty = \"testValue\"");
                    properties.Add("id = Guid.NewGuid().ToString()");
                }
                
                return $"new {{ {string.Join(", ", properties)} }}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating from schema: {ex.Message}");
                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
        }
        
        private static string GenerateValueFromSchema(string propertyName, OpenApiSchema propertySchema)
        {
            var lowerName = propertyName.ToLower();
            var type = propertySchema.Type?.ToLower();
            var format = propertySchema.Format?.ToLower();
            
            // Handle specific formats first
            if (format == "uuid" || lowerName.Contains("id"))
            {
                return "Guid.NewGuid().ToString()";
            }
            
            if (format == "date-time")
            {
                return "DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\")";
            }
            
            if (format == "date")
            {
                return "DateTime.Now.ToString(\"yyyy-MM-dd\")";
            }
            
            // Handle by type
            return type switch
            {
                "string" => $"\"test-{lowerName}\"",
                "integer" => "123",
                "number" => "123.45",
                "boolean" => "true",
                "array" => "new[] { \"test-item\" }",
                _ => $"\"test-{lowerName}\""
            };
        }
    }
}