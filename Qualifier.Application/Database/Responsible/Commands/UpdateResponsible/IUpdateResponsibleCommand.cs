namespace Qualifier.Application.Database.Responsible.Commands.UpdateResponsible
{
    public interface IUpdateResponsibleCommand
    {
        Task<Object> Execute(UpdateResponsibleDto model, int id);
    }
}

