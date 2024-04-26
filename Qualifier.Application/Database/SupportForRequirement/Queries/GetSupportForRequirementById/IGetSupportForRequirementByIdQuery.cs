namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById
{
    public interface IGetSupportForRequirementByIdQuery
    {
        Task<Object> Execute(int supportForRequirementId);
    }
}

