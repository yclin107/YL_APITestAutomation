
using API.TestBase.Models.PPSProformaModels.Entities.Filters;
using API.TestBase.Models.PPSProformaModels.Entities.Proformas;

namespace API.TestBase.Models.PPSProformaModels.Entities;

public class ProformaBucketDetails
{
    public IEnumerable<StatusFilterItem> Summary { get; set; } = new List<StatusFilterItem>();

    public DataContainer<ProformaListItem> ListResponse { get; set; } = new();

    public ProformaFilterValues ListFilter { get; set; } = new();
}
