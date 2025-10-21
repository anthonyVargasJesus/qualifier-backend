namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.DeleteConfidentialityLevel
{
    public interface IDeleteConfidentialityLevelCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

