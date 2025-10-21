namespace Qualifier.Application.Database.Personal.Commands.UpdatePersonal
{
    public interface IUpdatePersonalCommand
    {
        Task<Object> Execute(UpdatePersonalDto model, int id);
    }
}

