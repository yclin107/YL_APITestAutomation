using Newtonsoft.Json;

namespace APITestAutomation.Models.ProformaModels
{
    public class ProformaTrackingListFilter
    {
        [JsonProperty("matters", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ListFilterMatterInfo> Matters { get; set; }

        [JsonProperty("matterNumbers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> MatterNumbers { get; set; }

        [JsonProperty("clients", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> Clients { get; set; }

        [JsonProperty("clientNumbers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> ClientNumbers { get; set; }

        [JsonProperty("timekeepers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> Timekeepers { get; set; }

        [JsonProperty("timekeeperNumbers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> TimekeeperNumbers { get; set; }
    }
}
