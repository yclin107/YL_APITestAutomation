
namespace API.TestBase.Models.PPSProformaModels.Entities.Filters;

public class FilterOperation<T>
{
    public string? OperationName { get; set; }

    public T? OperandValue { get; set; }
}
