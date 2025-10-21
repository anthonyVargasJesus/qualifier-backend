namespace Qualifier.Application.Database.ActiveType.Commands.DeleteActiveType
{
    public interface IDeleteActiveTypeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

