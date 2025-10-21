namespace Qualifier.Application.Database.Section.Commands.CreateSection
{
    public interface ICreateSectionCommand
    {
        Task<Object> Execute(CreateSectionDto model);
    }
}

