namespace Qualifier.Application.Database.Personal.Queries.GetPersonalById
{
    public class GetPersonalByIdDto
    {
        public int personalId { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string position { get; set; }
        public string image { get; set; }
        public int companyId { get; set; }

    }
}

