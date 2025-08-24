
namespace API.TestBase.Models.PPSProformaModels.Entities;

public class DataContainer<T> where T : class
{
    public int RecordCount { get; set; }

    public IEnumerable<T> Data { get; set; } = new List<T>();
}
