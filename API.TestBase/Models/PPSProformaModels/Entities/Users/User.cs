namespace API.TestBase.Models.PPSProformaModels.Entities.Users
{
    public class User : EntityWithIdBase
    {
        public string UserName { get; set; }

        public int UserIndex { get; set; }

        public string UserNumber { get; set; }

        public string Office { get; set; }

        public string JobPosition { get; set; }
    }
}
