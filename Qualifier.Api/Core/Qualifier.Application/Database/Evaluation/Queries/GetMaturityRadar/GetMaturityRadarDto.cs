namespace Qualifier.Application.Database.Evaluation.Queries.GetMaturityRadar
{
    public class GetMaturityRadarDto
    {
        public int? standardId { get; set; }
        public string? standardName { get; set; }
        public int? currentEvaluationId { get; set; }
        public string? currentEvaluationDescription { get; set; }
        public int? previousEvaluationId { get; set; }
        public string? previousEvaluationDescription { get; set; }

        // Promedio de las tasas POR DOMINIO (no ítem por ítem) — a propósito distinto del "%
        // Controles" que ya muestran el dashboard de Inicio y Cumplimiento Histórico (ese
        // promedia todos los controles evaluados por igual, sin importar de qué dominio son).
        // Acá cada dominio pesa lo mismo sin importar cuántos controles tenga — es la métrica
        // correcta para "en promedio, ¿qué tan parejo está el SGSI entre dominios?"; puede no
        // coincidir con el % Controles si los dominios tienen tamaños muy distintos, y eso es
        // esperado, no un error.
        public decimal? overallCurrentRate { get; set; }
        public decimal? overallPreviousRate { get; set; }

        public List<GetMaturityRadarDomainDto> domains { get; set; } = new();
        public GetMaturityRadarDomainDto? weakestDomain { get; set; }
        public GetMaturityRadarDomainDto? strongestDomain { get; set; }
    }

    public class GetMaturityRadarDomainDto
    {
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public decimal? currentRate { get; set; }
        public decimal? previousRate { get; set; }
        public decimal? delta { get; set; }
        public int totalControls { get; set; }
        public int evaluatedControls { get; set; }
    }
}
