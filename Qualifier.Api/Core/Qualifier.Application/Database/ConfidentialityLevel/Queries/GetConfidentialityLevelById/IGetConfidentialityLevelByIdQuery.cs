namespace Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelById
{
    public interface IGetConfidentialityLevelByIdQuery
    {
        Task<Object> Execute(int confidentialityLevelId);
    }
}

