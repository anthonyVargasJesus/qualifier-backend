namespace Qualifier.Application.Database.Scope.Commands.CreateScope
{
    public interface ICreateScopeCommand
    {
        Task<Object> Execute(CreateScopeDto model);
    }
}

