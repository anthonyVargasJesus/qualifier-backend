namespace Qualifier.Application.Database.ImpactValuation.Commands.CreateImpactValuation
{
    public interface ICreateImpactValuationCommand
    {
        Task<Object> Execute(CreateImpactValuationDto model);
    }
}

