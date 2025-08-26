using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System.Text;
using System.Text.Json;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class RequestBodyGenerator
    {
        public Dictionary<string, string> GenerateRequestBodyFiles(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string className)
        {
            var requestBodyFiles = new Dictionary<string, string>();

            foreach (var endpoint in endpoints.Where(e => HasRequestBody(e)))
            {
                var requestBodyJson = ExtractRequestBodyFromOpenApi(endpoint, spec);
                var fileName = GenerateRequestBodyFileName(endpoint.Method, endpoint.Path);
                requestBodyFiles[fileName] = requestBodyJson;
            }

            return requestBodyFiles;
        }

        private bool HasRequestBody(OpenApiEndpointTest endpoint)
        {
            return endpoint.Method.ToUpper() == "POST" || 
                   endpoint.Method.ToUpper() == "PUT" || 
                   endpoint.Method.ToUpper() == "PATCH";
        }

        private string GenerateRequestBodyFileName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}.json";
        }

        private string ExtractRequestBodyFromOpenApi(OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            try
            {
                // Find the actual OpenAPI operation in the document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return GenerateFallbackRequestBody();

                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value?.RequestBody == null) return GenerateFallbackRequestBody();

                // Look for JSON content
                var jsonContent = operation.Value.RequestBody.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json") || c.Key.Contains("json"));

                if (jsonContent.HasValue && jsonContent.Value.Value != null)
                {
                    // First try to get example
                    if (jsonContent.Value.Value.Example != null)
                    {
                        return ConvertOpenApiValueToJson(jsonContent.Value.Value.Example);
                    }

                    // Try examples collection
                    if (jsonContent.Value.Value.Examples?.Any() == true)
                    {
                        var firstExample = jsonContent.Value.Value.Examples.First().Value;
                        if (firstExample.Value != null)
                        {
                            return ConvertOpenApiValueToJson(firstExample.Value);
                        }
                    }

                    // Fallback to schema
                    if (jsonContent.Value.Value.Schema != null)
                    {
                        return GenerateRequestBodyFromSchema(jsonContent.Value.Value.Schema);
                    }
                }

                return GenerateFallbackRequestBody();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting request body for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return GenerateFallbackRequestBody();
            }
        }

        private string ConvertOpenApiValueToJson(IOpenApiAny openApiValue)
        {
            try
            {
                switch (openApiValue)
                {
                    case OpenApiObject obj:
                        var properties = obj.ToDictionary(kvp => kvp.Key, kvp => ConvertOpenApiValueToObject(kvp.Value));
                        return JsonSerializer.Serialize(properties, new JsonSerializerOptions { WriteIndented = true });

                    case OpenApiArray arr:
                        var items = arr.Select(ConvertOpenApiValueToObject).ToArray();
                        return JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });

                    case OpenApiString str:
                        return JsonSerializer.Serialize(str.Value);

                    case OpenApiInteger intVal:
                        return JsonSerializer.Serialize(intVal.Value);

                    case OpenApiDouble doubleVal:
                        return JsonSerializer.Serialize(doubleVal.Value);

                    case OpenApiBoolean boolVal:
                        return JsonSerializer.Serialize(boolVal.Value);

                    default:
                        // Try to parse as JSON string
                        if (openApiValue.ToString() != null)
                        {
                            return openApiValue.ToString();
                        }
                        return JsonSerializer.Serialize((object)null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI value: {ex.Message}");
                return JsonSerializer.Serialize((object)null);
            }
        }

        private object ConvertOpenApiValueToObject(IOpenApiAny openApiValue)
        {
            switch (openApiValue)
            {
                case OpenApiObject obj:
                    return obj.ToDictionary(kvp => kvp.Key, kvp => ConvertOpenApiValueToObject(kvp.Value));
                case OpenApiArray arr:
                    return arr.Select(ConvertOpenApiValueToObject).ToArray();
                case OpenApiString str:
                    return str.Value;
                case OpenApiInteger intVal:
                    return intVal.Value;
                case OpenApiDouble doubleVal:
                    return doubleVal.Value;
                case OpenApiBoolean boolVal:
                    return boolVal.Value;

                default:
                    return "null";
            }
        }

        private string GenerateRequestBodyFromSchema(OpenApiSchema schema)
        {
            try
            {
                if (schema.Properties?.Any() == true)
                {
                    var properties = schema.Properties.ToDictionary(
                        prop => prop.Key, 
                        prop => GenerateValueFromSchema(prop.Value, prop.Key));
                    return JsonSerializer.Serialize(properties, new JsonSerializerOptions { WriteIndented = true });
                }

                return GenerateFallbackRequestBody();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating from schema: {ex.Message}");
                return GenerateFallbackRequestBody();
            }
        }

        private object GenerateValueFromSchema(OpenApiSchema schema, string propertyName)
        {
            if (schema.Format == "uuid" || propertyName.ToLower().Contains("id"))
            {
                return Guid.NewGuid().ToString();
            }

            return schema.Type?.ToLower() switch
            {
                "string" when schema.Format == "date-time" => DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                "string" when schema.Format == "date" => DateTime.Now.ToString("yyyy-MM-dd"),
                "string" => $"test-{propertyName.ToLower()}",
                "integer" => 123,
                "number" => 123.45,
                "boolean" => true,
                "array" => new[] { "test-item" },
                _ => $"test-{propertyName.ToLower()}"
            };
        }

        private string GenerateFallbackRequestBody()
        {
            var fallback = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };
            return JsonSerializer.Serialize(fallback, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}