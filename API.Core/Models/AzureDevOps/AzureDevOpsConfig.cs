using System.Text.Json.Serialization;

namespace API.Core.Models.AzureDevOps
{
    public class AzureDevOpsConfig
    {
        [JsonPropertyName("organizationUrl")]
        public string OrganizationUrl { get; set; } = string.Empty;

        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonPropertyName("personalAccessToken")]
        public string PersonalAccessToken { get; set; } = string.Empty;

        [JsonPropertyName("areaPath")]
        public string AreaPath { get; set; } = string.Empty;

        [JsonPropertyName("iterationPath")]
        public string IterationPath { get; set; } = string.Empty;

        [JsonPropertyName("storyTemplate")]
        public WorkItemTemplate StoryTemplate { get; set; } = new();

        [JsonPropertyName("testCaseTemplate")]
        public WorkItemTemplate TestCaseTemplate { get; set; } = new();
    }

    public class WorkItemTemplate
    {
        [JsonPropertyName("workItemType")]
        public string WorkItemType { get; set; } = string.Empty;

        [JsonPropertyName("fields")]
        public Dictionary<string, string> Fields { get; set; } = new();

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();
    }
}