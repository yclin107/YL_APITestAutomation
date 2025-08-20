using APITestAutomation.Models.OpenAPI;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APITestAutomation.Services.OpenAPI
{
    public class OpenApiTestManager
    {
        private readonly string _configPath;
        private readonly string _testsPath;
        private readonly string _reportsPath;

        public OpenApiTestManager(string configPath = "Config/OpenAPI", string testsPath = "Generated", string reportsPath = "Reports")
        {
            _configPath = Path.Combine(AppContext.BaseDirectory, configPath);
            _testsPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "APITestAutomationTest", testsPath);
            _reportsPath = Path.Combine(AppContext.BaseDirectory, reportsPath);
            
            Directory.CreateDirectory(_configPath);
            Directory.CreateDirectory(_testsPath);
            Directory.CreateDirectory(_reportsPath);
        }

        public async Task<OpenApiTestSpec> LoadSpecificationAsync(string specPath, string? baseUrl = null)
        {
            var spec = await OpenApiSpecReader.ReadSpecificationAsync(specPath, baseUrl);
            
            // Save current spec for future comparisons (without the full OpenAPI document to avoid cycles)
            var configFile = Path.Combine(_configPath, "current-spec.json");
            var specForSerialization = new OpenApiTestSpecForSerialization
            {
                SpecificationPath = spec.SpecificationPath,
                BaseUrl = spec.BaseUrl,
                LastModified = spec.LastModified,
                Version = spec.Version,
                EndpointTests = spec.EndpointTests,
                DocumentInfo = new OpenApiDocumentInfo
                {
                    Title = spec.Document.Info?.Title ?? "Unknown",
                    Version = spec.Document.Info?.Version ?? "1.0.0",
                    Description = spec.Document.Info?.Description ?? ""
                }
            };
            
            var jsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var json = JsonSerializer.Serialize(specForSerialization, jsonOptions);
            await File.WriteAllTextAsync(configFile, json);
            
            return spec;
        }

        public async Task<List<TestGenerationChange>> DetectChangesAsync(string specPath, string? baseUrl = null)
        {
            var newSpec = await OpenApiSpecReader.ReadSpecificationAsync(specPath, baseUrl);
            var configFile = Path.Combine(_configPath, "current-spec.json");
            
            if (!File.Exists(configFile))
            {
                // First time loading, all endpoints are new
                return newSpec.EndpointTests.Values.Select(e => new TestGenerationChange
                {
                    Type = ChangeType.Added,
                    Path = e.Path,
                    Method = e.Method,
                    Description = $"New endpoint {e.Method} {e.Path}"
                }).ToList();
            }

            var oldSpecJson = await File.ReadAllTextAsync(configFile);
            var jsonOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var oldSpecSerialized = JsonSerializer.Deserialize<OpenApiTestSpecForSerialization>(oldSpecJson, jsonOptions);
            
            if (oldSpecSerialized == null)
                throw new InvalidOperationException("Could not deserialize previous specification");

            // Convert back to OpenApiTestSpec for comparison
            var oldSpec = new OpenApiTestSpec
            {
                SpecificationPath = oldSpecSerialized.SpecificationPath,
                BaseUrl = oldSpecSerialized.BaseUrl,
                LastModified = oldSpecSerialized.LastModified,
                Version = oldSpecSerialized.Version,
                EndpointTests = oldSpecSerialized.EndpointTests
            };

            return TestChangeDetector.DetectChanges(oldSpec, newSpec);
        }

        public async Task<string> GenerateTestsAsync(OpenApiTestSpec spec, string tenant, string userId, bool applyChanges = false)
        {
            if (!applyChanges)
            {
                // Return preview of what would be generated
                return GeneratePreview(spec);
            }

            var className = $"Generated_{spec.Document.Info?.Title?.Replace(" ", "").Replace("-", "") ?? "API"}Tests";
            var testCode = TestCodeGenerator.GenerateTestClass(spec, tenant, userId, className);
            
            // Add helper methods
            testCode = testCode.Replace("    }", TestCodeGenerator.GenerateHelperMethods() + "\n    }");
            
            var fileName = $"{className}.cs";
            var filePath = Path.Combine(_testsPath, fileName);
            
            await File.WriteAllTextAsync(filePath, testCode);
            
            // Update current spec
            var configFile = Path.Combine(_configPath, "current-spec.json");
            var specForSerialization = new OpenApiTestSpecForSerialization
            {
                SpecificationPath = spec.SpecificationPath,
                BaseUrl = spec.BaseUrl,
                LastModified = spec.LastModified,
                Version = spec.Version,
                EndpointTests = spec.EndpointTests,
                DocumentInfo = new OpenApiDocumentInfo
                {
                    Title = spec.Document.Info?.Title ?? "Unknown",
                    Version = spec.Document.Info?.Version ?? "1.0.0",
                    Description = spec.Document.Info?.Description ?? ""
                }
            };
            
            var jsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var json = JsonSerializer.Serialize(specForSerialization, jsonOptions);
            await File.WriteAllTextAsync(configFile, json);
            
            return $"Tests generated successfully at: {filePath}";
        }

        public async Task<string> GenerateChangeReportAsync(List<TestGenerationChange> changes)
        {
            var reportPath = Path.Combine(_reportsPath, $"change-report-{DateTime.Now:yyyyMMdd-HHmmss}.json");
            
            var report = new
            {
                Timestamp = DateTime.Now,
                TotalChanges = changes.Count,
                Changes = changes.GroupBy(c => c.Type).ToDictionary(
                    g => g.Key.ToString(),
                    g => g.Select(c => new
                    {
                        c.Path,
                        c.Method,
                        c.Description,
                        c.OldValue,
                        c.NewValue
                    }).ToList()
                )
            };
            
            var jsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var json = JsonSerializer.Serialize(report, jsonOptions);
            await File.WriteAllTextAsync(reportPath, json);
            
            return reportPath;
        }

        // Helper classes for serialization without circular references
        private class OpenApiTestSpecForSerialization
        {
            public string SpecificationPath { get; set; } = string.Empty;
            public string BaseUrl { get; set; } = string.Empty;
            public DateTime LastModified { get; set; }
            public string Version { get; set; } = string.Empty;
            public Dictionary<string, OpenApiEndpointTest> EndpointTests { get; set; } = new();
            public OpenApiDocumentInfo DocumentInfo { get; set; } = new();
        }

        private class OpenApiDocumentInfo
        {
            public string Title { get; set; } = string.Empty;
            public string Version { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }

        private string GeneratePreview(OpenApiTestSpec spec)
        {
            var preview = $@"
=== TEST GENERATION PREVIEW ===
API: {spec.Document.Info?.Title ?? "Unknown"}
Version: {spec.Document.Info?.Version ?? "Unknown"}
Base URL: {spec.BaseUrl}

ENDPOINTS TO BE TESTED:
";
            
            foreach (var endpoint in spec.EndpointTests.Values)
            {
                preview += $"  {endpoint.Method} {endpoint.Path}\n";
                preview += $"    - Positive test\n";
                if (endpoint.RequiresAuth)
                    preview += $"    - Unauthorized test\n";
                if (endpoint.Parameters.Any(p => p.Required))
                    preview += $"    - Missing required parameters test\n";
                if (endpoint.Responses.ContainsKey("200") || endpoint.Responses.ContainsKey("201"))
                    preview += $"    - Schema validation test\n";
                preview += "\n";
            }
            
            return preview;
        }

        public string FormatChangeSummary(List<TestGenerationChange> changes)
        {
            if (!changes.Any())
                return "No changes detected.";

            var summary = "=== DETECTED CHANGES ===\n\n";
            
            var grouped = changes.GroupBy(c => c.Type);
            
            foreach (var group in grouped)
            {
                summary += $"{group.Key.ToString().ToUpper()}:\n";
                foreach (var change in group)
                {
                    summary += $"  - {change.Description}\n";
                }
                summary += "\n";
            }
            
            return summary;
        }
    }
}