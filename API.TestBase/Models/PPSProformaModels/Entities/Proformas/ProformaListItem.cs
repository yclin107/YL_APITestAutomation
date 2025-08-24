using API.TestBase.Models.PPSProformaModels.Entities.Timekeepers;
using API.TestBase.Models.PPSProformaModels.Entities.Users;

namespace API.TestBase.Models.PPSProformaModels.Entities.Proformas
{
    public class ProformaListItem : ProformaWfIndex
    {
        public int ProformaNumber { get; set; }

        public string ClientNumber { get; set; } = string.Empty;

        public string Client { get; set; } = string.Empty;

        public string Matter { get; set; } = string.Empty;

        public string MatterNumber { get; set; } = string.Empty;

        public double Total { get; set; }

        public double TotalCharges { get; set; }

        public double TotalCosts { get; set; }

        public double TotalFees { get; set; }

        public double? ApplicableFunds { get; set; }

        public double? AvailableFunds { get; set; }

        public DateTime? UrgencyDate { get; set; }

        public ProformaStatus Status { get; set; }

        public ProformaSubstatus Substatus { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime? InvoiceDate { get; set; }

        public string CurrencyCode { get; set; } = string.Empty;

        public bool IsPriority { get; set; }

        public User? LockedBy { get; set; }

        public User? LockedByAuthenticated { get; set; }

        public bool IsForwarded { get; set; }

        public bool IsCombiningGroupProforma { get; set; }

        public IEnumerable<TimekeeperWithNameOnly> Forwarded { get; set; } = new List<TimekeeperWithNameOnly>();

        public string BillGroup { get; set; } = string.Empty;

        public bool HasComments { get; set; }

        public int CommentsCount { get; set; }

        public string BillingTimekeeperName { get; set; } = string.Empty;

        public string BillingTimekeeperNumber { get; set; } = string.Empty;

        public Guid Piid { get; set; }

        public bool? CanGenerateBill { get; set; }

        public int CollaboratorsCount { get; set; }

        public int CompletedCollaboratorsCount { get; set; }

        public string Owner { get; set; } = string.Empty;

        public string CoOwner { get; set; } = string.Empty;

        public DateTime ProfDate { get; set; }

        public DateTime Timestamp { get; set; }

        public IList<string> Approvers { get; set; } = new List<string>();
    }
}
