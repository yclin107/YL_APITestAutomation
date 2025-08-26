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

namespace API.TestBase.Tests.Generated.Divide
{
    [TestFixture]
    [AllureFeature("Divide API Tests")]
    public class DivideTests : TestBase
    {

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_fees_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/fees/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_fees_initResponse", () =>
            {
                AttachResponse("post_divide_fees_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_fees_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/fees/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_fees_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_fees_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_card_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_fees_card_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/fees/card/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/fees/card/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_fees_card_fieldResponse", () =>
            {
                AttachResponse("patch_divide_fees_card_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_fees_card_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_card_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/fees/card/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/fees/card/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_card_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/fees/card/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/fees/card/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_card_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            var response = AllureApi.Step("Execute PATCH /divide/fees/card/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/fees/card/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_fees_card_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_fees_card_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_add_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_fees_card_addRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/fees/card/add", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/add")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_fees_card_addResponse", () =>
            {
                AttachResponse("post_divide_fees_card_addResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_fees_card_add response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_add_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/card/add without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/add")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_add_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/card/add with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/add")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_add_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /divide/fees/card/add for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/add")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_fees_card_addSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_fees_card_add(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_fees_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/fees/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/fees/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_fees_fieldResponse", () =>
            {
                AttachResponse("patch_divide_fees_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_fees_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/fees/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/fees/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/fees/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/fees/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_fees_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            var response = AllureApi.Step("Execute PATCH /divide/fees/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/fees/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_fees_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_fees_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_recalcrate_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_fees_card_recalcrateRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/fees/card/recalcRate", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/recalcRate")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_fees_card_recalcrateResponse", () =>
            {
                AttachResponse("post_divide_fees_card_recalcrateResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_fees_card_recalcrate response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_recalcrate_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/card/recalcRate without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/recalcRate")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_recalcrate_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/fees/card/recalcRate with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/recalcRate")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_fees_card_recalcrate_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/fees/card/recalcRate for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/fees/card/recalcRate")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_fees_card_recalcrateSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_fees_card_recalcRate(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_costs_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/costs/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_costs_initResponse", () =>
            {
                AttachResponse("post_divide_costs_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_costs_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/costs/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_costs_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_costs_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_card_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_costs_card_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/costs/card/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/costs/card/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_costs_card_fieldResponse", () =>
            {
                AttachResponse("patch_divide_costs_card_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_costs_card_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_card_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/costs/card/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/costs/card/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_card_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/costs/card/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/costs/card/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_card_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            var response = AllureApi.Step("Execute PATCH /divide/costs/card/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/costs/card/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_costs_card_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_costs_card_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_add_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_costs_card_addRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/costs/card/add", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/add")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_costs_card_addResponse", () =>
            {
                AttachResponse("post_divide_costs_card_addResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_costs_card_add response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_add_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/card/add without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/add")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_add_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/card/add with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/add")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_add_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /divide/costs/card/add for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/add")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_costs_card_addSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_costs_card_add(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_costs_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/costs/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/costs/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_costs_fieldResponse", () =>
            {
                AttachResponse("patch_divide_costs_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_costs_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/costs/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/costs/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/costs/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/costs/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_costs_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            var response = AllureApi.Step("Execute PATCH /divide/costs/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/costs/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_costs_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_costs_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_recalcrate_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_costs_card_recalcrateRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/costs/card/recalcRate", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/recalcRate")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_costs_card_recalcrateResponse", () =>
            {
                AttachResponse("post_divide_costs_card_recalcrateResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_costs_card_recalcrate response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_recalcrate_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/card/recalcRate without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/recalcRate")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_recalcrate_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/costs/card/recalcRate with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/recalcRate")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_costs_card_recalcrate_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/costs/card/recalcRate for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/costs/card/recalcRate")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_costs_card_recalcrateSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_costs_card_recalcRate(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_init_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_charges_initRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/charges/init", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/charges/init")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_charges_initResponse", () =>
            {
                AttachResponse("post_divide_charges_initResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_charges_init response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_init_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/charges/init without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/charges/init")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_init_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/charges/init with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/charges/init")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_init_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/charges/init for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/charges/init")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_charges_initSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_charges_init(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_card_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_charges_card_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/charges/card/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/charges/card/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_charges_card_fieldResponse", () =>
            {
                AttachResponse("patch_divide_charges_card_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_charges_card_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_card_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/charges/card/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/charges/card/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_card_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/charges/card/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/charges/card/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_card_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } }, cardId = "string" };

            var response = AllureApi.Step("Execute PATCH /divide/charges/card/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/charges/card/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_charges_card_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_charges_card_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_card_add_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_charges_card_addRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/charges/card/add", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/charges/card/add")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_charges_card_addResponse", () =>
            {
                AttachResponse("post_divide_charges_card_addResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_charges_card_add response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_card_add_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/charges/card/add without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/charges/card/add")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_card_add_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/charges/card/add with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/charges/card/add")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_charges_card_add_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /divide/charges/card/add for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/charges/card/add")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_charges_card_addSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_charges_card_add(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_field_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("patch_divide_charges_fieldRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute PATCH /divide/charges/field", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/charges/field")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach patch_divide_charges_fieldResponse", () =>
            {
                AttachResponse("patch_divide_charges_fieldResponse", rawJson);
            });

            AllureApi.Step("Assert patch_divide_charges_field response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_field_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/charges/field without authorization", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/charges/field")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_field_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute PATCH /divide/charges/field with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Patch($"{baseUrl}/divide/charges/field")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_patch_divide_charges_field_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", field = new { name = "string", value = new {  } } };

            var response = AllureApi.Step("Execute PATCH /divide/charges/field for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Patch($"{baseUrl}/divide/charges/field")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("patch_divide_charges_fieldSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_PATCH_divide_charges_field(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_card_matter_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", searchText = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_card_matterRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/card/matter", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/card/matter")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_card_matterResponse", () =>
            {
                AttachResponse("post_divide_card_matterResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_card_matter response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_card_matter_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/card/matter without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/card/matter")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_card_matter_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/card/matter with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/card/matter")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_card_matter_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", searchText = "string" };

            var response = AllureApi.Step("Execute POST /divide/card/matter for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/card/matter")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_card_matterSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_card_matter(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cardtype_card_remove_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_cardtype_card_removeRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/{cardType}/card/remove", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/{GetTestValue("integer")}/card/remove")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_cardtype_card_removeResponse", () =>
            {
                AttachResponse("post_divide_cardtype_card_removeResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_cardtype_card_remove response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cardtype_card_remove_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/{cardType}/card/remove without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/{GetTestValue("integer")}/card/remove")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cardtype_card_remove_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/{cardType}/card/remove with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/{GetTestValue("integer")}/card/remove")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cardtype_card_remove_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string", cardId = "string" };

            var response = AllureApi.Step("Execute POST /divide/{cardType}/card/remove for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/{GetTestValue("integer")}/card/remove")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_cardtype_card_removeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_cardType_card_remove(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cancel_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_cancelRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/cancel", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/cancel")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_cancelResponse", () =>
            {
                AttachResponse("post_divide_cancelResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_cancel response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cancel_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/cancel without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/cancel")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cancel_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/cancel with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/cancel")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_cancel_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /divide/cancel for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/cancel")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_cancelSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_cancel(rawJson).Wait();
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_commit_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            AllureApi.Step("Attach Request Body", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("post_divide_commitRequest", requestBodyJson);
            });

            var response = AllureApi.Step("Execute POST /divide/commit", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/commit")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_divide_commitResponse", () =>
            {
                AttachResponse("post_divide_commitResponse", rawJson);
            });

            AllureApi.Step("Assert post_divide_commit response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_commit_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/commit without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/commit")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_commit_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /divide/commit with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/divide/commit")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Divide")]
        public void Divide_API_post_divide_commit_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Divide API Feature");
            var baseUrl = GetBaseUrl();

            var requestBody = new { proformaId = "string" };

            var response = AllureApi.Step("Execute POST /divide/commit for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .Body(requestBody)
                    .When()
                    .Post($"{baseUrl}/divide/commit")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_divide_commitSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_divide_commit(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_divide_fees_init(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
          ""type"": ""array""
        },
        ""listOptions"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_fees_card_field(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
          ""type"": ""object""
        },
        ""card"": {
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

        private async Task ValidateResponseSchema_POST_divide_fees_card_add(string jsonResponse)
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
        ""cardId"": {
          ""type"": ""string""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""workAmount"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""targetProformaNumber"": {
          ""type"": ""object""
        },
        ""isInclude"": {
          ""type"": ""object""
        },
        ""isNoCharge"": {
          ""type"": ""object""
        },
        ""isNB"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""listOptions"": {
          ""type"": ""object""
        },
        ""billRate"": {
          ""type"": ""object""
        },
        ""billHours"": {
          ""type"": ""object""
        },
        ""workHours"": {
          ""type"": ""object""
        },
        ""workRate"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_fees_field(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
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

        private async Task ValidateResponseSchema_POST_divide_fees_card_recalcRate(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
          ""type"": ""object""
        },
        ""card"": {
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

        private async Task ValidateResponseSchema_POST_divide_costs_init(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
          ""type"": ""array""
        },
        ""listOptions"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_costs_card_field(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
          ""type"": ""object""
        },
        ""card"": {
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

        private async Task ValidateResponseSchema_POST_divide_costs_card_add(string jsonResponse)
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
        ""cardId"": {
          ""type"": ""string""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""workAmount"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""targetProformaNumber"": {
          ""type"": ""object""
        },
        ""isInclude"": {
          ""type"": ""object""
        },
        ""isNoCharge"": {
          ""type"": ""object""
        },
        ""isNB"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""listOptions"": {
          ""type"": ""object""
        },
        ""availableWorkUnits"": {
          ""type"": ""object""
        },
        ""availableBillUnits"": {
          ""type"": ""object""
        },
        ""workUnits"": {
          ""type"": ""object""
        },
        ""billUnits"": {
          ""type"": ""object""
        },
        ""billRate"": {
          ""type"": ""object""
        },
        ""workRate"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_costs_field(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
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

        private async Task ValidateResponseSchema_POST_divide_costs_card_recalcRate(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
          ""type"": ""object""
        },
        ""card"": {
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

        private async Task ValidateResponseSchema_POST_divide_charges_init(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
          ""type"": ""array""
        },
        ""listOptions"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_charges_card_field(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
          ""type"": ""object""
        },
        ""card"": {
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

        private async Task ValidateResponseSchema_POST_divide_charges_card_add(string jsonResponse)
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
        ""cardId"": {
          ""type"": ""string""
        },
        ""codes"": {
          ""type"": ""object""
        },
        ""narrative"": {
          ""type"": ""object""
        },
        ""workAmount"": {
          ""type"": ""object""
        },
        ""billAmount"": {
          ""type"": ""object""
        },
        ""matter"": {
          ""type"": ""object""
        },
        ""targetProformaNumber"": {
          ""type"": ""object""
        },
        ""isInclude"": {
          ""type"": ""object""
        },
        ""isNoCharge"": {
          ""type"": ""object""
        },
        ""isNB"": {
          ""type"": ""object""
        },
        ""comments"": {
          ""type"": ""object""
        },
        ""listOptions"": {
          ""type"": ""object""
        },
        ""availableWorkAmount"": {
          ""type"": ""object""
        },
        ""availableBillAmount"": {
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

        private async Task ValidateResponseSchema_PATCH_divide_charges_field(string jsonResponse)
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
        ""popupInstanceId"": {
          ""type"": ""string""
        },
        ""divideRowId"": {
          ""type"": ""string""
        },
        ""piid"": {
          ""type"": ""string""
        },
        ""reasonCode"": {
          ""type"": ""object""
        },
        ""isAdjustRate"": {
          ""type"": ""object""
        },
        ""timekeeperName"": {
          ""type"": ""object""
        },
        ""timekeeperNumber"": {
          ""type"": ""object""
        },
        ""isPurge"": {
          ""type"": ""object""
        },
        ""purgeType"": {
          ""type"": ""object""
        },
        ""originalCard"": {
          ""type"": ""object""
        },
        ""newCardList"": {
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

        private async Task ValidateResponseSchema_POST_divide_card_matter(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_divide_cardType_card_remove(string jsonResponse)
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
        ""availableWorkValue"": {
          ""type"": ""object""
        },
        ""availableBillValue"": {
          ""type"": ""object""
        },
        ""availableAmount"": {
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

        private async Task ValidateResponseSchema_POST_divide_cancel(string jsonResponse)
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

        private async Task ValidateResponseSchema_POST_divide_commit(string jsonResponse)
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

        #endregion

    }
}
