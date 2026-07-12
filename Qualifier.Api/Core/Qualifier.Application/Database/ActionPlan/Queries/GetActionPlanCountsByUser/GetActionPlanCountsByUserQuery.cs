using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByUser
{
    // Reporte "carga por responsable" para el dueño del requisito: quién tiene qué planes
    // de acción de la evaluación actual y en qué estado — no reutiliza /actionPlan/progress
    // porque ese endpoint ya alimenta un dashboard existente (con gráficos que este reporte
    // no necesita) y no expone la foto del usuario.
    internal class GetActionPlanCountsByUserQuery : IGetActionPlanCountsByUserQuery
    {
        private const string PENDIENTE = "PEN";
        private const string EN_CURSO = "PROG";
        private const string COMPLETADO = "COMP";
        private const string CERRADO = "CERR";

        private readonly IDatabaseService _databaseService;

        public GetActionPlanCountsByUserQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var currentEvaluation = await (
                    from eval in _databaseService.Evaluation
                    where (eval.isDeleted == null || eval.isDeleted == false)
                        && eval.companyId == companyId
                        && eval.isCurrent
                    select new { eval.evaluationId }
                ).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetActionPlanCountsByUserDto { hasCurrentEvaluation = false };

                var plans = await (
                    from ap in _databaseService.ActionPlan
                    join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                    join user in _databaseService.User on ap.userId equals user.userId into userJoin
                    from user in userJoin.DefaultIfEmpty()
                    where (ap.isDeleted == null || ap.isDeleted == false)
                        && ap.companyId == companyId
                        && ap.evaluationId == currentEvaluation.evaluationId
                        && ap.userId != null
                    select new
                    {
                        userId = ap.userId!.Value,
                        userName = user == null ? "Sin asignar" : (user.name ?? "") + " " + (user.firstName ?? ""),
                        userImage = user == null ? null : user.image,
                        ap.dueDate,
                        statusAbbreviation = status.abbreviation,
                    }
                ).ToListAsync();

                var today = DateTime.UtcNow.Date;
                bool IsCompleted(string abbreviation) => abbreviation == COMPLETADO || abbreviation == CERRADO;

                var users = plans
                    .GroupBy(p => p.userId)
                    .Select(g =>
                    {
                        var items = g.ToList();
                        var first = items[0];
                        return new GetActionPlanCountsByUserItemDto
                        {
                            userId = g.Key,
                            displayName = first.userName,
                            image = first.userImage,
                            pendientes = items.Count(p => p.statusAbbreviation == PENDIENTE),
                            enCurso = items.Count(p => p.statusAbbreviation == EN_CURSO),
                            completadas = items.Count(p => IsCompleted(p.statusAbbreviation)),
                            vencidas = items.Count(p => p.dueDate.Date < today && !IsCompleted(p.statusAbbreviation)),
                            total = items.Count,
                        };
                    })
                    .OrderByDescending(u => u.vencidas)
                    .ThenByDescending(u => u.pendientes)
                    .ThenBy(u => u.displayName)
                    .ToList();

                return new GetActionPlanCountsByUserDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    users = users,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
