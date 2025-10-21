namespace Qualifier.Application.Database.Scope.Commands.DeleteScope
{
    public interface IDeleteScopeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

