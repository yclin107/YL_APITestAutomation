
using API.TestBase.Models.PPSProformaModels.Entities.Users;

namespace API.TestBase.Models.PPSProformaModels.Entities.Filters;

public class ProformaFilterValues
{
    public IEnumerable<UserFilterItem> LockedBy { get; set; } = new List<UserFilterItem>();

    public IEnumerable<string> ClientNames { get; set; } = new List<string>();

    public IEnumerable<string> ClientNumbers { get; set; } = new List<string>();

    public IEnumerable<ProformaFilterMatter> Matters { get; set; } = new List<ProformaFilterMatter>();

    public IEnumerable<string> MatterNumbers { get; set; } = new List<string>();

    public IEnumerable<string> MatterCurrencies { get; set; } = new List<string>();

    public IEnumerable<string> BillGroups { get; set; } = new List<string>();

    public bool ApprovalsPending { get; set; }

    public IEnumerable<string> Owners { get; set; } = new List<string>();
}
