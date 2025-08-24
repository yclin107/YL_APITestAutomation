using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.TestBase.Models.PPSProformaModels.Entities
{
    public abstract class EntityWithIdBase
    {
        [BindRequired]
        public Guid Id { get; set; }
    }
}
