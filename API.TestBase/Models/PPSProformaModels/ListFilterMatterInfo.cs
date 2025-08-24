using Newtonsoft.Json;

namespace API.TestBase.Models.ProformaModels
{
    public class ListFilterMatterInfo
    {
        [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("description", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("number", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Number { get; set; }
    }
}
