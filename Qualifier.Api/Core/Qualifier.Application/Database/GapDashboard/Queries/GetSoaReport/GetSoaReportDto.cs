namespace Qualifier.Application.Database.GapDashboard.Queries.GetSoaReport
{
    public class GetSoaReportDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public string standardName { get; set; } = string.Empty;
        public List<GetSoaReportItemDto> controls { get; set; } = new();
    }

    public class GetSoaReportItemDto
    {
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string theme { get; set; } = string.Empty;

        // null = todavía sin evaluar ("Pendiente"); true/false = aplica o no.
        public bool? aplica { get; set; }
        public string implementacion { get; set; } = string.Empty;
        public string justificacion { get; set; } = string.Empty;
    }
}
