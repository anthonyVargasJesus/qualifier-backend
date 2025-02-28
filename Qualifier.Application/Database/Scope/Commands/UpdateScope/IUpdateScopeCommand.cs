namespace Qualifier.Application.Database.Scope.Commands.UpdateScope
{
    public interface IUpdateScopeCommand
    {
        Task<Object> Execute(UpdateScopeDto model, int id);
    }
}

