

namespace Qualifier.Application.Database.MaturityLevel.Commands.DeleteMaturityLevel
{
    public interface IDeleteMaturityLevelCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}
