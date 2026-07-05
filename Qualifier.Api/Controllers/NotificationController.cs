using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Notifications.Commands.MarkAllNotificationsAsRead;
using Qualifier.Application.Database.Notifications.Commands.MarkNotificationAsRead;
using Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController(
    IGetNotificationsByUserIdQuery getByUserQuery,
    IMarkNotificationAsReadCommand markAsReadCommand,
    IMarkAllNotificationsAsReadCommand markAllAsReadCommand
) : ApiBaseController
{
    // Siempre las del usuario logeado (JWT) — nunca las de otro usuario.
    [HttpGet]
    public async Task<IActionResult> Get(int skip = 0, int pageSize = 50)
    {
        var res = await getByUserQuery.Execute(UserId, skip, pageSize);
        return ProcessResponse(res);
    }

    [HttpPut("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var res = await markAsReadCommand.Execute(id, UserId);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var res = await markAllAsReadCommand.Execute(UserId);
        return ProcessResponse(res, wrapWithData: true);
    }
}
