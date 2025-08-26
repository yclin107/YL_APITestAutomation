using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using API.Core.Helpers;
using System.Net;
using System.Text;
using System.Text.Json;
using static RestAssured.Dsl;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.Validation;

namespace API.TestBase.Tests.Generated.Diagnostics
{
    [TestFixture]
    [AllureFeature("Diagnostics API Tests")]
    public class DiagnosticsTests : TestBase
    {

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_version_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/version", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/version")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_diagnostics_versionResponse", () =>
            {
                AttachResponse("get_diagnostics_versionResponse", rawJson);
            });

            AllureApi.Step("Assert get_diagnostics_version response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_version_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/version without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/version")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_version_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/version for schema validation", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/version")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_diagnostics_versionSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_diagnostics_version(rawJson).Wait();
            });
        }

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_loglevel_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/logLevel", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/logLevel")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_diagnostics_loglevelResponse", () =>
            {
                AttachResponse("get_diagnostics_loglevelResponse", rawJson);
            });

            AllureApi.Step("Assert get_diagnostics_loglevel response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_loglevel_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/logLevel without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/logLevel")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Diagnostics")]
        public void Diagnostics_API_get_diagnostics_loglevel_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Diagnostics API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /diagnostics/logLevel for schema validation", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/diagnostics/logLevel")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_diagnostics_loglevelSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_diagnostics_logLevel(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_GET_diagnostics_version(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""string"",
  ""additionalProperties"": true
}";

                AllureApi.AddAttachment("Expected Schema", "application/json", Encoding.UTF8.GetBytes(schemaJson));

                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);
                var validator = new JsonSchemaValidator();
                var errors = validator.Validate(jsonResponse, schema);

                if (errors.Any())
                {
                    var errorMessages = errors.Select(e => $"{e.Path}: {e.Kind} - {e.Property}");
                    var allErrors = string.Join(", ", errorMessages);
                    Assert.Fail($"Response schema validation failed. Errors: {allErrors}");
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Schema validation error: {ex.Message}");
            }
        }

        private async Task ValidateResponseSchema_GET_diagnostics_logLevel(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: GET /diagnostics/logLevel""
}";

                AllureApi.AddAttachment("Expected Schema", "application/json", Encoding.UTF8.GetBytes(schemaJson));

                var schema = await NJsonSchema.JsonSchema.FromJsonAsync(schemaJson);
                var validator = new JsonSchemaValidator();
                var errors = validator.Validate(jsonResponse, schema);

                if (errors.Any())
                {
                    var errorMessages = errors.Select(e => $"{e.Path}: {e.Kind} - {e.Property}");
                    var allErrors = string.Join(", ", errorMessages);
                    Assert.Fail($"Response schema validation failed. Errors: {allErrors}");
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Schema validation error: {ex.Message}");
            }
        }

        #endregion

    }
}
