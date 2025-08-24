@@ .. @@
     public ProfileManager()
     {
-        // Point to the test project's Profiles folder
-        var currentDir = AppContext.BaseDirectory;
-        var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
-        _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Profiles");
+        // Point to Config/Profiles folder in same project
+        var currentDir = AppContext.BaseDirectory;
+        var projectRoot = Path.Combine(currentDir, "..", "..", "..");
+        _profilesPath = Path.Combine(projectRoot, "Config", "Profiles");
         _profilesPath = Path.GetFullPath(_profilesPath);
     }