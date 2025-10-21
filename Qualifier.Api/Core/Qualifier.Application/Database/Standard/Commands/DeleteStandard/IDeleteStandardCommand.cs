namespace Qualifier.Application.Database.Standard.Commands.DeleteStandard
{
    public interface IDeleteStandardCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

