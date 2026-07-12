namespace Qualifier.Application.Database.GapDashboard.Queries.GetMissingEvidenceReport
{
    public class GetMissingEvidenceReportDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public int evaluatedItemsCount { get; set; }
        public int missingEvidenceCount { get; set; }
        public List<GetMissingEvidenceItemDto> items { get; set; } = new();
    }

    public class GetMissingEvidenceItemDto
    {
        public string tipo { get; set; } = string.Empty;
        public int itemId { get; set; }
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string theme { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
