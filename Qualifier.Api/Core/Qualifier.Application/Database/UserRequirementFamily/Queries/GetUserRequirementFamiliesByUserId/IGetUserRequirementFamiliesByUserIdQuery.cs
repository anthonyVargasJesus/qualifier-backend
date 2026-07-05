namespace Qualifier.Application.Database.UserRequirementFamily.Queries.GetUserRequirementFamiliesByUserId
{
    public interface IGetUserRequirementFamiliesByUserIdQuery
    {
        Task<Object> Execute(int userId, int standardId);
    }
}
