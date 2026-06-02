using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Enumerations;

namespace Qualifier.Application.Database.User.Queries.GetUserActivity
{
    internal class GetUserActivityQuery : IGetUserActivityQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetUserActivityQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var users = await (from user in _databaseService.User
                                   join userState in _databaseService.UserState
                                       on user.userStateId equals userState.userStateId
                                   where (user.isDeleted == null || user.isDeleted == false)
                                         && user.companyId == companyId
                                   select new UserEntity
                                   {
                                       userId = user.userId,
                                       name = user.name,
                                       firstName = user.firstName,
                                       lastName = user.lastName,
                                       email = user.email,
                                       image = user.image,
                                       lastAccess = user.lastAccess,
                                       userStateId = user.userStateId,
                                       userState = new UserStateEntity
                                       {
                                           name = userState.name,
                                           value = userState.value,
                                       },
                                   }).ToListAsync();

                var userIds = users.Select(x => x.userId).ToList();

                var roles = await (from ru in _databaseService.RoleInUser
                                   join r in _databaseService.Role on ru.roleId equals r.roleId
                                   where userIds.Contains(ru.userId)
                                   select new { ru.userId, roleName = r.name }).ToListAsync();

                var today = DateTime.UtcNow.Date;

                var userDtos = users.Select(user =>
                {
                    var userRoles = roles.Where(x => x.userId == user.userId).Select(x => x.roleName).ToList();
                    var rolesText = userRoles.Count > 2
                        ? string.Join(", ", userRoles.Take(2)) + "..."
                        : userRoles.Count == 0 ? "Sin roles asignados"
                        : string.Join(", ", userRoles);

                    int? daysSince = user.lastAccess.HasValue
                        ? (int)(today - user.lastAccess.Value.Date).TotalDays
                        : null;

                    string bucket = daysSince == null ? "Nunca"
                        : daysSince == 0 ? "Hoy"
                        : daysSince <= 7 ? "Última semana"
                        : daysSince <= 30 ? "Último mes"
                        : daysSince <= 60 ? "Hace más de 30 días"
                        : "Hace más de 60 días";

                    return new GetUserActivityUserDto
                    {
                        userId = user.userId,
                        name = user.name,
                        firstName = user.firstName,
                        lastName = user.lastName,
                        email = user.email,
                        image = user.image,
                        rolesText = rolesText,
                        userStateName = user.userState?.name,
                        userStateValue = user.userState?.value ?? 0,
                        lastAccess = user.lastAccess,
                        daysSinceLastAccess = daysSince,
                        accessBucket = bucket,
                    };
                })
                .OrderBy(x => x.daysSinceLastAccess == null ? int.MaxValue : x.daysSinceLastAccess)
                .ToList();

                var stateChart = userDtos
                    .GroupBy(x => x.userStateName ?? "Sin estado")
                    .Select(g => new GetUserActivityPieDto { name = g.Key, value = g.Count() })
                    .ToList();

                var bucketOrder = new[] { "Hoy", "Última semana", "Último mes", "Hace más de 30 días", "Hace más de 60 días", "Nunca" };
                var accessChart = bucketOrder
                    .Select(b => new GetUserActivityPieDto { name = b, value = userDtos.Count(x => x.accessBucket == b) })
                    .Where(x => x.value > 0)
                    .ToList();

                var roleChart = roles
                    .GroupBy(x => x.roleName)
                    .Select(g => new GetUserActivityBarDto { name = g.Key, value = g.Count() })
                    .OrderByDescending(x => x.value)
                    .ToList();

                return new GetUserActivityDto
                {
                    totalUsers = userDtos.Count,
                    totalActive = userDtos.Count(x => x.userStateValue == (int)EnumUserState.Active),
                    totalInactive = userDtos.Count(x => x.userStateValue != (int)EnumUserState.Active),
                    neverAccessed = userDtos.Count(x => x.lastAccess == null),
                    notAccessedIn30Days = userDtos.Count(x => x.daysSinceLastAccess > 30),
                    users = userDtos,
                    userStateChart = stateChart,
                    accessFrequencyChart = accessChart,
                    roleDistributionChart = roleChart,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
