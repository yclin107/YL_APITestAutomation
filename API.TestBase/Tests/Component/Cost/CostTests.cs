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

namespace API.TestBase.Tests.Generated.Cost
{
    [TestFixture]
    [AllureFeature("Cost API Tests")]
    public class CostTests : TestBase
    {

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/costs", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/costs")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/costs without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/costs")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/costs with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/costs")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/costs for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/costs")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_costs_piid_piid_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_costs(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_groupcosts_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_groupcosts_piid_piidRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCosts", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCosts")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_groupcosts_piid_piidResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_groupcosts_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_groupcosts_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_groupcosts_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCosts without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCosts")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_groupcosts_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCosts with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCosts")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_groupcosts_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { itemIds = new[] { "string" }, appliedActions = new[] { "string" }, timekeepersIndices = new[] { "string" }, isStatusValid = true, isEdited = true, taxCodes = new[] { "string" }, taxJurisdictions = new[] { "string" }, phases1 = new[] { "string" }, phases2 = new[] { "string" }, tasks1 = new[] { "string" }, tasks2 = new[] { "string" }, activities1 = new[] { "string" }, activities2 = new[] { "string" }, group = new { type = "string", value = new {  } } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupCosts for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupCosts")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_groupcosts_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_groupCosts(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_bulkedit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, narrative = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_costs_bulkeditRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/bulkEdit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/bulkEdit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_costs_bulkeditResponse", () =>
            {
                AttachResponse("post_proforma_actions_costs_bulkeditResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_costs_bulkedit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_bulkedit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/bulkEdit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/bulkEdit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_bulkedit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/bulkEdit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/bulkEdit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_bulkedit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, narrative = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/bulkEdit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/bulkEdit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_costs_bulkeditSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_costs_bulkEdit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_purge_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, purgeType = "string", proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_costs_purgeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/purge", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/purge")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_costs_purgeResponse", () =>
            {
                AttachResponse("post_proforma_actions_costs_purgeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_costs_purge response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_purge_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/purge without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/purge")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_purge_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/purge with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/purge")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_purge_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, purgeType = "string", proformaId = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/purge for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/purge")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_costs_purgeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_costs_purge(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_costs_combine_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_costs_combine_initResponse", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_costs_combine_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_costs_combine_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_recalc_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", value = 0, fieldName = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_costs_combine_recalcRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/recalc", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/recalc")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_costs_combine_recalcResponse", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_recalcResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_costs_combine_recalc response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_recalc_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/recalc without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/recalc")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_recalc_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/recalc with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/recalc")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_recalc_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", value = 0, fieldName = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/recalc for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/recalc")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_recalcSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_costs_combine_recalc(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_commit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, card = new { id = "string", timekeeperName = "string", timekeeperNumber = "string", date = "string", narrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presNarrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presAmt = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, comments = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, codes = new { phase1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, phase2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxJur = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxCode = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } } }, cardTypes = new[] { new { name = "string", value = "string", isDefault = true, attributes = new {  } } }, selectedCardType = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, availableCodes = new {  }, appliedAction = "string", proformaId = "string", cardIndex = 0, timekeeperIndex = 0, timekeeperJobPosition = "string", amount = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, isCalculated = true, matter = new { name = "string", matterNumber = "string", matterStatus = "string", isEBill = "string", paymentTermsInfo = "string", billingFrequency = "string", department = "string", practiceGroup = "string", rateCode = "string", rateDescription = "string", rateExcDescription = "string" }, isErrorsPresented = true, messages = new[] { new { fieldName = "string", text = "string" } }, disposition = "string", eBillingValidationDetails = new[] { new { message = "string", severity = 0, type = "string" } }, currency = "string", amountDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, units = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, rate = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, unitsDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 } }, combineType = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_costs_combine_commitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/commit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/commit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_costs_combine_commitResponse", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_commitResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_costs_combine_commit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_commit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/commit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/commit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_commit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/commit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/commit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_actions_costs_combine_commit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } }, card = new { id = "string", timekeeperName = "string", timekeeperNumber = "string", date = "string", narrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presNarrative = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, presAmt = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, comments = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, codes = new { phase1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, phase2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, task2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity1 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, activity2 = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxJur = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } }, taxCode = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" } } }, cardTypes = new[] { new { name = "string", value = "string", isDefault = true, attributes = new {  } } }, selectedCardType = new { value = "string", isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = "string" }, availableCodes = new {  }, appliedAction = "string", proformaId = "string", cardIndex = 0, timekeeperIndex = 0, timekeeperJobPosition = "string", amount = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, isCalculated = true, matter = new { name = "string", matterNumber = "string", matterStatus = "string", isEBill = "string", paymentTermsInfo = "string", billingFrequency = "string", department = "string", practiceGroup = "string", rateCode = "string", rateDescription = "string", rateExcDescription = "string" }, isErrorsPresented = true, messages = new[] { new { fieldName = "string", text = "string" } }, disposition = "string", eBillingValidationDetails = new[] { new { message = "string", severity = 0, type = "string" } }, currency = "string", amountDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, units = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, rate = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 }, unitsDecimal = new { value = 0, isEditable = true, isRequired = true, aliasValue = "string", description = "string", displayValue = "string", audit = new { newValue = "string", previousValue = "string", timeStamp = "string", userName = "string" }, originalValue = 0 } }, combineType = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/costs/combine/commit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/costs/combine/commit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_costs_combine_commitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_costs_combine_commit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_genericactions_costs_actionname_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_genericactions_costs_actionnameRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/genericActions/costs/{actionName}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/costs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_genericactions_costs_actionnameResponse", () =>
            {
                AttachResponse("post_proforma_genericactions_costs_actionnameResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_genericactions_costs_actionname response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_genericactions_costs_actionname_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/costs/{actionName} without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/costs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_genericactions_costs_actionname_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/costs/{actionName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/costs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_genericactions_costs_actionname_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { isUndo = true, comment = "string", ids = new[] { "string" }, cardsData = new[] { new { id = "string", isActionApplied = true } } };

            var response = AllureApi.Step("Execute POST /proforma/genericActions/costs/{actionName} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/costs/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_genericactions_costs_actionnameSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_genericActions_costs_actionName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_savecost_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, rate = 0, units = 0, amount = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_savecostRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/saveCost", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveCost")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_savecostResponse", () =>
            {
                AttachResponse("post_proforma_savecostResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_savecost response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_savecost_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveCost without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveCost")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_savecost_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveCost with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveCost")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_savecost_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, rate = 0, units = 0, amount = 0 };

            var response = AllureApi.Step("Execute POST /proforma/saveCost for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveCost")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_savecostSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_saveCost(rawJson).Wait();
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { date = "string", timekeeperId = "string", selectedCardIds = new[] { "string" } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/costs", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/costs")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/costs without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/costs")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/costs with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/costs")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Cost")]
        public void Cost_API_post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Cost API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { date = "string", timekeeperId = "string", selectedCardIds = new[] { "string" } };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/costs for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/costs")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_history_costs_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_history_costs(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_proformaId_costs(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_groupCosts(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_actions_costs_bulkEdit(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_actions_costs_purge(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_actions_costs_combine_init(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_actions_costs_combine_recalc(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""notChanged"": {
      ""type"": ""boolean""
    },
    ""value"": {
      ""type"": ""number"",
      ""format"": ""double""
    },
    ""rate"": {
      ""type"": ""number"",
      ""format"": ""double""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_costs_combine_commit(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_genericActions_costs_actionName(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_saveCost(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_history_costs(string jsonResponse)
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
