namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById
{
    public interface IGetDefaultSectionByIdQuery
    {
        Task<Object> Execute(int defaultSectionId);
    }
}

