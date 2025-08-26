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

namespace API.TestBase.Tests.Generated.Transfer
{
    [TestFixture]
    [AllureFeature("Transfer API Tests")]
    public class TransferTests : TestBase
    {

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, proformaId = "string", cardType = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_transfer_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /transfer/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_initResponse", () =>
            {
                AttachResponse("post_transfer_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, proformaId = "string", cardType = 0 };

            var response = AllureApi.Step("Execute POST /transfer/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_get_transfer_matter_proformaid_proformaid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /transfer/matter", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_transfer_matter_proformaid_proformaidResponse", () =>
            {
                AttachResponse("get_transfer_matter_proformaid_proformaidResponse", rawJson);
            });

            AllureApi.Step("Assert get_transfer_matter_proformaid_proformaid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_get_transfer_matter_proformaid_proformaid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /transfer/matter without authorization", () =>
            {
                return Given()
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_get_transfer_matter_proformaid_proformaid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /transfer/matter with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_get_transfer_matter_proformaid_proformaid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /transfer/matter for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/transfer/matter")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_transfer_matter_proformaid_proformaidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_transfer_matter(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_matter_proformaid_proformaid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/matter", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_matter_proformaid_proformaidResponse", () =>
            {
                AttachResponse("post_transfer_matter_proformaid_proformaidResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_matter_proformaid_proformaid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_matter_proformaid_proformaid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/matter without authorization", () =>
            {
                return Given()
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_matter_proformaid_proformaid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/matter with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/matter")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_matter_proformaid_proformaid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/matter for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/matter")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_matter_proformaid_proformaidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_matter(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_pta_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", phase1 = "string", task1 = "string", activity1 = "string", phase2 = "string", task2 = "string", activity2 = "string", removedValue = "string", changedField = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_transfer_ptaRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /transfer/pta", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/pta")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_transfer_ptaResponse", () =>
            {
                AttachResponse("patch_transfer_ptaResponse", rawJson);
            });

            AllureApi.Step("Assert patch_transfer_pta response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_pta_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/pta without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/pta")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_pta_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/pta with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/pta")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_pta_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", phase1 = "string", task1 = "string", activity1 = "string", phase2 = "string", task2 = "string", activity2 = "string", removedValue = "string", changedField = "string" };

            var response = AllureApi.Step("Execute PATCH /transfer/pta for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/pta")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_transfer_ptaSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_transfer_pta(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_updatepta_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/updatePta", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/updatePta")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_updateptaResponse", () =>
            {
                AttachResponse("post_transfer_updateptaResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_updatepta response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_updatepta_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/updatePta without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/updatePta")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_updatepta_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/updatePta with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/updatePta")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_updatepta_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/updatePta for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/updatePta")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_updateptaSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_updatePta(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_changerecalcrate_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", recalculateRate = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_transfer_changerecalcrateRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /transfer/changeRecalcRate", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/changeRecalcRate")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_changerecalcrateResponse", () =>
            {
                AttachResponse("post_transfer_changerecalcrateResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_changerecalcrate response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_changerecalcrate_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/changeRecalcRate without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/changeRecalcRate")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_changerecalcrate_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/changeRecalcRate with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/changeRecalcRate")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_changerecalcrate_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", recalculateRate = true };

            var response = AllureApi.Step("Execute POST /transfer/changeRecalcRate for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/changeRecalcRate")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_changerecalcrateSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_changeRecalcRate(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_committransfer_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", nonBillable = true, targetProformaNumber = 0, changeReasonCode = "string", timekeeperIndex = 0, rows = new[] { new { rowId = "string", nonBillable = true, noCharge = true } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_transfer_committransferRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /transfer/commitTransfer", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/commitTransfer")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_committransferResponse", () =>
            {
                AttachResponse("post_transfer_committransferResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_committransfer response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_committransfer_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/commitTransfer without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/commitTransfer")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_committransfer_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/commitTransfer with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/commitTransfer")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_committransfer_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", nonBillable = true, targetProformaNumber = 0, changeReasonCode = "string", timekeeperIndex = 0, rows = new[] { new { rowId = "string", nonBillable = true, noCharge = true } } };

            var response = AllureApi.Step("Execute POST /transfer/commitTransfer for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/transfer/commitTransfer")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_committransferSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_commitTransfer(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_fees_undo_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/fees/undo", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/fees/undo")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_fees_undoResponse", () =>
            {
                AttachResponse("post_transfer_fees_undoResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_fees_undo response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_fees_undo_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/fees/undo without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/fees/undo")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_fees_undo_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/fees/undo with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/fees/undo")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_fees_undo_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/fees/undo for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/fees/undo")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_fees_undoSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_fees_undo(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_costs_undo_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/costs/undo", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/costs/undo")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_costs_undoResponse", () =>
            {
                AttachResponse("post_transfer_costs_undoResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_costs_undo response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_costs_undo_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/costs/undo without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/costs/undo")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_costs_undo_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/costs/undo with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/costs/undo")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_costs_undo_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/costs/undo for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/costs/undo")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_costs_undoSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_costs_undo(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_charges_undo_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/charges/undo", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/charges/undo")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_transfer_charges_undoResponse", () =>
            {
                AttachResponse("post_transfer_charges_undoResponse", rawJson);
            });

            AllureApi.Step("Assert post_transfer_charges_undo response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_charges_undo_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/charges/undo without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/charges/undo")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_charges_undo_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/charges/undo with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/transfer/charges/undo")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_post_transfer_charges_undo_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /transfer/charges/undo for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/transfer/charges/undo")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_transfer_charges_undoSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_transfer_charges_undo(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_excludedtime_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", value = true, excludedTimeType = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_transfer_excludedtimeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /transfer/excludedTime", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/excludedTime")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_transfer_excludedtimeResponse", () =>
            {
                AttachResponse("patch_transfer_excludedtimeResponse", rawJson);
            });

            AllureApi.Step("Assert patch_transfer_excludedtime response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_excludedtime_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/excludedTime without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/excludedTime")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_excludedtime_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/excludedTime with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/excludedTime")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_excludedtime_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", rowId = "string", value = true, excludedTimeType = "string" };

            var response = AllureApi.Step("Execute PATCH /transfer/excludedTime for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/excludedTime")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_transfer_excludedtimeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_transfer_excludedTime(rawJson).Wait();
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_targetproforma_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", targetProformaNumber = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_transfer_targetproformaRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /transfer/targetProforma", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/targetProforma")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_transfer_targetproformaResponse", () =>
            {
                AttachResponse("patch_transfer_targetproformaResponse", rawJson);
            });

            AllureApi.Step("Assert patch_transfer_targetproforma response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_targetproforma_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/targetProforma without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/targetProforma")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_targetproforma_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /transfer/targetProforma with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/transfer/targetProforma")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Transfer")]
        public void Transfer_API_patch_transfer_targetproforma_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Transfer API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", targetProformaNumber = 0 };

            var response = AllureApi.Step("Execute PATCH /transfer/targetProforma for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/transfer/targetProforma")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_transfer_targetproformaSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_transfer_targetProforma(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_transfer_init(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_GET_transfer_matter(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""rowKey"": {
        ""type"": ""string""
      },
      ""clientName"": {
        ""type"": ""string""
      },
      ""matterNumber"": {
        ""type"": ""string""
      },
      ""matterDescription"": {
        ""type"": ""string""
      },
      ""matterName"": {
        ""type"": ""string""
      },
      ""nonBillMatType"": {
        ""type"": ""object""
      }
    }
  },
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

        private async Task ValidateResponseSchema_POST_transfer_matter(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_PATCH_transfer_pta(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_updatePta(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_changeRecalcRate(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_commitTransfer(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_fees_undo(string jsonResponse)
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
    ""card"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""timekeeperName"": {
          ""type"": ""string""
        },
        ""timekeeperNumber"": {
          ""type"": ""string""
        },
        ""date"": {
          ""type"": ""string""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""presNarrative"": {
          ""type"": ""object""
        },
        ""presAmt"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""cardTypes"": {
          ""type"": ""array""
        },
        ""selectedCardType"": {
          ""type"": ""object""
        },
        ""availableCodes"": {
          ""type"": ""object""
        },
        ""appliedAction"": {
          ""type"": ""string""
        },
        ""proformaId"": {
          ""type"": ""string""
        },
        ""cardIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperJobPosition"": {
          ""type"": ""string""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""isCalculated"": {
          ""type"": ""boolean""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""isErrorsPresented"": {
          ""type"": ""boolean""
        },
        ""messages"": {
          ""type"": ""array""
        },
        ""disposition"": {
          ""type"": ""string""
        },
        ""eBillingValidationDetails"": {
          ""type"": ""array""
        },
        ""currency"": {
          ""type"": ""string""
        },
        ""amountDecimal"": {
          ""type"": ""object""
        },
        ""hours"": {
          ""type"": ""object""
        },
        ""rate"": {
          ""type"": ""object""
        },
        ""hoursDecimal"": {
          ""type"": ""object""
        }
      }
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_costs_undo(string jsonResponse)
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
    ""card"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""timekeeperName"": {
          ""type"": ""string""
        },
        ""timekeeperNumber"": {
          ""type"": ""string""
        },
        ""date"": {
          ""type"": ""string""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""presNarrative"": {
          ""type"": ""object""
        },
        ""presAmt"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""cardTypes"": {
          ""type"": ""array""
        },
        ""selectedCardType"": {
          ""type"": ""object""
        },
        ""availableCodes"": {
          ""type"": ""object""
        },
        ""appliedAction"": {
          ""type"": ""string""
        },
        ""proformaId"": {
          ""type"": ""string""
        },
        ""cardIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperJobPosition"": {
          ""type"": ""string""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""isCalculated"": {
          ""type"": ""boolean""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""isErrorsPresented"": {
          ""type"": ""boolean""
        },
        ""messages"": {
          ""type"": ""array""
        },
        ""disposition"": {
          ""type"": ""string""
        },
        ""eBillingValidationDetails"": {
          ""type"": ""array""
        },
        ""currency"": {
          ""type"": ""string""
        },
        ""amountDecimal"": {
          ""type"": ""object""
        },
        ""units"": {
          ""type"": ""object""
        },
        ""rate"": {
          ""type"": ""object""
        },
        ""unitsDecimal"": {
          ""type"": ""object""
        }
      }
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_POST_transfer_charges_undo(string jsonResponse)
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
    ""card"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""timekeeperName"": {
          ""type"": ""string""
        },
        ""timekeeperNumber"": {
          ""type"": ""string""
        },
        ""date"": {
          ""type"": ""string""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""presNarrative"": {
          ""type"": ""object""
        },
        ""presAmt"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""cardTypes"": {
          ""type"": ""array""
        },
        ""selectedCardType"": {
          ""type"": ""object""
        },
        ""availableCodes"": {
          ""type"": ""object""
        },
        ""appliedAction"": {
          ""type"": ""string""
        },
        ""proformaId"": {
          ""type"": ""string""
        },
        ""cardIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperIndex"": {
          ""type"": ""integer""
        },
        ""timekeeperJobPosition"": {
          ""type"": ""string""
        },
        ""amount"": {
          ""type"": ""object""
        },
        ""isCalculated"": {
          ""type"": ""boolean""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""isErrorsPresented"": {
          ""type"": ""boolean""
        },
        ""messages"": {
          ""type"": ""array""
        },
        ""disposition"": {
          ""type"": ""string""
        },
        ""eBillingValidationDetails"": {
          ""type"": ""array""
        },
        ""currency"": {
          ""type"": ""string""
        },
        ""amountDecimal"": {
          ""type"": ""object""
        }
      }
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_PATCH_transfer_excludedTime(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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

        private async Task ValidateResponseSchema_PATCH_transfer_targetProforma(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardRows"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""nonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""recalculateRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""reasonCode"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""timekeeperNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""matterName"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""targetProformaNumber"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""integer""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        }
      }
    },
    ""codes"": {
      ""type"": ""object"",
      ""properties"": {
        ""phase1"": {
          ""type"": ""object""
        },
        ""phase2"": {
          ""type"": ""object""
        },
        ""task1"": {
          ""type"": ""object""
        },
        ""task2"": {
          ""type"": ""object""
        },
        ""activity1"": {
          ""type"": ""object""
        },
        ""activity2"": {
          ""type"": ""object""
        },
        ""taxJur"": {
          ""type"": ""object""
        },
        ""taxCode"": {
          ""type"": ""object""
        }
      }
    },
    ""reasonCodes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformas"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""message"": {
      ""type"": ""string""
    },
    ""messages"": {
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
