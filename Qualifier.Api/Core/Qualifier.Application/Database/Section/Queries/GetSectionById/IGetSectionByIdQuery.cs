namespace Qualifier.Application.Database.Section.Queries.GetSectionById
{
    public interface IGetSectionByIdQuery
    {
        Task<Object> Execute(int sectionId);
    }
}

