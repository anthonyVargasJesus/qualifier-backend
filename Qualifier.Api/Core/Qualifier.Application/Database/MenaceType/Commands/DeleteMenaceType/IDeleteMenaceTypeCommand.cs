namespace Qualifier.Application.Database.MenaceType.Commands.DeleteMenaceType
{
    public interface IDeleteMenaceTypeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

