
namespace API.TestBase.Models.PPSProformaModels.Entities.Filters;

public class FilterOperation<T>
{
    public string OperationName { get; set; } = string.Empty;

    public T OperandValue { get; set; }
}
