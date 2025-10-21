namespace Qualifier.Application.Database.Section.Commands.UpdateSection
{
    public interface IUpdateSectionCommand
    {
        Task<Object> Execute(UpdateSectionDto model, int id);
    }
}

