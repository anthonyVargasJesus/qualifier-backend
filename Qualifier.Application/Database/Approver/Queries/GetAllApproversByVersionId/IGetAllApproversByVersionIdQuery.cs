namespace Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId
{
    public interface IGetAllApproversByVersionIdQuery
    {
        Task<Object> Execute(int versionId);
    }
}

