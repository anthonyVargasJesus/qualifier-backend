namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.CreateConfidentialityLevel
{
    public interface ICreateConfidentialityLevelCommand
    {
        Task<Object> Execute(CreateConfidentialityLevelDto model);
    }
}

