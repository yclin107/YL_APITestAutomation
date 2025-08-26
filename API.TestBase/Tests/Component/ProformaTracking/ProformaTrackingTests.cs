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

namespace API.TestBase.Tests.Generated.ProformaTracking
{
    [TestFixture]
    [AllureFeature("ProformaTracking API Tests")]
    public class ProformaTrackingTests : TestBase
    {

        [Test]
        [Category("ProformaTracking")]
        public void ProformaTracking_API_post_proforma_tracking_list_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaTracking API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isNotStarted = true, isInProgress = true, isCompleted = true, isArchived = true, clientName = "string", clientNumber = "string", billingTimekeeperName = "string", billingTimekeeperNumber = "string", matterName = "string", matterNumber = "string", proformaIndex = "string", profDate = new[] { "string" } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_tracking_list_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/tracking/list", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/tracking/list")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_tracking_list_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_tracking_list_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_tracking_list_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaTracking")]
        public void ProformaTracking_API_post_proforma_tracking_list_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaTracking API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/tracking/list without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/tracking/list")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaTracking")]
        public void ProformaTracking_API_post_proforma_tracking_list_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaTracking API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/tracking/list with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/tracking/list")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaTracking")]
        public void ProformaTracking_API_post_proforma_tracking_list_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaTracking API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isNotStarted = true, isInProgress = true, isCompleted = true, isArchived = true, clientName = "string", clientNumber = "string", billingTimekeeperName = "string", billingTimekeeperNumber = "string", matterName = "string", matterNumber = "string", proformaIndex = "string", profDate = new[] { "string" } };

            var response = AllureApi.Step("Execute POST /proforma/tracking/list for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/tracking/list")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_tracking_list_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_tracking_list(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_tracking_list(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""summary"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""listResponse"": {
      ""type"": ""object"",
      ""properties"": {
        ""recordCount"": {
          ""type"": ""integer""
        },
        ""data"": {
          ""type"": ""array""
        }
      }
    },
    ""listFilter"": {
      ""type"": ""object"",
      ""properties"": {
        ""lockedBy"": {
          ""type"": ""array""
        },
        ""clientNames"": {
          ""type"": ""array""
        },
        ""clientNumbers"": {
          ""type"": ""array""
        },
        ""matters"": {
          ""type"": ""array""
        },
        ""matterNumbers"": {
          ""type"": ""array""
        },
        ""matterCurrencies"": {
          ""type"": ""array""
        },
        ""billGroups"": {
          ""type"": ""array""
        },
        ""approvalsPending"": {
          ""type"": ""boolean""
        },
        ""owners"": {
          ""type"": ""array""
        },
        ""openedStatuses"": {
          ""type"": ""array""
        }
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
