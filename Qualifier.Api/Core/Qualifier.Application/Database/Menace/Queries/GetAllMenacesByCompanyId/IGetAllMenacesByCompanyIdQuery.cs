namespace Qualifier.Application.Database.Menace.Queries.GetAllMenacesByCompanyId
{
    public interface IGetAllMenacesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

