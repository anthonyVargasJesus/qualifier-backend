namespace Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection
{
    public interface IUpdateDefaultSectionCommand
    {
        Task<Object> Execute(UpdateDefaultSectionDto model, int id);
    }
}

