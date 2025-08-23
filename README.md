# APITestAutomation

## OpenAPI Test Generator

This framework now includes an automatic test generator based on OpenAPI (Swagger) specifications.

### Features

- **Automatic import** of OpenAPI JSON/YAML files
- **Automatic test generation** for all endpoints
- **Automatic change detection** when specification is updated
- **Built-in validations**:
  - Expected status codes
  - Schema validation
  - Negative path tests (unauthorized, missing parameters)
  - Response validation
- **BDD Support** with reusable step definitions
- **Interactive CLI menu** for easy operation
- **Manual test protection** - preserves your custom modifications

### Usage

#### Interactive Menu
```bash
npm run menu
# or
dotnet run --project APITestAutomation
```

#### Direct Commands

#### 1. Generate tests from OpenAPI specification

```bash
dotnet run --project APITestAutomation generate swagger.json
```

#### 2. Detect changes in specification

```bash
dotnet run --project APITestAutomation detect swagger.json
```

#### 3. Preview tests that would be generated

```bash
dotnet run --project APITestAutomation preview swagger.json
```

### Workflow

1. **First time**: Run `generate` to create initial tests
2. **Updates**: Run `detect` to see what changes exist
3. **Apply changes**: Run `generate` again, system will ask if you want to apply changes
4. **Report**: Automatically generates a report with all applied changes

### Generated file structure

- `APITestAutomationTest/Generated/` - Auto-generated tests
- `APITestAutomation/Config/OpenAPI/` - Saved configurations and specifications
- `APITestAutomation/Reports/` - Change reports
- `APITestAutomation/Specifications/` - OpenAPI specification files
- `APITestAutomation/Profiles/` - Encrypted tenant profiles (gitignored)

### BDD Support

The framework includes BDD support with reusable step definitions:

- `APITestAutomationTest/StepDefinitions/` - Reusable step definitions
- `APITestAutomationTest/Features/` - BDD feature files
- Easy workflow composition using multiple endpoints

### Example OpenAPI specification

You can find an example in `APITestAutomation/Specifications/sample-openapi.json`

### Auto-generated tests

For each endpoint, the following tests are generated:
- **Positive test**: Verifies endpoint works correctly
- **Authorization test**: Verifies protected endpoints require authentication
- **Required parameters test**: Verifies validation of mandatory parameters
- **Schema validation test**: Verifies response complies with defined schema

### Manual Test Protection

When you modify auto-generated tests manually:
- System detects your changes
- Asks if you want to preserve or overwrite your modifications
- Maintains a backup of your custom changes

### Profile Management

The framework uses encrypted JSON profiles to manage tenant configurations and user credentials:

- **Profiles**: Store tenant info, users, and passwords in JSON files
- **Encryption**: Profiles can be encrypted with a master password for security
- **Parallel Execution**: Multiple users per tenant for parallel test execution
- **No Environment Variables**: Passwords stored in profiles, not environment variables

#### Profile Structure
```json
{
  "tenantId": "your-tenant-id",
  "tenantName": "Development Environment", 
  "baseUrl": "https://api.example.com/v1",
  "users": [
    {
      "userId": "user1",
      "username": "user1@domain.com",
      "password": "password123",
      "firstName": "Test",
      "lastName": "User"
    }
  ]
}
```

#### Running Tests
```bash
# Set profile and run tests
TEST_PROFILE=dev-profile dotnet test

# With master password for encrypted profiles
MASTER_PASSWORD=your-master-password TEST_PROFILE=dev-profile dotnet test
```