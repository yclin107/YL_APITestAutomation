using Microsoft.OpenApi.Models;
using System.Text;
using System.Net;
using API.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;

namespace API.Core.Services.OpenAPI
{
    public class TestCodeGenerator
    {
        private static readonly SchemaValidator _schemaValidator = new();

        public static string GenerateTestClassByTag(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tenant, string userId, string className, string tag)
        {
            // Generate all supporting files first
            GenerateEndpointClasses(spec, endpoints, tag);
            GenerateSchemaFiles(spec, endpoints, tag);
            GenerateRequestBodyFiles(spec, endpoints, tag);
            GenerateRequestMethods(spec, endpoints, tag);
            
            // Generate the main test class
            return GenerateMainTestClass(endpoints, className, tag);
        }

        private static void GenerateEndpointClasses(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var sanitizedTag = SanitizeForFileSystem(tag);
            var endpointsDir = Path.Combine(GetSourcePath(), "Endpoints", sanitizedTag);
            Directory.CreateDirectory(endpointsDir);

            foreach (var endpoint in endpoints)
            {
                var endpointClass = GenerateEndpointClass(endpoint);
                var fileName = $"{SanitizeMethodName(endpoint.OperationId)}Endpoint.cs";
                var filePath = Path.Combine(endpointsDir, fileName);
                File.WriteAllText(filePath, endpointClass);
            }
        }

        private static void GenerateSchemaFiles(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var sanitizedTag = SanitizeForFileSystem(tag);
            var schemasDir = Path.Combine(GetSourcePath(), "Schemas", sanitizedTag);
            Directory.CreateDirectory(schemasDir);

            foreach (var endpoint in endpoints)
            {
                try
                {
                    var schema = ExtractResponseSchema(spec, endpoint);
                    var fileName = $"{SanitizeMethodName(endpoint.OperationId)}Schema.json";
                    var filePath = Path.Combine(schemasDir, fileName);
                    File.WriteAllText(filePath, schema);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️  Memory error processing schema for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                    var fallbackSchema = GenerateFallbackSchema(endpoint);
                    var fileName = $"{SanitizeMethodName(endpoint.OperationId)}Schema.json";
                    var filePath = Path.Combine(schemasDir, fileName);
                    File.WriteAllText(filePath, fallbackSchema);
                }
            }
        }

        private static void GenerateRequestBodyFiles(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var sanitizedTag = SanitizeForFileSystem(tag);
            var requestBodiesDir = Path.Combine(GetSourcePath(), "RequestBodies", sanitizedTag);
            Directory.CreateDirectory(requestBodiesDir);

            foreach (var endpoint in endpoints)
            {
                if (endpoint.RequestBody != null)
                {
                    var requestBodyCode = GenerateRequestBodyFromOpenApi(spec, endpoint);
                    var fileName = $"{SanitizeMethodName(endpoint.OperationId)}RequestBody.cs";
                    var filePath = Path.Combine(requestBodiesDir, fileName);
                    
                    var fullClass = $@"namespace API.TestBase.Source.RequestBodies.{SanitizeForFileSystem(tag)}
{{
    public static class {SanitizeMethodName(endpoint.OperationId)}RequestBody
    {{
        public static object GetRequestBody()
        {{
            return {requestBodyCode};
        }}
    }}
}}";
                    File.WriteAllText(filePath, fullClass);
                }
            }
        }

        private static void GenerateRequestMethods(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var sanitizedTag = SanitizeForFileSystem(tag);
            var methodsDir = Path.Combine(GetSourcePath(), "Methods", sanitizedTag);
            Directory.CreateDirectory(methodsDir);

            var methodsClass = GenerateRequestMethodsClass(spec, endpoints, tag);
            var fileName = $"{sanitizedTag}RequestMethods.cs";
            var filePath = Path.Combine(methodsDir, fileName);
            File.WriteAllText(filePath, methodsClass);
        }

        private static string GenerateMainTestClass(List<OpenApiEndpointTest> endpoints, string className, string tag)
        {
            var testMethods = new StringBuilder();
            var sanitizedTag = SanitizeForFileSystem(tag);

            foreach (var endpoint in endpoints)
            {
                var testTypes = new[]
                {
                    ("PositiveTest", "200"),
                    ("UnauthorizedTest", "401"),
                    ("MissingRequiredParametersTest", "400"),
                    ("SchemaValidationTest", "schema")
                };

                foreach (var (testType, expectedResult) in testTypes)
                {
                    testMethods.AppendLine(GenerateTestMethod(endpoint, testType, expectedResult, sanitizedTag));
                }
            }

            return $@"using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using API.Core.Helpers;
using API.TestBase.Source.Methods.{sanitizedTag};
using System.Net;
using System.Text;
using System.Text.Json;
using NJsonSchema;
using NJsonSchema.Validation;

namespace API.TestBase.Tests.Generated.{sanitizedTag}
{{
    [TestFixture]
    [AllureFeature(""{tag} API Tests"")]
    public class {className} : TestBase
    {{
        private {sanitizedTag}RequestMethods _requestMethods;

        [SetUp]
        public void Setup()
        {{
            _requestMethods = new {sanitizedTag}RequestMethods();
        }}

{testMethods}
    }}
}}";
        }

        private static string GenerateTestMethod(OpenApiEndpointTest endpoint, string testType, string expectedResult, string tag)
        {
            var methodName = SanitizeMethodName(endpoint.OperationId);
            var category = tag;
            
            return testType switch
            {
                "PositiveTest" => GeneratePositiveTest(endpoint, methodName, category),
                "UnauthorizedTest" => GenerateUnauthorizedTest(endpoint, methodName, category),
                "MissingRequiredParametersTest" => GenerateMissingParamsTest(endpoint, methodName, category),
                "SchemaValidationTest" => GenerateSchemaValidationTest(endpoint, methodName, category),
                _ => ""
            };
        }

        private static string GeneratePositiveTest(OpenApiEndpointTest endpoint, string methodName, string category)
        {
            return $@"        [Test]
        [Category(""{category}"")]
        public void {methodName}_PositiveTest()
        {{
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, ""{category} API Feature"");

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path}"", () =>
            {{
                var response = _requestMethods.Execute{methodName}(context);
                
                Assert.Multiple(() =>
                {{
                    Assert.That(response.IsSuccessStatusCode, Is.True, ""Request should be successful"");
                    Assert.That(string.IsNullOrEmpty(response.Content), Is.False, ""Response should not be empty"");
                }});
            }});
        }}
