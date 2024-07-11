namespace Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypeById
{
    public interface IGetMenaceTypeByIdQuery
    {
        Task<Object> Execute(int menaceTypeId);
    }
}

