using Microsoft.OpenApi.Models;
using System.Text;
using API.Core.Models;

namespace API.Core.Services.OpenAPI.Generator
{
    public class TestMethodGenerator
    {
        public string GenerateTestMethods(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string className)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using System.Text.Json;");
            sb.AppendLine("using static RestAssured.Dsl;");
            sb.AppendLine("using API.TestBase.Source.Endpoints;");
            sb.AppendLine("using API.TestBase.Source.Schemas;");
            sb.AppendLine("using API.TestBase.Source.RequestBodies;");
            sb.AppendLine();
            sb.AppendLine("namespace API.TestBase.Source.Methods");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {className} : TestBase");
            sb.AppendLine("    {");
            sb.AppendLine("        private readonly string _baseUrl;");
            sb.AppendLine("        private readonly string _token;");
            sb.AppendLine("        private readonly TestContext _context;");
            sb.AppendLine($"        private readonly {GetEndpointClassName(className)} _endpoints;");
            sb.AppendLine();
            sb.AppendLine($"        public {className}(string baseUrl, string token, TestContext context)");
            sb.AppendLine("        {");
            sb.AppendLine("            _baseUrl = baseUrl;");
            sb.AppendLine("            _token = token;");
            sb.AppendLine("            _context = context;");
            sb.AppendLine($"            _endpoints = new {GetEndpointClassName(className)}(baseUrl, token, context);");
            sb.AppendLine("        }");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                GenerateTestMethod(sb, endpoint, spec, className);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private void GenerateTestMethod(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string className)
        {
            var methodName = GenerateTestMethodName(endpoint.Method, endpoint.Path);
            var endpointMethodName = GenerateEndpointMethodName(endpoint.Method, endpoint.Path);
            var schemaConstantName = GenerateSchemaConstantName(endpoint.Method, endpoint.Path, className);
            var requestBodyMethodName = GenerateRequestBodyMethodName(endpoint.Method, endpoint.Path, className);

            sb.AppendLine($"        public async Task<(bool Success, string Response, List<string> ValidationErrors)> {methodName}(");
            
            // Add parameters
            var parameters = GetMethodParameters(endpoint);
            if (parameters.Any())
            {
                sb.AppendLine($"            {string.Join(",\n            ", parameters)}");
            }
            
            sb.AppendLine("        )");
            sb.AppendLine("        {");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            
            // Get request body if needed
            if (HasRequestBody(endpoint))
            {
                sb.AppendLine($"                var requestBody = requestBody ?? {requestBodyMethodName}();");
            }

            sb.AppendLine($"                var response = _endpoints.{endpointMethodName}(");
            
            // Add method call parameters
            var callParameters = GetMethodCallParameters(endpoint);
            if (callParameters.Any())
            {
                sb.AppendLine($"                    {string.Join(",\n                    ", callParameters)}");
            }
            
            sb.AppendLine("                );");
            sb.AppendLine();
            sb.AppendLine("                var rawResponse = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("                var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine("                // Schema validation for success responses");
            sb.AppendLine("                var validationErrors = new List<string>();");
            sb.AppendLine("                if ((int)statusCode >= 200 && (int)statusCode < 300)");
            sb.AppendLine("                {");
            sb.AppendLine($"                    validationErrors = await {GetSchemaClassName(className)}.GetValidationErrors({schemaConstantName}, rawResponse);");
            sb.AppendLine("                }");
            sb.AppendLine();
            sb.AppendLine("                return (");
            sb.AppendLine("                    Success: validationErrors.Count == 0,");
            sb.AppendLine("                    Response: rawResponse,");
            sb.AppendLine("                    ValidationErrors: validationErrors");
            sb.AppendLine("                );");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                return (");
            sb.AppendLine("                    Success: false,");
            sb.AppendLine("                    Response: $\"Error: {ex.Message}\",");
            sb.AppendLine("                    ValidationErrors: new List<string> { ex.Message }");
            sb.AppendLine("                );");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private string GenerateTestMethodName(string method, string path)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"Execute_{method.ToUpper()}_{pathPart}";
        }

        private string GenerateEndpointMethodName(string method, string path)
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

        private string GenerateSchemaConstantName(string method, string path, string className)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{GetSchemaClassName(className)}.{method.ToUpper()}_{pathPart}_Schema";
        }

        private string GenerateRequestBodyMethodName(string method, string path, string className)
        {
            var cleanPath = path.Replace("/", "_")
                               .Replace("{", "")
                               .Replace("}", "")
                               .Replace("-", "_")
                               .Trim('_');
            
            var parts = cleanPath.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedParts = parts.Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower()).ToArray();
            var pathPart = string.Join("_", capitalizedParts);
            
            return $"{GetRequestBodyClassName(className)}.Get{method.ToUpper()}_{pathPart}_RequestBody";
        }

        private List<string> GetMethodParameters(OpenApiEndpointTest endpoint)
        {
            var parameters = new List<string>();

            // Add request body parameter for POST/PUT/PATCH
            if (HasRequestBody(endpoint))
            {
                parameters.Add("object requestBody = null");
            }

            // Add header parameters (except special ones)
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                {
                    parameters.Add($"string {GetParameterName(param.Name)} = null");
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
                parameters.Add($"{paramType} {GetParameterName(param.Name)}");
            }

            return parameters;
        }

        private List<string> GetMethodCallParameters(OpenApiEndpointTest endpoint)
        {
            var parameters = new List<string>();

            // Add request body parameter for POST/PUT/PATCH
            if (HasRequestBody(endpoint))
            {
                parameters.Add("requestBody");
            }

            // Add header parameters (except special ones)
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name != "X-3E-UserId" && param.Name != "X-3E-InstanceId" && param.Name != "x-3e-tenantid")
                {
                    parameters.Add(GetParameterName(param.Name));
                }
            }

            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                parameters.Add(GetParameterName(param.Name));
            }

            // Add path parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                parameters.Add(GetParameterName(param.Name));
            }

            return parameters;
        }

        private bool HasRequestBody(OpenApiEndpointTest endpoint)
        {
            return endpoint.Method.ToUpper() == "POST" || 
                   endpoint.Method.ToUpper() == "PUT" || 
                   endpoint.Method.ToUpper() == "PATCH";
        }

        private string GetParameterName(string name)
        {
            return name.Replace("-", "").Replace(".", "").ToLower();
        }

        private string GetCSharpType(string openApiType)
        {
            return openApiType?.ToLower() switch
            {
                "integer" => "int?",
                "number" => "double?",
                "boolean" => "bool?",
                "array" => "object[]",
                _ => "string"
            };
        }

        private string GetEndpointClassName(string baseClassName)
        {
            return $"{baseClassName}_Endpoints";
        }

        private string GetSchemaClassName(string baseClassName)
        {
            return $"{baseClassName}_Schemas";
        }

        private string GetRequestBodyClassName(string baseClassName)
        {
            return $"{baseClassName}_RequestBodies";
        }
    }
}