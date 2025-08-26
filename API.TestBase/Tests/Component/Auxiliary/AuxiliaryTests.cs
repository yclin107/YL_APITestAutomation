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

namespace API.TestBase.Tests.Generated.Auxiliary
{
    [TestFixture]
    [AllureFeature("Auxiliary API Tests")]
    public class AuxiliaryTests : TestBase
    {

        [Test]
        [Category("Auxiliary")]
        public void Auxiliary_API_post_auxiliary_textsdifferences_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Auxiliary API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { textBefore = "string", textAfter = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_auxiliary_textsdifferencesRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /auxiliary/textsDifferences", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/auxiliary/textsDifferences")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_auxiliary_textsdifferencesResponse", () =>
            {
                AttachResponse("post_auxiliary_textsdifferencesResponse", rawJson);
            });

            AllureApi.Step("Assert post_auxiliary_textsdifferences response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Auxiliary")]
        public void Auxiliary_API_post_auxiliary_textsdifferences_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Auxiliary API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /auxiliary/textsDifferences without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/auxiliary/textsDifferences")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Auxiliary")]
        public void Auxiliary_API_post_auxiliary_textsdifferences_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Auxiliary API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /auxiliary/textsDifferences with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/auxiliary/textsDifferences")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Auxiliary")]
        public void Auxiliary_API_post_auxiliary_textsdifferences_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Auxiliary API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { textBefore = "string", textAfter = "string" };

            var response = AllureApi.Step("Execute POST /auxiliary/textsDifferences for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/auxiliary/textsDifferences")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_auxiliary_textsdifferencesSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_auxiliary_textsDifferences(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_auxiliary_textsDifferences(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""lines"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    }
  },
  ""additionalProperties"": false
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
