namespace Qualifier.Application.Database.Notifications.Commands.MarkAllNotificationsAsRead
{
    public interface IMarkAllNotificationsAsReadCommand
    {
        Task<Object> Execute(int userId);
    }
}
