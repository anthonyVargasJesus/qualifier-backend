namespace Qualifier.Application.Database.Personal.Commands.CreatePersonal
{
    public interface ICreatePersonalCommand
    {
        Task<Object> Execute(CreatePersonalDto model);
    }
}

