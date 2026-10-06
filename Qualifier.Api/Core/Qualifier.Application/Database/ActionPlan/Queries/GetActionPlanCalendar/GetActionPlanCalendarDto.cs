namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCalendar
{
    // Respuesta compuesta (no BaseResponseDto<T> plano): "items" va paginado para la grilla,
    // pero "statusSummary" (una fila por cada estado real de MAE_ACTION_PLAN_STATUS, con su
    // conteo mensual) se calcula sobre TODOS los planes de la evaluación, no solo la página
    // visible -- si no, el resumen mensual cambiaría según qué página estés mirando.
    //
    // No hay una fila "Avance %" fija: los estados (cuántos son, cómo se llaman, en qué orden)
    // salen del catálogo de la empresa tal cual está configurado -- si mañana agregan o
    // renombran un estado, esta pantalla no necesita ningún cambio.
    public class GetActionPlanCalendarResponseDto
    {
        public List<GetActionPlanCalendarItemDto> items { get; set; } = new();
        public Object? pagination { get; set; }
        public List<GetActionPlanCalendarStatusSummaryDto> statusSummary { get; set; } = new();
    }

    public class GetActionPlanCalendarItemDto
    {
        public int actionPlanId { get; set; }
        public string controlCode { get; set; } = string.Empty;
        public string controlName { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string? responsibleName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; } = string.Empty;
        public string statusAbbreviation { get; set; } = string.Empty;
        public string statusColor { get; set; } = string.Empty;
    }

    // monthCounts: 12 posiciones (0=Ene ... 11=Dic) -- cuántos planes en ESTE estado tienen
    // ese mes dentro de su rango de fechas.
    public class GetActionPlanCalendarStatusSummaryDto
    {
        public int statusId { get; set; }
        public string statusName { get; set; } = string.Empty;
        public string statusColor { get; set; } = string.Empty;
        public int[] monthCounts { get; set; } = new int[12];
    }
}
