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

namespace API.TestBase.Tests.Generated.ProformaGeneration
{
    [TestFixture]
    [AllureFeature("ProformaGeneration API Tests")]
    public class ProformaGenerationTests : TestBase
    {

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { matterIndices = new[] { 0 }, startDate = "string", endDate = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_initResponse", () =>
            {
                AttachResponse("post_proforma_generation_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { matterIndices = new[] { 0 }, startDate = "string", endDate = "string" };

            var response = AllureApi.Step("Execute POST /proforma/generation/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_cancel_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_cancelRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/cancel", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/cancel")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_cancelResponse", () =>
            {
                AttachResponse("post_proforma_generation_cancelResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_cancel response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_cancel_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/cancel without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/cancel")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_cancel_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/cancel with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/cancel")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_cancel_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string" };

            var response = AllureApi.Step("Execute POST /proforma/generation/cancel for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/cancel")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_cancelSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_cancel(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string", rowId = "string", field = new { name = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_fieldResponse", () =>
            {
                AttachResponse("post_proforma_generation_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string", rowId = "string", field = new { name = "string", value = new {  } } };

            var response = AllureApi.Step("Execute POST /proforma/generation/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_generate_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_generateRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/generate", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/generate")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_generateResponse", () =>
            {
                AttachResponse("post_proforma_generation_generateResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_generate response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_generate_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/generate without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/generate")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_generate_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/generate with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/generate")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_generate_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { piid = "string" };

            var response = AllureApi.Step("Execute POST /proforma/generation/generate for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/generate")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_generateSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_generate(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_remove_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaRowId = "string", matterRowIds = new[] { "string" }, piid = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_matter_removeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/remove", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/remove")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_matter_removeResponse", () =>
            {
                AttachResponse("post_proforma_generation_matter_removeResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_matter_remove response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_remove_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/remove without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/remove")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_remove_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/remove with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/remove")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_remove_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaRowId = "string", matterRowIds = new[] { "string" }, piid = "string" };

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/remove for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/remove")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_matter_removeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_matter_remove(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_restore_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaRowId = "string", matterRowIds = new[] { "string" }, piid = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_proforma_generation_matter_restoreRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/restore", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/restore")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_proforma_generation_matter_restoreResponse", () =>
            {
                AttachResponse("post_proforma_generation_matter_restoreResponse", rawJson);
            });

            AllureApi.Step("Assert post_proforma_generation_matter_restore response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_restore_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/restore without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/restore")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_restore_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/restore with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/restore")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_post_proforma_generation_matter_restore_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaRowId = "string", matterRowIds = new[] { "string" }, piid = "string" };

            var response = AllureApi.Step("Execute POST /proforma/generation/matter/restore for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/proforma/generation/matter/restore")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_proforma_generation_matter_restoreSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_proforma_generation_matter_restore(rawJson).Wait();
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_get_proforma_generation_preview_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/generation/preview", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("Piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/generation/preview")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_proforma_generation_preview_piid_piidResponse", () =>
            {
                AttachResponse("get_proforma_generation_preview_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_proforma_generation_preview_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_get_proforma_generation_preview_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/generation/preview without authorization", () =>
            {
                return Given()
                    .QueryParam("Piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/generation/preview")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_get_proforma_generation_preview_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/generation/preview with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/proforma/generation/preview")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("ProformaGeneration")]
        public void ProformaGeneration_API_get_proforma_generation_preview_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "ProformaGeneration API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /proforma/generation/preview for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("Piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/proforma/generation/preview")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_proforma_generation_preview_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_proforma_generation_preview(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_proforma_generation_init(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""proformaStatusCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""changeStatusCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""coOwnerCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformaData"": {
      ""type"": ""object"",
      ""properties"": {
        ""description"": {
          ""type"": ""object""
        },
        ""profStatus"": {
          ""type"": ""object""
        },
        ""profStatusOther"": {
          ""type"": ""object""
        },
        ""isCreateSingleProforma"": {
          ""type"": ""object""
        },
        ""isIncludeOtherProforma"": {
          ""type"": ""object""
        },
        ""isIgnoreExcludedEntry"": {
          ""type"": ""object""
        },
        ""isIncludeProformasWithoutWIP"": {
          ""type"": ""object""
        },
        ""invDate"": {
          ""type"": ""object""
        },
        ""timeStart"": {
          ""type"": ""object""
        },
        ""timeEnd"": {
          ""type"": ""object""
        },
        ""costStart"": {
          ""type"": ""object""
        },
        ""costEnd"": {
          ""type"": ""object""
        },
        ""chrgStart"": {
          ""type"": ""object""
        },
        ""chrgEnd"": {
          ""type"": ""object""
        },
        ""coOwner"": {
          ""type"": ""object""
        }
      }
    },
    ""piid"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""rowId"": {
      ""type"": ""string""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
    },
    ""messages"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""matterItems"": {
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

        private async Task ValidateResponseSchema_POST_proforma_generation_cancel(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""isProcessCompleted"": {
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

        private async Task ValidateResponseSchema_POST_proforma_generation_field(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""proformaStatusCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""changeStatusCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""coOwnerCatalog"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""object""
      }
    },
    ""proformaData"": {
      ""type"": ""object"",
      ""properties"": {
        ""description"": {
          ""type"": ""object""
        },
        ""profStatus"": {
          ""type"": ""object""
        },
        ""profStatusOther"": {
          ""type"": ""object""
        },
        ""isCreateSingleProforma"": {
          ""type"": ""object""
        },
        ""isIncludeOtherProforma"": {
          ""type"": ""object""
        },
        ""isIgnoreExcludedEntry"": {
          ""type"": ""object""
        },
        ""isIncludeProformasWithoutWIP"": {
          ""type"": ""object""
        },
        ""invDate"": {
          ""type"": ""object""
        },
        ""timeStart"": {
          ""type"": ""object""
        },
        ""timeEnd"": {
          ""type"": ""object""
        },
        ""costStart"": {
          ""type"": ""object""
        },
        ""costEnd"": {
          ""type"": ""object""
        },
        ""chrgStart"": {
          ""type"": ""object""
        },
        ""chrgEnd"": {
          ""type"": ""object""
        },
        ""coOwner"": {
          ""type"": ""object""
        }
      }
    },
    ""piid"": {
      ""type"": ""string"",
      ""format"": ""uuid""
    },
    ""rowId"": {
      ""type"": ""string""
    },
    ""isSuccess"": {
      ""type"": ""boolean""
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

        private async Task ValidateResponseSchema_POST_proforma_generation_generate(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_generation_matter_remove(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_proforma_generation_matter_restore(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_proforma_generation_preview(string jsonResponse)
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
        ""groupProforma"": {
          ""type"": ""integer""
        },
        ""singleProforma"": {
          ""type"": ""integer""
        },
        ""infoList"": {
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
