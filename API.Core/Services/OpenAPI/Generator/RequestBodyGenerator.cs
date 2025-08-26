using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System.Text;
using System.Text.Json;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class RequestBodyGenerator
    {
        public string GenerateRequestBodyClass(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string className)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine();
            sb.AppendLine("namespace API.TestBase.Source.RequestBodies");
            sb.AppendLine("{");
            sb.AppendLine($"    public static class {className}");
            sb.AppendLine("    {");

            foreach (var endpoint in endpoints.Where(e => HasRequestBody(e)))
            {
                GenerateRequestBodyMethod(sb, endpoint, spec);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private bool HasRequestBody(OpenApiEndpointTest endpoint)
        {
            return endpoint.Method.ToUpper() == "POST" || 
                   endpoint.Method.ToUpper() == "PUT" || 
                   endpoint.Method.ToUpper() == "PATCH";
        }

        private void GenerateRequestBodyMethod(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            var methodName = GenerateRequestBodyMethodName(endpoint.Method, endpoint.Path);
            var requestBodyCode = ExtractRequestBodyFromOpenApi(endpoint, spec);

            sb.AppendLine($"        public static object Get{methodName}()");
            sb.AppendLine("        {");
            sb.AppendLine($"            return {requestBodyCode};");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private string GenerateRequestBodyMethodName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}_RequestBody";
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
                        return ConvertOpenApiValueToCSharp(jsonContent.Value.Value.Example);
                    }

                    // Try examples collection
                    if (jsonContent.Value.Value.Examples?.Any() == true)
                    {
                        var firstExample = jsonContent.Value.Value.Examples.First().Value;
                        if (firstExample.Value != null)
                        {
                            return ConvertOpenApiValueToCSharp(firstExample.Value);
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

        private string ConvertOpenApiValueToCSharp(IOpenApiAny openApiValue)
        {
            try
            {
                switch (openApiValue)
                {
                    case OpenApiObject obj:
                        var properties = obj.Select(kvp => 
                            $"{kvp.Key} = {ConvertOpenApiValueToCSharp(kvp.Value)}");
                        return $"new {{ {string.Join(", ", properties)} }}";

                    case OpenApiArray arr:
                        var items = arr.Select(ConvertOpenApiValueToCSharp);
                        return $"new object[] {{ {string.Join(", ", items)} }}";

                    case OpenApiString str:
                        return $"\"{str.Value}\"";

                    case OpenApiInteger intVal:
                        return intVal.Value.ToString();

                    case OpenApiDouble doubleVal:
                        return doubleVal.Value.ToString();

                    case OpenApiBoolean boolVal:
                        return boolVal.Value.ToString().ToLower();

                    default:
                        // Try to parse as JSON string
                        if (openApiValue.ToString() != null)
                        {
                            try
                            {
                                var jsonElement = JsonSerializer.Deserialize<JsonElement>(openApiValue.ToString());
                                return ConvertJsonElementToCSharp(jsonElement);
                            }
                            catch
                            {
                                return $"\"{openApiValue}\"";
                            }
                        }
                        return "null";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI value: {ex.Message}");
                return "null";
            }
        }

        private string ConvertJsonElementToCSharp(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var objProperties = element.EnumerateObject()
                        .Select(prop => $"{prop.Name} = {ConvertJsonElementToCSharp(prop.Value)}");
                    return $"new {{ {string.Join(", ", objProperties)} }}";

                case JsonValueKind.Array:
                    var arrayItems = element.EnumerateArray()
                        .Select(ConvertJsonElementToCSharp);
                    return $"new object[] {{ {string.Join(", ", arrayItems)} }}";

                case JsonValueKind.String:
                    return $"\"{element.GetString()}\"";

                case JsonValueKind.Number:
                    return element.GetDecimal().ToString();

                case JsonValueKind.True:
                    return "true";

                case JsonValueKind.False:
                    return "false";

                case JsonValueKind.Null:
                    return "null";

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
                    var properties = schema.Properties.Select(prop => 
                        $"{prop.Key} = {GenerateValueFromSchema(prop.Value, prop.Key)}");
                    return $"new {{ {string.Join(", ", properties)} }}";
                }

                return GenerateFallbackRequestBody();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating from schema: {ex.Message}");
                return GenerateFallbackRequestBody();
            }
        }

        private string GenerateValueFromSchema(OpenApiSchema schema, string propertyName)
        {
            if (schema.Format == "uuid" || propertyName.ToLower().Contains("id"))
            {
                return "Guid.NewGuid().ToString()";
            }

            return schema.Type?.ToLower() switch
            {
                "string" when schema.Format == "date-time" => "DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\")",
                "string" when schema.Format == "date" => "DateTime.Now.ToString(\"yyyy-MM-dd\")",
                "string" => $"\"test-{propertyName.ToLower()}\"",
                "integer" => "123",
                "number" => "123.45",
                "boolean" => "true",
                "array" => "new[] { \"test-item\" }",
                _ => $"\"test-{propertyName.ToLower()}\""
            };
        }

        private string GenerateFallbackRequestBody()
        {
            return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
        }
    }
}