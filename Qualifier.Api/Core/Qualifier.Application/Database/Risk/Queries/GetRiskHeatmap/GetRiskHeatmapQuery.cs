using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRiskHeatmap
{
    // Mapa de calor de riesgos: Amenaza (eje X) x Vulnerabilidad (eje Y), en dos vistas —
    // "Inherente" (evaluación cruda, sin tratamiento) y "Residual" (exposición real hoy,
    // descontando el avance efectivo de los controles). Reutiliza el mismo cálculo de riesgo
    // residual que ya usa GetRiskMonitoringQuery (para que los números coincidan con
    // "Seguimiento de Riesgos"), pero con una diferencia deliberada: acá se cubren TODOS los
    // riesgos de la evaluación (no solo los que ya están "en seguimiento"), así que un riesgo
    // recién identificado y sin tratamiento aún NO puede quedar en 0 (eso lo pintaría como
    // "ya mitigado" cuando en realidad nadie lo ha tocado) — ver ComputeResidual() abajo.
    internal class GetRiskHeatmapQuery : IGetRiskHeatmapQuery
    {
        private const int NUM_BINS = 5;

        private readonly IDatabaseService _databaseService;

        public GetRiskHeatmapQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId, int? evaluationId = null)
        {
            try
            {
                int? resolvedEvaluationId = evaluationId;
                string? evaluationDescription = null;

                if (resolvedEvaluationId == null)
                {
                    var current = await (from eval in _databaseService.Evaluation
                                          where (eval.isDeleted == null || eval.isDeleted == false)
                                                && eval.companyId == companyId && eval.isCurrent
                                          select new { eval.evaluationId, eval.description }).FirstOrDefaultAsync();
                    resolvedEvaluationId = current?.evaluationId;
                    evaluationDescription = current?.description;
                }
                else
                {
                    evaluationDescription = await (from eval in _databaseService.Evaluation
                                                    where eval.evaluationId == resolvedEvaluationId
                                                    select eval.description).FirstOrDefaultAsync();
                }

                var dto = new GetRiskHeatmapDto
                {
                    evaluationId = resolvedEvaluationId,
                    evaluationDescription = evaluationDescription,
                    generatedAt = DateTime.UtcNow,
                };

                if (resolvedEvaluationId == null)
                    return dto;

                // Catálogo de niveles de riesgo de la empresa, ordenado de menos a más severo
                // (por su límite inferior) — se usa tanto para "en qué banda cae este valor"
                // como para el ranking mejoró/empeoró.
                var riskLevels = await (from rl in _databaseService.RiskLevel
                                         where (rl.isDeleted == null || rl.isDeleted == false) && rl.companyId == companyId
                                         orderby rl.minimum
                                         select rl).ToListAsync();

                // Mismo join que GetRiskMonitoringQuery (activo, amenaza/vulnerabilidad,
                // evaluación de riesgo, tratamiento planificado, controles implementados) pero
                // sin filtrar por estado ni paginar: acá interesa el panorama completo de la
                // evaluación actual, no solo los riesgos "en seguimiento".
                var rows = await (from risk in _databaseService.Risk

                                   join assessment in _databaseService.RiskAssessment
                                       .Where(a => a.isDeleted == null || a.isDeleted == false)
                                   on risk.riskId equals assessment.riskId into assessmentJoin
                                   from riskAssessment in assessmentJoin.DefaultIfEmpty()

                                   join treatment in _databaseService.RiskTreatment
                                       .Where(t => t.isDeleted == null || t.isDeleted == false)
                                   on risk.riskId equals treatment.riskId into treatmentJoin
                                   from riskTreatment in treatmentJoin.DefaultIfEmpty()

                                   let plannedControls = _databaseService.ControlImplementation
                                       .Count(ci => ci.riskId == risk.riskId && (ci.isDeleted == null || ci.isDeleted == false))
                                   let effectiveControls = _databaseService.ControlImplementation
                                       .Count(ci => ci.riskId == risk.riskId && ci.isImplemented == true && (ci.isDeleted == null || ci.isDeleted == false))

                                   where (risk.isDeleted == null || risk.isDeleted == false)
                                         && risk.evaluationId == resolvedEvaluationId

                                   select new
                                   {
                                       risk.riskId,
                                       risk.name,
                                       risk.activesInventoryName,
                                       riskAssessment,
                                       riskTreatment,
                                       plannedControls,
                                       effectiveControls,
                                   })
                                   .Distinct()
                                   .ToListAsync();

                dto.totalRisks = rows.Count;

                var assessed = rows.Where(r => r.riskAssessment != null).ToList();
                dto.unassessedRisks = rows.Count - assessed.Count;

                if (!assessed.Any())
                {
                    dto.axisX = new GetRiskHeatmapAxisDto { label = "Nivel de amenaza" };
                    dto.axisY = new GetRiskHeatmapAxisDto { label = "Nivel de vulnerabilidad" };
                    return dto;
                }

                // Bandas del catálogo de la empresa (ej. BAJO 1–8, MEDIO 9–19, ALTO 20–45) casi
                // siempre tienen huecos entre el máximo de una y el mínimo de la siguiente
                // (19→20 en el ejemplo) porque se cargan con enteros redondos, pero los valores
                // reales son decimales (CID * amenaza * vulnerabilidad) y sí caen ahí (ej.
                // 19.24). Si eso ocurre, redondear hacia la banda MÁS severa (nunca a la más
                // baja): es preferible advertir de más a un riesgo límite que mostrarlo como
                // "bajo" por un hueco de calibración del catálogo.
                RiskLevelEntity? BandFor(decimal value)
                {
                    var exact = riskLevels.FirstOrDefault(rl => value >= rl.minimum && value <= (rl.maximum ?? decimal.MaxValue));
                    if (exact != null) return exact;
                    if (!riskLevels.Any()) return null;
                    if (value < riskLevels[0].minimum) return riskLevels[0];
                    return riskLevels.FirstOrDefault(rl => rl.minimum > value) ?? riskLevels[^1];
                }

                int RankOf(RiskLevelEntity? level) => level == null ? -1 : riskLevels.IndexOf(level);

                // Un punto por riesgo: coordenadas inherentes tal cual quedaron en la
                // evaluación de riesgo, y coordenadas/valor residual calculados por
                // ComputeResidual (ver comentario de clase).
                var points = assessed.Select(r =>
                {
                    var a = r.riskAssessment!;
                    var inherentLevel = BandFor(a.riskAssessmentValue);

                    var (residualValue, residualVulnerability, residualLevel) = ComputeResidual(
                        a, r.riskTreatment, r.plannedControls, r.effectiveControls, riskLevels, BandFor);

                    return new
                    {
                        r.riskId,
                        r.name,
                        r.activesInventoryName,
                        x = a.menaceLevelValue,
                        yInherent = a.vulnerabilityLevelValue,
                        inherentValue = a.riskAssessmentValue,
                        inherentLevel,
                        yResidual = residualVulnerability,
                        residualValue,
                        residualLevel,
                        plannedControls = r.plannedControls,
                        effectiveControls = r.effectiveControls,
                    };
                }).ToList();

                // Amenaza: mismo valor en ambas vistas (no es algo que la organización trate
                // con controles internos, así que el eje X no se mueve entre inherente/residual).
                decimal minX = points.Min(p => p.x);
                decimal maxX = points.Max(p => p.x);

                // Vulnerabilidad: se toma el rango combinado inherente+residual, para que ambas
                // grillas compartan exactamente los mismos límites de celda y un mismo riesgo
                // sea comparable de una vista a otra (se "mueve" dentro de la misma grilla).
                decimal minY = Math.Min(points.Min(p => p.yInherent), points.Min(p => p.yResidual));
                decimal maxY = Math.Max(points.Max(p => p.yInherent), points.Max(p => p.yResidual));

                dto.axisX = BuildAxis("Nivel de amenaza", minX, maxX);
                dto.axisY = BuildAxis("Nivel de vulnerabilidad", minY, maxY);

                dto.inherent = BuildGrid(
                    points.Select(p => (p.x, p.yInherent, p.inherentLevel)).ToList(),
                    minX, maxX, minY, maxY, riskLevels);

                dto.residual = BuildGrid(
                    points.Select(p => (p.x, p.yResidual, p.residualLevel)).ToList(),
                    minX, maxX, minY, maxY, riskLevels);

                foreach (var p in points)
                {
                    var inherentRank = RankOf(p.inherentLevel);
                    var residualRank = RankOf(p.residualLevel);
                    if (residualRank < 0 || inherentRank < 0) continue;
                    if (residualRank < inherentRank) dto.reducedCount++;
                    else if (residualRank > inherentRank) dto.worsenedCount++;
                    else dto.unchangedCount++;
                }

                dto.topResidualRisks = points
                    .OrderByDescending(p => p.residualValue)
                    .Take(10)
                    .Select(p => new GetRiskHeatmapTopRiskDto
                    {
                        riskId = p.riskId,
                        name = p.name,
                        activesInventoryName = p.activesInventoryName,
                        inherentValue = p.inherentValue,
                        inherentLevel = p.inherentLevel?.name,
                        inherentColor = p.inherentLevel?.color,
                        residualValue = p.residualValue,
                        residualLevel = p.residualLevel?.name,
                        residualColor = p.residualLevel?.color,
                        controlSummary = $"{p.effectiveControls} / {p.plannedControls}"
                            + (p.plannedControls > 0 && p.effectiveControls == p.plannedControls ? " ✅" : p.plannedControls > 0 ? " ⚠️" : ""),
                    }).ToList();

                return dto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        // Valor/vulnerabilidad residual real de HOY: interpola entre lo inherente y lo
        // planificado (RiskTreatment) según qué proporción de los controles planificados ya
        // están efectivamente implementados. A diferencia de GetRiskMonitoringQuery (que
        // solo mira riesgos ya "en seguimiento", donde siempre hay tratamiento y controles
        // asociados), acá puede llegar un riesgo recién identificado sin tratamiento — para
        // ese caso el residual real es igual al inherente (nadie lo ha mitigado todavía),
        // nunca 0: 0 significaría "riesgo eliminado", que sería falso y engañoso para el
        // oficial de cumplimiento.
        private static (decimal value, decimal vulnerability, RiskLevelEntity? level) ComputeResidual(
            RiskAssessmentEntity assessment,
            RiskTreatmentEntity? treatment,
            int plannedControls,
            int effectiveControls,
            List<RiskLevelEntity> riskLevels,
            Func<decimal, RiskLevelEntity?> bandFor)
        {
            if (treatment == null || plannedControls <= 0)
                return (assessment.riskAssessmentValue, assessment.vulnerabilityLevelValue, bandFor(assessment.riskAssessmentValue));

            decimal ratio = (decimal)effectiveControls / plannedControls;
            decimal targetValue = treatment.riskAssessmentValue ?? assessment.riskAssessmentValue;
            decimal targetVulnerability = treatment.vulnerabilityLevelValue ?? assessment.vulnerabilityLevelValue;

            decimal residualValue = assessment.riskAssessmentValue - ratio * (assessment.riskAssessmentValue - targetValue);
            decimal residualVulnerability = assessment.vulnerabilityLevelValue - ratio * (assessment.vulnerabilityLevelValue - targetVulnerability);
            if (residualVulnerability < 0) residualVulnerability = 0;

            return (residualValue, residualVulnerability, bandFor(residualValue));
        }

        private static GetRiskHeatmapAxisDto BuildAxis(string label, decimal min, decimal max)
        {
            decimal span = max - min;
            if (span <= 0) span = 1;
            decimal binWidth = span / NUM_BINS;

            var axis = new GetRiskHeatmapAxisDto { label = label, min = min, max = max };
            for (int i = 0; i < NUM_BINS; i++)
            {
                decimal low = min + i * binWidth;
                decimal high = min + (i + 1) * binWidth;
                axis.binLabels.Add($"{Math.Round(low, 1)} – {Math.Round(high, 1)}");
            }
            return axis;
        }

        private static int BinIndex(decimal value, decimal min, decimal max)
        {
            decimal span = max - min;
            if (span <= 0) return NUM_BINS / 2;
            decimal binWidth = span / NUM_BINS;
            int index = (int)Math.Floor((double)((value - min) / binWidth));
            return Math.Clamp(index, 0, NUM_BINS - 1);
        }

        private static GetRiskHeatmapGridDto BuildGrid(
            List<(decimal x, decimal y, RiskLevelEntity? level)> points,
            decimal minX, decimal maxX, decimal minY, decimal maxY,
            List<RiskLevelEntity> riskLevels)
        {
            var grid = new GetRiskHeatmapGridDto();

            // Celdas vacías por defecto: color de fondo neutro (el frontend ya cae a un gris
            // claro cuando color es null). Se rellenan solo con lo que realmente hay ahí, no
            // con una estimación — un cálculo posicional (amenaza_media x vulnerabilidad_media
            // x algún CID "representativo") resultó engañoso: el CID (valoración del activo)
            // varía mucho riesgo a riesgo y no depende de la posición en la grilla, así que dos
            // riesgos en la misma celda pueden ser de niveles muy distintos. Mejor mostrar el
            // peor nivel real presente que inventar uno "típico".
            var worstPerCell = new RiskLevelEntity?[NUM_BINS, NUM_BINS];
            for (int row = 0; row < NUM_BINS; row++)
            {
                var rowCells = new List<GetRiskHeatmapCellDto>();
                for (int col = 0; col < NUM_BINS; col++)
                    rowCells.Add(new GetRiskHeatmapCellDto { row = row, col = col, count = 0 });
                grid.matrix.Add(rowCells);
            }

            foreach (var p in points)
            {
                int col = BinIndex(p.x, minX, maxX);
                int row = BinIndex(p.y, minY, maxY);
                grid.matrix[row][col].count++;

                // "Peor caso manda": solo reemplaza el nivel ya asignado a la celda si el nuevo
                // es más severo (índice más alto en riskLevels, ordenado ascendente).
                if (p.level != null && (worstPerCell[row, col] == null || riskLevels.IndexOf(p.level) > riskLevels.IndexOf(worstPerCell[row, col]!)))
                    worstPerCell[row, col] = p.level;
            }

            for (int row = 0; row < NUM_BINS; row++)
                for (int col = 0; col < NUM_BINS; col++)
                {
                    var worst = worstPerCell[row, col];
                    grid.matrix[row][col].color = worst?.color;
                    grid.matrix[row][col].levelName = worst?.name;
                }

            int total = points.Count;
            grid.byLevel = riskLevels
                .Select(rl => new { rl.name, rl.color, count = points.Count(p => p.level?.riskLevelId == rl.riskLevelId) })
                .Where(x => x.count > 0)
                .Select(x => new GetRiskHeatmapLevelSummaryDto
                {
                    name = x.name,
                    color = x.color,
                    count = x.count,
                    percentage = total > 0 ? Math.Round((decimal)x.count / total * 100, 1) : 0,
                }).ToList();

            return grid;
        }
    }
}
