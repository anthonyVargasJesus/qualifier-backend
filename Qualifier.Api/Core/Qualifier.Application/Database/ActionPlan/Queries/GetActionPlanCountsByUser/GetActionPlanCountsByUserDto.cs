namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByUser
{
    public class GetActionPlanCountsByUserDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public List<GetActionPlanCountsByUserItemDto> users { get; set; } = new();
    }

    public class GetActionPlanCountsByUserItemDto
    {
        public int userId { get; set; }
        public string displayName { get; set; } = string.Empty;
        public string? image { get; set; }
        public int pendientes { get; set; }
        public int enCurso { get; set; }
        public int completadas { get; set; }
        public int vencidas { get; set; }
        public int total { get; set; }
    }
}
