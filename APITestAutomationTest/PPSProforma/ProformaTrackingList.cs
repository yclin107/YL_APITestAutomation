using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using APITestAutomation.Endpoints;
using APITestAutomation.Helpers;
using APITestAutomation.Models.PPSProformaModels.Entities;
using APITestAutomation.Models.PPSProformaModels.Entities.Filters;
using APITestAutomation.Models.PPSProformaModels.Entities.Proformas;
using APITestAutomation.Models.PPSProformaModels.PPSProformaConstants;
using APITestAutomation.Models.ProformaModels;
using System.Text.Json;
using static RestAssured.Dsl;

namespace APITestAutomationTest.PPSProforma
{
    [TestFixture]
    [AllureFeature("Proforma Tracking List")]
    public class ProformaTrackingList : TestBase
    {
        [Test]
        [Category("Smoke")]
        [TestCase("ptpd68r3nke7q5pnutzaaw", "PPSAutoTestUser2")]
        public void PPS_API_ProformaTrackingList_MyProformas_FilterBy_BillingTimekeeperNumber(string tenant, string userId)
        {
            InitContext(tenant, userId , "Proforma Tracking List Feature");
            var user = ConfigSetup.GetUser(tenant, userId);
            var ppsToken = APITestAutomationServices.Authentications.TokenService.PPSProformaToken(tenant, user);         
            var baseUrl = ConfigSetup.GetTenantConfig(tenant).ProformaApiUrl;

            var requestBody = new TrackingListFilter
            {
                IsNotStarted = true,
                IsIncomplete = true,
                IsArchived = true,
                BillingTimekeeperNumber = user.DefaultTimekeeperNumber
            };

            AllureApi.Step("Generate & Attach Proforma Tracking List By Timekeeper Number Request", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("ProformaTrackingListByBillingTimekeeperNumber", requestBodyJson);
            });

