using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
public interface IOptionRepository
{
Task Delete(int id, int updateUserId);
Task Update(int id, OptionEntity entity);
}
}

//services.AddScoped<IOptionRepository, OptionRepository>();
