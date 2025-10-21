namespace Qualifier.Application.Database.MenaceType.Queries.GetAllMenaceTypesByCompanyId
{
    public interface IGetAllMenaceTypesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

