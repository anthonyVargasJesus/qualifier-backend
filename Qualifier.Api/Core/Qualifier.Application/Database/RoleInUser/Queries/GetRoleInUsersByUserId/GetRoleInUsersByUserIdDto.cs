namespace Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId
{
    public class GetRoleInUsersByUserIdDto
    {
        public int roleInUserId { get; set; }
        public int roleId { get; set; }
        public GetRoleInUsersByUserIdRoleDto? role { get; set; }
    }
    public class GetRoleInUsersByUserIdRoleDto
    {
        public string name { get; set; }

    }
}

