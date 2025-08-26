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

namespace API.TestBase.Tests.Generated.Charge
{
    [TestFixture]
    [AllureFeature("Charge API Tests")]
    public class ChargeTests : TestBase
    {

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendi_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendiRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/charges", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/charges")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendiResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendiResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendi response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendi_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/charges without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/charges")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendi_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/charges with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/charges")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendi_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/charges for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/charges")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_charges_piid_piid_orderby_orderby_ascending_ascendiSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_charges(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_groupcharges_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_groupcharges_piid_piidRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCharges", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCharges")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_groupcharges_piid_piidResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_groupcharges_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_groupcharges_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_groupcharges_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCharges without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCharges")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_groupcharges_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCharges with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCharges")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_groupcharges_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCharges for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCharges")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_groupcharges_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_groupCharges(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_bulkedit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, narrative = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_charges_bulkeditRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/bulkEdit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/bulkEdit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_charges_bulkeditResponse", () =>
            {
                AttachResponse("post_proforma_actions_charges_bulkeditResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_charges_bulkedit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_bulkedit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/bulkEdit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/bulkEdit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_bulkedit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/bulkEdit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/bulkEdit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_bulkedit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, narrative = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/bulkEdit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/bulkEdit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_charges_bulkeditSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_charges_bulkEdit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_purge_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, purgeType = "string", proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_charges_purgeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/purge", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/purge")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_charges_purgeResponse", () =>
            {
                AttachResponse("post_proforma_actions_charges_purgeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_charges_purge response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_purge_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/purge without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/purge")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_purge_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/purge with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/purge")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_purge_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, purgeType = "string", proformaId = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/purge for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/purge")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_charges_purgeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_charges_purge(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_charges_combine_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_charges_combine_initResponse", () =>
            {
                AttachResponse("post_proforma_actions_charges_combine_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_charges_combine_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_charges_combine_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_charges_combine_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_commit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, card = new { id = "string", timekeeperName = "string", timekeeperNumber = "string", date = "string", narrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presNarrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presAmt = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, comments = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, codes = new { phase1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, phase2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxJur = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxCode = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } } }, cardTypes = new[] { new { name = "string", value = "string", isDefault = true, attributes = new {  } } }, selectedCardType = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, availableCodes = new {  }, appliedAction = "string", proformaId = "string", cardIndex = 0, timekeeperIndex = 0, timekeeperJobPosition = "string", amount = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, isCalculated = true, matter = new { name = "string", matterNumber = "string", matterStatus = "string", isEBill = "string", paymentTermsInfo = "string", billingFrequency = "string", department = "string", practiceGroup = "string", rateCode = "string", rateDescription = "string", rateExcDescription = "string" }, isErrorsPresented = true, messages = new[] { new { fieldName = "string", text = "string" } }, disposition = "string", eBillingValidationDetails = new[] { new { message = "string", severity = 0, type = "string" } }, currency = "string", amountDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 } }, combineType = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_charges_combine_commitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/commit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/commit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_charges_combine_commitResponse", () =>
            {
                AttachResponse("post_proforma_actions_charges_combine_commitResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_charges_combine_commit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_commit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/commit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/commit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_commit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/commit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/commit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_actions_charges_combine_commit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, card = new { id = "string", timekeeperName = "string", timekeeperNumber = "string", date = "string", narrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presNarrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presAmt = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, comments = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, codes = new { phase1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, phase2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxJur = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxCode = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } } }, cardTypes = new[] { new { name = "string", value = "string", isDefault = true, attributes = new {  } } }, selectedCardType = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, availableCodes = new {  }, appliedAction = "string", proformaId = "string", cardIndex = 0, timekeeperIndex = 0, timekeeperJobPosition = "string", amount = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, isCalculated = true, matter = new { name = "string", matterNumber = "string", matterStatus = "string", isEBill = "string", paymentTermsInfo = "string", billingFrequency = "string", department = "string", practiceGroup = "string", rateCode = "string", rateDescription = "string", rateExcDescription = "string" }, isErrorsPresented = true, messages = new[] { new { fieldName = "string", text = "string" } }, disposition = "string", eBillingValidationDetails = new[] { new { message = "string", severity = 0, type = "string" } }, currency = "string", amountDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 } }, combineType = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/charges/combine/commit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/charges/combine/commit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_charges_combine_commitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_charges_combine_commit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_genericactions_charges_actionname_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_genericactions_charges_actionnameRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/genericActions/charges/{actionName}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/charges/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_genericactions_charges_actionnameResponse", () =>
            {
                AttachResponse("post_proforma_genericactions_charges_actionnameResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_genericactions_charges_actionname response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_genericactions_charges_actionname_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/charges/{actionName} without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/charges/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_genericactions_charges_actionname_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/charges/{actionName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/charges/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_genericactions_charges_actionname_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            var response = AllureApi.Step("Execute POST /proforma/genericActions/charges/{actionName} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/charges/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_genericactions_charges_actionnameSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_genericActions_charges_actionName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_savecharge_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, amount = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_savechargeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/saveCharge", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveCharge")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_savechargeResponse", () =>
            {
                AttachResponse("post_proforma_savechargeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_savecharge response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_savecharge_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveCharge without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveCharge")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_savecharge_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveCharge with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveCharge")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_savecharge_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, amount = 0 };

            var response = AllureApi.Step("Execute POST /proforma/saveCharge for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveCharge")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_savechargeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_saveCharge(rawJson).Wait();
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { date = "string", timekeeperId = "string", selectedCardIds = new[] { "string" } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/charges", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/charges")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/charges without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/charges")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/charges with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/charges")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Charge")]
        public void Charge_API_post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Charge API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { date = "string", timekeeperId = "string", selectedCardIds = new[] { "string" } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/charges for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/charges")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_history_charges_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_history_charges(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_proformaId_charges(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""data"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""cardActions"": {
      ""type"": ""object"",
      ""properties"": {
        ""bulkActions"": {
          ""type"": ""array""
        },
        ""singleActions"": {
          ""type"": ""array""
        },
        ""combinedActions"": {
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_groupCharges(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""data"": {
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

        private async Task ValidateResponseSchema_POST_proforma_actions_charges_bulkEdit(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""card"": {
        ""type"": ""object""
      },
      ""message"": {
        ""type"": ""string""
      },
      ""messages"": {
        ""type"": ""array""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_charges_purge(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""card"": {
        ""type"": ""object""
      },
      ""message"": {
        ""type"": ""string""
      },
      ""messages"": {
        ""type"": ""array""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_charges_combine_init(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isCombiningAllowed"": {
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
    ""combineTypes"": {
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
    ""narratives"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""originalCardTypes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""message"": {
      ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_charges_combine_commit(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_genericActions_charges_actionName(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""card"": {
        ""type"": ""object""
      },
      ""message"": {
        ""type"": ""string""
      },
      ""messages"": {
        ""type"": ""array""
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

        private async Task ValidateResponseSchema_POST_proforma_saveCharge(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_history_charges(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""recordCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""data"": {
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
