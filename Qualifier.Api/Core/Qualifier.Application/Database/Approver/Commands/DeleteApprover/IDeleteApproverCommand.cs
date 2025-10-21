namespace Qualifier.Application.Database.Approver.Commands.DeleteApprover
{
    public interface IDeleteApproverCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

