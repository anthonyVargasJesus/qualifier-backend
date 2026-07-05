using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId
{
    public class GetNotificationsByUserIdQuery : IGetNotificationsByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetNotificationsByUserIdQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int userId, int skip, int pageSize)
        {
            try
            {
                var query = _databaseService.Notification
                    .Where(n => n.userId == userId && (n.isDeleted == null || n.isDeleted == false));

                var totalRows = await query.CountAsync();
                var unreadCount = await query.CountAsync(n => !n.isRead);

                var data = await query
                    .OrderByDescending(n => n.creationDate)
                    .Skip(skip).Take(pageSize)
                    .Select(n => new GetNotificationsByUserIdDto
                    {
                        notificationId = n.notificationId,
                        title = n.title,
                        body = n.body,
                        type = n.type,
                        actionPlanId = n.actionPlanId,
                        breachId = n.breachId,
                        isRead = n.isRead,
                        creationDate = n.creationDate,
                    })
                    .ToListAsync();

                BaseResponseDto<GetNotificationsByUserIdDto> baseResponseDto = new BaseResponseDto<GetNotificationsByUserIdDto>();
                baseResponseDto.data = data;
                baseResponseDto.pagination = new
                {
                    totalRows = totalRows,
                    totalPages = Pagination.GetTotalPages(totalRows, pageSize),
                    unreadCount = unreadCount,
                };
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
