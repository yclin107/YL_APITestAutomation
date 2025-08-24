using API.TestBase.Models.PPSProformaModels.Entities.Proformas;

namespace API.TestBase.Models.PPSProformaModels.Entities.Filters;

public class ProformaFilter
{
    public FilterOperation<double>? Total { get; set; }

    public IEnumerable<string>? ClientNames { get; set; }

    public IEnumerable<string>? ClientNumbers { get; set; }

    public IEnumerable<string>? Matters { get; set; }

    public IEnumerable<string>? MatterDescriptions { get; set; }

    public IEnumerable<string>? MatterNumbers { get; set; }

    public FilterOperation<double>? TotalFees { get; set; }

    public FilterOperation<double>? TotalCosts { get; set; }

    public FilterOperation<double>? TotalCharges { get; set; }

    public IEnumerable<string>? MatterCurrencies { get; set; }

    public IEnumerable<string>? BillGroups { get; set; }

    public FilterOperation<double>? AvailableFunds { get; set; }

    public IEnumerable<ProformaStatus>? Statuses { get; set; }

    public bool? Flagged { get; set; }

    public bool? Forwarded { get; set; }

    public IEnumerable<string>? Locked { get; set; }

    public bool OnlyApprovalsPending { get; set; }

    public FilterOperation<decimal>? TotalWIP { get; set; }

    public FilterOperation<decimal>? Fees { get; set; }

    public FilterOperation<decimal>? Costs { get; set; }

    public FilterOperation<decimal>? Charges { get; set; }

    public FilterOperation<int>? ProformaIndex { get; set; }

    public IEnumerable<string>? Owners { get; set; }
}
