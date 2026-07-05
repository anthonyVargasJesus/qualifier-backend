namespace Qualifier.Application.Database.Notifications.Commands.MarkNotificationAsRead
{
    public interface IMarkNotificationAsReadCommand
    {
        Task<Object> Execute(int id, int userId);
    }
}
