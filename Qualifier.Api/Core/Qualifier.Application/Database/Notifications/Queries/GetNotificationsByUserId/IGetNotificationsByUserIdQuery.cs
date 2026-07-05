namespace Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId
{
    public interface IGetNotificationsByUserIdQuery
    {
        Task<Object> Execute(int userId, int skip, int pageSize);
    }
}
