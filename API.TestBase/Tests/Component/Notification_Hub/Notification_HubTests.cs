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

namespace API.TestBase.Tests.Generated.Notification_Hub
{
    [TestFixture]
    [AllureFeature("Notification_Hub API Tests")]
    public class Notification_HubTests : TestBase
    {

        [Test]
        [Category("Notification_Hub")]
        public void Notification_Hub_API_post_notifications_hub_negotiate_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Notification_Hub API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /notifications/hub/negotiate", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/notifications/hub/negotiate")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_notifications_hub_negotiateResponse", () =>
            {
                AttachResponse("post_notifications_hub_negotiateResponse", rawJson);
            });

            AllureApi.Step("Assert post_notifications_hub_negotiate response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Notification_Hub")]
        public void Notification_Hub_API_post_notifications_hub_negotiate_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Notification_Hub API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /notifications/hub/negotiate without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/notifications/hub/negotiate")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Notification_Hub")]
        public void Notification_Hub_API_post_notifications_hub_negotiate_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Notification_Hub API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /notifications/hub/negotiate for schema validation", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/notifications/hub/negotiate")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_notifications_hub_negotiateSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_notifications_hub_negotiate(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_notifications_hub_negotiate(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /notifications/hub/negotiate""
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
