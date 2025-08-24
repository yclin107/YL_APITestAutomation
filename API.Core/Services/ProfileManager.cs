public async Task EncryptAllProfilesAsync(string masterPassword)
        {
            var sourcePath = Path.Combine(_profilesPath, "Source");
            if (!Directory.Exists(sourcePath))
                return;
            
            foreach (var teamDir in Directory.GetDirectories(sourcePath))
            {
                var teamName = Path.GetFileName(teamDir);
                foreach (var envDir in Directory.GetDirectories(teamDir))
                {
                    var envName = Path.GetFileName(envDir);
                    foreach (var file in Directory.GetFiles(envDir, "*.json"))
                    {
                        var tenantId = Path.GetFileNameWithoutExtension(file);
                        
                        // Read directly from source file to preserve exact JSON structure
                        var content = await File.ReadAllTextAsync(file);
                        var profile = JsonSerializer.Deserialize<TenantProfile>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                        
                        if (profile != null)
                        {
                            await SaveProfileAsync(profile, teamName, envName, tenantId, masterPassword);
                            Console.WriteLine($"✅ Encrypted profile: {teamName}/{envName}/{tenantId}");
                        }
                    }
                }
            }
        }

    public async Task DecryptAllProfilesAsync(string masterPassword)
        {
            var encryptedPath = Path.Combine(_profilesPath, "Encrypted");
            if (!Directory.Exists(encryptedPath))
                return;
            
            foreach (var teamDir in Directory.GetDirectories(encryptedPath))
            {
                var teamName = Path.GetFileName(teamDir);
                foreach (var envDir in Directory.GetDirectories(teamDir))
                {
                    var envName = Path.GetFileName(envDir);
                    foreach (var file in Directory.GetFiles(envDir, "*.json"))
                    {
                        var tenantId = Path.GetFileNameWithoutExtension(file);
                        try
                        {
                            var profile = await LoadProfileAsync(teamName, envName, tenantId, masterPassword);
                            if (profile != null)
                            {
                                // Save to Source folder without encryption
                                await SaveProfileAsync(profile, teamName, envName, tenantId);
                                Console.WriteLine($"✅ Decrypted profile: {teamName}/{envName}/{tenantId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to decrypt profile {teamName}/{envName}/{tenantId}: {ex.Message}");
                        }
                    }
                }
            }
        }