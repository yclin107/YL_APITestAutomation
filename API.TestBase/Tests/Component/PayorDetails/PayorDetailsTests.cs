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

namespace API.TestBase.Tests.Generated.PayorDetails
{
    [TestFixture]
    [AllureFeature("PayorDetails API Tests")]
    public class PayorDetailsTests : TestBase
    {

        [Test]
        [Category("PayorDetails")]
        public void PayorDetails_API_get_proforma_proformaid_payordetails_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "PayorDetails API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/payorDetails", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/payorDetails")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_payordetails_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_payordetails_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_payordetails_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("PayorDetails")]
        public void PayorDetails_API_get_proforma_proformaid_payordetails_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "PayorDetails API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/payorDetails without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/payorDetails")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("PayorDetails")]
        public void PayorDetails_API_get_proforma_proformaid_payordetails_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "PayorDetails API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/payorDetails with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/payorDetails")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("PayorDetails")]
        public void PayorDetails_API_get_proforma_proformaid_payordetails_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "PayorDetails API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/payorDetails for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/payorDetails")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_payordetails_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_payorDetails(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_GET_proforma_proformaId_payorDetails(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isLayered"": {
      ""type"": ""boolean""
    },
    ""layerDetails"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""layerName"": {
          ""type"": ""string""
        },
        ""layerPercentage"": {
          ""type"": ""string""
        },
        ""currentIndex"": {
          ""type"": ""integer""
        },
        ""isDefault"": {
          ""type"": ""boolean""
        },
        ""totalLayers"": {
          ""type"": ""integer""
        }
      }
    },
    ""payors"": {
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
