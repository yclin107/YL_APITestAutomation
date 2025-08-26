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

namespace API.TestBase.Tests.Generated.System
{
    [TestFixture]
    [AllureFeature("System API Tests")]
    public class SystemTests : TestBase
    {

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_notificationid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/{notificationId}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_system_notifications_dismiss_notificationidResponse", () =>
            {
                AttachResponse("post_system_notifications_dismiss_notificationidResponse", rawJson);
            });

            AllureApi.Step("Assert post_system_notifications_dismiss_notificationid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_notificationid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/{notificationId} without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_notificationid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/{notificationId} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_notificationid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/{notificationId} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_system_notifications_dismiss_notificationidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_system_notifications_dismiss_notificationId(rawJson).Wait();
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_all_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/all", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/all")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach post_system_notifications_dismiss_allResponse", () =>
            {
                AttachResponse("post_system_notifications_dismiss_allResponse", rawJson);
            });

            AllureApi.Step("Assert post_system_notifications_dismiss_all response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_all_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/all without authorization", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/all")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_all_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/all with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/all")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_post_system_notifications_dismiss_all_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute POST /system/notifications/dismiss/all for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Post($"{baseUrl}/system/notifications/dismiss/all")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("post_system_notifications_dismiss_allSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_POST_system_notifications_dismiss_all(rawJson).Wait();
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_settings_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/settings", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/settings")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_system_settingsResponse", () =>
            {
                AttachResponse("get_system_settingsResponse", rawJson);
            });

            AllureApi.Step("Assert get_system_settings response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_settings_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/settings without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/settings")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_settings_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/settings with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/settings")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_settings_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/settings for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/settings")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_system_settingsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_system_settings(rawJson).Wait();
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_notifications_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/notifications", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/notifications")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_system_notificationsResponse", () =>
            {
                AttachResponse("get_system_notificationsResponse", rawJson);
            });

            AllureApi.Step("Assert get_system_notifications response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_notifications_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/notifications without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/notifications")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_notifications_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/notifications with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/notifications")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_notifications_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/notifications for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/notifications")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_system_notificationsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_system_notifications(rawJson).Wait();
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_featureflags_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/featureFlags", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/featureFlags")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_system_featureflagsResponse", () =>
            {
                AttachResponse("get_system_featureflagsResponse", rawJson);
            });

            AllureApi.Step("Assert get_system_featureflags response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_featureflags_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/featureFlags without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/featureFlags")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_featureflags_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/featureFlags with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/featureFlags")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_featureflags_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/featureFlags for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/featureFlags")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_system_featureflagsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_system_featureFlags(rawJson).Wait();
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_groupsettings_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/groupsettings", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/groupsettings")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_system_groupsettingsResponse", () =>
            {
                AttachResponse("get_system_groupsettingsResponse", rawJson);
            });

            AllureApi.Step("Assert get_system_groupsettings response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_groupsettings_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/groupsettings without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/groupsettings")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_groupsettings_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/groupsettings with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/system/groupsettings")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("System")]
        public void System_API_get_system_groupsettings_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "System API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /system/groupsettings for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/system/groupsettings")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_system_groupsettingsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_system_groupsettings(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_POST_system_notifications_dismiss_notificationId(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /system/notifications/dismiss/{notificationId}""
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

        private async Task ValidateResponseSchema_POST_system_notifications_dismiss_all(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - No content in response"",
  ""_note"": ""Endpoint: POST /system/notifications/dismiss/all""
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

        private async Task ValidateResponseSchema_GET_system_settings(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - Schema validation failed"",
  ""_note"": ""Endpoint: GET /system/settings""
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

        private async Task ValidateResponseSchema_GET_system_notifications(string jsonResponse)
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
      ""proformaNumber"": {
        ""type"": ""string""
      },
      ""message"": {
        ""type"": ""string""
      },
      ""status"": {
        ""type"": ""integer""
      },
      ""timestamp"": {
        ""type"": ""string""
      },
      ""matterNumber"": {
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

        private async Task ValidateResponseSchema_GET_system_featureFlags(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""additionalProperties"": true,
  ""description"": ""Fallback response schema - Schema validation failed"",
  ""_note"": ""Endpoint: GET /system/featureFlags""
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

        private async Task ValidateResponseSchema_GET_system_groupsettings(string jsonResponse)
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
      ""description"": {
        ""type"": ""string""
      },
      ""systemValue"": {
        ""type"": ""string""
      },
      ""firmValue"": {
        ""type"": ""string""
      },
      ""userValue"": {
        ""type"": ""string""
      },
      ""typeOfValueAsString"": {
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

        #endregion

    }
}
