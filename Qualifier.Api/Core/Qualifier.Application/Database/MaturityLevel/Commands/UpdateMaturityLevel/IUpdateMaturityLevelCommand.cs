
using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;

namespace Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel
{
    public interface IUpdateMaturityLevelCommand
    {
        Task<Object> Execute(UpdateMaturityLevelDto model, int id);
    }
}
