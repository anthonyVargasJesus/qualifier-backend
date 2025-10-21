namespace Qualifier.Application.Database.DefaultSection.Commands.CreateDefaultSection
{
    public interface ICreateDefaultSectionCommand
    {
        Task<Object> Execute(CreateDefaultSectionDto model);
    }
}

