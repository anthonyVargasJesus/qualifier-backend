namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId
{
    public interface IGetRequirementsByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

