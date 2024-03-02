namespace Qualifier.Application.Database.Indicator.Queries.GetIndicatorById
{
    public interface IGetIndicatorByIdQuery
    {
        Task<Object> Execute(int indicatorId);
    }
}

