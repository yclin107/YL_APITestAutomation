using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using API.Core.Models.OpenAPI;
using YamlDotNet.Serialization;
using System.Text.Json;

namespace API.Core.Services.OpenAPI
{
    public class OpenApiSpecReader
    {
        public static async Task<OpenApiTestSpec> ReadSpecificationAsync(string filePath, string? baseUrl = null)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"OpenAPI specification file not found: {filePath}");

            var fileInfo = new FileInfo(filePath);
            var content = await File.ReadAllTextAsync(filePath);
            
            OpenApiDocument document;
            
            try
            {
                if (filePath.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) || 
                    filePath.EndsWith(".yml", StringComparison.OrdinalIgnoreCase))
                {
                    // Convert YAML to JSON first
                    var deserializer = new DeserializerBuilder().Build();
                    var yamlObject = deserializer.Deserialize(content);
                    var serializer = new SerializerBuilder().JsonCompatible().Build();
                    var json = serializer.Serialize(yamlObject);
                    
                    var reader = new OpenApiStringReader();
                    document = reader.Read(json, out var diagnostic);
                }
                else
                {
                    var reader = new OpenApiStringReader();
                    document = reader.Read(content, out var diagnostic);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to parse OpenAPI specification: {ex.Message}", ex);
            }

            var spec = new OpenApiTestSpec
            {
                SpecificationPath = filePath,
                BaseUrl = baseUrl ?? GetBaseUrlFromDocument(document),
                Document = document,
                LastModified = fileInfo.LastWriteTime,
                Version = document.Info?.Version ?? "1.0.0"
            };

            spec.EndpointTests = ExtractEndpointTests(document);
            
            return spec;
        }

        private static string GetBaseUrlFromDocument(OpenApiDocument document)
        {
            if (document.Servers?.Any() == true)
            {
                return document.Servers.First().Url;
            }
            return "https://api.example.com"; // Default fallback
        }

        private static Dictionary<string, OpenApiEndpointTest> ExtractEndpointTests(OpenApiDocument document)
        {
            var endpointTests = new Dictionary<string, OpenApiEndpointTest>();

            foreach (var path in document.Paths)
            {
                foreach (var operation in path.Value.Operations)
                {
                    var key = $"{operation.Key.ToString().ToUpper()}:{path.Key}";
                    var endpointTest = new OpenApiEndpointTest
                    {
                        Path = path.Key,
                        Method = operation.Key.ToString().ToUpper(),
                        OperationId = operation.Value.OperationId ?? GenerateOperationId(operation.Key.ToString(), path.Key),
                        Summary = operation.Value.Summary ?? string.Empty,
                        Description = operation.Value.Description ?? string.Empty,
                        Parameters = operation.Value.Parameters?.Select(p => new OpenApiParameter
                        {
                            Name = p.Name,
                            In = p.In,
                            Required = p.Required,
                            Schema = p.Schema != null ? new OpenApiSchema 
                            { 
                                Type = p.Schema.Type,
                                Format = p.Schema.Format
                            } : null
                        }).ToList() ?? new List<OpenApiParameter>(),
                        Responses = operation.Value.Responses?.ToDictionary(
                            r => r.Key, 
                            r => new OpenApiResponse 
                            { 
                                Description = r.Value.Description 
                            }) ?? new Dictionary<string, OpenApiResponse>(),
                        RequestBody = operation.Value.RequestBody != null ? new OpenApiRequestBody
                        {
                            Required = operation.Value.RequestBody.Required,
                            Description = operation.Value.RequestBody.Description
                        } : null,
                        Tags = operation.Value.Tags?.Select(t => t.Name).ToList() ?? new List<string>(),
                        RequiresAuth = operation.Value.Security?.Any() == true,
                        SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>()
                    };

                    endpointTests[key] = endpointTest;
                }
            }

            return endpointTests;
        }

        private static string GenerateOperationId(string method, string path)
        {
            var cleanPath = path.Replace("/", "_").Replace("{", "").Replace("}", "").Trim('_');
            return $"{method}_{cleanPath}";
        }
    }
}