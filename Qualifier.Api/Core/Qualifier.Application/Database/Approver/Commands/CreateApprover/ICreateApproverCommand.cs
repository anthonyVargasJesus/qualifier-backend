namespace Qualifier.Application.Database.Approver.Commands.CreateApprover
{
    public interface ICreateApproverCommand
    {
        Task<Object> Execute(CreateApproverDto model);
    }
}