            var response = AllureApi.Step("Get Proforma Tracking List filtered by Billing Timekeeper Number", () =>
            {
                return Given()
                .OAuth2(ppsToken)
                .Header("x-3e-tenantid", tenant)
                .Header("X-3E-InstanceId", tenant)
                .QueryParam("orderby", "Client")
                .QueryParam("ascending", true)
                .QueryParam("page", "1")
                .QueryParam("count", "100")
                .QueryParam("searchText", "")
                .Body(requestBody)
                .When()
                .Post($"{baseUrl}/{PPSProformaEndpoints.ProformaTrackingList}")
                .Then()
                .StatusCode(200);
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach ProformaTrackingListResponse", () =>
            {
                AttachResponse("ProformaTrackingListResponse", rawJson);
            });


            var proformaTrackingListResponse = JsonSerializer.Deserialize<ProformaTrackingListResponse>(rawJson, CachedJsonSerializerOptions);


            AllureApi.Step("Assert proformaTrackingListResponse is not null", () =>{
               
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse, Is.Not.Null, "ProformaTrackingList is null");
                    Assert.That(proformaTrackingListResponse?.Summary, Is.Not.Null, "ProformaTrackingList Summary response is null");
                    Assert.That(proformaTrackingListResponse?.ListResponse, Is.Not.Null, "ProformaTrackingList ListResponse is null");
                    Assert.That(proformaTrackingListResponse?.ListFilter, Is.Not.Null, "ProformaTrackingList List Filter is null");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse is not empty", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse?.Summary.ToString(), Is.Not.Empty, "Summary is empty");
                    Assert.That(proformaTrackingListResponse?.ListResponse.ToString(), Is.Not.Empty, "ListResponse is empty");
                    Assert.That(proformaTrackingListResponse?.ListFilter.ToString(), Is.Not.Empty, "ListFilter is empty");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse Summary content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse?.Summary.Completed, Is.GreaterThanOrEqualTo(0), "Completed proformas are 0");
                    Assert.That(proformaTrackingListResponse?.Summary.Incomplete, Is.GreaterThanOrEqualTo(0), "Incomplete proformas are empty");
                    Assert.That(proformaTrackingListResponse?.Summary.NotStarted, Is.GreaterThanOrEqualTo(0), "NotStarted proformas are empty");
                    Assert.That(proformaTrackingListResponse?.Summary.TimekeeperName, Does.Contain(user.FirstName), $"User name doesn't contains {user.FirstName}");
                    Assert.That(proformaTrackingListResponse?.Summary.TimekeeperNumber, Is.EqualTo(user.DefaultTimekeeperNumber), "TimekeeperNumber doesn't match");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse List Response content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse?.ListResponse.TotalRowCount, Is.GreaterThan(0), "TotalRowCount is O");
                    Assert.That(proformaTrackingListResponse?.ListResponse.Proformas.Count, Is.GreaterThan(0), "Proformas list is empty");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse List Filter content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse?.ListFilter.Matters.Count, Is.GreaterThan(0), "Matters list is empty");
                    Assert.That(proformaTrackingListResponse?.ListFilter.MatterNumbers.Count, Is.GreaterThan(0), "Matter numbers list is empty");
                    Assert.That(proformaTrackingListResponse?.ListFilter.Clients.Count, Is.GreaterThan(0), "Clients list is empty");
                    Assert.That(proformaTrackingListResponse?.ListFilter.ClientNumbers.Count, Is.GreaterThan(0), "Clients number list is empty");
                    Assert.That(proformaTrackingListResponse?.ListFilter.Timekeepers.Count, Is.EqualTo(1), "Timekeepers list is  different from response");
                    Assert.That(proformaTrackingListResponse?.ListFilter.TimekeeperNumbers.Count, Is.EqualTo(1), "Timekeepers numbers is different from response");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse is filtered by TimeKeeperNumber", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(proformaTrackingListResponse.ListFilter.TimekeeperNumbers.Contains(user.DefaultTimekeeperNumber), $"Timekeeper number is different than {user.DefaultTimekeeperNumber}");
                    Assert.That(proformaTrackingListResponse.ListResponse.Proformas.All(p => p.BillingTimekeeperNumber == user.DefaultTimekeeperNumber),
                       $"Not all proformas have the expected TimeKeeperNumber {user.DefaultTimekeeperNumber}");
                });
            });
        }

        [Test]
        [Category("Smoke")]
        [TestCase("ptpd68r3nke7q5pnutzaaw", "PPSAutoTestUser2")]
        public void PPS_API_ProformaTrackingList_MyProformas_FilterBy_ClientName(string tenant, string userId)
        {
            InitContext(tenant, userId, "Proforma Tracking List Feature");
            var user = ConfigSetup.GetUser(tenant, userId);
            var ppsToken = APITestAutomationServices.Authentications.TokenService.PPSProformaToken(tenant, user);
            var baseUrl = ConfigSetup.GetTenantConfig(tenant).ProformaApiUrl;
            var endpoint = PPSProformaEndpoints.ProformaBucket.Replace("{bucketName}", ProformaBucket.My.ToString());

            var  ppsProformaBucketReponse = AllureApi.Step("Get Available Proformas in a Bucket", () =>
            {
                return Given()
                .OAuth2(ppsToken)
                .Header("x-3e-tenantid", tenant)
                .Header("X-3E-InstanceId", tenant)
                .QueryParam("orderby", "Client")
                .QueryParam("ascending", true)
                .QueryParam("page", "1")
                .QueryParam("count", "100")
                .QueryParam("searchText", "")
                .Body(new ProformaFilter { })
                .When()
                .Post($"{baseUrl}/{endpoint}")
                .Then()
                .StatusCode(200);
            });

            var ppsProformaBucketReponseJson = ppsProformaBucketReponse.Extract().Response().Content.ReadAsStringAsync().Result;

            AllureApi.Step("Generate & Attach ProformaBucketReponse", () =>
            {
                AttachResponse("ProformaBucketReponse", ppsProformaBucketReponseJson);
            });

            var ProformaBucketReponse = JsonSerializer.Deserialize<ProformaBucketDetails>(ppsProformaBucketReponseJson, CachedJsonSerializerOptions);

            ProformaListItem randomProformaListItem = RandomGenerator.GetRandomFromList(ProformaBucketReponse.ListResponse.Data.ToList());

            var requestBody = new TrackingListFilter
            {
                IsNotStarted = true,
                IsIncomplete = true,
                IsArchived = true,
                ClientName = randomProformaListItem.Client
            };

            AllureApi.Step("Generate & Attach Proforma Tracking List By Client Name Request", () =>
            {
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("ProformaTrackingListByClientName", requestBodyJson);
            });

            var response = AllureApi.Step("Get Proforma Tracking List filtered by Client Name", () =>
            {
                return Given()
                .OAuth2(ppsToken)
                .Header("x-3e-tenantid", tenant)
                .Header("X-3E-InstanceId", tenant)
                .QueryParam("orderby", "Client")
                .QueryParam("ascending", true)
                .QueryParam("page", "1")
                .QueryParam("count", "100")
                .QueryParam("searchText", "")
                .Body(requestBody)
                .When()
                .Post($"{baseUrl}/{PPSProformaEndpoints.ProformaTrackingList}")
                .Then()
                .StatusCode(200);
                
            });

            var rawJson = response.Extract().Response().Content.ReadAsStringAsync().Result;
            var statusCode = response.Extract().Response().StatusCode;

            AllureApi.Step("Get & Attach ProformaTrackingListResponse", () =>
            {
                AttachResponse("ProformaTrackingListResponse", rawJson);
            });

            var ppProformaTrackingListResponse = JsonSerializer.Deserialize<ProformaTrackingListResponse>(rawJson, CachedJsonSerializerOptions);

            AllureApi.Step("Assert proformaTrackingListResponse is not null", () =>{
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse, Is.Not.Null, "ProformaTrackingList is null");
                    Assert.That(ppProformaTrackingListResponse?.Summary, Is.Not.Null, "ProformaTrackingList Summary response is null");
                    Assert.That(ppProformaTrackingListResponse?.ListResponse, Is.Not.Null, "ProformaTrackingList ListResponse is null");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter, Is.Not.Null, "ProformaTrackingList List Filter is null");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse is not empty", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse?.Summary.ToString(), Is.Not.Empty, "Summary is empty");
                    Assert.That(ppProformaTrackingListResponse?.ListResponse.ToString(), Is.Not.Empty, "ListResponse is empty");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.ToString(), Is.Not.Empty, "ListFilter is empty");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse Summary content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse?.Summary.Completed, Is.GreaterThanOrEqualTo(0), "Completed proformas are 0");
                    Assert.That(ppProformaTrackingListResponse?.Summary.Incomplete, Is.GreaterThanOrEqualTo(0), "Incomplete proformas are empty");
                    Assert.That(ppProformaTrackingListResponse?.Summary.NotStarted, Is.GreaterThanOrEqualTo(0), "NotStarted proformas are empty");
                    Assert.That(ppProformaTrackingListResponse?.Summary.ClientName, Does.Contain(randomProformaListItem.Client), $"Client name doesn't contains {randomProformaListItem.Client}");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse List Response content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse?.ListResponse.TotalRowCount, Is.GreaterThan(0), "TotalRowCount is O");
                    Assert.That(ppProformaTrackingListResponse?.ListResponse.Proformas.Count, Is.GreaterThan(0), "Proformas list is empty");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse List Filter content", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.Matters, Is.Not.Empty, "Matters list is empty");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.MatterNumbers.Count, Is.GreaterThan(0), "Matter numbers list is empty");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.Clients.Count, Is.EqualTo(1), "Clients is different than single client filter");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.ClientNumbers.Count, Is.EqualTo(1), "Clients is different than single client filter");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.Timekeepers.Count, Is.GreaterThan(0), "Timekeepers list is empty");
                    Assert.That(ppProformaTrackingListResponse?.ListFilter.TimekeeperNumbers.Count, Is.GreaterThan(0), "Timekeepers numbers list is empty");
                });
            });

            AllureApi.Step("Assert proformaTrackingListResponse is filtered by TimeKeeperNumber", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(ppProformaTrackingListResponse.ListFilter.Clients.Contains(randomProformaListItem.Client), $"Client name doesn't contains {randomProformaListItem.Client}");
                    Assert.That(ppProformaTrackingListResponse.ListResponse.Proformas.All(p => p.ClientName == randomProformaListItem.Client),
                       $"Not all proformas have the expected ClientName: {randomProformaListItem.Client}");
                });
            });
        }
    }
}
