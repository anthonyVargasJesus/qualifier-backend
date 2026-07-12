using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Breach
{
    // Resuelve si un nivel de madurez debe generar una brecha (y con qué severidad) y evita
    // duplicar brechas abiertas para el mismo ítem evaluado. Compartido entre los commands de
    // Create/Update de ControlEvaluation y RequirementEvaluation para que ambos flujos no puedan
    // divergir en esta regla de negocio.
    public class MaturityLevelBreachGenerator
    {
        public const int BREACH_STATUS_OPEN = 1;
        public const string BREACH_TYPE_REQUIREMENT = "1";
        public const string BREACH_TYPE_CONTROL = "2";

        private readonly IDatabaseService _databaseService;

        public MaturityLevelBreachGenerator(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int?> ResolveBreachSeverityIdAsync(int? maturityLevelId)
        {
            if (maturityLevelId == null)
                return null;

            var maturityLevel = await _databaseService.MaturityLevel
                .Where(m => m.maturityLevelId == maturityLevelId)
                .FirstOrDefaultAsync();

            if (maturityLevel == null || !maturityLevel.generatesBreach || maturityLevel.breachSeverityId == null)
                return null;

            return maturityLevel.breachSeverityId;
        }

        public async Task<BreachEntity?> GetOpenBreachAsync(int evaluationId, string type, int controlId, int requirementId)
        {
            return await _databaseService.Breach.FirstOrDefaultAsync(b =>
                (b.isDeleted == null || b.isDeleted == false)
                && b.breachStatusId == BREACH_STATUS_OPEN
                && b.evaluationId == evaluationId
                && b.type == type
                && (type == BREACH_TYPE_CONTROL ? b.controlId == controlId : b.requirementId == requirementId));
        }

        // Si ya hay una brecha abierta para el mismo ítem y el nivel vigente amerita otra
        // severidad, se actualiza esa brecha en vez de crear una duplicada.
        public async Task SyncOpenBreachSeverityAsync(BreachEntity existingBreach, int severityId)
        {
            if (existingBreach.breachSeverityId == severityId)
                return;

            existingBreach.breachSeverityId = severityId;
            await _databaseService.SaveAsync();
        }
    }
}
