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

namespace API.TestBase.Tests.Generated.Proforma
{
    [TestFixture]
    [AllureFeature("Proforma API Tests")]
    public class ProformaTests : TestBase
    {

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_statusfilter_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/statusFilter", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/statusFilter")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_statusfilterResponse", () =>
            {
                AttachResponse("get_proforma_statusfilterResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_statusfilter response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_statusfilter_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/statusFilter without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/statusFilter")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_statusfilter_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/statusFilter with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/statusFilter")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_statusfilter_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/statusFilter for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/statusFilter")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_statusfilterSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_statusFilter(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_id_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{id}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_idResponse", () =>
            {
                AttachResponse("get_proforma_idResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_id response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_id_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{id} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_id_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{id} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_id_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{id} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_idSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_id(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformamatterinfo_proformaid_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaMatterInfo/{proformaId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaMatterInfo/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_getproformamatterinfo_proformaid_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_getproformamatterinfo_proformaid_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_getproformamatterinfo_proformaid_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformamatterinfo_proformaid_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaMatterInfo/{proformaId} without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaMatterInfo/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformamatterinfo_proformaid_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaMatterInfo/{proformaId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaMatterInfo/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformamatterinfo_proformaid_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaMatterInfo/{proformaId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaMatterInfo/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_getproformamatterinfo_proformaid_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_getProformaMatterInfo_proformaId(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_group_details_proformaid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/group/details/{proformaId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/group/details/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_group_details_proformaidResponse", () =>
            {
                AttachResponse("get_proforma_group_details_proformaidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_group_details_proformaid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_group_details_proformaid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/group/details/{proformaId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/group/details/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_group_details_proformaid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/group/details/{proformaId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/group/details/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_group_details_proformaid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/group/details/{proformaId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/group/details/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_group_details_proformaidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_group_details_proformaId(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_unlock_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, sendPiid = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_unlockRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/unlock", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/unlock")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_unlockResponse", () =>
            {
                AttachResponse("post_proforma_unlockResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_unlock response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_unlock_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/unlock without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/unlock")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_unlock_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/unlock with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/unlock")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_unlock_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, sendPiid = true };

            var response = AllureApi.Step("Execute POST /proforma/unlock for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/unlock")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_unlockSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_unlock(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_savetotalsinstructions_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", instructions = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_savetotalsinstructionsRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/saveTotalsInstructions", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveTotalsInstructions")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_savetotalsinstructionsResponse", () =>
            {
                AttachResponse("post_proforma_savetotalsinstructionsResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_savetotalsinstructions response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_savetotalsinstructions_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveTotalsInstructions without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveTotalsInstructions")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_savetotalsinstructions_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/saveTotalsInstructions with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/saveTotalsInstructions")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_savetotalsinstructions_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", instructions = "string" };

            var response = AllureApi.Step("Execute POST /proforma/saveTotalsInstructions for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/saveTotalsInstructions")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_savetotalsinstructionsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_saveTotalsInstructions(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformafilter_bucketname_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaFilter/{bucketName}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaFilter/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_getproformafilter_bucketnameResponse", () =>
            {
                AttachResponse("get_proforma_getproformafilter_bucketnameResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_getproformafilter_bucketname response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformafilter_bucketname_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaFilter/{bucketName} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaFilter/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformafilter_bucketname_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaFilter/{bucketName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaFilter/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_getproformafilter_bucketname_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/getProformaFilter/{bucketName} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/getProformaFilter/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_getproformafilter_bucketnameSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_getProformaFilter_bucketName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_filtervalues_cardtype_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/filterValues/{cardType}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/filterValues/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_filtervalues_cardtype_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_filtervalues_cardtype_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_filtervalues_cardtype_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_filtervalues_cardtype_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/filterValues/{cardType} without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/filterValues/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_filtervalues_cardtype_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/filterValues/{cardType} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/filterValues/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_filtervalues_cardtype_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/filterValues/{cardType} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/filterValues/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_filtervalues_cardtype_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_filterValues_cardType(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", isArchived = true, isCompleted = true, isDetails = true, isLockedByAnotherUser = true, isApprovalPending = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actionsRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actionsResponse", () =>
            {
                AttachResponse("post_proforma_actionsResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", isArchived = true, isCompleted = true, isDetails = true, isLockedByAnotherUser = true, isApprovalPending = true };

            var response = AllureApi.Step("Execute POST /proforma/actions for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actionsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_comments_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/comments", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/comments")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_commentsResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_commentsResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_comments response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_comments_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/comments without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/comments")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_comments_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/comments with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/comments")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_comments_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/comments for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/comments")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_commentsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_comments(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_addcomment_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", text = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_addcommentRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/addComment", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/addComment")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_addcommentResponse", () =>
            {
                AttachResponse("post_proforma_actions_addcommentResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_addcomment response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_addcomment_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/addComment without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/addComment")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_addcomment_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/addComment with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/addComment")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_addcomment_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", text = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/addComment for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/addComment")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_addcommentSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_addComment(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generatebillRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generateBill", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generatebillResponse", () =>
            {
                AttachResponse("post_proforma_generatebillResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generatebill response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            var response = AllureApi.Step("Execute POST /proforma/generateBill for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generatebillSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generateBill(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_submit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, sendEmailList = new { toList = "string", ccList = "string", bccList = "string", from = "string", subject = "string", message = "string", attachmentNames = new[] { "string" } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generatebill_submitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generateBill/submit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/submit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generatebill_submitResponse", () =>
            {
                AttachResponse("post_proforma_generatebill_submitResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generatebill_submit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_submit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill/submit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/submit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_submit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill/submit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/submit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_submit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, sendEmailList = new { toList = "string", ccList = "string", bccList = "string", from = "string", subject = "string", message = "string", attachmentNames = new[] { "string" } } };

            var response = AllureApi.Step("Execute POST /proforma/generateBill/submit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/submit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generatebill_submitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generateBill_submit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_cancel_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generatebill_cancelRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generateBill/cancel", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/cancel")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generatebill_cancelResponse", () =>
            {
                AttachResponse("post_proforma_generatebill_cancelResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generatebill_cancel response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_cancel_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill/cancel without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/cancel")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_cancel_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generateBill/cancel with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/cancel")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_generatebill_cancel_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            var response = AllureApi.Step("Execute POST /proforma/generateBill/cancel for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generateBill/cancel")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generatebill_cancelSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generateBill_cancel(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_forward_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, users = new[] { new { timekeeperIndex = 0, permission = 0 } }, requireProformaDetails = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_forwardRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/forward", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/forward")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_forwardResponse", () =>
            {
                AttachResponse("post_proforma_actions_forwardResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_forward response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_forward_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/forward without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/forward")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_forward_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/forward with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/forward")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_forward_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, users = new[] { new { timekeeperIndex = 0, permission = 0 } }, requireProformaDetails = true };

            var response = AllureApi.Step("Execute POST /proforma/actions/forward for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/forward")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_forwardSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_forward(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_find_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { searchFees = true, searchCharges = true, searchCosts = true, proformaId = "string", textToFind = "string", matchCase = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_findandreplace_findRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/find", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/find")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_findandreplace_findResponse", () =>
            {
                AttachResponse("post_proforma_actions_findandreplace_findResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_findandreplace_find response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_find_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/find without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/find")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_find_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/find with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/find")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_find_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { searchFees = true, searchCharges = true, searchCosts = true, proformaId = "string", textToFind = "string", matchCase = true };

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/find for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/find")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_findandreplace_findSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_findAndReplace_find(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_replace_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", replaceToText = "string", cardsToReplace = new {  } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_findandreplace_replaceRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/replace", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/replace")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_findandreplace_replaceResponse", () =>
            {
                AttachResponse("post_proforma_actions_findandreplace_replaceResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_findandreplace_replace response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_replace_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/replace without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/replace")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_replace_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/replace with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/replace")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_findandreplace_replace_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", replaceToText = "string", cardsToReplace = new {  } };

            var response = AllureApi.Step("Execute POST /proforma/actions/findAndReplace/replace for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/findAndReplace/replace")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_findandreplace_replaceSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_findAndReplace_replace(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, requireProformaDetails = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_submitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/submit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_submitResponse", () =>
            {
                AttachResponse("post_proforma_actions_submitResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_submit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/submit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/submit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, requireProformaDetails = true };

            var response = AllureApi.Step("Execute POST /proforma/actions/submit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_submitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_submit(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_check_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_submit_checkRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/submit/check", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit/check")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_submit_checkResponse", () =>
            {
                AttachResponse("post_proforma_actions_submit_checkResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_submit_check response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_check_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/submit/check without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit/check")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_check_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/submit/check with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit/check")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_submit_check_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            var response = AllureApi.Step("Execute POST /proforma/actions/submit/check for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/submit/check")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_submit_checkSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_submit_check(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_collaborators_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", requireProformaDetails = true, collaborators = new[] { new { timekeeperIndex = 0, permission = 0 } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_collaboratorsRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/collaborators", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/collaborators")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_collaboratorsResponse", () =>
            {
                AttachResponse("post_proforma_actions_collaboratorsResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_collaborators response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_collaborators_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/collaborators without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/collaborators")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_collaborators_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/collaborators with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/collaborators")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_collaborators_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", requireProformaDetails = true, collaborators = new[] { new { timekeeperIndex = 0, permission = 0 } } };

            var response = AllureApi.Step("Execute POST /proforma/actions/collaborators for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/collaborators")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_collaboratorsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_collaborators(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_invoicenarrative_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", invoiceNarrative = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_invoicenarrativeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/invoiceNarrative", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/invoiceNarrative")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_invoicenarrativeResponse", () =>
            {
                AttachResponse("post_proforma_actions_invoicenarrativeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_invoicenarrative response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_invoicenarrative_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/invoiceNarrative without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/invoiceNarrative")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_invoicenarrative_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/invoiceNarrative with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/invoiceNarrative")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_invoicenarrative_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { id = "string", invoiceNarrative = "string" };

            var response = AllureApi.Step("Execute POST /proforma/actions/invoiceNarrative for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/invoiceNarrative")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_invoicenarrativeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_invoiceNarrative(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_print_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, mergeFiles = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_printRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/print", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/print")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_printResponse", () =>
            {
                AttachResponse("post_proforma_actions_printResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_print response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_print_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/print without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/print")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_print_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/print with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/print")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_print_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, mergeFiles = true };

            var response = AllureApi.Step("Execute POST /proforma/actions/print for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/print")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_printSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_print(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_billpreview_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, mergeFiles = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_actions_billpreviewRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/actions/billPreview", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/billPreview")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_actions_billpreviewResponse", () =>
            {
                AttachResponse("post_proforma_actions_billpreviewResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_actions_billpreview response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_billpreview_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/billPreview without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/billPreview")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_billpreview_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/actions/billPreview with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/actions/billPreview")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_actions_billpreview_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaIds = new[] { "string" }, mergeFiles = true };

            var response = AllureApi.Step("Execute POST /proforma/actions/billPreview for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/actions/billPreview")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_actions_billpreviewSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_actions_billPreview(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_actions_print_docs_printrequestid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/actions/print/docs/{printRequestId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/actions/print/docs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_actions_print_docs_printrequestidResponse", () =>
            {
                AttachResponse("get_proforma_actions_print_docs_printrequestidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_actions_print_docs_printrequestid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_actions_print_docs_printrequestid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/actions/print/docs/{printRequestId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/actions/print/docs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_actions_print_docs_printrequestid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/actions/print/docs/{printRequestId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/actions/print/docs/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_actions_print_docs_printrequestid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/actions/print/docs/{printRequestId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/actions/print/docs/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_actions_print_docs_printrequestidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_actions_print_docs_printRequestId(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_genericactions_actionname_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, requireProformaDetails = true, clear3ECache = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_genericactions_actionnameRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/genericActions/{actionName}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_genericactions_actionnameResponse", () =>
            {
                AttachResponse("post_proforma_genericactions_actionnameResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_genericactions_actionname response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_genericactions_actionname_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/{actionName} without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_genericactions_actionname_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/genericActions/{actionName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_genericactions_actionname_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { ids = new[] { "string" }, requireProformaDetails = true, clear3ECache = true };

            var response = AllureApi.Step("Execute POST /proforma/genericActions/{actionName} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/genericActions/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_genericactions_actionnameSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_genericActions_actionName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_history_cardtype_edits_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { selectedCardIds = new[] { "string" }, timekeeperId = "string", latestChangesOnly = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_history_cardtype_editsRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/{cardType}/edits", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/{GetTestValue("string")}/edits")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_history_cardtype_editsResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_history_cardtype_editsResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_history_cardtype_edits response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_history_cardtype_edits_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/{cardType}/edits without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/{GetTestValue("string")}/edits")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_history_cardtype_edits_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/{cardType}/edits with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/{GetTestValue("string")}/edits")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_history_cardtype_edits_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { selectedCardIds = new[] { "string" }, timekeeperId = "string", latestChangesOnly = true };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/history/{cardType}/edits for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/history/{GetTestValue("string")}/edits")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_history_cardtype_editsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_history_cardType_edits(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardCodes/{cardType}/{cardId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardCodes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardCodes/{cardType}/{cardId} without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardCodes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardCodes/{cardType}/{cardId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardCodes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardCodes/{cardType}/{cardId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardCodes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_getcardcodes_cardtype_cardid_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_getCardCodes_cardType_cardId(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_reasoncodes_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/reasonCodes", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_reasoncodesResponse", () =>
            {
                AttachResponse("get_proforma_reasoncodesResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_reasoncodes response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_reasoncodes_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/reasonCodes without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_reasoncodes_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/reasonCodes with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_reasoncodes_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/reasonCodes for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/reasonCodes")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_reasoncodesSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_reasonCodes(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_patch_proforma_reasoncodes_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_proforma_reasoncodesRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /proforma/reasonCodes", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_proforma_reasoncodesResponse", () =>
            {
                AttachResponse("patch_proforma_reasoncodesResponse", rawJson);
            });

            AllureApi.Step("Assert patch_proforma_reasoncodes response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_patch_proforma_reasoncodes_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/reasonCodes without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_patch_proforma_reasoncodes_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /proforma/reasonCodes with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/proforma/reasonCodes")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_patch_proforma_reasoncodes_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { testProperty = "testValue", id = Guid.NewGuid().ToString() };

            var response = AllureApi.Step("Execute PATCH /proforma/reasonCodes for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/proforma/reasonCodes")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_proforma_reasoncodesSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_proforma_reasonCodes(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" }, startDate = "string", endDate = "string", includeCardsFromOtherProformas = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_wip_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/wip", () =>
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
                    .Post($"{baseUrl}/proforma/wip")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_wip_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_wip_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_wip_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/wip without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/wip")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/wip with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/wip")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" }, startDate = "string", endDate = "string", includeCardsFromOtherProformas = true };

            var response = AllureApi.Step("Execute POST /proforma/wip for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/wip")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_wip_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_wip(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_wip_groupmatterlist_billinggroup_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/wip/groupMatterList/{billingGroup}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/wip/groupMatterList/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_wip_groupmatterlist_billinggroupResponse", () =>
            {
                AttachResponse("get_proforma_wip_groupmatterlist_billinggroupResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_wip_groupmatterlist_billinggroup response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_wip_groupmatterlist_billinggroup_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/wip/groupMatterList/{billingGroup} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/wip/groupMatterList/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_wip_groupmatterlist_billinggroup_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/wip/groupMatterList/{billingGroup} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/wip/groupMatterList/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_wip_groupmatterlist_billinggroup_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/wip/groupMatterList/{billingGroup} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/wip/groupMatterList/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_wip_groupmatterlist_billinggroupSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_wip_groupMatterList_billingGroup(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_filterlist_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { startDate = "string", endDate = "string", includeCardsFromOtherProformas = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_wip_filterlistRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/wip/filterList", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/wip/filterList")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_wip_filterlistResponse", () =>
            {
                AttachResponse("post_proforma_wip_filterlistResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_wip_filterlist response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_filterlist_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/wip/filterList without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/wip/filterList")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_filterlist_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/wip/filterList with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/wip/filterList")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_wip_filterlist_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { startDate = "string", endDate = "string", includeCardsFromOtherProformas = true };

            var response = AllureApi.Step("Execute POST /proforma/wip/filterList for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/wip/filterList")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_wip_filterlistSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_wip_filterList(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_groupeditaction_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string", groupEditType = 0, isUndo = true };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_proformaid_groupeditactionRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupEditAction", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupEditAction")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_proformaid_groupeditactionResponse", () =>
            {
                AttachResponse("post_proforma_proformaid_groupeditactionResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_proformaid_groupeditaction response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_groupeditaction_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupEditAction without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupEditAction")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_groupeditaction_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupEditAction with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupEditAction")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_proformaid_groupeditaction_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string", groupEditType = 0, isUndo = true };

            var response = AllureApi.Step("Execute POST /proforma/{proformaId}/groupEditAction for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/{GetTestValue("string")}/groupEditAction")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_proformaid_groupeditactionSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_proformaId_groupEditAction(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_navigation_bucketname_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_navigation_bucketname_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/navigation/{bucketName}", () =>
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
                    .Post($"{baseUrl}/proforma/navigation/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_navigation_bucketname_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_navigation_bucketname_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_navigation_bucketname_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_navigation_bucketname_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/navigation/{bucketName} without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/navigation/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_navigation_bucketname_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/navigation/{bucketName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/navigation/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_navigation_bucketname_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" } };

            var response = AllureApi.Step("Execute POST /proforma/navigation/{bucketName} for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/navigation/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_navigation_bucketname_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_navigation_bucketName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_bucket_bucketname_orderby_orderby_ascending_ascending_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_bucket_bucketname_orderby_orderby_ascending_ascendingRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/bucket/{bucketName}", () =>
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
                    .Post($"{baseUrl}/proforma/bucket/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_bucket_bucketname_orderby_orderby_ascending_ascendingResponse", () =>
            {
                AttachResponse("post_proforma_bucket_bucketname_orderby_orderby_ascending_ascendingResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_bucket_bucketname_orderby_orderby_ascending_ascending response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_bucket_bucketname_orderby_orderby_ascending_ascending_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/bucket/{bucketName} without authorization", () =>
            {
                return Given()
                    .QueryParam("OrderBy", GetTestValue("string"))
                    .QueryParam("Ascending", GetTestValue("boolean"))
                    .When()
                    .Post($"{baseUrl}/proforma/bucket/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_bucket_bucketname_orderby_orderby_ascending_ascending_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/bucket/{bucketName} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/bucket/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_bucket_bucketname_orderby_orderby_ascending_ascending_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { total = new { operationName = "string", operandValue = 0 }, clientNames = new[] { "string" }, clientNumbers = new[] { "string" }, matters = new[] { "string" }, matterDescriptions = new[] { "string" }, matterNumbers = new[] { "string" }, totalFees = new { operationName = "string", operandValue = 0 }, totalCosts = new { operationName = "string", operandValue = 0 }, totalCharges = new { operationName = "string", operandValue = 0 }, matterCurrencies = new[] { "string" }, billGroups = new[] { "string" }, availableFunds = new { operationName = "string", operandValue = 0 }, statuses = new[] { 0 }, flagged = true, forwarded = true, locked = new[] { "string" }, onlyApprovalsPending = true, totalWIP = new { operationName = "string", operandValue = 0 }, fees = new { operationName = "string", operandValue = 0 }, costs = new { operationName = "string", operandValue = 0 }, charges = new { operationName = "string", operandValue = 0 }, proformaIndex = new { operationName = "string", operandValue = 0 }, owners = new[] { "string" }, openedStatuses = new[] { true }, roleStatus = new[] { "string" } };

            var response = AllureApi.Step("Execute POST /proforma/bucket/{bucketName} for schema validation", () =>
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
                    .Post($"{baseUrl}/proforma/bucket/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_bucket_bucketname_orderby_orderby_ascending_ascendingSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_bucket_bucketName(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_count_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late/count", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late/count")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_cards_late_count_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_cards_late_count_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_cards_late_count_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_count_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late/count without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late/count")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_count_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late/count with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late/count")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_count_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late/count for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late/count")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_cards_late_count_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_cards_late_count(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_cards_late_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_cards_late_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_cards_late_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_cards_late_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/cards/late for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/cards/late")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_cards_late_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_cards_late(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_cards_late_include_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { lateCards = new[] { new { popupId = "string", popupRowId = "string", cardIds = new[] { "string" } } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_cards_late_include_piid_piidRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/cards/late/include", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/cards/late/include")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_cards_late_include_piid_piidResponse", () =>
            {
                AttachResponse("post_proforma_cards_late_include_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_cards_late_include_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_cards_late_include_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/cards/late/include without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/proforma/cards/late/include")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_cards_late_include_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/cards/late/include with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/cards/late/include")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_post_proforma_cards_late_include_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { lateCards = new[] { new { popupId = "string", popupRowId = "string", cardIds = new[] { "string" } } } };

            var response = AllureApi.Step("Execute POST /proforma/cards/late/include for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/cards/late/include")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_cards_late_include_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_cards_late_include(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_lockedcards_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/lockedCards", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/lockedCards")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_lockedcards_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_lockedcards_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_lockedcards_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_lockedcards_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/lockedCards without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/lockedCards")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_lockedcards_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/lockedCards with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/lockedCards")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_lockedcards_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/lockedCards for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/lockedCards")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_lockedcards_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_lockedCards(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardtypes_cardtype_cardid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardTypes/{cardType}/{cardId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardTypes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_proformaid_getcardtypes_cardtype_cardidResponse", () =>
            {
                AttachResponse("get_proforma_proformaid_getcardtypes_cardtype_cardidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_proformaid_getcardtypes_cardtype_cardid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardtypes_cardtype_cardid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardTypes/{cardType}/{cardId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardTypes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardtypes_cardtype_cardid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardTypes/{cardType}/{cardId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardTypes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_proformaid_getcardtypes_cardtype_cardid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/{proformaId}/getCardTypes/{cardType}/{cardId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/{GetTestValue("string")}/getCardTypes/{GetTestValue("string")}/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_proformaid_getcardtypes_cardtype_cardidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_proformaId_getCardTypes_cardType_cardId(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_activity_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/activity", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/activity")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_activityResponse", () =>
            {
                AttachResponse("get_proforma_activityResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_activity response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_activity_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/activity without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/activity")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_activity_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/activity with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/activity")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_activity_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/activity for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/activity")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_activitySchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_activity(rawJson).Wait();
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_infos_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/infos", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/infos")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_infosResponse", () =>
            {
                AttachResponse("get_proforma_infosResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_infos response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_infos_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/infos without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/infos")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_infos_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/infos with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/infos")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Proforma")]
        public void Proforma_API_get_proforma_infos_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Proforma API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/infos for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/infos")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_infosSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_infos(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_GET_proforma_statusFilter(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""name"": {
        ""type"": ""string""
      },
      ""value"": {
        ""type"": ""integer""
      },
      ""isDefault"": {
        ""type"": ""boolean""
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

        private async Task ValidateResponseSchema_GET_proforma_id(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: GET /proforma/{id}""
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

        private async Task ValidateResponseSchema_GET_proforma_getProformaMatterInfo_proformaId(string jsonResponse)
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
    ""matterNumber"": {
      ""type"": ""string""
    },
    ""proformaDate"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""timeThrough"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""costThrough"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""chargeThrough"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""billingGroup"": {
      ""type"": ""string""
    },
    ""billTemplate"": {
      ""type"": ""string""
    },
    ""matterCurrency"": {
      ""type"": ""string""
    },
    ""billingTimekeeperName"": {
      ""type"": ""string""
    },
    ""responsibleTimekeeperName"": {
      ""type"": ""string""
    },
    ""supervisorTimekeeperName"": {
      ""type"": ""string""
    },
    ""matterIndex"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""isEBill"": {
      ""type"": ""boolean""
    },
    ""mattStatus"": {
      ""type"": ""string""
    },
    ""paymentTermsInfo"": {
      ""type"": ""string""
    },
    ""billingFrequency"": {
      ""type"": ""string""
    },
    ""department"": {
      ""type"": ""string""
    },
    ""practiceGroup"": {
      ""type"": ""string""
    },
    ""rateCode"": {
      ""type"": ""string""
    },
    ""rateDescription"": {
      ""type"": ""string""
    },
    ""rateExcDescription"": {
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

        private async Task ValidateResponseSchema_GET_proforma_group_details_proformaId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""clientName"": {
        ""type"": ""string""
      },
      ""matterName"": {
        ""type"": ""string""
      },
      ""matterDescription"": {
        ""type"": ""string""
      },
      ""matterNumber"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_unlock(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/unlock""
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

        private async Task ValidateResponseSchema_POST_proforma_saveTotalsInstructions(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/saveTotalsInstructions""
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

        private async Task ValidateResponseSchema_GET_proforma_getProformaFilter_bucketName(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""lockedBy"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""clientNames"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""clientNumbers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""matters"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""matterNumbers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""matterCurrencies"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""billGroups"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""approvalsPending"": {
      ""type"": ""boolean""
    },
    ""owners"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""openedStatuses"": {
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_filterValues_cardType(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - Schema validation failed"",
  ""_note"": ""Endpoint: GET /proforma/{proformaId}/filterValues/{cardType}""
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

        private async Task ValidateResponseSchema_POST_proforma_actions(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""name"": {
        ""type"": ""string""
      },
      ""singleActionUrl"": {
        ""type"": ""string""
      },
      ""batchActionUrl"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_comments(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_actions_addComment(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/actions/addComment""
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

        private async Task ValidateResponseSchema_POST_proforma_generateBill(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_generateBill_submit(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_generateBill_cancel(string jsonResponse)
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
      ""messages"": {
        ""type"": ""array""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_forward(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
      },
      ""timekeepersStatuses"": {
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

        private async Task ValidateResponseSchema_POST_proforma_actions_findAndReplace_find(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""id"": {
        ""type"": ""string""
      },
      ""typeName"": {
        ""type"": ""string""
      },
      ""searchResult"": {
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

        private async Task ValidateResponseSchema_POST_proforma_actions_findAndReplace_replace(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/actions/findAndReplace/replace""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_submit(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_submit_check(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""proformaNumber"": {
        ""type"": ""integer""
      },
      ""clientName"": {
        ""type"": ""string""
      },
      ""matterName"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_collaborators(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""proformaId"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""isProcessCompleted"": {
      ""type"": ""boolean""
    },
    ""isReasonCodeRequired"": {
      ""type"": ""boolean""
    },
    ""proforma"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""wfItemStepIndex"": {
          ""type"": ""integer""
        },
        ""proformaNumber"": {
          ""type"": ""integer""
        },
        ""clientNumber"": {
          ""type"": ""string""
        },
        ""client"": {
          ""type"": ""string""
        },
        ""matter"": {
          ""type"": ""string""
        },
        ""matterNumber"": {
          ""type"": ""string""
        },
        ""total"": {
          ""type"": ""number""
        },
        ""totalCharges"": {
          ""type"": ""number""
        },
        ""totalCosts"": {
          ""type"": ""number""
        },
        ""totalFees"": {
          ""type"": ""number""
        },
        ""applicableFunds"": {
          ""type"": ""number""
        },
        ""availableFunds"": {
          ""type"": ""number""
        },
        ""urgencyDate"": {
          ""type"": ""string""
        },
        ""status"": {
          ""type"": ""integer""
        },
        ""substatus"": {
          ""type"": ""integer""
        },
        ""isOpened"": {
          ""type"": ""boolean""
        },
        ""invoiceNumber"": {
          ""type"": ""string""
        },
        ""invoiceDate"": {
          ""type"": ""string""
        },
        ""currencyCode"": {
          ""type"": ""string""
        },
        ""isPriority"": {
          ""type"": ""boolean""
        },
        ""lockedBy"": {
          ""type"": ""object""
        },
        ""lockedByAuthenticated"": {
          ""type"": ""object""
        },
        ""isForwarded"": {
          ""type"": ""boolean""
        },
        ""isCombiningGroupProforma"": {
          ""type"": ""boolean""
        },
        ""forwarded"": {
          ""type"": ""array""
        },
        ""billGroup"": {
          ""type"": ""string""
        },
        ""hasComments"": {
          ""type"": ""boolean""
        },
        ""commentsCount"": {
          ""type"": ""integer""
        },
        ""billingTimekeeperName"": {
          ""type"": ""string""
        },
        ""billingTimekeeperNumber"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""canGenerateBill"": {
          ""type"": ""boolean""
        },
        ""collaboratorsCount"": {
          ""type"": ""integer""
        },
        ""completedCollaboratorsCount"": {
          ""type"": ""integer""
        },
        ""owner"": {
          ""type"": ""string""
        },
        ""coOwner"": {
          ""type"": ""string""
        },
        ""profDate"": {
          ""type"": ""string""
        },
        ""timestamp"": {
          ""type"": ""string""
        },
        ""approvers"": {
          ""type"": ""array""
        },
        ""roleStatus"": {
          ""type"": ""string""
        },
        ""workedHours"": {
          ""type"": ""number""
        },
        ""billedHours"": {
          ""type"": ""number""
        },
        ""originalFees"": {
          ""type"": ""number""
        },
        ""adjTotal"": {
          ""type"": ""number""
        },
        ""totalEdits"": {
          ""type"": ""number""
        },
        ""originalCost"": {
          ""type"": ""number""
        },
        ""billedCost"": {
          ""type"": ""number""
        },
        ""originalCharges"": {
          ""type"": ""number""
        },
        ""billedCharges"": {
          ""type"": ""number""
        },
        ""estFeeRealization"": {
          ""type"": ""number""
        },
        ""instructions"": {
          ""type"": ""string""
        },
        ""instructionsHistory"": {
          ""type"": ""object""
        },
        ""invoiceNarrative"": {
          ""type"": ""string""
        },
        ""matterTotalsLtdCosts"": {
          ""type"": ""number""
        },
        ""matterTotalsLtdFees"": {
          ""type"": ""number""
        },
        ""matterTotalsLtdWriteOffs"": {
          ""type"": ""number""
        },
        ""payors"": {
          ""type"": ""array""
        },
        ""matterAddress"": {
          ""type"": ""string""
        },
        ""clientAddress"": {
          ""type"": ""string""
        },
        ""matterBillingInstructions"": {
          ""type"": ""string""
        },
        ""clientBillingInstructions"": {
          ""type"": ""string""
        },
        ""isReadonly"": {
          ""type"": ""boolean""
        },
        ""warnings"": {
          ""type"": ""array""
        },
        ""warningMsg"": {
          ""type"": ""string""
        },
        ""attachmentCount"": {
          ""type"": ""integer""
        },
        ""emailBillTo"": {
          ""type"": ""string""
        },
        ""billArrangement"": {
          ""type"": ""string""
        },
        ""billArrangementDescription"": {
          ""type"": ""string""
        },
        ""flatFees"": {
          ""type"": ""array""
        },
        ""workflowOwnership"": {
          ""type"": ""object""
        },
        ""readonlyFallbackReason"": {
          ""type"": ""string""
        },
        ""appliedGroupEditActions"": {
          ""type"": ""array""
        },
        ""proformaStatus"": {
          ""type"": ""string""
        }
      }
    },
    ""proformaListItem"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""wfItemStepIndex"": {
          ""type"": ""integer""
        },
        ""proformaNumber"": {
          ""type"": ""integer""
        },
        ""clientNumber"": {
          ""type"": ""string""
        },
        ""client"": {
          ""type"": ""string""
        },
        ""matter"": {
          ""type"": ""string""
        },
        ""matterNumber"": {
          ""type"": ""string""
        },
        ""total"": {
          ""type"": ""number""
        },
        ""totalCharges"": {
          ""type"": ""number""
        },
        ""totalCosts"": {
          ""type"": ""number""
        },
        ""totalFees"": {
          ""type"": ""number""
        },
        ""applicableFunds"": {
          ""type"": ""number""
        },
        ""availableFunds"": {
          ""type"": ""number""
        },
        ""urgencyDate"": {
          ""type"": ""string""
        },
        ""status"": {
          ""type"": ""integer""
        },
        ""substatus"": {
          ""type"": ""integer""
        },
        ""isOpened"": {
          ""type"": ""boolean""
        },
        ""invoiceNumber"": {
          ""type"": ""string""
        },
        ""invoiceDate"": {
          ""type"": ""string""
        },
        ""currencyCode"": {
          ""type"": ""string""
        },
        ""isPriority"": {
          ""type"": ""boolean""
        },
        ""lockedBy"": {
          ""type"": ""object""
        },
        ""lockedByAuthenticated"": {
          ""type"": ""object""
        },
        ""isForwarded"": {
          ""type"": ""boolean""
        },
        ""isCombiningGroupProforma"": {
          ""type"": ""boolean""
        },
        ""forwarded"": {
          ""type"": ""array""
        },
        ""billGroup"": {
          ""type"": ""string""
        },
        ""hasComments"": {
          ""type"": ""boolean""
        },
        ""commentsCount"": {
          ""type"": ""integer""
        },
        ""billingTimekeeperName"": {
          ""type"": ""string""
        },
        ""billingTimekeeperNumber"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""canGenerateBill"": {
          ""type"": ""boolean""
        },
        ""collaboratorsCount"": {
          ""type"": ""integer""
        },
        ""completedCollaboratorsCount"": {
          ""type"": ""integer""
        },
        ""owner"": {
          ""type"": ""string""
        },
        ""coOwner"": {
          ""type"": ""string""
        },
        ""profDate"": {
          ""type"": ""string""
        },
        ""timestamp"": {
          ""type"": ""string""
        },
        ""approvers"": {
          ""type"": ""array""
        },
        ""roleStatus"": {
          ""type"": ""string""
        }
      }
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""responseType"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""stepType"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""printInfo"": {
      ""type"": ""object"",
      ""properties"": {
        ""saveOptions"": {
          ""type"": ""boolean""
        },
        ""printType"": {
          ""type"": ""integer""
        },
        ""printToScreen"": {
          ""type"": ""boolean""
        },
        ""condensePrint"": {
          ""type"": ""boolean""
        },
        ""name"": {
          ""type"": ""string""
        },
        ""originalPrintJobName"": {
          ""type"": ""string""
        },
        ""orientation"": {
          ""type"": ""integer""
        },
        ""allowReprint"": {
          ""type"": ""boolean""
        },
        ""saveLocal"": {
          ""type"": ""boolean""
        },
        ""fontSize"": {
          ""type"": ""integer""
        },
        ""printCoverPage"": {
          ""type"": ""boolean""
        },
        ""printWorklist"": {
          ""type"": ""boolean""
        },
        ""printWorklistItems"": {
          ""type"": ""integer""
        },
        ""paperSize"": {
          ""type"": ""integer""
        },
        ""fitMethod"": {
          ""type"": ""integer""
        },
        ""keepWithNext"": {
          ""type"": ""boolean""
        },
        ""intelliBreak"": {
          ""type"": ""boolean""
        },
        ""gridFormat"": {
          ""type"": ""integer""
        },
        ""printHeaderAtTheTop"": {
          ""type"": ""boolean""
        },
        ""printGrandTotalOnNewPage"": {
          ""type"": ""boolean""
        },
        ""suppressIfSame"": {
          ""type"": ""boolean""
        },
        ""useNameAsReportHeader"": {
          ""type"": ""boolean""
        },
        ""hasLocalFileTarget"": {
          ""type"": ""boolean""
        },
        ""hasLocalPrintTarget"": {
          ""type"": ""boolean""
        },
        ""renderId"": {
          ""type"": ""string""
        },
        ""printToScreenFileName"": {
          ""type"": ""string""
        },
        ""templateName"": {
          ""type"": ""string""
        },
        ""templateFormat"": {
          ""type"": ""string""
        },
        ""templateSchemaId"": {
          ""type"": ""string""
        },
        ""templates"": {
          ""type"": ""array""
        },
        ""sendEmailList"": {
          ""type"": ""array""
        },
        ""printersList"": {
          ""type"": ""array""
        },
        ""distributionSetup"": {
          ""type"": ""array""
        },
        ""printToFileName"": {
          ""type"": ""array""
        },
        ""defaultFromEmail"": {
          ""type"": ""string""
        },
        ""disableFromEmail"": {
          ""type"": ""boolean""
        },
        ""accessInfo"": {
          ""type"": ""object""
        },
        ""edgStandaloneEnabled"": {
          ""type"": ""boolean""
        },
        ""showOptionsBeforePrinting"": {
          ""type"": ""boolean""
        },
        ""autoFillToEmail"": {
          ""type"": ""boolean""
        },
        ""defaultToEmail"": {
          ""type"": ""string""
        },
        ""forceAsync"": {
          ""type"": ""boolean""
        },
        ""source"": {
          ""type"": ""string""
        },
        ""activity"": {
          ""type"": ""string""
        }
      }
    },
    ""piid"": {
      ""type"": ""string"",
      ""format"": ""uuid""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_invoiceNarrative(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""proformaId"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""isProcessCompleted"": {
      ""type"": ""boolean""
    },
    ""isReasonCodeRequired"": {
      ""type"": ""boolean""
    },
    ""proforma"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""wfItemStepIndex"": {
          ""type"": ""integer""
        },
        ""proformaNumber"": {
          ""type"": ""integer""
        },
        ""clientNumber"": {
          ""type"": ""string""
        },
        ""client"": {
          ""type"": ""string""
        },
        ""matter"": {
          ""type"": ""string""
        },
        ""matterNumber"": {
          ""type"": ""string""
        },
        ""total"": {
          ""type"": ""number""
        },
        ""totalCharges"": {
          ""type"": ""number""
        },
        ""totalCosts"": {
          ""type"": ""number""
        },
        ""totalFees"": {
          ""type"": ""number""
        },
        ""applicableFunds"": {
          ""type"": ""number""
        },
        ""availableFunds"": {
          ""type"": ""number""
        },
        ""urgencyDate"": {
          ""type"": ""string""
        },
        ""status"": {
          ""type"": ""integer""
        },
        ""substatus"": {
          ""type"": ""integer""
        },
        ""isOpened"": {
          ""type"": ""boolean""
        },
        ""invoiceNumber"": {
          ""type"": ""string""
        },
        ""invoiceDate"": {
          ""type"": ""string""
        },
        ""currencyCode"": {
          ""type"": ""string""
        },
        ""isPriority"": {
          ""type"": ""boolean""
        },
        ""lockedBy"": {
          ""type"": ""object""
        },
        ""lockedByAuthenticated"": {
          ""type"": ""object""
        },
        ""isForwarded"": {
          ""type"": ""boolean""
        },
        ""isCombiningGroupProforma"": {
          ""type"": ""boolean""
        },
        ""forwarded"": {
          ""type"": ""array""
        },
        ""billGroup"": {
          ""type"": ""string""
        },
        ""hasComments"": {
          ""type"": ""boolean""
        },
        ""commentsCount"": {
          ""type"": ""integer""
        },
        ""billingTimekeeperName"": {
          ""type"": ""string""
        },
        ""billingTimekeeperNumber"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""canGenerateBill"": {
          ""type"": ""boolean""
        },
        ""collaboratorsCount"": {
          ""type"": ""integer""
        },
        ""completedCollaboratorsCount"": {
          ""type"": ""integer""
        },
        ""owner"": {
          ""type"": ""string""
        },
        ""coOwner"": {
          ""type"": ""string""
        },
        ""profDate"": {
          ""type"": ""string""
        },
        ""timestamp"": {
          ""type"": ""string""
        },
        ""approvers"": {
          ""type"": ""array""
        },
        ""roleStatus"": {
          ""type"": ""string""
        },
        ""workedHours"": {
          ""type"": ""number""
        },
        ""billedHours"": {
          ""type"": ""number""
        },
        ""originalFees"": {
          ""type"": ""number""
        },
        ""adjTotal"": {
          ""type"": ""number""
        },
        ""totalEdits"": {
          ""type"": ""number""
        },
        ""originalCost"": {
          ""type"": ""number""
        },
        ""billedCost"": {
          ""type"": ""number""
        },
        ""originalCharges"": {
          ""type"": ""number""
        },
        ""billedCharges"": {
          ""type"": ""number""
        },
        ""estFeeRealization"": {
          ""type"": ""number""
        },
        ""instructions"": {
          ""type"": ""string""
        },
        ""instructionsHistory"": {
          ""type"": ""object""
        },
        ""invoiceNarrative"": {
          ""type"": ""string""
        },
        ""matterTotalsLtdCosts"": {
          ""type"": ""number""
        },
        ""matterTotalsLtdFees"": {
          ""type"": ""number""
        },
        ""matterTotalsLtdWriteOffs"": {
          ""type"": ""number""
        },
        ""payors"": {
          ""type"": ""array""
        },
        ""matterAddress"": {
          ""type"": ""string""
        },
        ""clientAddress"": {
          ""type"": ""string""
        },
        ""matterBillingInstructions"": {
          ""type"": ""string""
        },
        ""clientBillingInstructions"": {
          ""type"": ""string""
        },
        ""isReadonly"": {
          ""type"": ""boolean""
        },
        ""warnings"": {
          ""type"": ""array""
        },
        ""warningMsg"": {
          ""type"": ""string""
        },
        ""attachmentCount"": {
          ""type"": ""integer""
        },
        ""emailBillTo"": {
          ""type"": ""string""
        },
        ""billArrangement"": {
          ""type"": ""string""
        },
        ""billArrangementDescription"": {
          ""type"": ""string""
        },
        ""flatFees"": {
          ""type"": ""array""
        },
        ""workflowOwnership"": {
          ""type"": ""object""
        },
        ""readonlyFallbackReason"": {
          ""type"": ""string""
        },
        ""appliedGroupEditActions"": {
          ""type"": ""array""
        },
        ""proformaStatus"": {
          ""type"": ""string""
        }
      }
    },
    ""proformaListItem"": {
      ""type"": ""object"",
      ""properties"": {
        ""id"": {
          ""type"": ""string""
        },
        ""wfItemStepIndex"": {
          ""type"": ""integer""
        },
        ""proformaNumber"": {
          ""type"": ""integer""
        },
        ""clientNumber"": {
          ""type"": ""string""
        },
        ""client"": {
          ""type"": ""string""
        },
        ""matter"": {
          ""type"": ""string""
        },
        ""matterNumber"": {
          ""type"": ""string""
        },
        ""total"": {
          ""type"": ""number""
        },
        ""totalCharges"": {
          ""type"": ""number""
        },
        ""totalCosts"": {
          ""type"": ""number""
        },
        ""totalFees"": {
          ""type"": ""number""
        },
        ""applicableFunds"": {
          ""type"": ""number""
        },
        ""availableFunds"": {
          ""type"": ""number""
        },
        ""urgencyDate"": {
          ""type"": ""string""
        },
        ""status"": {
          ""type"": ""integer""
        },
        ""substatus"": {
          ""type"": ""integer""
        },
        ""isOpened"": {
          ""type"": ""boolean""
        },
        ""invoiceNumber"": {
          ""type"": ""string""
        },
        ""invoiceDate"": {
          ""type"": ""string""
        },
        ""currencyCode"": {
          ""type"": ""string""
        },
        ""isPriority"": {
          ""type"": ""boolean""
        },
        ""lockedBy"": {
          ""type"": ""object""
        },
        ""lockedByAuthenticated"": {
          ""type"": ""object""
        },
        ""isForwarded"": {
          ""type"": ""boolean""
        },
        ""isCombiningGroupProforma"": {
          ""type"": ""boolean""
        },
        ""forwarded"": {
          ""type"": ""array""
        },
        ""billGroup"": {
          ""type"": ""string""
        },
        ""hasComments"": {
          ""type"": ""boolean""
        },
        ""commentsCount"": {
          ""type"": ""integer""
        },
        ""billingTimekeeperName"": {
          ""type"": ""string""
        },
        ""billingTimekeeperNumber"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""canGenerateBill"": {
          ""type"": ""boolean""
        },
        ""collaboratorsCount"": {
          ""type"": ""integer""
        },
        ""completedCollaboratorsCount"": {
          ""type"": ""integer""
        },
        ""owner"": {
          ""type"": ""string""
        },
        ""coOwner"": {
          ""type"": ""string""
        },
        ""profDate"": {
          ""type"": ""string""
        },
        ""timestamp"": {
          ""type"": ""string""
        },
        ""approvers"": {
          ""type"": ""array""
        },
        ""roleStatus"": {
          ""type"": ""string""
        }
      }
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""responseType"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""stepType"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""printInfo"": {
      ""type"": ""object"",
      ""properties"": {
        ""saveOptions"": {
          ""type"": ""boolean""
        },
        ""printType"": {
          ""type"": ""integer""
        },
        ""printToScreen"": {
          ""type"": ""boolean""
        },
        ""condensePrint"": {
          ""type"": ""boolean""
        },
        ""name"": {
          ""type"": ""string""
        },
        ""originalPrintJobName"": {
          ""type"": ""string""
        },
        ""orientation"": {
          ""type"": ""integer""
        },
        ""allowReprint"": {
          ""type"": ""boolean""
        },
        ""saveLocal"": {
          ""type"": ""boolean""
        },
        ""fontSize"": {
          ""type"": ""integer""
        },
        ""printCoverPage"": {
          ""type"": ""boolean""
        },
        ""printWorklist"": {
          ""type"": ""boolean""
        },
        ""printWorklistItems"": {
          ""type"": ""integer""
        },
        ""paperSize"": {
          ""type"": ""integer""
        },
        ""fitMethod"": {
          ""type"": ""integer""
        },
        ""keepWithNext"": {
          ""type"": ""boolean""
        },
        ""intelliBreak"": {
          ""type"": ""boolean""
        },
        ""gridFormat"": {
          ""type"": ""integer""
        },
        ""printHeaderAtTheTop"": {
          ""type"": ""boolean""
        },
        ""printGrandTotalOnNewPage"": {
          ""type"": ""boolean""
        },
        ""suppressIfSame"": {
          ""type"": ""boolean""
        },
        ""useNameAsReportHeader"": {
          ""type"": ""boolean""
        },
        ""hasLocalFileTarget"": {
          ""type"": ""boolean""
        },
        ""hasLocalPrintTarget"": {
          ""type"": ""boolean""
        },
        ""renderId"": {
          ""type"": ""string""
        },
        ""printToScreenFileName"": {
          ""type"": ""string""
        },
        ""templateName"": {
          ""type"": ""string""
        },
        ""templateFormat"": {
          ""type"": ""string""
        },
        ""templateSchemaId"": {
          ""type"": ""string""
        },
        ""templates"": {
          ""type"": ""array""
        },
        ""sendEmailList"": {
          ""type"": ""array""
        },
        ""printersList"": {
          ""type"": ""array""
        },
        ""distributionSetup"": {
          ""type"": ""array""
        },
        ""printToFileName"": {
          ""type"": ""array""
        },
        ""defaultFromEmail"": {
          ""type"": ""string""
        },
        ""disableFromEmail"": {
          ""type"": ""boolean""
        },
        ""accessInfo"": {
          ""type"": ""object""
        },
        ""edgStandaloneEnabled"": {
          ""type"": ""boolean""
        },
        ""showOptionsBeforePrinting"": {
          ""type"": ""boolean""
        },
        ""autoFillToEmail"": {
          ""type"": ""boolean""
        },
        ""defaultToEmail"": {
          ""type"": ""string""
        },
        ""forceAsync"": {
          ""type"": ""boolean""
        },
        ""source"": {
          ""type"": ""string""
        },
        ""activity"": {
          ""type"": ""string""
        }
      }
    },
    ""piid"": {
      ""type"": ""string"",
      ""format"": ""uuid""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_print(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
      },
      ""printRequestId"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_actions_billPreview(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
      },
      ""printRequestId"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_GET_proforma_actions_print_docs_printRequestId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: GET /proforma/actions/print/docs/{printRequestId}""
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

        private async Task ValidateResponseSchema_POST_proforma_genericActions_actionName(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""proformaId"": {
        ""type"": ""string""
      },
      ""isSuccess"": {
        ""type"": ""boolean""
      },
      ""isProcessCompleted"": {
        ""type"": ""boolean""
      },
      ""isReasonCodeRequired"": {
        ""type"": ""boolean""
      },
      ""proforma"": {
        ""type"": ""object""
      },
      ""proformaListItem"": {
        ""type"": ""object""
      },
      ""messages"": {
        ""type"": ""array""
      },
      ""responseType"": {
        ""type"": ""integer""
      },
      ""stepType"": {
        ""type"": ""integer""
      },
      ""printInfo"": {
        ""type"": ""object""
      },
      ""piid"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_history_cardType_edits(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""timekeeperId"": {
        ""type"": ""string""
      },
      ""timekeeperName"": {
        ""type"": ""string""
      },
      ""date"": {
        ""type"": ""string""
      },
      ""authenticatedUserName"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_getCardCodes_cardType_cardId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - Schema validation failed"",
  ""_note"": ""Endpoint: GET /proforma/{proformaId}/getCardCodes/{cardType}/{cardId}""
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

        private async Task ValidateResponseSchema_GET_proforma_reasonCodes(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""code"": {
        ""type"": ""string""
      },
      ""description"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_PATCH_proforma_reasonCodes(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: PATCH /proforma/reasonCodes""
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

        private async Task ValidateResponseSchema_POST_proforma_wip(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_proforma_wip_groupMatterList_billingGroup(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""matterNumber"": {
        ""type"": ""string""
      },
      ""matterDescription"": {
        ""type"": ""string""
      },
      ""matterIndex"": {
        ""type"": ""integer""
      },
      ""isLead"": {
        ""type"": ""boolean""
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

        private async Task ValidateResponseSchema_POST_proforma_wip_filterList(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""clientNames"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""clientNumbers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""billGroups"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""matterDescriptions"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    },
    ""matterNumbers"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_proformaId_groupEditAction(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_navigation_bucketName(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""string""
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

        private async Task ValidateResponseSchema_POST_proforma_bucket_bucketName(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_cards_late_count(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""feeCardsCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""costCardsCount"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""chargeCardsCount"": {
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_cards_late(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""feeCards"": {
      ""type"": ""object"",
      ""properties"": {
        ""cards"": {
          ""type"": ""array""
        },
        ""popupId"": {
          ""type"": ""string""
        },
        ""popupRowId"": {
          ""type"": ""string""
        }
      }
    },
    ""costCards"": {
      ""type"": ""object"",
      ""properties"": {
        ""cards"": {
          ""type"": ""array""
        },
        ""popupId"": {
          ""type"": ""string""
        },
        ""popupRowId"": {
          ""type"": ""string""
        }
      }
    },
    ""chargeCards"": {
      ""type"": ""object"",
      ""properties"": {
        ""cards"": {
          ""type"": ""array""
        },
        ""popupId"": {
          ""type"": ""string""
        },
        ""popupRowId"": {
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

        private async Task ValidateResponseSchema_POST_proforma_cards_late_include(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /proforma/cards/late/include""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_lockedCards(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""cardIndex"": {
        ""type"": ""integer""
      },
      ""baseUserName"": {
        ""type"": ""string""
      },
      ""cardType"": {
        ""type"": ""integer""
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

        private async Task ValidateResponseSchema_GET_proforma_proformaId_getCardTypes_cardType_cardId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""name"": {
        ""type"": ""string""
      },
      ""value"": {
        ""type"": ""string""
      },
      ""isDefault"": {
        ""type"": ""boolean""
      },
      ""attributes"": {
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

        private async Task ValidateResponseSchema_GET_proforma_activity(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""modifiedDate"": {
        ""type"": ""string""
      },
      ""name"": {
        ""type"": ""string""
      },
      ""role"": {
        ""type"": ""string""
      },
      ""step"": {
        ""type"": ""string""
      },
      ""outputRule"": {
        ""type"": ""string""
      },
      ""activity"": {
        ""type"": ""string""
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

        private async Task ValidateResponseSchema_GET_proforma_infos(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""proformaNumber"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""proformaId"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""totalAmount"": {
      ""type"": ""number"",
      ""format"": ""double""
    },
    ""owner"": {
      ""type"": ""string""
    },
    ""clientName"": {
      ""type"": ""string""
    },
    ""clientNumber"": {
      ""type"": ""string""
    },
    ""currency"": {
      ""type"": ""string""
    },
    ""matter"": {
      ""type"": ""string""
    },
    ""matterNumber"": {
      ""type"": ""string""
    },
    ""approverName"": {
      ""type"": ""string""
    },
    ""totalCollaborators"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""completedCollaborators"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""lastViewedDate"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""creationDate"": {
      ""type"": ""string"",
      ""format"": ""date-time""
    },
    ""status"": {
      ""type"": ""string""
    },
    ""trackingStatus"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""collaboratorActivity"": {
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
