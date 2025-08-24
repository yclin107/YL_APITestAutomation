namespace API.Core.Models.AzureDevOps
{
    public class WorkItemSyncResult
    {
        public int CreatedStories { get; set; }
        public int UpdatedStories { get; set; }
        public int DeletedStories { get; set; }
        public int CreatedTestCases { get; set; }
        public int UpdatedTestCases { get; set; }
        public int DeletedTestCases { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<SyncedWorkItem> SyncedItems { get; set; } = new();
    }

    public class SyncedWorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // Created, Updated, Deleted
        public string Url { get; set; } = string.Empty;
    }

    public enum SyncAction
    {
        Created,
        Updated,
        Deleted,
        Skipped
    }
}