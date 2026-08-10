-- =====================================================================
-- Seed: nueva evaluación 'Evaluación - Mayo 2026' (MAE_EVALUATION)
-- Alcance: standardId=4 (ISO 27001), companyId=1 (ONPE).
-- Periodo: 01/05/2026 - 31/05/2026. Punto de control intermedio entre
-- 'ANÁLISIS DE BRECHAS - DICIEMBRE' (id=3, sin datos de detalle) y
-- 'Evaluación - Junio 2026' (id=4). NO se marca como "actual": Julio
-- 2026 (id=5) sigue siendo la evaluación activa.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_EVALUATION" (
  "N_STANDARD_ID_FK", "C_DESCRIPTION", "D_START_DATE", "D_END_DATE",
  "N_IS_CURRENT", "N_EVALUATION_STATE_ID_FK", "N_IS_GAP_ANALYSIS", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
VALUES (
  4, 'Evaluación - Mayo 2026', '2026-05-01', '2026-05-31',
  false, 2, true, 1,
  1, now(), false
);

COMMIT;
