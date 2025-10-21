namespace Qualifier.Application.Database.DefaultSection.Commands.DeleteDefaultSection
{
    public interface IDeleteDefaultSectionCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

