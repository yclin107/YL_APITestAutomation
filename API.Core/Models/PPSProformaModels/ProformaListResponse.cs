using Newtonsoft.Json;

namespace APITestAutomation.Models.ProformaModels
{
    public class ProformaListResponse
    {
        [JsonProperty("totalRowCount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int TotalRowCount { get; set; }

        [JsonProperty("proformas", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ProformaInfo> Proformas { get; set; }
    }
}
