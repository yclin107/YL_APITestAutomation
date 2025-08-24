using Newtonsoft.Json;

namespace API.TestBase.Models.ProformaModels
{
    public class ProformaTrackingListResponse
    {
        [JsonProperty("summary", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ProformaTrackingSummaryResponse Summary { get; set; }

        [JsonProperty("listResponse", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ProformaListResponse ListResponse { get; set; }

        [JsonProperty("listFilter", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ProformaTrackingListFilter ListFilter { get; set; }
    }
}
