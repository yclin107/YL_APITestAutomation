using API.Core.Models;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates individual test methods for endpoints
    /// </summary>
    public static class TestMethodGenerator
    {
        public static string GenerateTestMethodsForEndpoint(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string tenant, string userId)
        {
            var codeBuilder = new StringBuilder();
            var sanitizedMethodName = SanitizeMethodName($"{endpoint.Method}_{endpoint.Path}");
            
            // Generate positive test
            codeBuilder.AppendLine(GeneratePositiveTest(spec, endpoint, sanitizedMethodName));
            
            // Generate unauthorized test if endpoint requires auth
            if (endpoint.RequiresAuth)
            {
                codeBuilder.AppendLine(GenerateUnauthorizedTest(spec, endpoint, sanitizedMethodName));
            }
            
            // Generate missing parameters test if endpoint has required parameters
            var requiredParams = endpoint.Parameters?.Where(p => p.Required).ToList() ?? new List<OpenApiParameter>();
            if (requiredParams.Any())
            {
                codeBuilder.AppendLine(GenerateMissingParametersTest(spec, endpoint, sanitizedMethodName));
            }
            
            // Generate schema validation test for success responses
            var successResponses = endpoint.Responses?.Where(r => r.Key.StartsWith("2")).ToList() ?? new List<KeyValuePair<string, OpenApiResponse>>();
            if (successResponses.Any())
            {
                codeBuilder.AppendLine(GenerateSchemaValidationTest(spec, endpoint, sanitizedMethodName));
            }
            
            return codeBuilder.ToString();
        }
        
        public static string GenerateFallbackTestMethods(OpenApiEndpointTest endpoint, string tenant, string userId)
        {
            var codeBuilder = new StringBuilder();
            var sanitizedMethodName = SanitizeMethodName($"{endpoint.Method}_{endpoint.Path}");
            
            codeBuilder.AppendLine($@"
        [Test]
        [Category(""Generated"")]
        [AllureTag(""{endpoint.Tags.FirstOrDefault() ?? "General"}"")]
        public void {sanitizedMethodName}_Positive_Test()
        {{
            var context = GetTestContext();
            InitContext(""{endpoint.Tags.FirstOrDefault() ?? "General"} API Feature"");
            var token = GetAuthToken(context);
            var baseUrl = GetBaseUrl();

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} - Positive Test"", () =>
            {{
                // NOTE: Simplified test due to memory constraints for {endpoint.Method} {endpoint.Path}
                var response = Given()
                    .OAuth2(token)
                    .Header(""x-3e-tenantid"", context.TenantId)
                    .Header(""X-3E-InstanceId"", context.InstanceId)
                    .When()
                    .{endpoint.Method.ToLower().Capitalize()}($""{{baseUrl}}{endpoint.Path}"")
                    .Then();

                var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
                AttachResponse(""{endpoint.Method}_{endpoint.Path.Replace("/", "_")}_Response"", rawJson);
                
                // Basic validation - endpoint is reachable
                Assert.That(response.Extract().Response().StatusCode, Is.Not.EqualTo(System.Net.HttpStatusCode.NotFound), 
                    ""Endpoint should be reachable"");
            }});
        }}");
            
            return codeBuilder.ToString();
        }
        
        private static string GeneratePositiveTest(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string sanitizedMethodName)
        {
            var codeBuilder = new StringBuilder();
            var hasRequestBody = endpoint.RequestBody != null;
            var requestBodyCode = hasRequestBody ? RequestBodyGenerator.GenerateRequestBodyFromOpenApi(spec, endpoint) : "";
            var headersCode = HeadersGenerator.GenerateHeadersFromOpenApi(endpoint);
            var parametersCode = ParametersGenerator.GenerateParametersFromOpenApi(endpoint);
            
            codeBuilder.AppendLine($@"
        [Test]
        [Category(""Generated"")]
        [AllureTag(""{endpoint.Tags.FirstOrDefault() ?? "General"}"")]
        public void {sanitizedMethodName}_Positive_Test()
        {{
            var context = GetTestContext();
            InitContext(""{endpoint.Tags.FirstOrDefault() ?? "General"} API Feature"");
            var token = GetAuthToken(context);
            var baseUrl = GetBaseUrl();

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} - Positive Test"", () =>
            {{");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine($@"                {requestBodyCode}");
            }

            codeBuilder.AppendLine($@"
                var response = Given()
                    .OAuth2(token){headersCode}{parametersCode}");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine("                    .Body(requestBody)");
            }

            codeBuilder.AppendLine($@"                    .When()
                    .{endpoint.Method.ToLower().Capitalize()}($""{{baseUrl}}{endpoint.Path}"")
                    .Then();

                var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
                AttachResponse(""{endpoint.Method}_{endpoint.Path.Replace("/", "_")}_Response"", rawJson);
                AllureApi.AddAttachment(""Response JSON"", ""application/json"", rawJson);
                
                // Validate successful response
                Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, 
                    ""Expected successful response"");
            }});
        }}");
            
            return codeBuilder.ToString();
        }
        
        private static string GenerateUnauthorizedTest(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string sanitizedMethodName)
        {
            var codeBuilder = new StringBuilder();
            var hasRequestBody = endpoint.RequestBody != null;
            var requestBodyCode = hasRequestBody ? RequestBodyGenerator.GenerateRequestBodyFromOpenApi(spec, endpoint) : "";
            var headersCode = HeadersGenerator.GenerateHeadersFromOpenApi(endpoint, excludeAuth: true);
            var parametersCode = ParametersGenerator.GenerateParametersFromOpenApi(endpoint);
            
            codeBuilder.AppendLine($@"
        [Test]
        [Category(""Generated"")]
        [AllureTag(""{endpoint.Tags.FirstOrDefault() ?? "General"}"")]
        public void {sanitizedMethodName}_Unauthorized_Test()
        {{
            var context = GetTestContext();
            InitContext(""{endpoint.Tags.FirstOrDefault() ?? "General"} API Feature"");
            var baseUrl = GetBaseUrl();

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} - Unauthorized Test"", () =>
            {{");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine($@"                {requestBodyCode}");
            }

            codeBuilder.AppendLine($@"
                var response = Given(){headersCode}{parametersCode}");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine("                    .Body(requestBody)");
            }

            codeBuilder.AppendLine($@"                    .When()
                    .{endpoint.Method.ToLower().Capitalize()}($""{{baseUrl}}{endpoint.Path}"")
                    .Then();

                var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
                AttachResponse(""{endpoint.Method}_{endpoint.Path.Replace("/", "_")}_Unauthorized_Response"", rawJson);
                AllureApi.AddAttachment(""Unauthorized Response JSON"", ""application/json"", rawJson);
                
                // Validate unauthorized response
                response.StatusCode(401);
            }});
        }}");
            
            return codeBuilder.ToString();
        }
        
        private static string GenerateMissingParametersTest(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string sanitizedMethodName)
        {
            var codeBuilder = new StringBuilder();
            var hasRequestBody = endpoint.RequestBody != null;
            var requestBodyCode = hasRequestBody ? RequestBodyGenerator.GenerateRequestBodyFromOpenApi(spec, endpoint) : "";
            var headersCode = HeadersGenerator.GenerateHeadersFromOpenApi(endpoint);
            
            codeBuilder.AppendLine($@"
        [Test]
        [Category(""Generated"")]
        [AllureTag(""{endpoint.Tags.FirstOrDefault() ?? "General"}"")]
        public void {sanitizedMethodName}_MissingParameters_Test()
        {{
            var context = GetTestContext();
            InitContext(""{endpoint.Tags.FirstOrDefault() ?? "General"} API Feature"");
            var token = GetAuthToken(context);
            var baseUrl = GetBaseUrl();

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} - Missing Parameters Test"", () =>
            {{");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine($@"                {requestBodyCode}");
            }

            codeBuilder.AppendLine($@"
                var response = Given()
                    .OAuth2(token){headersCode}");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine("                    .Body(requestBody)");
            }

            codeBuilder.AppendLine($@"                    .When()
                    .{endpoint.Method.ToLower().Capitalize()}($""{{baseUrl}}{endpoint.Path}"")
                    .Then();

                var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
                AttachResponse(""{endpoint.Method}_{endpoint.Path.Replace("/", "_")}_MissingParams_Response"", rawJson);
                AllureApi.AddAttachment(""Missing Parameters Response JSON"", ""application/json"", rawJson);
                
                // Validate bad request response for missing parameters
                response.StatusCode(400);
            }});
        }}");
            
            return codeBuilder.ToString();
        }
        
        private static string GenerateSchemaValidationTest(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string sanitizedMethodName)
        {
            var codeBuilder = new StringBuilder();
            var hasRequestBody = endpoint.RequestBody != null;
            var requestBodyCode = hasRequestBody ? RequestBodyGenerator.GenerateRequestBodyFromOpenApi(spec, endpoint) : "";
            var headersCode = HeadersGenerator.GenerateHeadersFromOpenApi(endpoint);
            var parametersCode = ParametersGenerator.GenerateParametersFromOpenApi(endpoint);
            
            codeBuilder.AppendLine($@"
        [Test]
        [Category(""Generated"")]
        [AllureTag(""{endpoint.Tags.FirstOrDefault() ?? "General"}"")]
        public void {sanitizedMethodName}_SchemaValidation_Test()
        {{
            var context = GetTestContext();
            InitContext(""{endpoint.Tags.FirstOrDefault() ?? "General"} API Feature"");
            var token = GetAuthToken(context);
            var baseUrl = GetBaseUrl();

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} - Schema Validation Test"", () =>
            {{");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine($@"                {requestBodyCode}");
            }

            codeBuilder.AppendLine($@"
                var response = Given()
                    .OAuth2(token){headersCode}{parametersCode}");

            if (hasRequestBody)
            {
                codeBuilder.AppendLine("                    .Body(requestBody)");
            }

            codeBuilder.AppendLine($@"                    .When()
                    .{endpoint.Method.ToLower().Capitalize()}($""{{baseUrl}}{endpoint.Path}"")
                    .Then();

                var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
                AttachResponse(""{endpoint.Method}_{endpoint.Path.Replace("/", "_")}_Schema_Response"", rawJson);
                AllureApi.AddAttachment(""Schema Validation Response JSON"", ""application/json"", rawJson);
            }});

            AllureApi.Step(""Validate response schema"", () =>
            {{
                if (response.Extract().Response().IsSuccessStatusCode)
                {{
                    ValidateResponseSchema_{sanitizedMethodName}(rawJson);
                }}
                else
                {{
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, ""Response should not be empty even for error responses"");
                }}
            }});
        }}");
            
            return codeBuilder.ToString();
        }
        
        private static string SanitizeMethodName(string input)
        {
            return input.Replace("/", "_")
                       .Replace("{", "")
                       .Replace("}", "")
                       .Replace("-", "_")
                       .Replace(":", "_")
                       .Replace(" ", "_")
                       .Replace("$", "_")
                       .Trim('_');
        }
    }
    
    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}