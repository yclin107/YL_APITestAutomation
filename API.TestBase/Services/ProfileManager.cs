@@ .. @@
     public ProfileManager()
     {
        // Point to the test project's Config/Profiles folder
        var currentDir = AppContext.BaseDirectory;
        var solutionRoot = Path.Combine(currentDir, "..", "..", "..", "..");
        _profilesPath = Path.Combine(solutionRoot, "API.TestBase", "Config", "Profiles");
        _profilesPath = Path.GetFullPath(_profilesPath);
    }

        public async Task<List<string>> GetAvailableProfilesAsync()
        {
            var profiles = new List<string>();
            
            if (!Directory.Exists(_profilesPath))
                return profiles;

            // Check both Source and Encrypted folders
            var sourcePath = Path.Combine(_profilesPath, "Source");
            var encryptedPath = Path.Combine(_profilesPath, "Encrypted");
            
            var pathsToCheck = new List<string>();
            if (Directory.Exists(sourcePath)) pathsToCheck.Add(sourcePath);
            if (Directory.Exists(encryptedPath)) pathsToCheck.Add(encryptedPath);
            
            foreach (var basePath in pathsToCheck)
            {
                foreach (var teamDir in Directory.GetDirectories(basePath))
                {
                    var teamName = Path.GetFileName(teamDir);
                    foreach (var envDir in Directory.GetDirectories(teamDir))
                    {
                        var envName = Path.GetFileName(envDir);
                        foreach (var file in Directory.GetFiles(envDir, "*.json"))
                        {
                            var tenantId = Path.GetFileNameWithoutExtension(file);
                            var profileKey = $"{teamName}/{envName}/{tenantId}";
                            if (!profiles.Contains(profileKey))
                            {
                                profiles.Add(profileKey);
                            }
                        }
                    }
                }
            }

            return profiles;
        }