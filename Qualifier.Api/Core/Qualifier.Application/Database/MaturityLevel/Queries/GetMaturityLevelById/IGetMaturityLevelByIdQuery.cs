
namespace Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById
{
    public interface IGetMaturityLevelByIdQuery
    {
        Task<Object> Execute(int maturityLevelId);
    }
}
