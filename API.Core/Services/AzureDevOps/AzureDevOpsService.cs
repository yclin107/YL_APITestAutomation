using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using API.Core.Models.AzureDevOps;
using API.Core.Models;
using System.Text.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using WorkItemTemplate = API.Core.Models.AzureDevOps.WorkItemTemplate;

namespace API.Core.Services.AzureDevOps
{
    public class AzureDevOpsService
    {
        private readonly AzureDevOpsConfig _config;
        private readonly VssConnection _connection;
        private readonly WorkItemTrackingHttpClient _witClient;
        private readonly string _configPath;

        public AzureDevOpsService()
        {
            // Try multiple possible paths for the config file
            var possiblePaths = new[]
            {
                Path.Combine(AppContext.BaseDirectory, "Config", "AzureDevOps", "devops-config.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "Config", "AzureDevOps", "devops-config.json"),
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "API.TestBase", "Config", "AzureDevOps", "devops-config.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "API.TestBase", "Config", "AzureDevOps", "devops-config.json")
            };
            
            _configPath = possiblePaths.FirstOrDefault(File.Exists) ?? possiblePaths[0];
            
            _config = LoadConfiguration();
            
            _connection = new VssConnection(new Uri(_config.OrganizationUrl), new VssBasicCredential(string.Empty, _config.PersonalAccessToken));
            _witClient = _connection.GetClient<WorkItemTrackingHttpClient>();
        }

        public async Task<WorkItemSyncResult> SyncOpenApiSpecAsync(OpenApiTestSpec spec)
        {
            var result = new WorkItemSyncResult();
            
            try
            {
                Console.WriteLine("🔄 Starting Azure DevOps synchronization...");
                Console.WriteLine($"📋 Project: {_config.ProjectName}");
                Console.WriteLine($"🌐 Organization: {_config.OrganizationUrl}");
                Console.WriteLine();

                // Get existing work items
                var existingItems = await GetExistingWorkItemsAsync();
                var existingStories = existingItems.Where(w => w.Fields["System.WorkItemType"].ToString() == _config.StoryTemplate.WorkItemType).ToList();
                var existingTestCases = existingItems.Where(w => w.Fields["System.WorkItemType"].ToString() == _config.TestCaseTemplate.WorkItemType).ToList();

                // Group endpoints by tag (each tag becomes a Story)
                var endpointsByTag = spec.EndpointTests.Values
                    .GroupBy(e => e.Tags.FirstOrDefault() ?? "General")
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Sync Stories and Test Cases
                foreach (var tagGroup in endpointsByTag)
                {
                    var tag = tagGroup.Key;
                    var endpoints = tagGroup.Value;
                    
                    Console.WriteLine($"📁 Processing tag: {tag} ({endpoints.Count} endpoints)");
                    
                    // Create or update Story for this tag
                    var story = await SyncStoryAsync(tag, endpoints, existingStories, result);
                    
                    if (story != null)
                    {
                        // Create or update Test Cases for each endpoint
                        foreach (var endpoint in endpoints)
                        {
                            await SyncTestCasesForEndpointAsync(story, endpoint, existingTestCases, result);
                        }
                    }
                }

                // Delete orphaned work items
                await DeleteOrphanedWorkItemsAsync(spec, existingStories, existingTestCases, result);

                Console.WriteLine();
                Console.WriteLine("✅ Azure DevOps synchronization completed!");
                PrintSyncSummary(result);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Sync failed: {ex.Message}");
                Console.WriteLine($"❌ Sync error: {ex.Message}");
            }

            return result;
        }

        private async Task<WorkItem?> SyncStoryAsync(string tag, List<OpenApiEndpointTest> endpoints, List<WorkItem> existingStories, WorkItemSyncResult result)
        {
            var storyTitle = $"API Endpoints - {tag}";
            var storyDescription = $"Story for {tag} API endpoints containing {endpoints.Count} endpoints:\n\n" +
                                 string.Join("\n", endpoints.Select(e => $"• {e.Method} {e.Path} - {e.Summary}"));

            // Check if story already exists
            var existingStory = existingStories.FirstOrDefault(s => 
                s.Fields["System.Title"].ToString().Contains(tag) && 
                s.Fields["System.Tags"].ToString().Contains("AutoGenerated"));

            if (existingStory != null)
            {
                // Update existing story
                var updateDoc = new JsonPatchDocument();
                updateDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Replace,
                    Path = "/fields/System.Description",
                    Value = storyDescription
                });

                var updatedStory = await _witClient.UpdateWorkItemAsync(updateDoc, existingStory.Id.Value);
                result.UpdatedStories++;
                result.SyncedItems.Add(new SyncedWorkItem
                {
                    Id = updatedStory.Id.Value,
                    Title = storyTitle,
                    Type = "Story",
                    Action = "Updated",
                    Url = updatedStory.Url
                });

                Console.WriteLine($"  📝 Updated Story: {storyTitle} (ID: {updatedStory.Id})");
                return updatedStory;
            }
            else
            {
                // Create new story
                var createDoc = new JsonPatchDocument();
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.WorkItemType",
                    Value = _config.StoryTemplate.WorkItemType
                });
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Title",
                    Value = storyTitle
                });
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = storyDescription
                });
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AreaPath",
                    Value = _config.AreaPath
                });
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.IterationPath",
                    Value = _config.IterationPath
                });
                createDoc.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Tags",
                    Value = "AutoGenerated;API;OpenAPI;" + tag
                });

                // Add custom fields from template
                foreach (var field in _config.StoryTemplate.Fields)
                {
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{field.Key}",
                        Value = field.Value
                    });
                }

                var newStory = await _witClient.CreateWorkItemAsync(createDoc, _config.ProjectName, _config.StoryTemplate.WorkItemType);
                result.CreatedStories++;
                result.SyncedItems.Add(new SyncedWorkItem
                {
                    Id = newStory.Id.Value,
                    Title = storyTitle,
                    Type = "Story",
                    Action = "Created",
                    Url = newStory.Url
                });

                Console.WriteLine($"  ✅ Created Story: {storyTitle} (ID: {newStory.Id})");
                return newStory;
            }
        }

        private async Task SyncTestCasesForEndpointAsync(WorkItem story, OpenApiEndpointTest endpoint, List<WorkItem> existingTestCases, WorkItemSyncResult result)
        {
            var testTypes = new[]
            {
                ("Positive", "Verify successful response for valid request"),
                ("Unauthorized", "Verify 401 response when authentication is missing or invalid"),
                ("Missing Parameters", "Verify 400 response when required parameters are missing"),
                ("Schema Validation", "Verify response matches expected schema")
            };

            foreach (var (testType, description) in testTypes)
            {
                var testCaseTitle = $"{endpoint.Method} {endpoint.Path} - {testType} Test";
                var testCaseDescription = $"{description}\n\nEndpoint: {endpoint.Method} {endpoint.Path}\nSummary: {endpoint.Summary}\nDescription: {endpoint.Description}";

                // Check if test case already exists
                var existingTestCase = existingTestCases.FirstOrDefault(tc =>
                    tc.Fields["System.Title"].ToString() == testCaseTitle &&
                    tc.Fields["System.Tags"].ToString().Contains("AutoGenerated"));

                if (existingTestCase != null)
                {
                    // Update existing test case
                    var updateDoc = new JsonPatchDocument();
                    updateDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Replace,
                        Path = "/fields/System.Description",
                        Value = testCaseDescription
                    });

                    var updatedTestCase = await _witClient.UpdateWorkItemAsync(updateDoc, existingTestCase.Id.Value);
                    result.UpdatedTestCases++;
                    result.SyncedItems.Add(new SyncedWorkItem
                    {
                        Id = updatedTestCase.Id.Value,
                        Title = testCaseTitle,
                        Type = "Test Case",
                        Action = "Updated",
                        Url = updatedTestCase.Url
                    });

                    Console.WriteLine($"    📝 Updated Test Case: {testType}");
                }
                else
                {
                    // Create new test case
                    var createDoc = new JsonPatchDocument();
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.WorkItemType",
                        Value = _config.TestCaseTemplate.WorkItemType
                    });
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Title",
                        Value = testCaseTitle
                    });
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Description",
                        Value = testCaseDescription
                    });
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.AreaPath",
                        Value = _config.AreaPath
                    });
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.IterationPath",
                        Value = _config.IterationPath
                    });
                    createDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Tags",
                        Value = $"AutoGenerated;API;TestCase;{endpoint.Tags.FirstOrDefault() ?? "General"};{testType}"
                    });

                    // Add custom fields from template
                    foreach (var field in _config.TestCaseTemplate.Fields)
                    {
                        createDoc.Add(new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = $"/fields/{field.Key}",
                            Value = field.Value
                        });
                    }

                    var newTestCase = await _witClient.CreateWorkItemAsync(createDoc, _config.ProjectName, _config.TestCaseTemplate.WorkItemType);
                    
                    // Link test case to story
                    await LinkWorkItemsAsync(story.Id.Value, newTestCase.Id.Value, "Child");

                    result.CreatedTestCases++;
                    result.SyncedItems.Add(new SyncedWorkItem
                    {
                        Id = newTestCase.Id.Value,
                        Title = testCaseTitle,
                        Type = "Test Case",
                        Action = "Created",
                        Url = newTestCase.Url
                    });

                    Console.WriteLine($"    ✅ Created Test Case: {testType} (ID: {newTestCase.Id})");
                }
            }
        }

        private async Task<List<WorkItem>> GetExistingWorkItemsAsync()
        {
            var wiql = new Wiql()
            {
                Query = $@"
                    SELECT [System.Id], [System.Title], [System.WorkItemType], [System.Tags], [System.Description]
                    FROM WorkItems 
                    WHERE [System.TeamProject] = '{_config.ProjectName}' 
                    AND [System.Tags] CONTAINS 'AutoGenerated'
                    AND [System.State] <> 'Removed'"
            };

            var result = await _witClient.QueryByWiqlAsync(wiql);
            if (result.WorkItems.Any())
            {
                var ids = result.WorkItems.Select(wi => wi.Id).ToArray();
                return await _witClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.Fields);
            }

            return new List<WorkItem>();
        }

        private async Task DeleteOrphanedWorkItemsAsync(OpenApiTestSpec spec, List<WorkItem> existingStories, List<WorkItem> existingTestCases, WorkItemSyncResult result)
        {
            var currentTags = spec.EndpointTests.Values
                .SelectMany(e => e.Tags)
                .Distinct()
                .ToHashSet();

            // Delete orphaned stories
            foreach (var story in existingStories)
            {
                var storyTitle = story.Fields["System.Title"].ToString();
                var isOrphaned = !currentTags.Any(tag => storyTitle.Contains(tag));

                if (isOrphaned)
                {
                    await DeleteWorkItemAsync(story.Id.Value);
                    result.DeletedStories++;
                    result.SyncedItems.Add(new SyncedWorkItem
                    {
                        Id = story.Id.Value,
                        Title = storyTitle,
                        Type = "Story",
                        Action = "Deleted",
                        Url = story.Url
                    });
                    Console.WriteLine($"  🗑️ Deleted orphaned Story: {storyTitle}");
                }
            }

            // Delete orphaned test cases
            var currentEndpoints = spec.EndpointTests.Values
                .Select(e => $"{e.Method} {e.Path}")
                .ToHashSet();

            foreach (var testCase in existingTestCases)
            {
                var testCaseTitle = testCase.Fields["System.Title"].ToString();
                var isOrphaned = !currentEndpoints.Any(endpoint => testCaseTitle.Contains(endpoint));

                if (isOrphaned)
                {
                    await DeleteWorkItemAsync(testCase.Id.Value);
                    result.DeletedTestCases++;
                    result.SyncedItems.Add(new SyncedWorkItem
                    {
                        Id = testCase.Id.Value,
                        Title = testCaseTitle,
                        Type = "Test Case",
                        Action = "Deleted",
                        Url = testCase.Url
                    });
                    Console.WriteLine($"  🗑️ Deleted orphaned Test Case: {testCaseTitle}");
                }
            }
        }

        private async Task LinkWorkItemsAsync(int parentId, int childId, string linkType)
        {
            var patchDoc = new JsonPatchDocument();
            patchDoc.Add(new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/relations/-",
                Value = new
                {
                    rel = $"System.LinkTypes.Hierarchy-{linkType}",
                    url = $"{_config.OrganizationUrl}/{_config.ProjectName}/_apis/wit/workItems/{childId}"
                }
            });

            await _witClient.UpdateWorkItemAsync(patchDoc, parentId);
        }

        private async Task DeleteWorkItemAsync(int workItemId)
        {
            await _witClient.DeleteWorkItemAsync(workItemId, destroy: true);
        }

        private AzureDevOpsConfig LoadConfiguration()
        {
            Console.WriteLine($"🔍 Looking for config at: {_configPath}");
            
            if (!File.Exists(_configPath))
            {
                Console.WriteLine("❌ Config file not found, creating default...");
                CreateDefaultConfiguration();
            }
            else
            {
                Console.WriteLine("✅ Config file found!");
            }

            try
            {
                var json = File.ReadAllText(_configPath);
                Console.WriteLine($"📄 Config content preview: {json.Substring(0, Math.Min(100, json.Length))}...");
                
                var config = JsonSerializer.Deserialize<AzureDevOpsConfig>(json, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });
                
                if (config != null)
                {
                    Console.WriteLine($"✅ Config loaded - Org: {config.OrganizationUrl}, Project: {config.ProjectName}");
                    return config;
                }
                else
                {
                    Console.WriteLine("❌ Config deserialization returned null");
                    return new AzureDevOpsConfig();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading config: {ex.Message}");
                return new AzureDevOpsConfig();
            }
        }

        private void CreateDefaultConfiguration()
        {
            var defaultConfig = new AzureDevOpsConfig
            {
                OrganizationUrl = "https://dev.azure.com/tr-legal-3E",
                ProjectName = "3EProject",
                PersonalAccessToken = "YOUR_PAT_TOKEN_HERE",
                AreaPath = "3EProject\\API Tests",
                IterationPath = "3EProject",
                StoryTemplate = new WorkItemTemplate
                {
                    WorkItemType = "User Story",
                    Fields = new Dictionary<string, string>
                    {
                        { "System.AssignedTo", "" },
                        { "Microsoft.VSTS.Common.Priority", "2" },
                        { "Microsoft.VSTS.Common.ValueArea", "Business" }
                    },
                    Tags = new List<string> { "AutoGenerated", "API", "OpenAPI" }
                },
                TestCaseTemplate = new WorkItemTemplate
                {
                    WorkItemType = "Test Case",
                    Fields = new Dictionary<string, string>
                    {
                        { "System.AssignedTo", "" },
                        { "Microsoft.VSTS.Common.Priority", "2" },
                        { "Microsoft.VSTS.TCM.AutomationStatus", "Automated" }
                    },
                    Tags = new List<string> { "AutoGenerated", "API", "TestCase" }
                }
            };

            Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);
            var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configPath, json);

            Console.WriteLine($"📝 Created default Azure DevOps configuration at: {_configPath}");
            Console.WriteLine("Please update the configuration with your Azure DevOps details.");
        }

        private void PrintSyncSummary(WorkItemSyncResult result)
        {
            Console.WriteLine("📊 SYNC SUMMARY:");
            Console.WriteLine($"   Stories: {result.CreatedStories} created, {result.UpdatedStories} updated, {result.DeletedStories} deleted");
            Console.WriteLine($"   Test Cases: {result.CreatedTestCases} created, {result.UpdatedTestCases} updated, {result.DeletedTestCases} deleted");
            Console.WriteLine($"   Total Items: {result.SyncedItems.Count}");
            
            if (result.Errors.Any())
            {
                Console.WriteLine($"   Errors: {result.Errors.Count}");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"     ❌ {error}");
                }
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                Console.WriteLine("🔌 Testing Azure DevOps connection...");
                Console.WriteLine($"📋 Organization: {_config.OrganizationUrl}");
                Console.WriteLine($"📁 Project: {_config.ProjectName}");
                Console.WriteLine();

                // Test 1: Get project info
                Console.Write("1. Testing project access... ");
                var projectClient = _connection.GetClient<ProjectHttpClient>();
                var project = await projectClient.GetProject(_config.ProjectName);
                Console.WriteLine($"✅ Project found: {project.Name}");

                // Test 2: Test work item queries
                Console.Write("2. Testing work item access... ");
                var wiql = new Wiql()
                {
                    Query = $"SELECT [System.Id] FROM WorkItems WHERE [System.TeamProject] = '{_config.ProjectName}' ORDER BY [System.Id] DESC"
                };
                var queryResult = await _witClient.QueryByWiqlAsync(wiql, top: 1);
                Console.WriteLine($"✅ Query successful ({queryResult.WorkItems.Count()} items found)");

                // Test 3: Test area path
                Console.Write("3. Testing area path... ");
                try
                {
                    // Just validate the format, don't fail if area doesn't exist
                    if (string.IsNullOrEmpty(_config.AreaPath) || !_config.AreaPath.Contains(_config.ProjectName))
                    {
                        Console.WriteLine($"⚠️  Area path format may be incorrect: {_config.AreaPath}");
                    }
                    else
                    {
                        Console.WriteLine($"✅ Area path format valid: {_config.AreaPath}");
                    }
                }
                catch
                {
                    Console.WriteLine($"⚠️  Could not validate area path: {_config.AreaPath}");
                }

                // Test 4: Test work item creation permissions
                Console.Write("4. Testing work item creation permissions... ");
                try
                {
                    // Try to create a minimal test work item and immediately delete it
                    var testDoc = new JsonPatchDocument();
                    testDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.WorkItemType",
                        Value = "Task"
                    });
                    testDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Title",
                        Value = "Connection Test - DELETE ME"
                    });
                    testDoc.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.Description",
                        Value = "This is a connection test work item. It will be deleted immediately."
                    });

                    var testWorkItem = await _witClient.CreateWorkItemAsync(testDoc, _config.ProjectName, "Task");
                    
                    // Immediately delete the test work item
                    await _witClient.DeleteWorkItemAsync(testWorkItem.Id.Value, destroy: true);
                    
                    Console.WriteLine("✅ Create/Delete permissions confirmed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Permission test failed: {ex.Message}");
                    return false;
                }

                Console.WriteLine();
                Console.WriteLine("🎉 All connection tests passed!");
                Console.WriteLine("✅ Ready to sync OpenAPI specifications to Azure DevOps");
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection test failed: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("💡 Common issues:");
                Console.WriteLine("  • Invalid Personal Access Token (PAT)");
                Console.WriteLine("  • Incorrect Organization URL");
                Console.WriteLine("  • Project name doesn't exist or no access");
                Console.WriteLine("  • Insufficient permissions (need Work Items Read & Write)");
                Console.WriteLine("  • Network connectivity issues");
                
                return false;
            }
        }

        public async Task<List<WorkItemSyncResult>> GetSyncHistoryAsync()
        {
            var historyPath = Path.Combine(AppContext.BaseDirectory, "Config", "AzureDevOps", "sync-history.json");
            
            if (!File.Exists(historyPath))
            {
                return new List<WorkItemSyncResult>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(historyPath);
                var history = JsonSerializer.Deserialize<List<SyncHistoryEntry>>(json) ?? new List<SyncHistoryEntry>();
                
                return history.Select(h => new WorkItemSyncResult
                {
                    CreatedStories = h.CreatedStories,
                    UpdatedStories = h.UpdatedStories,
                    DeletedStories = h.DeletedStories,
                    CreatedTestCases = h.CreatedTestCases,
                    UpdatedTestCases = h.UpdatedTestCases,
                    DeletedTestCases = h.DeletedTestCases,
                    Errors = h.Errors,
                    SyncedItems = h.SyncedItems
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Could not load sync history: {ex.Message}");
                return new List<WorkItemSyncResult>();
            }
        }

        private async Task SaveSyncHistoryAsync(WorkItemSyncResult result, string specificationPath)
        {
            var historyPath = Path.Combine(AppContext.BaseDirectory, "Config", "AzureDevOps", "sync-history.json");
            Directory.CreateDirectory(Path.GetDirectoryName(historyPath)!);

            var history = await GetSyncHistoryAsync();
            
            var newEntry = new SyncHistoryEntry
            {
                Timestamp = DateTime.UtcNow,
                SpecificationPath = specificationPath,
                CreatedStories = result.CreatedStories,
                UpdatedStories = result.UpdatedStories,
                DeletedStories = result.DeletedStories,
                CreatedTestCases = result.CreatedTestCases,
                UpdatedTestCases = result.UpdatedTestCases,
                DeletedTestCases = result.DeletedTestCases,
                Errors = result.Errors,
                SyncedItems = result.SyncedItems
            };

            var historyEntries = history.Select(h => new SyncHistoryEntry
            {
                Timestamp = DateTime.UtcNow, // This should be preserved from original
                SpecificationPath = specificationPath,
                CreatedStories = h.CreatedStories,
                UpdatedStories = h.UpdatedStories,
                DeletedStories = h.DeletedStories,
                CreatedTestCases = h.CreatedTestCases,
                UpdatedTestCases = h.UpdatedTestCases,
                DeletedTestCases = h.DeletedTestCases,
                Errors = h.Errors,
                SyncedItems = h.SyncedItems
            }).ToList();

            historyEntries.Insert(0, newEntry); // Add newest first
            
            // Keep only last 50 entries
            if (historyEntries.Count > 50)
            {
                historyEntries = historyEntries.Take(50).ToList();
            }

            var json = JsonSerializer.Serialize(historyEntries, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(historyPath, json);
        }
    }

    public class SyncHistoryEntry
    {
        public DateTime Timestamp { get; set; }
        public string SpecificationPath { get; set; } = string.Empty;
        public int CreatedStories { get; set; }
        public int UpdatedStories { get; set; }
        public int DeletedStories { get; set; }
        public int CreatedTestCases { get; set; }
        public int UpdatedTestCases { get; set; }
        public int DeletedTestCases { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<SyncedWorkItem> SyncedItems { get; set; } = new();
    }
}