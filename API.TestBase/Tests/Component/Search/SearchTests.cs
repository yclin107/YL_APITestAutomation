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

namespace API.TestBase.Tests.Generated.Search
{
    [TestFixture]
    [AllureFeature("Search API Tests")]
    public class SearchTests : TestBase
    {

        [Test]
        [Category("Search")]
        public void Search_API_get_search_offices_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/offices", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/offices")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_officesResponse", () =>
            {
                AttachResponse("get_search_officesResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_offices response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_offices_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/offices without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/offices")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_offices_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/offices with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/offices")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_offices_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/offices for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/offices")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_officesSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_offices(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_jobpositions_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/jobPositions", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/jobPositions")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_jobpositionsResponse", () =>
            {
                AttachResponse("get_search_jobpositionsResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_jobpositions response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_jobpositions_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/jobPositions without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/jobPositions")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_jobpositions_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/jobPositions with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/jobPositions")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_jobpositions_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/jobPositions for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/jobPositions")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_jobpositionsSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_jobPositions(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/timekeepers")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_timekeepersResponse", () =>
            {
                AttachResponse("get_search_timekeepersResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_timekeepers response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/timekeepers")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/timekeepers")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/timekeepers")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_timekeepersSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_timekeepers(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_info_timekeeperindex_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers/info/{timekeeperIndex}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/timekeepers/info/{GetTestValue("integer")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_timekeepers_info_timekeeperindexResponse", () =>
            {
                AttachResponse("get_search_timekeepers_info_timekeeperindexResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_timekeepers_info_timekeeperindex response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_info_timekeeperindex_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers/info/{timekeeperIndex} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/timekeepers/info/{GetTestValue("integer")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_info_timekeeperindex_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers/info/{timekeeperIndex} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/timekeepers/info/{GetTestValue("integer")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_timekeepers_info_timekeeperindex_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/timekeepers/info/{timekeeperIndex} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/timekeepers/info/{GetTestValue("integer")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_timekeepers_info_timekeeperindexSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_timekeepers_info_timekeeperIndex(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_workingastimekeepers_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/workingAsTimekeepers", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/workingAsTimekeepers")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_workingastimekeepersResponse", () =>
            {
                AttachResponse("get_search_workingastimekeepersResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_workingastimekeepers response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_workingastimekeepers_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/workingAsTimekeepers without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/workingAsTimekeepers")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_workingastimekeepers_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/workingAsTimekeepers with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/workingAsTimekeepers")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_workingastimekeepers_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/workingAsTimekeepers for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/workingAsTimekeepers")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_workingastimekeepersSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_workingAsTimekeepers(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_collaborators_proformaid_proformaid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/collaborators", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/collaborators")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_collaborators_proformaid_proformaidResponse", () =>
            {
                AttachResponse("get_search_collaborators_proformaid_proformaidResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_collaborators_proformaid_proformaid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_collaborators_proformaid_proformaid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/collaborators without authorization", () =>
            {
                return Given()
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/collaborators")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_collaborators_proformaid_proformaid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/collaborators with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/collaborators")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_collaborators_proformaid_proformaid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/collaborators for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("proformaId", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/collaborators")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_collaborators_proformaid_proformaidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_collaborators(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_lookup_lookuptype_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/lookup/{lookupType}", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/lookup/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_lookup_lookuptypeResponse", () =>
            {
                AttachResponse("get_search_lookup_lookuptypeResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_lookup_lookuptype response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_lookup_lookuptype_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/lookup/{lookupType} without authorization", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/lookup/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_lookup_lookuptype_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/lookup/{lookupType} with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/lookup/{GetTestValue("string")}")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_lookup_lookuptype_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/lookup/{lookupType} for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/lookup/{GetTestValue("string")}")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_lookup_lookuptypeSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_lookup_lookupType(rawJson).Wait();
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_authorizedtimekeepers_piid_piid_PositiveTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/authorizedTimekeepers", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/authorizedTimekeepers")
                    .Then()
                    .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach get_search_authorizedtimekeepers_piid_piidResponse", () =>
            {
                AttachResponse("get_search_authorizedtimekeepers_piid_piidResponse", rawJson);
            });

            AllureApi.Step("Assert get_search_authorizedtimekeepers_piid_piid response is successful", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(response.Extract().Response().IsSuccessStatusCode, Is.True, "Request should be successful");
                    Assert.That(string.IsNullOrEmpty(rawJson), Is.False, "Response should not be empty");
                });
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_authorizedtimekeepers_piid_piid_UnauthorizedTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/authorizedTimekeepers without authorization", () =>
            {
                return Given()
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/authorizedTimekeepers")
                    .Then()
                    .StatusCode(401);
            });

            AllureApi.Step("Assert unauthorized response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_authorizedtimekeepers_piid_piid_MissingRequiredParametersTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/authorizedTimekeepers with missing required parameters", () =>
            {
                return Given()
                    .When()
                    .Get($"{baseUrl}/search/authorizedTimekeepers")
                    .Then()
                    .StatusCode(400);
            });

            AllureApi.Step("Assert bad request response", () =>
            {
                Assert.That(response.Extract().Response().StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            });
        }

        [Test]
        [Category("Search")]
        public void Search_API_get_search_authorizedtimekeepers_piid_piid_SchemaValidationTest()
        {
            var context = GetTestContext();
            InitContext(context.TenantId, context.UserId, "Search API Feature");
            var baseUrl = GetBaseUrl();

            var response = AllureApi.Step("Execute GET /search/authorizedTimekeepers for schema validation", () =>
            {
                return Given()
                    .Header("X-3E-InstanceId", context.InstanceId)
                    .Header("x-pps-timekeeperindex", GetTestValue("string"))
                    .Header("x-pps-sessionid", GetTestValue("string"))
                    .Header("x-pps-overridenfeatureflags", GetTestValue("string"))
                    .QueryParam("piid", GetTestValue("string"))
                    .When()
                    .Get($"{baseUrl}/search/authorizedTimekeepers")
                    .Then();
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Get & Attach Schema Validation Response", () =>
            {
                AttachResponse("get_search_authorizedtimekeepers_piid_piidSchemaValidationResponse", rawJson);
                AllureApi.AddAttachment("Response JSON", "application/json", Encoding.UTF8.GetBytes(rawJson));
            });

            AllureApi.Step("Validate response schema", () =>
            {
               ValidateResponseSchema_GET_search_authorizedTimekeepers(rawJson).Wait();
            });
        }

        #region Schema Validation Methods

        private async Task ValidateResponseSchema_GET_search_offices(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_search_jobPositions(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_search_timekeepers(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""timekeeperName"": {
        ""type"": ""string""
      },
      ""timekeeperIndex"": {
        ""type"": ""integer""
      },
      ""timekeeperNumber"": {
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

        private async Task ValidateResponseSchema_GET_search_timekeepers_info_timekeeperIndex(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""object"",
  ""properties"": {
    ""timekeeperName"": {
      ""type"": ""string""
    },
    ""timekeeperIndex"": {
      ""type"": ""integer"",
      ""format"": ""int32""
    },
    ""timekeeperNumber"": {
      ""type"": ""string""
    },
    ""office"": {
      ""type"": ""string""
    },
    ""jobPosition"": {
      ""type"": ""string""
    },
    ""isInApproverRole"": {
      ""type"": ""boolean""
    },
    ""isInProformaUserRole"": {
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

        private async Task ValidateResponseSchema_GET_search_workingAsTimekeepers(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""timekeeperName"": {
        ""type"": ""string""
      },
      ""timekeeperIndex"": {
        ""type"": ""integer""
      },
      ""timekeeperNumber"": {
        ""type"": ""string""
      },
      ""isDefault"": {
        ""type"": ""boolean""
      },
      ""configurationError"": {
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

        private async Task ValidateResponseSchema_GET_search_collaborators(string jsonResponse)
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
      ""userName"": {
        ""type"": ""string""
      },
      ""userIndex"": {
        ""type"": ""integer""
      },
      ""userNumber"": {
        ""type"": ""string""
      },
      ""office"": {
        ""type"": ""string""
      },
      ""jobPosition"": {
        ""type"": ""string""
      },
      ""isOpenedByTimekeeper"": {
        ""type"": ""boolean""
      },
      ""isCompletedByTimekeeper"": {
        ""type"": ""boolean""
      },
      ""assignmentDate"": {
        ""type"": ""string""
      },
      ""rights"": {
        ""type"": ""string""
      },
      ""permission"": {
        ""type"": ""integer""
      },
      ""isOwner"": {
        ""type"": ""boolean""
      },
      ""isForwarded"": {
        ""type"": ""boolean""
      },
      ""lastActivty"": {
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

        private async Task ValidateResponseSchema_GET_search_lookup_lookupType(string jsonResponse)
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

        private async Task ValidateResponseSchema_GET_search_authorizedTimekeepers(string jsonResponse)
        {
            AllureApi.AddAttachment("Actual Response", "application/json", Encoding.UTF8.GetBytes(jsonResponse));

            try
            {
                var schemaJson = @"{
  ""type"": ""array"",
  ""items"": {
    ""type"": ""object"",
    ""properties"": {
      ""timekeeperName"": {
        ""type"": ""string""
      },
      ""timekeeperIndex"": {
        ""type"": ""integer""
      },
      ""timekeeperNumber"": {
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
