using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IResidualRiskRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, ResidualRiskEntity entity);
    }
}


