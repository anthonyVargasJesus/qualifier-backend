namespace Qualifier.Application.Database.ImpactValuation.Commands.UpdateImpactValuation
{
    public interface IUpdateImpactValuationCommand
    {
        Task<Object> Execute(UpdateImpactValuationDto model, int id);
    }
}

