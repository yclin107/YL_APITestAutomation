using Allure.Net.Commons;
using API.Core.Helpers;
using System.Text.Json;
using static RestAssured.Dsl;

namespace API.TestBase.Tests.Workflows.StepDefinitions
{
    public class ApiStepDefinitions : TestBase
    {
        private string _baseUrl = string.Empty;
        private string _token = string.Empty;
        private string _tenant = string.Empty;
        private string _userId = string.Empty;
        private object? _requestBody;
        private object? _response;
        private string _rawResponse = string.Empty;
        private RestAssured.Response.VerifiableResponse? _validatableResponse;

        public void InitializeContext(string tenant, string userId, string baseUrl)
        {
            _tenant = tenant;
            _userId = userId;
            _baseUrl = baseUrl;
            InitContext(tenant, userId, "BDD API Feature");
        }

        public void AuthenticateUser()
        {
            AllureApi.Step("Authenticate user", async () =>
            {
                // Get profile from environment
                var profileManager = new API.Core.Helpers.ProfileManager();
                var profilePath = Environment.GetEnvironmentVariable("TEST_PROFILE");
                var masterPassword = Environment.GetEnvironmentVariable("MASTER_PASSWORD");
                
                if (string.IsNullOrEmpty(profilePath))
                    throw new InvalidOperationException("TEST_PROFILE environment variable not set");
                    
                var parts = profilePath.Split('/');
                if (parts.Length != 3)
                    throw new InvalidOperationException($"Invalid profile path format: {profilePath}");
                    
                var profile = await profileManager.LoadProfileAsync(parts[0], parts[1], parts[2], masterPassword);
                if (profile == null)
                    throw new InvalidOperationException($"Profile not found: {profilePath}");
                
                var user = profileManager.GetRandomUser(profile);
                var userConfig = new API.Core.Helpers.UserConfig
                {
                    LoginId = user.LoginId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    DefaultTimekeeperIndex = user.DefaultTimekeeperIndex,
                    DefaultTimekeeperNumber = user.DefaultTimekeeperNumber
                };
                
                _token = await API.Core.Helpers.TokenService.PPSProformaToken(_tenant, userConfig);
            });
        }

        public void PrepareRequestBody(object requestBody)
        {
            AllureApi.Step("Prepare request body", () =>
            {
                _requestBody = requestBody;
                string requestBodyJson = serializeToJson(requestBody);
                AttachResponse("RequestBody", requestBodyJson);
            });
        }

        public void ExecuteGetRequest(string endpoint, Dictionary<string, object>? queryParams = null, Dictionary<string, string>? pathParams = null)
        {
            AllureApi.Step($"Execute GET {endpoint}", () =>
            {
                var request = Given()
                    .OAuth2(_token)
                    .Header("x-3e-tenantid", _tenant)
                    .Header("X-3E-InstanceId", _tenant);

                // Add query parameters
                if (queryParams != null)
                {
                    foreach (var param in queryParams)
                    {
                        request = request.QueryParam(param.Key, param.Value);
                    }
                }

                // Replace path parameters
                var finalEndpoint = endpoint;
                if (pathParams != null)
                {
                    foreach (var param in pathParams)
                    {
                        finalEndpoint = finalEndpoint.Replace($"{{{param.Key}}}", param.Value);
                    }
                }

                _validatableResponse = request
                    .When()
                    .Get($"{_baseUrl}/{finalEndpoint}")
                    .Then();

                _rawResponse = _validatableResponse.Extract().Response().Content.ReadAsStringAsync().Result;

                AttachResponse("GetResponse", _rawResponse);
            });
        }

        public void ExecutePostRequest(string endpoint, Dictionary<string, string>? pathParams = null)
        {
            AllureApi.Step($"Execute POST {endpoint}", () =>
            {
                var request = Given()
                    .OAuth2(_token)
                    .Header("x-3e-tenantid", _tenant)
                    .Header("X-3E-InstanceId", _tenant);

                if (_requestBody != null)
                {
                    request = request.Body(_requestBody);
                }

                // Replace path parameters
                var finalEndpoint = endpoint;
                if (pathParams != null)
                {
                    foreach (var param in pathParams)
                    {
                        finalEndpoint = finalEndpoint.Replace($"{{{param.Key}}}", param.Value);
                    }
                }

                _validatableResponse = request
                    .When()
                    .Post($"{_baseUrl}/{finalEndpoint}")
                    .Then();

                _rawResponse = _validatableResponse.Extract().Response().Content.ReadAsStringAsync().Result;

                AttachResponse("PostResponse", _rawResponse);
            });
        }

