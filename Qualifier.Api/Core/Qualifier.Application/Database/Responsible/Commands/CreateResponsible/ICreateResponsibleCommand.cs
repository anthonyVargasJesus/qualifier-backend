namespace Qualifier.Application.Database.Responsible.Commands.CreateResponsible
{
    public interface ICreateResponsibleCommand
    {
        Task<Object> Execute(CreateResponsibleDto model);
    }
}

