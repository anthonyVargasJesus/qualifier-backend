using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Notifications.Commands.MarkAllNotificationsAsRead
{
    public class MarkAllNotificationsAsReadCommand : IMarkAllNotificationsAsReadCommand
    {
        private readonly IDatabaseService _databaseService;

        public MarkAllNotificationsAsReadCommand(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int userId)
        {
            try
            {
                var updated = await _databaseService.Notification
                    .Where(n => n.userId == userId && !n.isRead && (n.isDeleted == null || n.isDeleted == false))
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(n => n.isRead, true)
                        .SetProperty(n => n.updateDate, DateTime.UtcNow));

                return new { updated };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
