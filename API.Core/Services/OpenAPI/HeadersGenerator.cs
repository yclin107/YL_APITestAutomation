using API.Core.Models;
using System.Text;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates header code from OpenAPI specifications
    /// </summary>
    public static class HeadersGenerator
    {
        public static string GenerateHeadersFromOpenApi(OpenApiEndpointTest endpoint, bool excludeAuth = false)
        {
            var codeBuilder = new StringBuilder();
            
            // Always add standard headers
            if (!excludeAuth)
            {
                // OAuth2 is handled separately in the main request
            }
            
            // Add tenant and instance headers (these are special cases)
            codeBuilder.AppendLine();
            codeBuilder.Append("                    .Header(\"x-3e-tenantid\", context.TenantId)");
            codeBuilder.AppendLine();
            codeBuilder.Append("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
            
            // Add headers from OpenAPI parameters
            var headerParams = endpoint.Parameters?.Where(p => p.In == Microsoft.OpenApi.Models.ParameterLocation.Header).ToList() ?? new List<Microsoft.OpenApi.Models.OpenApiParameter>();
            
            foreach (var header in headerParams)
            {
                // Skip special headers that we handle differently
                if (IsSpecialHeader(header.Name))
                {
                    continue;
                }
                
                var headerValue = GenerateHeaderValue(header);
                codeBuilder.AppendLine();
                codeBuilder.Append($"                    .Header(\"{header.Name}\", {headerValue})");
            }
            
            return codeBuilder.ToString();
        }
        
        private static bool IsSpecialHeader(string headerName)
        {
            var specialHeaders = new[]
            {
                "authorization",
                "x-3e-tenantid", 
                "x-3e-instanceid",
                "x-3e-userid" // This one uses context.UserId
            };
            
            return specialHeaders.Contains(headerName.ToLower());
        }
        
        private static string GenerateHeaderValue(Microsoft.OpenApi.Models.OpenApiParameter header)
        {
            var headerNameLower = header.Name.ToLower();
            
            // Handle special context headers
            if (headerNameLower == "x-3e-userid")
            {
                return "context.UserId";
            }
            
            // Handle by schema type
            var type = header.Schema?.Type?.ToLower();
            return type switch
            {
                "string" => $"\"test-{header.Name.ToLower()}\"",
                "integer" => "123",
                "number" => "123.45",
                "boolean" => "true",
                _ => $"\"test-{header.Name.ToLower()}\""
            };
        }
    }
}