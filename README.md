@@ .. @@
 ## 📁 Project Structure
 
 ```
 APITestAutomation/
 ├── API.Core/                    # Generic utilities and services
 │   ├── Services/
 │   │   ├── OpenAPI/            # OpenAPI test generation
 │   │   └── ProfileManager.cs   # Profile management
 │   ├── Models/                 # Generic models
 │   ├── Helpers/               # Utilities
 │   └── Program.cs             # Interactive menu
 │
 └── API.TestBase/              # Business logic and tests
-    ├── Profiles/              # Test profiles by team/environment
+    ├── Config/
+    │   └── Profiles/          # Test profiles by team/environment
     │   └── 3E-Proforma/
     │       ├── dev/
     │       └── test/
     ├── Tests/
     │   ├── PPSProforma/       # Proforma-specific tests
     │   ├── Features/          # BDD workflow tests
     │   └── StepDefinitions/   # BDD step definitions
     ├── Models/                # Business models
     ├── Endpoints/             # API endpoints
     └── TestBase.cs           # Base test class
 ```