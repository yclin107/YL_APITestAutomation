using API.Core.Models;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates query and path parameters code from OpenAPI specifications
    /// </summary>
    public static class ParametersGenerator
    {
        public static string GenerateParametersFromOpenApi(OpenApiEndpointTest endpoint)
        {
            var codeBuilder = new StringBuilder();
            
            // Add query parameters
            var queryParams = endpoint.Parameters?.Where(p => p.In == ParameterLocation.Query).ToList() ?? new List<OpenApiParameter>();
            foreach (var param in queryParams)
            {
                var paramValue = GenerateParameterValue(param);
                codeBuilder.AppendLine();
                codeBuilder.Append($"                    .QueryParam(\"{param.Name}\", {paramValue})");
            }
            
            // Path parameters are handled in the URL itself, but we can validate they exist
            var pathParams = endpoint.Parameters?.Where(p => p.In == ParameterLocation.Path).ToList() ?? new List<OpenApiParameter>();
            if (pathParams.Any())
            {
                // Add comment about path parameters
                codeBuilder.AppendLine();
                codeBuilder.Append($"                    // Path parameters: {string.Join(", ", pathParams.Select(p => p.Name))}");
            }
            
            return codeBuilder.ToString();
        }
        
        private static string GenerateParameterValue(OpenApiParameter parameter)
        {
            var type = parameter.Schema?.Type?.ToLower();
            var format = parameter.Schema?.Format?.ToLower();
            var paramName = parameter.Name.ToLower();
            
            // Handle specific parameter names
            if (paramName.Contains("id") && (format == "uuid" || type == "string"))
            {
                return "Guid.NewGuid().ToString()";
            }
            
            if (paramName.Contains("page"))
            {
                return "1";
            }
            
            if (paramName.Contains("limit") || paramName.Contains("count") || paramName.Contains("size"))
            {
                return "10";
            }
            
            if (paramName.Contains("date"))
            {
                return format == "date-time" ? 
                    "DateTime.Now.ToString(\"yyyy-MM-ddTHH:mm:ssZ\")" : 
                    "DateTime.Now.ToString(\"yyyy-MM-dd\")";
            }
            
            // Handle by type
            return type switch
            {
                "string" => $"GetTestValue(\"string\")",
                "integer" => "GetTestValue(\"integer\")",
                "number" => "GetTestValue(\"number\")",
                "boolean" => "GetTestValue(\"boolean\")",
                "array" => "GetTestValue(\"array\")",
                _ => $"GetTestValue(\"string\")"
            };
        }
    }
}