namespace Qualifier.Application.Database.Section.Commands.DeleteSection
{
    public interface IDeleteSectionCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

