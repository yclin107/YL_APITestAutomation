using Microsoft.OpenApi.Models;
using System.Text;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class EndpointGenerator
    {
        public string GenerateEndpointClass(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string className)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using static RestAssured.Dsl;");
            sb.AppendLine("using API.TestBase;");
            sb.AppendLine();
            sb.AppendLine("namespace API.TestBase.Source.Endpoints");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {className} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine("        private readonly string _baseUrl;");
            sb.AppendLine("        private readonly string _token;");
            sb.AppendLine("        private readonly TestContext _context;");
            sb.AppendLine();
            sb.AppendLine($"        public {className}(string baseUrl, string token, TestContext context)");
            sb.AppendLine("        {");
            sb.AppendLine("            _baseUrl = baseUrl;");
            sb.AppendLine("            _token = token;");
            sb.AppendLine("            _context = context;");
            sb.AppendLine("        }");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                GenerateEndpointMethod(sb, endpoint, spec);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void GenerateEndpointMethod(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            var methodName = GenerateMethodName(endpoint.Method, endpoint.Path);
            var parameters = GetMethodParameters(endpoint);

            sb.AppendLine($"        public RestAssured.Response.VerifiableResponse {methodName}({parameters})");
            sb.AppendLine("        {");
            
            // Replace path parameters in the URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                var paramName = GetParameterName(param.Name);
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{{paramName}}}");
            }
            
            sb.AppendLine("            var request = Given()");
            sb.AppendLine("                .OAuth2(_token)");

            // Add default headers
            sb.AppendLine("                .Header(\"x-3e-tenantid\", _context.TenantId)");
            
            // Add headers from endpoint parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name == "X-3E-UserId")
                    sb.AppendLine("                .Header(\"X-3E-UserId\", _context.UserId)");
                else if (param.Name == "X-3E-InstanceId")
                    sb.AppendLine("                .Header(\"X-3E-InstanceId\", _context.InstanceId)");
                else if (param.Name != "x-3e-tenantid") // Skip duplicate
                    sb.AppendLine($"                .Header(\"{param.Name}\", {GetParameterName(param.Name)} ?? GetConfigValue(\"headers\", \"{param.Name}\"))");
            }

            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                var paramName = GetParameterName(param.Name);
                sb.AppendLine($"                .QueryParam(\"{param.Name}\", {paramName} ?? GetConfigValue(\"queryParams\", \"{param.Name}\"))");
            }

            // Add request body if needed
            if (endpoint.Method.ToUpper() == "POST" || endpoint.Method.ToUpper() == "PUT" || endpoint.Method.ToUpper() == "PATCH")
            {
                sb.AppendLine("                .Body(requestBody ?? GetConfigValue(\"requestBodies\", \"default\"))");
            }

            sb.AppendLine("                .When()");
            
            // Use string interpolation for path parameters
            if (endpoint.Parameters.Any(p => p.In == ParameterLocation.Path))
            {
                sb.AppendLine($"                .{GetRestAssuredMethod(endpoint.Method)}(BuildUrl(\"{urlPath}\", new Dictionary<string, object?> {{");
                foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
                {
                    var paramName = GetParameterName(param.Name);
                    sb.AppendLine($"                    {{ \"{param.Name}\", {paramName} ?? GetConfigValue(\"pathParams\", \"{param.Name}\") }},");
                }
                sb.AppendLine("                }))");
            }
            else
            {
                sb.AppendLine($"                .{GetRestAssuredMethod(endpoint.Method)}($\"{{_baseUrl}}{endpoint.Path}\")");
            }

            sb.AppendLine("                .Then();");
            sb.AppendLine();
            sb.AppendLine("            return request;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private string GenerateMethodName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{method.ToUpper()}_{pathPart}";
        }

        private string GetMethodParameters(OpenApiEndpointTest endpoint)
        {
            var parameters = new List<string>();

            // Add request body parameter for POST/PUT/PATCH
            if (endpoint.Method.ToUpper() == "POST" || endpoint.Method.ToUpper() == "PUT" || endpoint.Method.ToUpper() == "PATCH")
            {
                parameters.Add("object? requestBody = null");
            }

            // Add header parameters (except special ones)
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                {
                    var paramType = GetCSharpType(param.Schema?.Type ?? "string");
                    parameters.Add($"{paramType} {GetParameterName(param.Name)} = null");
                }
            }

            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                var paramType = GetCSharpType(param.Schema?.Type ?? "string");
                parameters.Add($"{paramType} {GetParameterName(param.Name)} = null");
            }

            // Add path parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                var paramType = GetCSharpType(param.Schema?.Type ?? "string");
                parameters.Add($"{paramType} {GetParameterName(param.Name)} = null");
            }

            return string.Join(", ", parameters);
        }

        private string GetParameterName(string name)
        {
            return name.Replace("-", "").Replace(".", "").Replace("_", "").ToLower();
        }

        private string GetCSharpType(string openApiType)
        {
            return openApiType?.ToLower() switch
            {
                "integer" => "int?",
                "number" => "double?",
                "boolean" => "bool?",
                "array" => "object?",
                _ => "string?"
            };
        }

        private string GetRestAssuredMethod(string httpMethod)
        {
            return httpMethod.ToUpper() switch
            {
                "GET" => "Get",
                "POST" => "Post",
                "PUT" => "Put",
                "DELETE" => "Delete",
                "PATCH" => "Patch",
                _ => "Get"
            };
        }
    }
}