";
        }

        private static string GenerateUnauthorizedTest(OpenApiEndpointTest endpoint, string methodName, string category)
        {
            return $@"        [Test]
        [Category(""{category}"")]
        public void {methodName}_UnauthorizedTest()
        {{
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, ""{category} API Feature"");

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} without authorization"", () =>
            {{
                var response = _requestMethods.Execute{methodName}Unauthorized();
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }});
        }}
";
        }

        private static string GenerateMissingParamsTest(OpenApiEndpointTest endpoint, string methodName, string category)
        {
            return $@"        [Test]
        [Category(""{category}"")]
        public void {methodName}_MissingRequiredParametersTest()
        {{
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, ""{category} API Feature"");

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} with missing required parameters"", () =>
            {{
                var response = _requestMethods.Execute{methodName}WithMissingParams();
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            }});
        }}
";
        }

        private static string GenerateSchemaValidationTest(OpenApiEndpointTest endpoint, string methodName, string category)
        {
            return $@"        [Test]
        [Category(""{category}"")]
        public void {methodName}_SchemaValidationTest()
        {{
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, ""{category} API Feature"");

            AllureApi.Step(""Execute {endpoint.Method} {endpoint.Path} for schema validation"", () =>
            {{
                var response = _requestMethods.Execute{methodName}(context);
                var schemaValidator = new {methodName}SchemaValidator();
                schemaValidator.ValidateResponse(response.Content);
            }});
        }}
