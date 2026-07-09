using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;

namespace Qualifier.Application.Database.Gap.Queries.GetEvaluacionBootstrap
{
    // Todo lo que la pestaña "Evaluación" (y, de rebote, el reporte SOA, que
    // reutiliza la misma pantalla sin volver a pedir nada) necesita para
    // cargar, en una sola respuesta — reemplaza 4 llamadas HTTP secuenciales
    // del frontend (evaluación actual, niveles de madurez, responsables,
    // cláusulas + controles) por una sola. requirements/groups son los mismos
    // objetos que ya devolvían /api/requirementEvaluation/all y
    // /api/controlEvaluation/all por separado, así que el frontend los
    // aplana con la misma lógica de siempre.
    public class GetEvaluacionBootstrapDto
    {
        public GetCurrentEvaluationDto evaluation { get; set; } = null!;
        public List<GetAllMaturityLevelsByCompanyIdDto> maturityLevels { get; set; } = new();
        public List<GetAllResponsiblesByStandardIdDto> responsibles { get; set; } = new();
        public List<GetRequirementEvaluationsByProcessRequirementDto> requirements { get; set; } = new();
        public List<GetControlEvaluationsByProcessControlGroupDto> groups { get; set; } = new();
    }
}
