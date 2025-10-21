using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface IMacroprocessRepository
    {
        Task Delete(int id, int updateUserId);
        Task Update(int id, MacroprocessEntity entity);
    }
}


