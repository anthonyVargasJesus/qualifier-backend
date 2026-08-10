-- =====================================================================
-- Seed: evaluaciones de Diciembre 2025 / Enero / Febrero / Marzo 2026,
-- para ISO 27001 y NTP-ISO/IEC 42001 — extiende hacia atrás la línea
-- de tiempo de "Evolución de Cumplimiento" (que hoy arranca en Mayo).
--
-- Alcance reducido (decisión explícita): SOLO evaluación + evaluaciones
-- de requisito/control (ver seed_requirement_evaluations_dic_ene_feb_mar.sql
-- / seed_control_evaluations_dic_ene_feb_mar.sql) — sin brechas/planes
-- de acción/riesgos, porque esas tablas solo se muestran en pantalla
-- para la evaluación "actual", nunca para una histórica; lo único que
-- lee evaluaciones pasadas es el gráfico de evolución, que solo
-- necesita el nivel de madurez por ítem.
--
-- ISO 27001 YA TIENE una evaluación de diciembre (id=3, "ANÁLISIS DE
-- BRECHAS - DICIEMBRE", creada en una sesión anterior con 0 filas de
-- detalle) — se reutiliza esa fila tal cual, no se crea una nueva.
-- NTP-ISO/IEC 42001 no tiene diciembre, así que se crea desde cero acá.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- Diciembre 2025 para NTP-ISO/IEC 42001 (ISO 27001 ya tiene la suya, id=3)
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD" WHERE "C_NAME" = 'NTP-ISO/IEC 42001'
)
INSERT INTO "MAE_EVALUATION" (
  "N_STANDARD_ID_FK", "C_DESCRIPTION", "D_START_DATE", "D_END_DATE",
  "N_IS_CURRENT", "N_EVALUATION_STATE_ID_FK", "N_IS_GAP_ANALYSIS", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT std.id, 'ANÁLISIS DE BRECHAS - DICIEMBRE', DATE '2025-12-01', DATE '2025-12-29', false, 2, true, 1, 1, now(), false
FROM std;

-- Enero / Febrero / Marzo 2026, para ambas normas (6 filas)
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id, "C_NAME" AS name FROM "MAE_STANDARD"
  WHERE "C_NAME" IN ('ISO 27001', 'NTP-ISO/IEC 42001')
)
INSERT INTO "MAE_EVALUATION" (
  "N_STANDARD_ID_FK", "C_DESCRIPTION", "D_START_DATE", "D_END_DATE",
  "N_IS_CURRENT", "N_EVALUATION_STATE_ID_FK", "N_IS_GAP_ANALYSIS", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT std.id, v.description, v.start_date, v.end_date, false, 2, true, 1, 1, now(), false
FROM std, (VALUES
  ('Evaluación - Enero 2026',   DATE '2026-01-01', DATE '2026-01-31'),
  ('Evaluación - Febrero 2026', DATE '2026-02-01', DATE '2026-02-28'),
  ('Evaluación - Marzo 2026',   DATE '2026-03-01', DATE '2026-03-31')
) AS v(description, start_date, end_date);

COMMIT;
