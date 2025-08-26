using Microsoft.OpenApi.Models;
using System.Text.Json;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class ConfigGenerator
    {
        public string GenerateEndpointConfig(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var config = new
            {
                headers = GenerateHeadersConfig(endpoints),
                queryParams = GenerateQueryParamsConfig(endpoints),
                pathParams = GeneratePathParamsConfig(endpoints),
                requestBodies = GenerateRequestBodiesConfig(endpoints)
            };

            return JsonSerializer.Serialize(config, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private Dictionary<string, object> GenerateHeadersConfig(List<OpenApiEndpointTest> endpoints)
        {
            var headers = new Dictionary<string, object>();

            foreach (var endpoint in endpoints)
            {
                foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
                {
                    if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                    {
                        if (!headers.ContainsKey(param.Name))
                        {
                            headers[param.Name] = GenerateDefaultValue(param.Schema?.Type ?? "string", param.Name);
                        }
                    }
                }
            }

            return headers;
        }

        private Dictionary<string, object> GenerateQueryParamsConfig(List<OpenApiEndpointTest> endpoints)
        {
            var queryParams = new Dictionary<string, object>();

            foreach (var endpoint in endpoints)
            {
                foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
                {
                    if (!queryParams.ContainsKey(param.Name))
                    {
                        queryParams[param.Name] = GenerateDefaultValue(param.Schema?.Type ?? "string", param.Name);
                    }
                }
            }

            return queryParams;
        }

        private Dictionary<string, object> GeneratePathParamsConfig(List<OpenApiEndpointTest> endpoints)
        {
            var pathParams = new Dictionary<string, object>();

            foreach (var endpoint in endpoints)
            {
                foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
                {
                    if (!pathParams.ContainsKey(param.Name))
                    {
                        pathParams[param.Name] = GenerateDefaultValue(param.Schema?.Type ?? "string", param.Name);
                    }
                }
            }

            return pathParams;
        }

        private Dictionary<string, object> GenerateRequestBodiesConfig(List<OpenApiEndpointTest> endpoints)
        {
            return new Dictionary<string, object>
            {
                ["default"] = new 
                { 
                    name = "Test Name",
                    email = "test@example.com",
                    id = Guid.NewGuid().ToString()
                }
            };
        }

        private object GenerateDefaultValue(string type, string paramName)
        {
            if (paramName.ToLower().Contains("id") || type == "uuid")
            {
                return Guid.NewGuid().ToString();
            }

            return type?.ToLower() switch
            {
                "string" when paramName.ToLower().Contains("date") => DateTime.Now.ToString("yyyy-MM-dd"),
                "string" when paramName.ToLower().Contains("time") => DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                "string" when paramName.ToLower().Contains("email") => "test@example.com",
                "string" => $"test-{paramName.ToLower()}",
                "integer" => 123,
                "number" => 123.45,
                "boolean" => true,
                "array" => new[] { "test-item" },
                _ => $"test-{paramName.ToLower()}"
            };
        }
    }
}