namespace Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId
{
    public class GetPersonalsByCompanyIdDto
    {
        public int personalId { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string image { get; set; }

    }
}

