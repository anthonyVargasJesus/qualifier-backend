-- =====================================================================
-- Seed: 3 controles (MAE_CONTROL_IMPLEMENTATION) por cada uno de los 3
-- riesgos 'En tratamiento' de 'Evaluación - Julio 2026'.
-- Mezcla L_IS_IMPLEMENTED = true/false para que el resumen "Controles"
-- (X implementados, Y pendientes) se vea realista.
-- Requiere haber ejecutado antes seed_risk_treatment_julio2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Julio 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
),
risk_lookup AS (
  SELECT r."N_RISK_ID_PK" AS risk_id, b."C_TYPE" AS breach_type,
         b."N_REQUIREMENT_ID_FK" AS requirement_id, b."N_CONTROL_ID_FK" AS control_id
  FROM "MAE_RISK" r
  JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
  WHERE r."N_EVALUATION_ID_FK" = (SELECT id FROM ev)
)
INSERT INTO "MAE_CONTROL_IMPLEMENTATION" (
  "N_RISK_ID_FK", "C_ACTIVITIES", "D_START_DATE", "D_VERIFICATION_DATE", "N_RESPONSIBLE_ID_FK",
  "C_OBSERVATION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "L_IS_IMPLEMENTED"
)
SELECT rl.risk_id, v.activity, v.start_date, v.verification_date, v.responsible_id,
       v.observation, 1, 1, now(), false, v.is_implemented
FROM risk_lookup rl
JOIN (VALUES
  -- Riesgo: req. 4.2.2 - partes interesadas
  ('1', 127, 0, 'Elaborar el borrador de la matriz de requisitos por cada parte interesada identificada.', DATE '2026-07-25', DATE '2026-08-05', 9, 'Borrador de matriz completado.', true),
  ('1', 127, 0, 'Validar la matriz con las áreas responsables de cada parte interesada.', DATE '2026-08-05', NULL, 9, NULL, false),
  ('1', 127, 0, 'Aprobar la matriz de requisitos con el Comité SGSI.', DATE '2026-08-20', NULL, 12, NULL, false),
  -- Riesgo: control 5.14 - transferencia de información
  ('2', 0, 16, 'Elaborar el documento final del procedimiento de transferencia segura de información.', DATE '2026-07-25', DATE '2026-08-01', 1, 'Documento de procedimiento finalizado.', true),
  ('2', 0, 16, 'Publicar el procedimiento en la intranet institucional.', DATE '2026-08-05', DATE '2026-08-10', 1, 'Procedimiento publicado y accesible para todo el personal.', true),
  ('2', 0, 16, 'Capacitar al personal que transfiere información crítica de forma habitual.', DATE '2026-08-15', NULL, 14, NULL, false),
  -- Riesgo: control 8.18 - utilidades privilegiadas
  ('2', 0, 77, 'Extender la política GPO de restricción de utilidades privilegiadas a los servidores no críticos restantes.', DATE '2026-07-25', DATE '2026-08-10', 3, 'GPO extendida a los servidores del centro de datos secundario.', true),
  ('2', 0, 77, 'Revisar los registros históricos de uso de utilidades privilegiadas de los últimos 6 meses.', DATE '2026-08-10', NULL, 3, NULL, false),
  ('2', 0, 77, 'Ajustar la política según los hallazgos de la revisión histórica.', DATE '2026-08-25', NULL, 3, NULL, false)
) AS v(breach_type, requirement_id, control_id, activity, start_date, verification_date, responsible_id, observation, is_implemented)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

COMMIT;
