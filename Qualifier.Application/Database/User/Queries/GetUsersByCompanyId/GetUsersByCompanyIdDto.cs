namespace Qualifier.Application.Database.User.Queries.GetUsersByCompanyId
{
    public class GetUsersByCompanyIdDto
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public string documentNumber { get; set; }

    }
}

