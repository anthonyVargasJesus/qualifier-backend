namespace Qualifier.Application.Database.Evaluation.Queries.GetMaturityRadar
{
    public interface IGetMaturityRadarQuery
    {
        Task<Object> Execute(int companyId, int? standardId = null);
    }
}
