using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommand : IMarkNotificationAsReadCommand
    {
        private readonly IDatabaseService _databaseService;

        public MarkNotificationAsReadCommand(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int id, int userId)
        {
            try
            {
                // Filtra por userId también: un usuario no debe poder marcar como leída una
                // notificación de otro solo adivinando el id.
                var entity = await _databaseService.Notification
                    .FirstOrDefaultAsync(n => n.notificationId == id && n.userId == userId);

                if (entity == null)
                    return BaseApplication.getExceptionErrorResponse();

                entity.isRead = true;
                entity.updateDate = DateTime.UtcNow;
                await _databaseService.SaveAsync();

                return new { notificationId = entity.notificationId, isRead = entity.isRead };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
