namespace Qualifier.Application.Database.Responsible.Commands.DeleteResponsible
{
    public interface IDeleteResponsibleCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

