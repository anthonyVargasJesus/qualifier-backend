namespace Qualifier.Application.Database.GapDashboard.Queries.GetDashboardONPE
{
    public class GetDashboardONPEDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public string standardName { get; set; } = string.Empty;
        // "Evaluación - Noviembre 2025 (SoA data de prueba)" -- para identificar de qué
        // evaluación es este dashboard en el header de arriba (app-gap-breach-map), igual que ya
        // se identifica en Análisis de Brechas / Plan de acción.
        public string evaluationDescription { get; set; } = string.Empty;

        // Promedio del Nivel de madurez (0-5) de todos los controles del Anexo A con nota
        // numérica -- celda D7 "Madurez General" del Excel FM03-GPP_GC SoA, hoja "Dashboard".
        public decimal madurezGeneral { get; set; }
        // D7/Meta -- celda F7 del Excel, como fracción (0.59), no como "59" -- el front la formatea a %.
        public decimal madurezGeneralPercent { get; set; }

        public List<GetDashboardONPEGroupDto> grupos { get; set; } = new();

        // Detalle control por control -- celdas B18:D65 / F18:H65 del Excel ("Grupo | Control |
        // MADUREZ"), acá como una sola lista plana ya ordenada (por grupo y luego por código),
        // no las dos columnas lado a lado del Excel -- eso era solo para que entrara en una
        // página impresa, no tiene significado propio.
        public List<GetDashboardONPEControlDto> controles { get; set; } = new();
    }

    public class GetDashboardONPEGroupDto
    {
        // "Controles Organizacionales" / "Controles de Personas" / "Controles Físicos" /
        // "Controles Tecnológicos" -- mismo texto que ControlGroup.name (theme). El MAYÚSCULAS
        // del Excel es solo estilo, se aplica en CSS (text-transform), no acá.
        public string grupo { get; set; } = string.Empty;
        public decimal actual { get; set; }
        public decimal meta { get; set; }
        public decimal actualPercent { get; set; }
    }

    public class GetDashboardONPEControlDto
    {
        public string grupo { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        // null = todavía sin evaluar ("Pendiente") -- a diferencia del promedio de "grupos"
        // (donde un pendiente cuenta como 0), acá se manda tal cual para que el front pueda
        // distinguir "Pendiente" de "0 - No implementado" en vez de mostrar los dos igual.
        public decimal? madurez { get; set; }
        // Color ya resuelto del nivel de madurez asignado (MAE_MATURITY_LEVEL.C_COLOR) -- mismo
        // color que usa el resto de la app para ese nivel, no una paleta nueva por pantalla.
        public string? madurezColor { get; set; }
        // Nombre del nivel asignado (p.ej. "Gestionado", "Definido", "No aplicable") o
        // "Pendiente" si el control todavía no tiene evaluación -- mismo texto que ya usa
        // GetSoaReportQuery en "implementacion" (viene de ItemState.estado), no se recalcula.
        public string nivel { get; set; } = string.Empty;
        public bool isNotApplicable { get; set; }
    }
}
