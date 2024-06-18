namespace Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId
{
    public class GetAllRoleInUsersByUserIdDto
    {
        public int roleInUserId { get; set; }
        public int roleId { get; set; }
        public GetAllRoleInUsersByUserIdRoleDto? role { get; set; }
    }
    public class GetAllRoleInUsersByUserIdRoleDto
    {
        public string name { get; set; }

    }
}

