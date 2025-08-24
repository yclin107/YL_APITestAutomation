@@ .. @@
     public ProfileManager()
     {
-        // Point to the test project's Profiles folder
-        var currentDir = AppContext.BaseDirectory;
-        var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
-        _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Profiles");
+        // Point to API.TestBase Config/Profiles folder
+        var currentDir = AppContext.BaseDirectory;
+        var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
+        _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Config", "Profiles");
         _profilesPath = Path.GetFullPath(_profilesPath);
     }