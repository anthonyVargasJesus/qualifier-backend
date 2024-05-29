namespace Qualifier.Application.Database.ImpactValuation.Commands.DeleteImpactValuation
{
    public interface IDeleteImpactValuationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

