using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APITestAutomation.Models.PPSProformaModels.Entities
{
    public abstract class EntityWithIdBase
    {
        [BindRequired]
        public Guid Id { get; set; }
    }
}
