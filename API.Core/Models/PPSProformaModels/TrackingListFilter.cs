

namespace APITestAutomation.Models.ProformaModels
{
    public class TrackingListFilter
    {
        public bool IsNotStarted { get; set; }

        public bool IsIncomplete { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsArchived { get; set; }

        public string ClientName { get; set; }

        public string ClientNumber { get; set; }

        public string BillingTimekeeperName { get; set; }

        public string BillingTimekeeperNumber { get; set; }

        public string MatterName { get; set; }

        public string MatterNumber { get; set; }

        public string ProformaIndex { get; set; }

        public ICollection<string> ProfDate { get; set; }

    }
}
