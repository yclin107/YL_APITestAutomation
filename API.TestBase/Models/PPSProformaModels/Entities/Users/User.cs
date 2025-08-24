namespace API.TestBase.Models.PPSProformaModels.Entities.Users
{
    public class User : EntityWithIdBase
    {
        public string UserName { get; set; } = string.Empty;

        public int UserIndex { get; set; }

        public string UserNumber { get; set; } = string.Empty;

        public string Office { get; set; } = string.Empty;

        public string JobPosition { get; set; } = string.Empty;
    }
}
