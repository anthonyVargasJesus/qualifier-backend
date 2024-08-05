using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, CompanyEntity entity);
    }
}


