using Newtonsoft.Json;


namespace API.TestBase.Models.ProformaModels
{
    public class ProformaTrackingSummaryResponse
    {
        [JsonProperty("completed", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int Completed { get; set; }

        [JsonProperty("incomplete", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int Incomplete { get; set; }

        [JsonProperty("notStarted", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int NotStarted { get; set; }

        [JsonProperty("matterName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterName { get; set; }

        [JsonProperty("matterNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterNumber { get; set; }

        [JsonProperty("clientName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientName { get; set; }

        [JsonProperty("clientNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientNumber { get; set; }

        [JsonProperty("timekeeperName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TimekeeperName { get; set; }

        [JsonProperty("timekeeperNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TimekeeperNumber { get; set; }
    }
}
