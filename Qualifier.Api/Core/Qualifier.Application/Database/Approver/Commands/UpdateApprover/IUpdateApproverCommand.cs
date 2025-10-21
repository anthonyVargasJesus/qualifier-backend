namespace Qualifier.Application.Database.Approver.Commands.UpdateApprover
{
    public interface IUpdateApproverCommand
    {
        Task<Object> Execute(UpdateApproverDto model, int id);
    }
}

