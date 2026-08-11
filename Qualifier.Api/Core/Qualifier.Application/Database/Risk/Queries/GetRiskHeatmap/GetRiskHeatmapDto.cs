namespace Qualifier.Application.Database.Risk.Queries.GetRiskHeatmap
{
    public class GetRiskHeatmapDto
    {
        public int? evaluationId { get; set; }
        public string? evaluationDescription { get; set; }
        public int totalRisks { get; set; }

        // Cuántos riesgos no entraron a la grilla por no tener evaluación de riesgo (menace/
        // vulnerability level) registrada todavía — para que el oficial sepa que el mapa no
        // está "incompleto", solo que hay riesgos identificados pendientes de valorar.
        public int unassessedRisks { get; set; }

        public GetRiskHeatmapAxisDto axisX { get; set; } = new(); // Amenaza
        public GetRiskHeatmapAxisDto axisY { get; set; } = new(); // Vulnerabilidad

        public GetRiskHeatmapGridDto inherent { get; set; } = new();
        public GetRiskHeatmapGridDto residual { get; set; } = new();

        // "¿Mejoró el tratamiento?" — comparación de nivel inherente vs. nivel residual real,
        // riesgo por riesgo (mismo espíritu que "mejoraron/empeoraron" de Cumplimiento Histórico).
        public int reducedCount { get; set; }
        public int unchangedCount { get; set; }
        public int worsenedCount { get; set; }

        // Los N riesgos con mayor exposición residual real — la lista accionable: esto es lo
        // que el oficial se lleva a la reunión de riesgos.
        public List<GetRiskHeatmapTopRiskDto> topResidualRisks { get; set; } = new();

        public DateTime generatedAt { get; set; }
    }

    public class GetRiskHeatmapAxisDto
    {
        public string label { get; set; } = string.Empty;
        public decimal min { get; set; }
        public decimal max { get; set; }
        public List<string> binLabels { get; set; } = new();
    }

    public class GetRiskHeatmapGridDto
    {
        // matrix[row][col] — row = bin de vulnerabilidad (0 = más bajo), col = bin de amenaza
        // (0 = más bajo). Se recorre de abajo hacia arriba en el frontend para que "arriba a
        // la derecha" sea la esquina crítica, como cualquier matriz de riesgo estándar.
        public List<List<GetRiskHeatmapCellDto>> matrix { get; set; } = new();
        public List<GetRiskHeatmapLevelSummaryDto> byLevel { get; set; } = new();
    }

    public class GetRiskHeatmapCellDto
    {
        public int row { get; set; }
        public int col { get; set; }
        public int count { get; set; }
        public string? color { get; set; }
        public string? levelName { get; set; }
    }

    public class GetRiskHeatmapLevelSummaryDto
    {
        public string name { get; set; } = string.Empty;
        public string? color { get; set; }
        public int count { get; set; }
        public decimal percentage { get; set; }
    }

    public class GetRiskHeatmapTopRiskDto
    {
        public int riskId { get; set; }
        public string name { get; set; } = string.Empty;
        public string? activesInventoryName { get; set; }
        public decimal inherentValue { get; set; }
        public string? inherentLevel { get; set; }
        public string? inherentColor { get; set; }
        public decimal residualValue { get; set; }
        public string? residualLevel { get; set; }
        public string? residualColor { get; set; }
        public string controlSummary { get; set; } = string.Empty;
    }
}
