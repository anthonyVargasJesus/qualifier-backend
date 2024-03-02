

namespace Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel
{
    public interface ICreateMaturityLevelCommand
    {
        Task<Object> Execute(CreateMaturityLevelDto model);
    }
}
