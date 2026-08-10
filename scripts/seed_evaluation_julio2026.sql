-- =====================================================================
-- Seed: nueva evaluación 'Evaluación - Julio 2026' (MAE_EVALUATION)
-- Alcance: standardId=4 (ISO 27001), companyId=1 (ONPE).
-- Periodo: 01/07/2026 - 31/07/2026. Queda marcada como "actual"
-- (N_IS_CURRENT); la evaluación 'Junio 2026' deja de estarlo.
--
-- Todos los nombres de columna y valores de este script fueron
-- verificados el 2026-08-09 con consultas de solo lectura contra la BD
-- real de Railway (no solo inferidos de los scripts de junio):
--   * el flag de evaluación activa es N_IS_CURRENT, no N_IS_ACTUAL.
--   * MAE_EVALUATION_STATE no tiene C_ABBREVIATION; las dos evaluaciones
--     reales existentes (Diciembre id=3, Junio id=4) están ambas en
--     N_EVALUATION_STATE_ID_FK=2 ("Edición"), no en el estado 1 ("Inicial")
--     que CreateEvaluationCommand.cs asigna al crear desde la UI — se usa
--     2 aquí para quedar consistente con el resto de evaluaciones activas.
--   * ambas evaluaciones reales tienen N_IS_GAP_ANALYSIS=true — se usa el
--     mismo valor aquí (ver advertencia de negocio abajo).
--
-- A diferencia de los scripts de Junio 2026 (que hardcodeaban
-- evaluationId=4), aquí y en todos los scripts de Julio 2026 el id de la
-- evaluación se resuelve dinámicamente vía subquery por C_DESCRIPTION,
-- para no depender de asumir cuál será el próximo valor de secuencia.
--
-- ADVERTENCIA DE NEGOCIO (informativa, no bloquea este script):
-- CreateEvaluationCommand.cs bloquea desde la UI crear una segunda
-- evaluación con N_IS_GAP_ANALYSIS=true en el mismo año ("Ya hay un
-- análisis de brechas para el año {year}"). Como 'Junio 2026' ya tiene
-- N_IS_GAP_ANALYSIS=true, este INSERT (que va directo a BD, sin pasar por
-- esa validación) deja una segunda evaluación de brechas para 2026 que la
-- UI no habría dejado crear por separado desde el botón "Nueva evaluación".
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- Junio 2026 deja de ser la evaluación "actual"
UPDATE "MAE_EVALUATION"
SET "N_IS_CURRENT" = false, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "C_DESCRIPTION" = 'Evaluación - Junio 2026'
  AND "N_STANDARD_ID_FK" = 4
  AND "N_COMPANY_ID_FK" = 1
  AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false);

INSERT INTO "MAE_EVALUATION" (
  "N_STANDARD_ID_FK", "C_DESCRIPTION", "D_START_DATE", "D_END_DATE",
  "N_IS_CURRENT", "N_EVALUATION_STATE_ID_FK", "N_IS_GAP_ANALYSIS", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
VALUES (
  4, 'Evaluación - Julio 2026', '2026-07-01', '2026-07-31',
  true, 2, true, 1,
  1, now(), false
);

COMMIT;
