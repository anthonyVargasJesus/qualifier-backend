namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementById
{
    public interface IGetRequirementByIdQuery
    {
        Task<Object> Execute(int requirementId);
    }
}

