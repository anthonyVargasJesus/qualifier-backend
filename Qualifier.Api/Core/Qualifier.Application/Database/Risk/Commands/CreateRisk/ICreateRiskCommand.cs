namespace Qualifier.Application.Database.Risk.Commands.CreateRisk
{
    public interface ICreateRiskCommand
    {
        Task<Object> Execute(CreateRiskDto model);
    }
}

