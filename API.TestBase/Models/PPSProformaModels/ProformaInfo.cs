using Newtonsoft.Json;

namespace API.TestBase.Models.ProformaModels
{
    public class ProformaInfo
    {
        [JsonProperty("proformaIndex", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int ProformaIndex { get; set; }

        [JsonProperty("proformaId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid ProformaId { get; set; }

        [JsonProperty("invNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InvNumber { get; set; }

        [JsonProperty("invDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime InvDate { get; set; }

        [JsonProperty("wfItemStepIndex", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int WfItemStepIndex { get; set; }

        [JsonProperty("clientName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientName { get; set; }

        [JsonProperty("clientNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientNumber { get; set; }

        [JsonProperty("clientAltNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClientAltNumber { get; set; }

        [JsonProperty("matterName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterName { get; set; }

        [JsonProperty("matterNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterNumber { get; set; }

        [JsonProperty("matterAltNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterAltNumber { get; set; }

        [JsonProperty("matterDescription", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MatterDescription { get; set; }

        [JsonProperty("billingGroup", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BillingGroup { get; set; }

        [JsonProperty("billingGroupDescription", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BillingGroupDescription { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("total", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Total { get; set; }

        [JsonProperty("feesAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double FeesAmount { get; set; }

        [JsonProperty("costAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double CostAmount { get; set; }

        [JsonProperty("chargeAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double ChargeAmount { get; set; }

        [JsonProperty("boaAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double BoaAmount { get; set; }

        [JsonProperty("trustAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double TrustAmount { get; set; }

        [JsonProperty("unallocatedAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double UnallocatedAmount { get; set; }

        [JsonProperty("totalAvailableFunds", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double TotalAvailableFunds { get; set; }

        [JsonProperty("otherAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double OtherAmount { get; set; }

        [JsonProperty("interestAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double InterestAmount { get; set; }

        [JsonProperty("taxAmount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double TaxAmount { get; set; }

        [JsonProperty("proformaStatus", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ProformaStatus { get; set; }

        [JsonProperty("status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("urgencyDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UrgencyDate { get; set; }

        [JsonProperty("substatus", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Substatus { get; set; }

        [JsonProperty("lockedBy", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LockedBy { get; set; }

        [JsonProperty("lockedByUserId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid LockedByUserId { get; set; }

        [JsonProperty("lockedByAuthenticated", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LockedByAuthenticated { get; set; }

        [JsonProperty("lockedByAuthenticatedUserId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid LockedByAuthenticatedUserId { get; set; }

        [JsonProperty("billingTimekeeperName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BillingTimekeeperName { get; set; }

        [JsonProperty("billingTimekeeperNumber", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BillingTimekeeperNumber { get; set; }

        [JsonProperty("forwardTimekeepers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> ForwardTimekeepers { get; set; }

        [JsonProperty("matterCount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int MatterCount { get; set; }

        [JsonProperty("disposition", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Disposition { get; set; }

        [JsonProperty("isPriority", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsPriority { get; set; }

        [JsonProperty("hasComments", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool HasComments { get; set; }

        [JsonProperty("approvers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> Approvers { get; set; }

        [JsonProperty("canGenerateBill", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool CanGenerateBill { get; set; }

        [JsonProperty("collaboratorsCount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int CollaboratorsCount { get; set; }

        [JsonProperty("completedCollaboratorsCount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int CompletedCollaboratorsCount { get; set; }

        [JsonProperty("owner", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

        [JsonProperty("coOwner", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string CoOwner { get; set; }

        [JsonProperty("profDate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ProfDate { get; set; }

        [JsonProperty("isNotStarted", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsNotStarted { get; set; }

        [JsonProperty("timestamp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Timestamp { get; set; }

        [JsonProperty("completeDate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CompleteDate { get; set; }
    }
}