        public void ExecutePutRequest(string endpoint, Dictionary<string, string>? pathParams = null)
        {
            AllureApi.Step($"Execute PUT {endpoint}", () =>
            {
                var request = Given()
                    .OAuth2(_token)
                    .Header("x-3e-tenantid", _tenant)
                    .Header("X-3E-InstanceId", _tenant);

                if (_requestBody != null)
                {
                    request = request.Body(_requestBody);
                }

                // Replace path parameters
                var finalEndpoint = endpoint;
                if (pathParams != null)
                {
                    foreach (var param in pathParams)
                    {
                        finalEndpoint = finalEndpoint.Replace($"{{{param.Key}}}", param.Value);
                    }
                }

                _validatableResponse = request
                    .When()
                    .Put($"{_baseUrl}/{finalEndpoint}")
                    .Then();

                _rawResponse = _validatableResponse.Extract().Response().Content.ReadAsStringAsync().Result;

                AttachResponse("PutResponse", _rawResponse);
            });
        }

        public void ExecuteDeleteRequest(string endpoint, Dictionary<string, string>? pathParams = null)
        {
            AllureApi.Step($"Execute DELETE {endpoint}", () =>
            {
                var request = Given()
                    .OAuth2(_token)
                    .Header("x-3e-tenantid", _tenant)
                    .Header("X-3E-InstanceId", _tenant);

                // Replace path parameters
                var finalEndpoint = endpoint;
                if (pathParams != null)
                {
                    foreach (var param in pathParams)
                    {
                        finalEndpoint = finalEndpoint.Replace($"{{{param.Key}}}", param.Value);
                    }
                }

                _validatableResponse = request
                    .When()
                    .Delete($"{_baseUrl}/{finalEndpoint}")
                    .Then();

                _rawResponse = _validatableResponse.Extract().Response().Content.ReadAsStringAsync().Result;

                AttachResponse("DeleteResponse", _rawResponse);
            });
        }

        public void ValidateStatusCode(int expectedStatusCode)
        {
            AllureApi.Step($"Validate status code is {expectedStatusCode}", () =>
            {
                _validatableResponse!.StatusCode(expectedStatusCode);
            });
        }

        public void ValidateResponseNotEmpty()
        {
            AllureApi.Step("Validate response is not empty", () =>
            {
                Assert.That(string.IsNullOrEmpty(_rawResponse), Is.False, "Response should not be empty");
            });
        }

        public void ValidateResponseContains(string expectedValue)
        {
            AllureApi.Step($"Validate response contains '{expectedValue}'", () =>
            {
                Assert.That(_rawResponse, Does.Contain(expectedValue), $"Response should contain '{expectedValue}'");
            });
        }

        public T DeserializeResponse<T>()
        {
            return AllureApi.Step($"Deserialize response to {typeof(T).Name}", () =>
            {
                return JsonSerializer.Deserialize<T>(_rawResponse, CachedJsonSerializerOptions)!;
            });
        }

        public void ValidateResponseProperty<T>(string propertyName, T expectedValue)
        {
            AllureApi.Step($"Validate {propertyName} equals {expectedValue}", () =>
            {
                var jsonDoc = JsonDocument.Parse(_rawResponse);
                var property = jsonDoc.RootElement.GetProperty(propertyName);
                
                T actualValue;
                if (typeof(T) == typeof(string))
                {
                    actualValue = (T)(object)property.GetString()!;
                }
                else if (typeof(T) == typeof(int))
                {
                    actualValue = (T)(object)property.GetInt32();
                }
                else if (typeof(T) == typeof(bool))
                {
                    actualValue = (T)(object)property.GetBoolean();
                }
                else
                {
                    actualValue = JsonSerializer.Deserialize<T>(property.GetRawText())!;
                }

                Assert.That(actualValue, Is.EqualTo(expectedValue), $"Property {propertyName} should equal {expectedValue}");
            });
        }

        public void ValidateArrayNotEmpty(string arrayPropertyName)
        {
            AllureApi.Step($"Validate {arrayPropertyName} array is not empty", () =>
            {
                var jsonDoc = JsonDocument.Parse(_rawResponse);
                var arrayProperty = jsonDoc.RootElement.GetProperty(arrayPropertyName);
                Assert.That(arrayProperty.GetArrayLength(), Is.GreaterThan(0), $"{arrayPropertyName} should not be empty");
            });
        }

        public void ClearContext()
        {
            _requestBody = null;
            _validatableResponse = null;
            _rawResponse = string.Empty;
        }
    }
}