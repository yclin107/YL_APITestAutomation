
using APITestAutomation.Models.PPSProformaModels.Entities.Users;

namespace APITestAutomation.Models.PPSProformaModels.Entities.Filters;

public class ProformaFilterValues
{
    public IEnumerable<UserFilterItem> LockedBy { get; set; }

    public IEnumerable<string> ClientNames { get; set; }

    public IEnumerable<string> ClientNumbers { get; set; }

    public IEnumerable<ProformaFilterMatter> Matters { get; set; }

    public IEnumerable<string> MatterNumbers { get; set; }

    public IEnumerable<string> MatterCurrencies { get; set; }

    public IEnumerable<string> BillGroups { get; set; }

    public bool ApprovalsPending { get; set; }

    public IEnumerable<string> Owners { get; set; }
}
