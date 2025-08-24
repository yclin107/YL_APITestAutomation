using Microsoft.OpenApi.Models;

namespace API.Core.Models.OpenAPI
{
    public class OpenApiTestSpec
    {
        public string SpecificationPath { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public OpenApiDocument Document { get; set; } = new();
        public DateTime LastModified { get; set; }
        public string Version { get; set; } = string.Empty;
        public Dictionary<string, OpenApiEndpointTest> EndpointTests { get; set; } = new();
    }

    public class OpenApiEndpointTest
    {
        public string Path { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string OperationId { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<OpenApiParameter> Parameters { get; set; } = new();
        public Dictionary<string, OpenApiResponse> Responses { get; set; } = new();
        public OpenApiRequestBody? RequestBody { get; set; }
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, OpenApiSecurityScheme> SecuritySchemes { get; set; } = new();
        public bool RequiresAuth { get; set; }
    }

    public class TestGenerationChange
    {
        public ChangeType Type { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }

    public enum ChangeType
    {
        Added,
        Modified,
        Removed,
        ParameterAdded,
        ParameterRemoved,
        ResponseAdded,
        ResponseRemoved,
        SchemaChanged
    }
}