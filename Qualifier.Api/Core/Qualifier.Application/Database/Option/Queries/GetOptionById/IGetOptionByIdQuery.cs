namespace Qualifier.Application.Database.Option.Queries.GetOptionById
{
    public interface IGetOptionByIdQuery
    {
        Task<Object> Execute(int optionId);
    }
}