";
        }

        private static string GenerateEndpointClass(OpenApiEndpointTest endpoint)
        {
            var methodName = SanitizeMethodName(endpoint.OperationId);
            
            return $@"namespace API.TestBase.Source.Endpoints
{{
    public static class {methodName}Endpoint
    {{
        public const string Path = ""{endpoint.Path}"";
        public const string Method = ""{endpoint.Method}"";
        public const string OperationId = ""{endpoint.OperationId}"";
        public const string Summary = ""{endpoint.Summary?.Replace("\"", "\\\"")}"";
        public const string Description = ""{endpoint.Description?.Replace("\"", "\\\"")}"";
        
        public static readonly string[] Tags = {{ {string.Join(", ", endpoint.Tags.Select(t => $"\"{t}\""))} }};
        public static readonly bool RequiresAuth = {endpoint.RequiresAuth.ToString().ToLower()};
    }}
}}";
        }

        private static string GenerateRequestMethodsClass(OpenApiTestSpec spec, List<OpenApiEndpointTest> endpoints, string tag)
        {
            var sanitizedTag = SanitizeForFileSystem(tag);
            var methods = new StringBuilder();

            foreach (var endpoint in endpoints)
            {
                var methodName = SanitizeMethodName(endpoint.OperationId);
                methods.AppendLine(GenerateRequestMethod(spec, endpoint, methodName));
                methods.AppendLine(GenerateUnauthorizedRequestMethod(spec, endpoint, methodName));
                methods.AppendLine(GenerateMissingParamsRequestMethod(spec, endpoint, methodName));
            }

            return $@"using static RestAssured.Dsl;
using API.TestBase.Source.RequestBodies.{sanitizedTag};
using API.TestBase.Source.Endpoints;
using System.Net;

namespace API.TestBase.Source.Methods.{sanitizedTag}
{{
    public class {sanitizedTag}RequestMethods
    {{
        private string GetBaseUrl(TestContext context)
        {{
            // Get from profile or context
            var profileManager = new API.Core.Helpers.ProfileManager();
            var profilePath = Environment.GetEnvironmentVariable(""TEST_PROFILE"");
            var masterPassword = Environment.GetEnvironmentVariable(""MASTER_PASSWORD"");
            
            if (string.IsNullOrEmpty(profilePath)) return ""https://api.example.com"";
            
            var parts = profilePath.Split('/');
            if (parts.Length != 3) return ""https://api.example.com"";
            
            var profile = profileManager.LoadProfileAsync(parts[0], parts[1], parts[2], masterPassword).Result;
            return profile?.ProformaApiUrl ?? ""https://api.example.com"";
        }}

        private string GetAuthToken(TestContext context)
        {{
            var userConfig = new API.Core.Helpers.UserConfig
            {{
                LoginId = context.User.LoginId,
                FirstName = context.User.FirstName,
                LastName = context.User.LastName,
                Password = context.User.Password,
                DefaultTimekeeperIndex = context.User.DefaultTimekeeperIndex,
                DefaultTimekeeperNumber = context.User.DefaultTimekeeperNumber
            }};
            
            return API.Core.Helpers.TokenService.PPSProformaToken(context.TenantId, userConfig).Result;
        }}

{methods}
    }}

    public class ApiResponse
    {{
        public bool IsSuccessStatusCode {{ get; set; }}
        public HttpStatusCode StatusCode {{ get; set; }}
        public string Content {{ get; set; }} = string.Empty;
    }}
}}";
        }

        private static string GenerateRequestMethod(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string methodName)
        {
            var headers = GenerateHeadersFromOpenApi(endpoint);
            var queryParams = GenerateQueryParamsFromOpenApi(endpoint);
            var pathParams = GeneratePathParamsFromOpenApi(endpoint);
            var requestBodyCall = endpoint.RequestBody != null ? $"{methodName}RequestBody.GetRequestBody()" : "null";
            
            return $@"        public ApiResponse Execute{methodName}(TestContext context)
        {{
            var baseUrl = GetBaseUrl(context);
            var token = GetAuthToken(context);
            var path = {methodName}Endpoint.Path;
            
            // Replace path parameters
{pathParams}
            
            var request = Given()
                .OAuth2(token)
{headers}
{queryParams};
            
{(endpoint.RequestBody != null ? $"            var requestBody = {requestBodyCall};\n            request = request.Body(requestBody);" : "")}
            
            var response = request
                .When()
                .{endpoint.Method.ToLower().Substring(0, 1).ToUpper() + endpoint.Method.ToLower().Substring(1)}($""{{baseUrl}}{{path}}"")
                .Then()
                .Extract().Response();
                
            return new ApiResponse
            {{
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Content = response.Content.ReadAsStringAsync().Result
            }};
        }}
";
        }

        private static string GenerateUnauthorizedRequestMethod(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string methodName)
        {
            var queryParams = GenerateQueryParamsFromOpenApi(endpoint);
            var pathParams = GeneratePathParamsFromOpenApi(endpoint);
            
            return $@"        public ApiResponse Execute{methodName}Unauthorized()
        {{
            var baseUrl = ""https://api.example.com""; // Default for unauthorized tests
            var path = {methodName}Endpoint.Path;
            
            // Replace path parameters
{pathParams}
            
            var request = Given()
{queryParams};
            
            var response = request
                .When()
                .{endpoint.Method.ToLower().Substring(0, 1).ToUpper() + endpoint.Method.ToLower().Substring(1)}($""{{baseUrl}}{{path}}"")
                .Then()
                .Extract().Response();
                
            return new ApiResponse
            {{
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Content = response.Content.ReadAsStringAsync().Result
            }};
        }}
";
        }

        private static string GenerateMissingParamsRequestMethod(OpenApiTestSpec spec, OpenApiEndpointTest endpoint, string methodName)
        {
            var pathParams = GeneratePathParamsFromOpenApi(endpoint);
            
            return $@"        public ApiResponse Execute{methodName}WithMissingParams()
        {{
            var baseUrl = ""https://api.example.com""; // Default for missing params tests
            var path = {methodName}Endpoint.Path;
            
            // Replace path parameters (but skip required query/header params)
{pathParams}
            
            var response = Given()
                .When()
                .{endpoint.Method.ToLower().Substring(0, 1).ToUpper() + endpoint.Method.ToLower().Substring(1)}($""{{baseUrl}}{{path}}"")
                .Then()
                .Extract().Response();
                
            return new ApiResponse
            {{
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Content = response.Content.ReadAsStringAsync().Result
            }};
        }}
";
        }

        private static string GenerateHeadersFromOpenApi(OpenApiEndpointTest endpoint)
        {
            var headers = new StringBuilder();
            
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                var headerValue = param.Name switch
                {
                    "X-3E-UserId" => "context.UserId",
                    "X-3E-InstanceId" => "context.InstanceId",
                    "x-3e-tenantid" => "context.TenantId",
                    _ => $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")"
                };
                
                headers.AppendLine($"                .Header(\"{param.Name}\", {headerValue})");
            }
            
            return headers.ToString();
        }

        private static string GenerateQueryParamsFromOpenApi(OpenApiEndpointTest endpoint)
        {
            var queryParams = new StringBuilder();
            
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                queryParams.AppendLine($"                .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
            }
            
            return queryParams.ToString();
        }

        private static string GeneratePathParamsFromOpenApi(OpenApiEndpointTest endpoint)
        {
            var pathParams = new StringBuilder();
            
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                pathParams.AppendLine($"            path = path.Replace(\"{{{param.Name}}}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\").ToString());");
            }
            
            return pathParams.ToString();
        }

        private static string GetSourcePath()
        {
            var currentDir = AppContext.BaseDirectory;
            var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
            var sourcePath = Path.Combine(solutionRoot, "API.TestBase", "Source");
            return Path.GetFullPath(sourcePath);
        }

        private static string ExtractResponseSchema(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // Find the endpoint in the OpenAPI document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return GenerateFallbackSchema(endpoint);

                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value == null) return GenerateFallbackSchema(endpoint);

                // Look for success responses (200, 201, etc.)
                var successResponse = operation.Value.Responses.FirstOrDefault(r => r.Key.StartsWith("2"));
                if (successResponse.Value == null) return GenerateFallbackSchema(endpoint);

                // Get JSON content
                var jsonContent = successResponse.Value.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json") || c.Key.Contains("json"));

                if (jsonContent.HasValue && jsonContent.Value.Value?.Schema != null)
                {
                    var schema = ConvertOpenApiSchemaToJsonSchema(jsonContent.Value.Value.Schema);
                    return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
                    { 
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }

                return GenerateFallbackSchema(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting schema for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return GenerateFallbackSchema(endpoint);
            }
        }

        private static string GenerateFallbackSchema(OpenApiEndpointTest endpoint)
        {
            var schema = new
            {
                type = "object",
                additionalProperties = true,
                description = "Fallback response schema - Memory allocation error",
                _note = $"Endpoint: {endpoint.Method} {endpoint.Path}"
            };

            return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

        private static object ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema)
        {
            var schema = new Dictionary<string, object>();
            
            if (!string.IsNullOrEmpty(openApiSchema.Type))
            {
                schema["type"] = openApiSchema.Type;
            }
            
            if (openApiSchema.Properties?.Any() == true)
            {
                var properties = new Dictionary<string, object>();
                foreach (var prop in openApiSchema.Properties)
                {
                    properties[prop.Key] = ConvertOpenApiSchemaToJsonSchema(prop.Value);
                }
                schema["properties"] = properties;
            }
            
            if (openApiSchema.Items != null)
            {
                schema["items"] = ConvertOpenApiSchemaToJsonSchema(openApiSchema.Items);
            }
            
            if (openApiSchema.Required?.Any() == true)
            {
                schema["required"] = openApiSchema.Required.ToArray();
            }
            
            schema["additionalProperties"] = openApiSchema.AdditionalPropertiesAllowed;
            
            return schema;
        }

        private static void GenerateSchemaValidationMethods(StringBuilder sb, List<OpenApiEndpointTest> endpoints, OpenApiTestSpec spec)
        {
            sb.AppendLine("        #region Schema Validation Methods");
            sb.AppendLine();

            foreach (var endpoint in endpoints)
            {
                var methodName = SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path);
                var schemaKey = $"{endpoint.Method}:{endpoint.Path}";
                
                // Try to get schema from successful responses (200, 201, etc.)
                var successResponse = endpoint.Responses.FirstOrDefault(r => r.Key.StartsWith("2"));
                
                if (!string.IsNullOrEmpty(successResponse.Key) && successResponse.Value != null)
                {
                    GenerateSchemaValidationMethod(sb, methodName, schemaKey, successResponse.Value, spec);
                }
                else
                {
                    // Generate a basic validation method if no schema is available
                    GenerateBasicValidationMethod(sb, methodName);
                }
            }

            sb.AppendLine("        #endregion");
            sb.AppendLine();
        }

        private static void GenerateSchemaValidationMethod(StringBuilder sb, string methodName, string schemaKey, OpenApiResponse response, OpenApiTestSpec spec)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            sb.AppendLine($"        private async Task ValidateResponseSchema_{methodName}(string jsonResponse)");
            sb.AppendLine("        {");
            sb.AppendLine("            AllureApi.AddAttachment(\"Actual Response\", \"application/json\", Encoding.UTF8.GetBytes(jsonResponse));");
            sb.AppendLine();
            
            // Generate schema JSON from OpenAPI response
            string schemaJson;
            try
            {
                schemaJson = GenerateSchemaFromResponse(response, spec, schemaKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Memory error generating schema for {endpointInfo}: {ex.Message}");
                schemaJson = CreateFallbackSchema("Memory allocation error - schema too large", endpointInfo);
            }
            
            var isRealSchema = !schemaJson.Contains("\"description\": \"Generic response schema\"") && 
                              !schemaJson.Contains("\"description\": \"Fallback response schema\"");

            if (isRealSchema)
            {
                // Generate real schema validation
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine($"                var schemaJson = @\"{FormatJsonForCSharp(schemaJson)}\";");
                sb.AppendLine();
                sb.AppendLine("                AllureApi.AddAttachment(\"Expected Schema\", \"application/json\", Encoding.UTF8.GetBytes(schemaJson));");
                sb.AppendLine();
                sb.AppendLine("                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);");
                sb.AppendLine("                var validator = new JsonSchemaValidator();");
                sb.AppendLine("                var errors = validator.Validate(jsonResponse, schema);");
                sb.AppendLine();
                sb.AppendLine("                if (errors.Any())");
                sb.AppendLine("                {");
                sb.AppendLine("                    var errorMessages = errors.Select(e => $\"{e.Path}: {e.Kind} - {e.Property}\");");
                sb.AppendLine("                    var allErrors = string.Join(\", \", errorMessages);");
                //sb.AppendLine("                    Console.WriteLine($\"📋 Expected schema type: object with specific properties\");");
                //sb.AppendLine("                    Console.WriteLine($\"📋 Actual response: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
                sb.AppendLine("                    Assert.Fail($\"Response schema validation failed. Errors: {allErrors}\");");
                sb.AppendLine("                }");
                sb.AppendLine("                else");
                sb.AppendLine("                {");
                //sb.AppendLine($"                    Console.WriteLine(\"✅ Schema validation passed\");");
                if (schemaJson.Contains("too complex") || schemaJson.Contains("Simplified schema") || schemaJson.Contains("Memory allocation error"))
                {
                    sb.AppendLine("                    // NOTE: Schema was simplified due to complexity - consider manual validation for critical fields");
                }
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (Exception ex)");
                sb.AppendLine("            {");
                //sb.AppendLine("                Console.WriteLine($\"❌ Schema validation exception: {ex.Message}\");");
                sb.AppendLine("                Assert.Fail($\"Schema validation error: {ex.Message}\");");
                sb.AppendLine("            }");
            }
            else
            {
                // Generate basic validation as fallback
                sb.AppendLine("            // Basic validation - ensure response is valid JSON");
                sb.AppendLine($"            // NOTE: Schema was simplified due to complexity");
                sb.AppendLine("            try");
                sb.AppendLine("            {");
                sb.AppendLine("                // Validate JSON structure");
                sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
                sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
                sb.AppendLine("                Console.WriteLine($\"📋 Actual response: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
                sb.AppendLine($"                Console.WriteLine(\"⚠️  Basic validation - schema was simplified\");");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (JsonException ex)");
                sb.AppendLine("            {");
                sb.AppendLine("                Console.WriteLine($\"❌ Invalid JSON: {ex.Message}\");");
                sb.AppendLine("                Assert.Fail($\"Response is not valid JSON: {ex.Message}\");");
                sb.AppendLine("            }");
            }
            
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateBasicValidationMethod(StringBuilder sb, string methodName)
        {
            sb.AppendLine($"        private void ValidateResponseSchema_{methodName}(string jsonResponse)");
            sb.AppendLine("        {");
            sb.AppendLine("            AllureApi.AddAttachment(\"Actual Response\", \"application/json\", Encoding.UTF8.GetBytes(jsonResponse));");
            sb.AppendLine();
            sb.AppendLine("            // Basic validation - ensure response is valid JSON");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                JsonDocument.Parse(jsonResponse);");
            sb.AppendLine("                Assert.That(string.IsNullOrEmpty(jsonResponse), Is.False, \"Response should not be empty\");");
                sb.AppendLine("                Console.WriteLine($\"📋 Response content: {(jsonResponse.Length > 200 ? jsonResponse.Substring(0, 200) + \"...\" : jsonResponse)}\");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (JsonException ex)");
            sb.AppendLine("            {");
                sb.AppendLine("                Console.WriteLine($\"❌ Invalid JSON response: {ex.Message}\");");
            sb.AppendLine("                Assert.Fail($\"Response is not valid JSON: {ex.Message}\");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string GenerateSchemaFromResponse(OpenApiResponse response, OpenApiTestSpec spec, string schemaKey)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            if (response == null)
            {
                return CreateFallbackSchema("Response is null", endpointInfo);
            }
            
            try
            {
                if (response.Content == null || !response.Content.Any())
                {
                    return CreateFallbackSchema("No content in response", endpointInfo);
                }

                // Try multiple JSON content type variations
                var jsonContent = response.Content.FirstOrDefault(c => 
                    c.Key.Equals("application/json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("json", StringComparison.OrdinalIgnoreCase) ||
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (jsonContent.Key == null)
                {
                    return CreateFallbackSchema("No JSON content type", endpointInfo);
                }
                
                var mediaType = jsonContent.Value;
                if (mediaType?.Schema == null)
                {
                    return CreateFallbackSchema("No schema in media type", endpointInfo);
                }

                var schema = mediaType.Schema;
                
                // Handle schema references
                if (schema.Reference != null)
                {
                    var resolvedSchema = ResolveSchemaReference(schema.Reference, spec);
                    if (resolvedSchema != null)
                    {
                        schema = resolvedSchema;
                    }
                }
                
                var convertedSchema = ConvertOpenApiSchemaToJsonSchema(schema, schemaKey);
                
                // Validate that we have a meaningful schema
                if (convertedSchema.Contains("\"type\"") && 
                    (convertedSchema.Contains("\"properties\"") || 
                     convertedSchema.Contains("\"items\"") || 
                     schema.Type == "string" || 
                     schema.Type == "number" || 
                     schema.Type == "integer" || 
                     schema.Type == "boolean"))
                {
                    return convertedSchema;
                }
                else
                {
                    return CreateFallbackSchema("Schema validation failed", endpointInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Memory error processing endpoint {endpointInfo}: {ex.Message}");
                return CreateFallbackSchema($"Memory allocation error - schema too large", endpointInfo);
            }
        }
        
        private static OpenApiSchema? ResolveSchemaReference(OpenApiReference reference, OpenApiTestSpec spec)
        {
            try
            {
                if (spec.Document?.Components?.Schemas == null)
                {
                    return null;
                }
                
                var schemaName = reference.Id;
                if (spec.Document.Components.Schemas.TryGetValue(schemaName, out var referencedSchema))
                {
                    return referencedSchema;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        private static string CreateFallbackSchema(string reason, string endpointInfo = "")
        {
            var schema = new
            {
                type = "object",
                additionalProperties = true,
                description = $"Fallback response schema - {reason}",
                _note = !string.IsNullOrEmpty(endpointInfo) ? $"Endpoint: {endpointInfo}" : null
            };
            
            return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
        
        private static string ExtractEndpointFromSchemaKey(string schemaKey)
        {
            try
            {
                var parts = schemaKey.Split(':');
                return parts.Length >= 2 ? $"{parts[0]} {parts[1]}" : schemaKey;
            }
            catch
            {
                return schemaKey;
            }
        }

        private static string ConvertOpenApiSchemaToJsonSchema(OpenApiSchema openApiSchema, string schemaKey)
        {
            var endpointInfo = ExtractEndpointFromSchemaKey(schemaKey);
            
            try
            {
                // Check if schema is too complex (might cause memory issues)
                var complexityScore = CalculateSchemaComplexity(openApiSchema);
                if (complexityScore > 1000)
                {
                    Console.WriteLine($"⚠️  Simplifying complex schema for endpoint {endpointInfo} (complexity: {complexityScore})");
                    return CreateSimplifiedSchema(openApiSchema, schemaKey, endpointInfo);
                }
                
                var schema = new Dictionary<string, object>();
                
                // Handle different schema types
                if (!string.IsNullOrEmpty(openApiSchema.Type))
                {
                    schema["type"] = openApiSchema.Type;
                }
                else if (openApiSchema.Properties?.Any() == true)
                {
                    schema["type"] = "object";
                }
                else if (openApiSchema.Items != null)
                {
                    schema["type"] = "array";
                }
                else
                {
                    schema["type"] = "object";
                }
                
                // Handle object properties
                if (openApiSchema.Properties?.Any() == true)
                {
                    var properties = new Dictionary<string, object>();
                    foreach (var prop in openApiSchema.Properties)
                    {
                        var propSchema = new Dictionary<string, object>();
                        
                        // Set property type
                        propSchema["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Type ?? "string");
                        
                        // Add format if specified
                        if (!string.IsNullOrEmpty(prop.Value.Format))
                        {
                            propSchema["format"] = prop.Value.Format;
                        }
                        
                        // Add description if available
                        if (!string.IsNullOrEmpty(prop.Value.Description))
                        {
                            propSchema["description"] = prop.Value.Description;
                        }
                        
                        // Handle nested objects
                        if (prop.Value.Properties?.Any() == true)
                        {
                            propSchema["type"] = "object";
                            var nestedProps = new Dictionary<string, object>();
                            foreach (var nestedProp in prop.Value.Properties)
                            {
                                nestedProps[nestedProp.Key] = new Dictionary<string, object>
                                {
                                    ["type"] = ConvertOpenApiTypeToJsonSchemaType(nestedProp.Value.Type ?? "string")
                                };
                            }
                            propSchema["properties"] = nestedProps;
                        }
                        
                        // Handle arrays
                        if (prop.Value.Items != null)
                        {
                            propSchema["type"] = "array";
                            propSchema["items"] = new Dictionary<string, object>
                            {
                                ["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Items.Type ?? "string")
                            };
                        }
                        
                        properties[prop.Key] = propSchema;
                    }
                    schema["properties"] = properties;
                }
                
                // Handle array items
                if (openApiSchema.Items != null)
                {
                    var itemSchema = new Dictionary<string, object>
                    {
                        ["type"] = ConvertOpenApiTypeToJsonSchemaType(openApiSchema.Items.Type ?? "object")
                    };
                    
                    if (openApiSchema.Items.Properties?.Any() == true)
                    {
                        itemSchema["type"] = "object";
                        var itemProperties = new Dictionary<string, object>();
                        foreach (var prop in openApiSchema.Items.Properties)
                        {
                            itemProperties[prop.Key] = new Dictionary<string, object>
                            {
                                ["type"] = ConvertOpenApiTypeToJsonSchemaType(prop.Value.Type ?? "string")
                            };
                        }
                        itemSchema["properties"] = itemProperties;
                    }
                    
                    schema["items"] = itemSchema;
                }
                
                // Handle required fields
                if (openApiSchema.Required?.Any() == true)
                {
                    schema["required"] = openApiSchema.Required.ToArray();
                }
                
                // Set additional properties
                schema["additionalProperties"] = openApiSchema.AdditionalPropertiesAllowed;
                
                return JsonSerializer.Serialize(schema, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Schema conversion error for endpoint {endpointInfo}: {ex.Message}");
                return CreateFallbackSchema($"Conversion error: {ex.Message}", endpointInfo);
            }
        }
        
        private static int CalculateSchemaComplexity(OpenApiSchema schema)
        {
            var complexity = 0;
            complexity += schema.Properties?.Count ?? 0;
            complexity += schema.Required?.Count ?? 0;
            
            // Add complexity for nested objects
            if (schema.Properties != null)
            {
                foreach (var prop in schema.Properties)
                {
                    if (prop.Value.Properties?.Any() == true)
                        complexity += prop.Value.Properties.Count * 2;
                    if (prop.Value.Items?.Properties?.Any() == true)
                        complexity += prop.Value.Items.Properties.Count;
                }
            }
            
            return complexity;
        }
        
        private static string CreateSimplifiedSchema(OpenApiSchema openApiSchema, string schemaKey, string endpointInfo)
        {
            var simplified = new
            {
                type = openApiSchema.Type ?? "object",
                additionalProperties = true,
                description = $"Simplified schema for {endpointInfo} - original was too complex for detailed validation",
                _note = openApiSchema.Properties?.Any() == true ? 
                    $"Original schema had {openApiSchema.Properties.Count} properties - simplified for performance" : 
                    "Schema was simplified due to complexity"
            };
            
            return JsonSerializer.Serialize(simplified, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
        
        private static string ConvertOpenApiTypeToJsonSchemaType(string openApiType)
        {
            return openApiType?.ToLower() switch
            {
                "integer" => "integer",
                "number" => "number", 
                "string" => "string",
                "boolean" => "boolean",
                "array" => "array",
                "object" => "object",
                _ => "string"
            };
        }

        private static string FormatJsonForCSharp(string json)
        {
            // Clean and format JSON for C# verbatim string
            try
            {
                // Parse and reformat to ensure valid JSON
                var jsonDoc = JsonDocument.Parse(json);
                var formattedJson = JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                
                // Escape only quotes for verbatim string
                return formattedJson.Replace("\"", "\"\"");
            }
            catch
            {
                // Fallback: just escape quotes
                return json.Replace("\"", "\"\"");
            }
        }

        private static void GenerateEndpointTestsWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string sanitizedTag, bool forceUnauthorizedTest = false)
        {
            var methodName = SanitizeIdentifier(endpoint.OperationId);
            
            // Generate positive test following the existing pattern
            GeneratePositiveTestWithAllurePattern(sb, endpoint, spec, methodName, sanitizedTag);
            
            // Generate unauthorized test for all endpoints (as requested)
            if (endpoint.RequiresAuth || forceUnauthorizedTest)
            {
                GenerateUnauthorizedTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            }
            
            if (endpoint.Parameters.Any(p => p.Required))
            {
                GenerateMissingRequiredParametersTestWithAllurePattern(sb, endpoint, methodName, sanitizedTag);
            }
            
            // Generate schema validation test if response has schema
            if (endpoint.Responses.ContainsKey("200") || endpoint.Responses.ContainsKey("201"))
            {
                GenerateSchemaValidationTestWithAllurePattern(sb, endpoint, spec, methodName, sanitizedTag);
            }
        }

        private static void GeneratePositiveTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_PositiveTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            // Generate request body from OpenAPI spec if needed
            if (endpoint.RequestBody != null)
            {
                var requestBodyJson = GenerateRequestBodyFromOpenApi(endpoint, spec);
                sb.AppendLine($"            var requestBody = {requestBodyJson};");
                sb.AppendLine();
                sb.AppendLine("            AllureApi.Step(\"Attach Request Body\", () =>");
                sb.AppendLine("            {");
                sb.AppendLine("                string requestBodyJson = serializeToJson(requestBody);");
                sb.AppendLine($"                AttachResponse(\"{methodName}Request\", requestBodyJson);");
                sb.AppendLine("            });");
                sb.AppendLine();
            }

            // Generate the main API call
            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path}\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
            }
            
            // Add headers from OpenAPI spec
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name.Equals("X-3E-UserId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-UserId\", context.UserId)");
                }
                else if (param.Name.Equals("X-3E-InstanceId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
                }
                else if (param.Name.Equals("x-3e-tenantid", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                }
                else if (!param.Name.Equals("authorization", StringComparison.OrdinalIgnoreCase))
                {
                    var headerValue = param.Required ? 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")" : 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")";
                    sb.AppendLine($"                    .Header(\"{param.Name}\", {headerValue})");
                }
            }
            
            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                if (param.Required)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            // Add request body if needed
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(requestBody)");
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            
            // Add expected status codes
            var successCodes = endpoint.Responses.Keys.Where(k => k.StartsWith("2")).ToList();
            if (successCodes.Any())
            {
                var primaryCode = successCodes.Contains("200") ? "200" : successCodes.First();
                sb.AppendLine($"                    .StatusCode({primaryCode});");
            }
            else
            {
                sb.AppendLine("                    .StatusCode(200);");
            }
            
            sb.AppendLine("            });");
            sb.AppendLine();
            
            // Extract and attach response
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine("            var statusCode = response.Extract().Response().StatusCode;");
            sb.AppendLine();
            sb.AppendLine($"            AllureApi.Step(\"Get & Attach {methodName}Response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                AttachResponse(\"{methodName}Response\", rawJson);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            // Assertions following the existing pattern
            sb.AppendLine($"            AllureApi.Step(\"Assert {methodName} response is successful\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.Multiple(() =>");
            sb.AppendLine("                {");
            sb.AppendLine("                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, \"Request should be successful\");");
            sb.AppendLine("                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, \"Response should not be empty\");");
            sb.AppendLine("                });");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateUnauthorizedTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_UnauthorizedTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} without authorization\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            // Add non-auth parameters
            foreach (var param in endpoint.Parameters.Where(p => p.Required && p.In != ParameterLocation.Header))
            {
                if (param.In == ParameterLocation.Query)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(401);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Assert unauthorized response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateMissingRequiredParametersTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_MissingRequiredParametersTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} with missing required parameters\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
                sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
            }
            
            // Handle path parameters in URL (still need these for the URL to be valid)
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then()");
            sb.AppendLine("                    .StatusCode(400);");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Assert bad request response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static void GenerateSchemaValidationTestWithAllurePattern(StringBuilder sb, OpenApiEndpointTest endpoint, OpenApiTestSpec spec, string methodName, string tag)
        {
            sb.AppendLine("        [Test]");
            sb.AppendLine($"        [Category(\"{tag}\")]");
            sb.AppendLine($"        public void {tag}_API_{methodName}_SchemaValidationTest()");
            sb.AppendLine("        {");
            sb.AppendLine($"            var context = GetTestContext();");
            sb.AppendLine($"            InitContext(context.TenantId, context.UserId, \"{tag} API Feature\");");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("            var token = GetAuthToken(context);");
            }
            
            sb.AppendLine("            var baseUrl = GetBaseUrl();");
            sb.AppendLine();

            // Generate request body from OpenAPI spec if needed
            if (endpoint.RequestBody != null)
            {
                var requestBodyJson = GenerateRequestBodyFromOpenApi(endpoint, spec);
                sb.AppendLine($"            var requestBody = {requestBodyJson};");
                sb.AppendLine();
            }

            sb.AppendLine($"            var response = AllureApi.Step(\"Execute {endpoint.Method} {endpoint.Path} for schema validation\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine("                return Given()");
            
            if (endpoint.RequiresAuth)
            {
                sb.AppendLine("                    .OAuth2(token)");
            }
            
            // Add headers from OpenAPI spec
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Header))
            {
                if (param.Name.Equals("X-3E-UserId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-UserId\", context.UserId)");
                }
                else if (param.Name.Equals("X-3E-InstanceId", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"X-3E-InstanceId\", context.InstanceId)");
                }
                else if (param.Name.Equals("x-3e-tenantid", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("                    .Header(\"x-3e-tenantid\", context.TenantId)");
                }
                else if (!param.Name.Equals("authorization", StringComparison.OrdinalIgnoreCase))
                {
                    var headerValue = param.Required ? 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")" : 
                        $"GetTestValue(\"{param.Schema?.Type ?? "string"}\")";
                    sb.AppendLine($"                    .Header(\"{param.Name}\", {headerValue})");
                }
            }
            
            // Add query parameters
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Query))
            {
                if (param.Required)
                {
                    sb.AppendLine($"                    .QueryParam(\"{param.Name}\", GetTestValue(\"{param.Schema?.Type ?? "string"}\"))");
                }
            }
            
            if (endpoint.RequestBody != null)
            {
                sb.AppendLine("                    .Body(requestBody)");
            }
            
            // Handle path parameters in URL
            var urlPath = endpoint.Path;
            foreach (var param in endpoint.Parameters.Where(p => p.In == ParameterLocation.Path))
            {
                urlPath = urlPath.Replace($"{{{param.Name}}}", $"{{GetTestValue(\"{param.Schema?.Type ?? "string"}\")}}");
            }
            
            sb.AppendLine("                    .When()");
            sb.AppendLine($"                    .{endpoint.Method.Substring(0, 1).ToUpper()}{endpoint.Method.Substring(1).ToLower()}($\"{{baseUrl}}{urlPath}\")");
            sb.AppendLine("                    .Then();");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;");
            sb.AppendLine();
            sb.AppendLine($"            AllureApi.Step(\"Get & Attach Schema Validation Response\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                AttachResponse(\"{methodName}SchemaValidationResponse\", rawJson);");
            sb.AppendLine("                AllureApi.AddAttachment(\"Response JSON\", \"application/json\", Encoding.UTF8.GetBytes(rawJson));");
            sb.AppendLine("            });");
            sb.AppendLine();
            
            sb.AppendLine("            AllureApi.Step(\"Validate response schema\", () =>");
            sb.AppendLine("            {");
            sb.AppendLine($"               ValidateResponseSchema_{SanitizeIdentifier(endpoint.Method + "_" + endpoint.Path)}(rawJson).Wait();");
            sb.AppendLine("            });");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private static string GenerateRequestBodyFromOpenApi(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // First try to get the actual OpenAPI request body from the document
                var actualRequestBody = GetActualRequestBodyFromDocument(spec, endpoint);
                if (actualRequestBody != null)
                {
                    return actualRequestBody;
                }

                // Fallback to endpoint request body if available
                if (endpoint.RequestBody?.Content == null)
                {
                    return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
                }

                // Try to find JSON content type
                var jsonContent = endpoint.RequestBody.Content.FirstOrDefault(c => 
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (jsonContent.Key != null && jsonContent.Value?.Schema != null)
                {
                    var schema = jsonContent.Value.Schema;
                    
                    // Handle schema references
                    if (schema.Reference != null)
                    {
                        var resolvedSchema = ResolveSchemaReference(schema.Reference, spec);
                        if (resolvedSchema != null)
                        {
                            schema = resolvedSchema;
                        }
                    }

                    return GenerateRequestBodyFromSchema(schema);
                }

                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating request body: {ex.Message}");
                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
        }

        private static string GenerateRequestBodyFromOpenApi(OpenApiEndpointTest endpoint, OpenApiTestSpec spec)
        {
            try
            {
                // First try to get the actual OpenAPI request body from the document
                var actualRequestBody = GetActualRequestBodyFromDocument(spec, endpoint);
                if (actualRequestBody != null)
                {
                    return actualRequestBody;
                }

                // Fallback to endpoint request body if available
                if (endpoint.RequestBody?.Content == null)
                {
                    return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
                }

                // Try to find JSON content type
                var jsonContent = endpoint.RequestBody.Content.FirstOrDefault(c => 
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (jsonContent.Key != null && jsonContent.Value?.Schema != null)
                {
                    var schema = jsonContent.Value.Schema;
                    
                    // Handle schema references
                    if (schema.Reference != null)
                    {
                        var resolvedSchema = ResolveSchemaReference(schema.Reference, spec);
                        if (resolvedSchema != null)
                        {
                            schema = resolvedSchema;
                        }
                    }

                    return GenerateRequestBodyFromSchema(schema);
                }

                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating request body: {ex.Message}");
                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
        }

        private static string GetActualRequestBodyFromDocument(OpenApiTestSpec spec, OpenApiEndpointTest endpoint)
        {
            try
            {
                // Find the path in the OpenAPI document
                var pathItem = spec.Document.Paths.FirstOrDefault(p => p.Key == endpoint.Path);
                if (pathItem.Value == null) return null;

                // Find the operation (GET, POST, etc.)
                var operation = pathItem.Value.Operations.FirstOrDefault(o => 
                    o.Key.ToString().Equals(endpoint.Method, StringComparison.OrdinalIgnoreCase));
                if (operation.Value?.RequestBody == null) return null;

                var requestBody = operation.Value.RequestBody;

                // Look for application/json content
                var jsonContent = requestBody.Content?.FirstOrDefault(c => 
                    c.Key.Contains("application/json", StringComparison.OrdinalIgnoreCase));
                
                if (!jsonContent.HasValue) return null;

                var mediaType = jsonContent.Value.Value;
                
                // First, try to get example
                if (mediaType.Example != null)
                {
                    return ConvertOpenApiValueToCSharp(mediaType.Example);
                }

                // Then try examples collection
                if (mediaType.Examples?.Any() == true)
                {
                    var firstExample = mediaType.Examples.First().Value;
                    if (firstExample.Value != null)
                    {
                        return ConvertOpenApiValueToCSharp(firstExample.Value);
                    }
                }

                // Finally, try to generate from schema
                if (mediaType.Schema != null)
                {
                    return GenerateRequestBodyFromSchema(mediaType.Schema);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error extracting request body for {endpoint.Method} {endpoint.Path}: {ex.Message}");
                return null;
            }
        }

        private static string ConvertOpenApiValueToCSharp(Microsoft.OpenApi.Any.IOpenApiAny openApiValue)
        {
            try
            {
                if (openApiValue is Microsoft.OpenApi.Any.OpenApiObject obj)
                {
                    var properties = new List<string>();
                    foreach (var prop in obj)
                    {
                        var value = ConvertOpenApiValueToString(prop.Value);
                        properties.Add($"{prop.Key} = {value}");
                    }
                    return "new { " + string.Join(", ", properties) + " }";
                }
                else if (openApiValue is Microsoft.OpenApi.Any.OpenApiString str)
                {
                    // If it's a JSON string, try to parse it
                    try
                    {
                        var jsonDoc = System.Text.Json.JsonDocument.Parse(str.Value);
                        return ConvertJsonElementToCSharp(jsonDoc.RootElement);
                    }
                    catch
                    {
                        return $"new {{ value = \"{str.Value}\" }}";
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error converting OpenAPI value: {ex.Message}");
                return null;
            }
        }

        private static string ConvertOpenApiValueToString(Microsoft.OpenApi.Any.IOpenApiAny value)
        {
            return value switch
            {
                Microsoft.OpenApi.Any.OpenApiString s => $"\"{s.Value}\"",
                Microsoft.OpenApi.Any.OpenApiInteger i => i.Value.ToString(),
                Microsoft.OpenApi.Any.OpenApiLong l => l.Value.ToString(),
                Microsoft.OpenApi.Any.OpenApiFloat f => f.Value.ToString("F2"),
                Microsoft.OpenApi.Any.OpenApiDouble d => d.Value.ToString("F2"),
                Microsoft.OpenApi.Any.OpenApiBoolean b => b.Value.ToString().ToLower(),
                Microsoft.OpenApi.Any.OpenApiArray arr => "new[] { " + string.Join(", ", arr.Select(ConvertOpenApiValueToString)) + " }",
                Microsoft.OpenApi.Any.OpenApiObject obj => "new { " + string.Join(", ", obj.Select(kv => $"{kv.Key} = {ConvertOpenApiValueToString(kv.Value)}")) + " }",
                _ => "\"test-value\""
            };
        }

        private static string ConvertJsonElementToCSharp(System.Text.Json.JsonElement element)
        {
            switch (element.ValueKind)
            {
                case System.Text.Json.JsonValueKind.Object:
                    var properties = new List<string>();
                    foreach (var prop in element.EnumerateObject())
                    {
                        var value = ConvertJsonElementToCSharp(prop.Value);
                        properties.Add($"{prop.Name} = {value}");
                    }
                    return "new { " + string.Join(", ", properties) + " }";

                case System.Text.Json.JsonValueKind.Array:
                    var items = new List<string>();
                    foreach (var item in element.EnumerateArray())
                    {
                        items.Add(ConvertJsonElementToCSharp(item));
                    }
                    return "new[] { " + string.Join(", ", items) + " }";

                case System.Text.Json.JsonValueKind.String:
                    return $"\"{element.GetString()}\"";

                case System.Text.Json.JsonValueKind.Number:
                    return element.TryGetInt32(out var intVal) ? intVal.ToString() : element.GetDouble().ToString("F2");

                case System.Text.Json.JsonValueKind.True:
                    return "true";

                case System.Text.Json.JsonValueKind.False:
                    return "false";

                case System.Text.Json.JsonValueKind.Null:
                    return "null";

                default:
                    return "\"test-value\"";
            }
        }

        private static string GenerateRequestBodyFromSchema(Microsoft.OpenApi.Models.OpenApiSchema schema)
        {
            try
            {
                if (schema.Properties?.Any() == true)
                {
                    var properties = new List<string>();
                    foreach (var prop in schema.Properties)
                    {
                        var value = GenerateValueFromSchema(prop.Value, prop.Key);
                        properties.Add($"{prop.Key} = {value}");
                    }
                    return "new { " + string.Join(", ", properties) + " }";
                }

                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error generating from schema: {ex.Message}");
                return "new { testProperty = \"testValue\", id = Guid.NewGuid().ToString() }";
            }
        }

        private static string GenerateValueFromSchema(Microsoft.OpenApi.Models.OpenApiSchema schema, string propertyName)
        {
            if (schema == null) return "\"test-value\"";

            // Check for examples first
            if (schema.Example != null)
            {
                return ConvertOpenApiValueToString(schema.Example);
            }

            // Generate based on type
            return schema.Type?.ToLower() switch
            {
                "string" when schema.Format == "uuid" => "Guid.NewGuid().ToString()",
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

        private static string SanitizeIdentifier(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return "Unknown";
                
            // Remove or replace invalid characters for C# identifiers
            var sanitized = identifier
                .Replace("-", "_")
                .Replace(" ", "_")
                .Replace(".", "_")
                .Replace("/", "_")
                .Replace("\\", "_")
                .Replace("(", "_")
                .Replace(")", "_")
                .Replace("[", "_")
                .Replace("]", "_")
                .Replace("{", "_")
                .Replace("}", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace(":", "_")
                .Replace(";", "_")
                .Replace(",", "_")
                .Replace("?", "_")
                .Replace("!", "_")
                .Replace("@", "_")
                .Replace("#", "_")
                .Replace("$", "_")
                .Replace("%", "_")
                .Replace("^", "_")
                .Replace("&", "_")
                .Replace("*", "_")
                .Replace("+", "_")
                .Replace("=", "_")
                .Replace("|", "_")
                .Replace("~", "_")
                .Replace("`", "_")
                .Replace("'", "_")
                .Replace("\"", "_");

            // Remove consecutive underscores
            while (sanitized.Contains("__"))
            {
                sanitized = sanitized.Replace("__", "_");
            }

            // Remove leading/trailing underscores
            sanitized = sanitized.Trim('_');

            // Ensure it starts with a letter or underscore (valid C# identifier)
            if (!char.IsLetter(sanitized[0]) && sanitized[0] != '_')
            {
                sanitized = "_" + sanitized;
            }

            // Ensure it's not empty
            if (string.IsNullOrEmpty(sanitized))
            {
                sanitized = "Unknown";
            }

            return sanitized;
        }

        private static string SanitizeForFileSystem(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Unknown";

            var sanitized = input
                .Replace(" ", "_")
                .Replace("-", "_")
                .Replace(".", "_")
                .Replace("/", "_")
                .Replace("\\", "_")
                .Replace(":", "_")
                .Replace("*", "_")
                .Replace("?", "_")
                .Replace("\"", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace("|", "_");

            // Remove consecutive underscores
            while (sanitized.Contains("__"))
            {
                sanitized = sanitized.Replace("__", "_");
            }

            return sanitized.Trim('_');
        }

        private static string SanitizeMethodName(string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
                return "Unknown";

            var sanitized = methodName
                .Replace("-", "_")
                .Replace(" ", "_")
                .Replace(".", "_")
                .Replace("/", "_")
                .Replace("\\", "_")
                .Replace("(", "_")
                .Replace(")", "_")
                .Replace("[", "_")
                .Replace("]", "_")
                .Replace("{", "_")
                .Replace("}", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace(":", "_")
                .Replace(";", "_")
                .Replace(",", "_")
                .Replace("?", "_")
                .Replace("!", "_")
                .Replace("@", "_")
                .Replace("#", "_")
                .Replace("$", "_")
                .Replace("%", "_")
                .Replace("^", "_")
                .Replace("&", "_")
                .Replace("*", "_")
                .Replace("+", "_")
                .Replace("=", "_")
                .Replace("|", "_")
                .Replace("~", "_")
                .Replace("`", "_")
                .Replace("'", "_")
                .Replace("\"", "_");

            // Remove consecutive underscores
            while (sanitized.Contains("__"))
            {
                sanitized = sanitized.Replace("__", "_");
            }

            // Remove leading/trailing underscores
            sanitized = sanitized.Trim('_');

            // Ensure it starts with a letter or underscore (valid C# identifier)
            if (!char.IsLetter(sanitized[0]) && sanitized[0] != '_')
            {
                sanitized = "_" + sanitized;
            }

            // Ensure it's not empty
            if (string.IsNullOrEmpty(sanitized))
            {
                sanitized = "Unknown";
            }

            return sanitized;
        }
    }
}