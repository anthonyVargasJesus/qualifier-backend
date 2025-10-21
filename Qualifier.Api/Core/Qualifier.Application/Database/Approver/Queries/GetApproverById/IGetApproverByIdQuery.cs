namespace Qualifier.Application.Database.Approver.Queries.GetApproverById
{
    public interface IGetApproverByIdQuery
    {
        Task<Object> Execute(int approverId);
    }
}

