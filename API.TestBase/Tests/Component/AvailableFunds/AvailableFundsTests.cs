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

namespace API.TestBase.Tests.Generated.AvailableFunds
{
    [TestFixture]
    [AllureFeature("AvailableFunds API Tests")]
    public class AvailableFundsTests : TestBase
    {

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_trust_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/trust", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_availablefunds_trustResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_trustResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_availablefunds_trust response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_trust_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/trust without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_trust_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/trust with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_trust_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/trust for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_trustSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_availableFunds_trust(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_boa_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/boa", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/boa")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_availablefunds_boaResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_boaResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_availablefunds_boa response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_boa_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/boa without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/boa")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_boa_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/boa with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/boa")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_boa_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/boa for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/boa")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_boaSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_availableFunds_boa(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_credit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/credit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/credit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_availablefunds_creditResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_creditResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_availablefunds_credit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_credit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/credit without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/credit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_credit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/credit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/credit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_credit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/credit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/credit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_creditSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_availableFunds_credit(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_summary_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/summary", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/summary")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_availablefunds_summaryResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_summaryResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_availablefunds_summary response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_summary_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/summary without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/summary")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_summary_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/summary with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/summary")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_summary_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/summary for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/summary")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_summarySchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_availableFunds_summary(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_type_itemid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/{type}/{itemId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_availablefunds_type_itemidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_type_itemidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_availablefunds_type_itemid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_type_itemid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/{type}/{itemId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_type_itemid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/{type}/{itemId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_get_proforma_proformaid_availablefunds_type_itemid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/availableFunds/{type}/{itemId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_availablefunds_type_itemidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_availableFunds_type_itemId(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_post_proforma_proformaid_availablefunds_type_itemid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", itemId = "string", type = "string", fields = new[] { new { name = "string", value = new {  } } }, action = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_availablefunds_type_itemidRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/availableFunds/{type}/{itemId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_availablefunds_type_itemidResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_availablefunds_type_itemidResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_availablefunds_type_itemid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_post_proforma_proformaid_availablefunds_type_itemid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/availableFunds/{type}/{itemId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_post_proforma_proformaid_availablefunds_type_itemid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/availableFunds/{type}/{itemId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_post_proforma_proformaid_availablefunds_type_itemid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", itemId = "string", type = "string", fields = new[] { new { name = "string", value = new {  } } }, action = "string" };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/availableFunds/{type}/{itemId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_availablefunds_type_itemidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_availableFunds_type_itemId(rawJson).Wait();
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_patch_proforma_proformaid_availablefunds_trust_itemid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", itemId = "string", type = "string", fields = new[] { new { name = "string", value = new {  } } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_proforma_proformaid_availablefunds_trust_itemidRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/availableFunds/trust/{itemId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_proforma_proformaid_availablefunds_trust_itemidResponse", () =>
            {
                AttachResponse("patch_proforma_proformaid_availablefunds_trust_itemidResponse", rawJson);
            });

            AllureApi.Step("Assert patch_proforma_proformaid_availablefunds_trust_itemid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_patch_proforma_proformaid_availablefunds_trust_itemid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/availableFunds/trust/{itemId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_patch_proforma_proformaid_availablefunds_trust_itemid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/availableFunds/trust/{itemId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AvailableFunds")]
        public void AvailableFunds_API_patch_proforma_proformaid_availablefunds_trust_itemid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AvailableFunds API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", itemId = "string", type = "string", fields = new[] { new { name = "string", value = new {  } } } };

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/availableFunds/trust/{itemId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/availableFunds/trust/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_proforma_proformaid_availablefunds_trust_itemidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_proforma_proformaId_availableFunds_trust_itemId(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_GET_proforma_proformaId_availableFunds_trust(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_availableFunds_boa(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_availableFunds_credit(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_availableFunds_summary(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""object"",
      ""properties"": {
        ""trust"": {
          ""type"": ""integer""
        },
        ""trustAdded"": {
          ""type"": ""integer""
        },
        ""unallocatedBOA"": {
          ""type"": ""integer""
        },
        ""unallocatedBOAAdded"": {
          ""type"": ""integer""
        },
        ""unallocatedCredit"": {
          ""type"": ""integer""
        },
        ""unallocatedCreditAdded"": {
          ""type"": ""integer""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_availableFunds_type_itemId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""object"",
      ""properties"": {
        ""disbursementType"": {
          ""type"": ""array""
        },
        ""adjustmentType"": {
          ""type"": ""array""
        },
        ""payee"": {
          ""type"": ""array""
        },
        ""flag1099"": {
          ""type"": ""array""
        },
        ""receiptType"": {
          ""type"": ""array""
        },
        ""taxCodes"": {
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_availableFunds_type_itemId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
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

        private async Task ValidateResponseSchema_PATCH_proforma_proformaId_availableFunds_trust_itemId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""data"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""name"": {
          ""type"": ""string""
        },
        ""availableAmount"": {
          ""type"": ""number""
        },
        ""currency"": {
          ""type"": ""string""
        },
        ""proformaCurrency"": {
          ""type"": ""string""
        },
        ""exchangeRate"": {
          ""type"": ""number""
        },
        ""isApplied"": {
          ""type"": ""boolean""
        },
        ""matter"": {
          ""type"": ""string""
        },
        ""matterNumber"": {
          ""type"": ""string""
        },
        ""matterName"": {
          ""type"": ""string""
        },
        ""bankAcctTrust"": {
          ""type"": ""string""
        },
        ""bankDescription"": {
          ""type"": ""string""
        },
        ""details"": {
          ""type"": ""object""
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
