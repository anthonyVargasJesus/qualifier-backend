namespace Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId
{
    public interface IGetAllRequirementsByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

