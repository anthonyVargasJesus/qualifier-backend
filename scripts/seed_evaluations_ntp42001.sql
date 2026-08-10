-- =====================================================================
-- Seed: 3 evaluaciones de NTP-ISO/IEC 42001 (MAE_EVALUATION) — Mayo,
-- Junio y Julio 2026. NINGUNA se marca N_IS_CURRENT=true: esa columna
-- es GLOBAL en todo el sistema (GetCurrentEvaluationQuery no filtra
-- por standardId, trae la única fila con isCurrent=true) y hoy la
-- ocupa "Evaluación - Julio 2026" de ISO 27001 (id=5). Marcar alguna
-- de estas como actual reemplazaría esa evaluación en toda la pantalla
-- "Análisis del GAP" — decisión explícita del usuario: ninguna.
--
-- Misma historia narrativa que se usó para ISO 27001: Junio es la
-- línea base (evaluación completa), Mayo es un punto de control
-- anterior con más brechas (retrocediendo ~15 ítems un nivel de
-- madurez respecto a junio) y Julio es una reevaluación parcial que
-- solo toca los ítems en brecha de junio, cerrando una parte.
-- N_EVALUATION_STATE_ID_FK=2 ("Edición"), N_IS_GAP_ANALYSIS=true —
-- mismo criterio ya usado para las evaluaciones de ISO 27001 de esta
-- sesión (ambas reales, Diciembre/Junio, están en ese mismo estado).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_EVALUATION" (
  "N_STANDARD_ID_FK", "C_DESCRIPTION", "D_START_DATE", "D_END_DATE",
  "N_IS_CURRENT", "N_EVALUATION_STATE_ID_FK", "N_IS_GAP_ANALYSIS", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT std.id, v.description, v.start_date, v.end_date, false, 2, true, 1, 1, now(), false
FROM std, (VALUES
  ('Evaluación - Mayo 2026',  DATE '2026-05-01', DATE '2026-05-31'),
  ('Evaluación - Junio 2026', DATE '2026-06-01', DATE '2026-06-30'),
  ('Evaluación - Julio 2026', DATE '2026-07-01', DATE '2026-07-31')
) AS v(description, start_date, end_date);

COMMIT;
