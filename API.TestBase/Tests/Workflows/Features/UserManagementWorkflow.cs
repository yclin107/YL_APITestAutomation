using Allure.NUnit.Attributes;
using API.TestBase.Tests.Workflows.StepDefinitions;

namespace API.TestBase.Tests.Workflows.Features
{
    //[TestFixture]
    [Ignore("Skip BDD workflow tests")]
    [AllureFeature("User Management Workflow")]
    public class UserManagementWorkflow : TestBase
    {
        private ApiStepDefinitions _steps;

        [SetUp]
        public void Setup()
        {
            _steps = new ApiStepDefinitions();
        }

        //[Test]
        [Category("BDD")]
        public void CompleteUserManagementWorkflow()
        {
            var context = GetTestContext();
            // Initialize context
            _steps.InitializeContext(context.TenantId, context.InstanceId, context.UserId, GetBaseUrl());
            
            // Authenticate
            _steps.AuthenticateUser();

            // Step 1: Get all users
            _steps.ExecuteGetRequest("users", new Dictionary<string, object>
            {
                { "page", 1 },
                { "limit", 10 }
            });
            _steps.ValidateStatusCode(200);
            _steps.ValidateResponseNotEmpty();

            // Step 2: Create a new user
            _steps.PrepareRequestBody(new
            {
                name = "Test User",
                email = "test@example.com"
            });
            _steps.ExecutePostRequest("users");
            _steps.ValidateStatusCode(201);
            
            // Extract user ID from response for next steps
            var createdUser = _steps.DeserializeResponse<dynamic>();
            _steps.ValidateResponseProperty("name", "Test User");

            // Step 3: Get the created user by ID
            // Note: In a real scenario, you'd extract the ID from the previous response
            _steps.ExecuteGetRequest("users/{id}", pathParams: new Dictionary<string, string>
            {
                { "id", "test-user-id" }
            });
            _steps.ValidateStatusCode(200);
            _steps.ValidateResponseProperty("email", "test@example.com");

            _steps.ClearContext();
        }

        //[Test]
        [Category("BDD")]
        public void UserAuthorizationWorkflow()
        {
            var context = GetTestContext();
            // Initialize context without authentication
            _steps.InitializeContext(context.TenantId, context.InstanceId, context.UserId, GetBaseUrl());
            
            // Try to access protected endpoint without auth
            _steps.ExecuteGetRequest("users");
            _steps.ValidateStatusCode(401);

            // Now authenticate and try again
            _steps.AuthenticateUser();
            _steps.ExecuteGetRequest("users");
            _steps.ValidateStatusCode(200);
            _steps.ValidateResponseNotEmpty();

            _steps.ClearContext();
        }
    }
}