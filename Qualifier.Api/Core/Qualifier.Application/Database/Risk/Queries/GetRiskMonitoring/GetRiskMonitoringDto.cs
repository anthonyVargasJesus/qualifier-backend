namespace Qualifier.Application.Database.Risk.Queries.GetRiskMonitoring
{
    public class GetRiskMonitoringRiskDto
    {
        public int riskId { get; set; }                     // Identificador del riesgo
        public string name { get; set; }                    // Nombre del riesgo
        public int activesInventoryId { get; set; }         // ID del activo afectado
        public string activesInventoryName { get; set; }    // Nombre del activo afectado

        public decimal initialRiskValue { get; set; }       // Nivel inicial numérico (ej. 28.02)
        public string initialRiskLevel { get; set; }        // Nivel inicial textual (Alto, Medio, Bajo)
        public string? initialRiskColor { get; set; }

        public decimal treatedRiskValue { get; set; }       // Riesgo tratado numérico
        public string? treatedRiskLevel { get; set; }        // Riesgo tratado textual
        public string? treatedRiskColor { get; set; }

        public decimal? residualRiskValue { get; set; }     // Riesgo residual real numérico
        public string? residualRiskLevel { get; set; }      // Riesgo residual real textual
        public string? residualRiskColor { get; set; }

        public int effectiveControls { get; set; }          // Controles efectivos (numerador)
        public int plannedControls { get; set; }            // Controles planificados (denominador)
        public string controlSummary { get; set; }          // Ejemplo: "2 / 3 ✅" o "1 / 3 ⚠️"

        public string? trend { get; set; }                   // Tendencia: "Subiendo", "Bajando", "Estable"
        public string status { get; set; }                  // Estado: "En seguimiento", "Cerrado", "Re-tratamiento"

        // Campos opcionales para interfaz o indicadores visuales
        public string trendIcon { get; set; }               // Ej. "📈", "📉", "➡️"
        public string riskColor { get; set; }               // Ej. "🔴", "🟠", "🟡", "🟢"
    }
}
