
using APITestAutomation.Models.PPSProformaModels.Entities.Filters;
using APITestAutomation.Models.PPSProformaModels.Entities.Proformas;

namespace APITestAutomation.Models.PPSProformaModels.Entities;

public class ProformaBucketDetails
{
    public IEnumerable<StatusFilterItem> Summary { get; set; }

    public DataContainer<ProformaListItem> ListResponse { get; set; }

    public ProformaFilterValues ListFilter { get; set; }
}
