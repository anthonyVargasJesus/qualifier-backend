namespace Qualifier.Application.Database.Personal.Commands.DeletePersonal
{
    public interface IDeletePersonalCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

