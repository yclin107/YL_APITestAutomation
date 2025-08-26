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

namespace API.TestBase.Tests.Generated.Adjustment
{
    [TestFixture]
    [AllureFeature("Adjustment API Tests")]
    public class AdjustmentTests : TestBase
    {

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_adjustmentRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_adjustmentResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustmentResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_adjustment response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustmentSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_adjustment(rawJson).Wait();
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_patch_proforma_proformaid_adjustment_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", field = new { name = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_proforma_proformaid_adjustmentRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/adjustment", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_proforma_proformaid_adjustmentResponse", () =>
            {
                AttachResponse("patch_proforma_proformaid_adjustmentResponse", rawJson);
            });

            AllureApi.Step("Assert patch_proforma_proformaid_adjustment response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_patch_proforma_proformaid_adjustment_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/adjustment without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_patch_proforma_proformaid_adjustment_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/adjustment with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_patch_proforma_proformaid_adjustment_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", field = new { name = "string", value = new {  } } };

            var response = AllureApi.Step("Execute PATCH /proforma/{proformaId}/adjustment for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_proforma_proformaid_adjustmentSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_proforma_proformaId_adjustment(rawJson).Wait();
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_delete_proforma_proformaid_adjustment_rowid_rowid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute DELETE /proforma/{proformaId}/adjustment", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("rowId", GetTestValue("string"))
                    .When()
                    .Delete($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach delete_proforma_proformaid_adjustment_rowid_rowidResponse", () =>
            {
                AttachResponse("delete_proforma_proformaid_adjustment_rowid_rowidResponse", rawJson);
            });

            AllureApi.Step("Assert delete_proforma_proformaid_adjustment_rowid_rowid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_delete_proforma_proformaid_adjustment_rowid_rowid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute DELETE /proforma/{proformaId}/adjustment without authorization", () =>
            {
                return Given()
                    .QueryParam("rowId", GetTestValue("string"))
                    .When()
                    .Delete($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_delete_proforma_proformaid_adjustment_rowid_rowid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute DELETE /proforma/{proformaId}/adjustment with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Delete($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_delete_proforma_proformaid_adjustment_rowid_rowid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute DELETE /proforma/{proformaId}/adjustment for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("rowId", GetTestValue("string"))
                    .When()
                    .Delete($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("delete_proforma_proformaid_adjustment_rowid_rowidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_DELETE_proforma_proformaId_adjustment(rawJson).Wait();
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { profAdjustType = new[] { "string" }, adjMethodList = new[] { "string" }, amount = new { operationName = "string", operandValue = 0 } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/list", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/list")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/list without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/list")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/list with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/list")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { profAdjustType = new[] { "string" }, adjMethodList = new[] { "string" }, amount = new { operationName = "string", operandValue = 0 } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/list for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/list")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustment_list_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_adjustment_list(rawJson).Wait();
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_get_proforma_proformaid_adjustment_options_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/adjustment/options", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/options")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_adjustment_optionsResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_adjustment_optionsResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_adjustment_options response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_get_proforma_proformaid_adjustment_options_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/adjustment/options without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/options")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_get_proforma_proformaid_adjustment_options_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/adjustment/options with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/options")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_get_proforma_proformaid_adjustment_options_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/adjustment/options for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/options")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_adjustment_optionsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_adjustment_options(rawJson).Wait();
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_save_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", fields = new[] { new { name = "string", value = new {  } } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_adjustment_saveRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/save", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/save")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_adjustment_saveResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustment_saveResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_adjustment_save response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_save_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/save without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/save")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_save_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/save with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/save")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Adjustment")]
        public void Adjustment_API_post_proforma_proformaid_adjustment_save_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Adjustment API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", fields = new[] { new { name = "string", value = new {  } } } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/adjustment/save for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/adjustment/save")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_adjustment_saveSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_adjustment_save(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_proformaId_adjustment(string jsonResponse)
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
        ""profAdjustType"": {
          ""type"": ""object""
        },
        ""adjMethodList"": {
          ""type"": ""object""
        },
        ""startDate"": {
          ""type"": ""object""
        },
        ""endDate"": {
          ""type"": ""object""
        },
        ""timekeeper"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""percentage"": {
          ""type"": ""object""
        },
        ""isIncludeFlatFeeCharges"": {
          ""type"": ""object""
        },
        ""rowId"": {
          ""type"": ""string""
        },
        ""currentTotal"": {
          ""type"": ""number""
        },
        ""adjustedTotal"": {
          ""type"": ""number""
        },
        ""feeTotal"": {
          ""type"": ""number""
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

        private async Task ValidateResponseSchema_PATCH_proforma_proformaId_adjustment(string jsonResponse)
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
        ""profAdjustType"": {
          ""type"": ""object""
        },
        ""adjMethodList"": {
          ""type"": ""object""
        },
        ""startDate"": {
          ""type"": ""object""
        },
        ""endDate"": {
          ""type"": ""object""
        },
        ""timekeeper"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""percentage"": {
          ""type"": ""object""
        },
        ""isIncludeFlatFeeCharges"": {
          ""type"": ""object""
        },
        ""rowId"": {
          ""type"": ""string""
        },
        ""currentTotal"": {
          ""type"": ""number""
        },
        ""adjustedTotal"": {
          ""type"": ""number""
        },
        ""feeTotal"": {
          ""type"": ""number""
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

        private async Task ValidateResponseSchema_DELETE_proforma_proformaId_adjustment(string jsonResponse)
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
      ""type"": ""object""
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_adjustment_list(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_adjustment_options(string jsonResponse)
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
        ""profAdjustType"": {
          ""type"": ""array""
        },
        ""adjMethodList"": {
          ""type"": ""array""
        },
        ""timekeeper"": {
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_adjustment_save(string jsonResponse)
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
        ""profAdjustType"": {
          ""type"": ""object""
        },
        ""adjMethodList"": {
          ""type"": ""object""
        },
        ""startDate"": {
          ""type"": ""object""
        },
        ""endDate"": {
          ""type"": ""object""
        },
        ""timekeeper"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""percentage"": {
          ""type"": ""object""
        },
        ""isIncludeFlatFeeCharges"": {
          ""type"": ""object""
        },
        ""rowId"": {
          ""type"": ""string""
        },
        ""currentTotal"": {
          ""type"": ""number""
        },
        ""adjustedTotal"": {
          ""type"": ""number""
        },
        ""feeTotal"": {
          ""type"": ""number""
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
