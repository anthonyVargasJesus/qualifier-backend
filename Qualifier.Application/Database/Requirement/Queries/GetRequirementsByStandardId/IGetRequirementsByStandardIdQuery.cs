namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId
{
    public interface IGetRequirementsByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

