namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel
{
    public interface IUpdateConfidentialityLevelCommand
    {
        Task<Object> Execute(UpdateConfidentialityLevelDto model, int id);
    }
}

