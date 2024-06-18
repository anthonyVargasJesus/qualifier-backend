namespace Qualifier.Application.Database.User.Queries.GetUserById
{
    public class GetUserByIdDto
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int userStateId { get; set; }
        public string image { get; set; }
        public string documentNumber { get; set; }
        public GetUserByIdUserStateDto userState { get; set; }
        public List<GetUserByIdRoleDto> roles { get; set; }
    }
    public class GetUserByIdUserStateDto
    {
        public string name { get; set; }

    }
    public class GetUserByIdRoleDto
    {
        public string name { get; set; }

    }
}

