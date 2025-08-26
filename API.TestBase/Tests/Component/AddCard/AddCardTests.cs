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

namespace API.TestBase.Tests.Generated.AddCard
{
    [TestFixture]
    [AllureFeature("AddCard API Tests")]
    public class AddCardTests : TestBase
    {

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindex_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addCost/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addCost/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindexResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindexResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindex response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindex_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addCost/init without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addCost/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindex_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addCost/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addCost/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindex_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addCost/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addCost/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_addcost_init_piid_piid_matterindex_matterindexSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_addCost_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindex_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addFee/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addFee/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindexResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindexResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindex response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindex_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addFee/init without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addFee/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindex_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addFee/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addFee/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindex_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/addFee/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .QueryParam("matterIndex", GetTestValue("integer"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/addFee/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_addfee_init_piid_piid_matterindex_matterindexSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_addFee_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewcost_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, workRate = 0, workAmount = 0, billRate = 0, billAmount = 0, timekeeperIndex = 0, date = "string", isDisplayed = true, isNonBillable = true, isNoCharge = true, isExclude = true, workQuantity = 0, billQuantity = 0, isAnticipated = true, isDoNotSummarize = true, anticipatedPayee = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_savenewcostRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/saveNewCost", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewCost")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_savenewcostResponse", () =>
            {
                AttachResponse("post_proforma_savenewcostResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_savenewcost response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewcost_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveNewCost without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewCost")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewcost_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveNewCost with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewCost")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewcost_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, workRate = 0, workAmount = 0, billRate = 0, billAmount = 0, timekeeperIndex = 0, date = "string", isDisplayed = true, isNonBillable = true, isNoCharge = true, isExclude = true, workQuantity = 0, billQuantity = 0, isAnticipated = true, isDoNotSummarize = true, anticipatedPayee = 0 };

            var response = AllureApi.Step("Execute POST /proforma/saveNewCost for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewCost")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_savenewcostSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_saveNewCost(rawJson).Wait();
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewfee_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, workRate = 0, workAmount = 0, billRate = 0, billAmount = 0, timekeeperIndex = 0, date = "string", isDisplayed = true, isNonBillable = true, isNoCharge = true, isExclude = true, workHours = 0, billHours = 0, isFlatFee = true, isAnticipated = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_savenewfeeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/saveNewFee", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewFee")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_savenewfeeResponse", () =>
            {
                AttachResponse("post_proforma_savenewfeeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_savenewfee response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewfee_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveNewFee without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewFee")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewfee_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveNewFee with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewFee")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_savenewfee_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", proformaId = "string", narrative = "string", comments = "string", codes = new { phase1 = "string", phase2 = "string", task1 = "string", task2 = "string", activity1 = "string", activity2 = "string", taxJur = "string", taxCode = "string" }, selectedCardType = "string", isNewCard = true, workRate = 0, workAmount = 0, billRate = 0, billAmount = 0, timekeeperIndex = 0, date = "string", isDisplayed = true, isNonBillable = true, isNoCharge = true, isExclude = true, workHours = 0, billHours = 0, isFlatFee = true, isAnticipated = true };

            var response = AllureApi.Step("Execute POST /proforma/saveNewFee for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveNewFee")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_savenewfeeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_saveNewFee(rawJson).Wait();
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/cancelCardCreation/{cardType}/{cardId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/cancelCardCreation/{GetTestValue("integer")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piidResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/cancelCardCreation/{cardType}/{cardId} without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/cancelCardCreation/{GetTestValue("integer")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/cancelCardCreation/{cardType}/{cardId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/cancelCardCreation/{GetTestValue("integer")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/cancelCardCreation/{cardType}/{cardId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/cancelCardCreation/{GetTestValue("integer")}/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_cancelcardcreation_cardtype_cardid_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_cancelCardCreation_cardType_cardId(rawJson).Wait();
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_card_commit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", id = "string", type = 0, piid = "string", matterIndex = 0 };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_card_commitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/card/commit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/card/commit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_card_commitResponse", () =>
            {
                AttachResponse("post_proforma_card_commitResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_card_commit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_card_commit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/card/commit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/card/commit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_card_commit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/card/commit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/card/commit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("AddCard")]
        public void AddCard_API_post_proforma_card_commit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "AddCard API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", id = "string", type = 0, piid = "string", matterIndex = 0 };

            var response = AllureApi.Step("Execute POST /proforma/card/commit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/card/commit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_card_commitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_card_commit(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_proformaId_addCost_init(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""id"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""timekeeperName"": {
      ""type"": ""string""
    },
    ""timekeeperNumber"": {
      ""type"": ""string""
    },
    ""date"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""narrative"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""presNarrative"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""presAmt"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""comments"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
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
    ""cardTypes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""selectedCardType"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""workRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""workAmount"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billAmount"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""isDisplayed"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isNonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isNoCharge"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isExclude"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""currency"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""refCurrency"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""timekeeperIndex"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""workQuantity"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billQuantity"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""isDoNotSummarize"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isAnticipated"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""anticipatedPayees"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""anticipatedPayee"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_addFee_init(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""id"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""timekeeperName"": {
      ""type"": ""string""
    },
    ""timekeeperNumber"": {
      ""type"": ""string""
    },
    ""date"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""narrative"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""presNarrative"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""presAmt"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""comments"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
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
    ""cardTypes"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""selectedCardType"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""availableCodes"": {
      ""type"": ""object""
    },
    ""timekeepers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""workRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""workAmount"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billRate"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billAmount"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""isDisplayed"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isNonBillable"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isNoCharge"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isExclude"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""currency"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""refCurrency"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""string""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""string""
        }
      }
    },
    ""timekeeperIndex"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""workHours"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""billHours"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""number""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""number""
        }
      }
    },
    ""isFlatFee"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
        }
      }
    },
    ""isAnticipated"": {
      ""type"": ""object"",
      ""properties"": {
        ""value"": {
          ""type"": ""boolean""
        },
        ""isEditable"": {
          ""type"": ""boolean""
        },
        ""isRequired"": {
          ""type"": ""boolean""
        },
        ""aliasValue"": {
          ""type"": ""string""
        },
        ""description"": {
          ""type"": ""string""
        },
        ""displayValue"": {
          ""type"": ""string""
        },
        ""audit"": {
          ""type"": ""object""
        },
        ""originalValue"": {
          ""type"": ""boolean""
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

        private async Task ValidateResponseSchema_POST_proforma_saveNewCost(string jsonResponse)
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
        ""timekeepers"": {
          ""type"": ""array""
        },
        ""workRate"": {
          ""type"": ""object""
        },
        ""workAmount"": {
          ""type"": ""object""
        },
        ""billRate"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""isDisplayed"": {
          ""type"": ""object""
        },
        ""isNonBillable"": {
          ""type"": ""object""
        },
        ""isNoCharge"": {
          ""type"": ""object""
        },
        ""isExclude"": {
          ""type"": ""object""
        },
        ""currency"": {
          ""type"": ""object""
        },
        ""refCurrency"": {
          ""type"": ""object""
        },
        ""timekeeperIndex"": {
          ""type"": ""integer""
        },
        ""workQuantity"": {
          ""type"": ""object""
        },
        ""billQuantity"": {
          ""type"": ""object""
        },
        ""isDoNotSummarize"": {
          ""type"": ""object""
        },
        ""isAnticipated"": {
          ""type"": ""object""
        },
        ""anticipatedPayees"": {
          ""type"": ""array""
        },
        ""anticipatedPayee"": {
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

        private async Task ValidateResponseSchema_POST_proforma_saveNewFee(string jsonResponse)
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
        ""timekeepers"": {
          ""type"": ""array""
        },
        ""workRate"": {
          ""type"": ""object""
        },
        ""workAmount"": {
          ""type"": ""object""
        },
        ""billRate"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""isDisplayed"": {
          ""type"": ""object""
        },
        ""isNonBillable"": {
          ""type"": ""object""
        },
        ""isNoCharge"": {
          ""type"": ""object""
        },
        ""isExclude"": {
          ""type"": ""object""
        },
        ""currency"": {
          ""type"": ""object""
        },
        ""refCurrency"": {
          ""type"": ""object""
        },
        ""timekeeperIndex"": {
          ""type"": ""integer""
        },
        ""workHours"": {
          ""type"": ""object""
        },
        ""billHours"": {
          ""type"": ""object""
        },
        ""isFlatFee"": {
          ""type"": ""object""
        },
        ""isAnticipated"": {
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_cancelCardCreation_cardType_cardId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/{proformaId}/cancelCardCreation/{cardType}/{cardId}""
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

        private async Task ValidateResponseSchema_POST_proforma_card_commit(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""cardId"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""cardIndex"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
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
