namespace Qualifier.Application.Database.Menace.Commands.DeleteMenace
{
    public interface IDeleteMenaceCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

