-- =====================================================================
-- Seed: evaluación completa de los 87 requisitos para 'Evaluación - Mayo
-- 2026', copiando en bloque el resultado real ya cargado en 'Evaluación -
-- Junio 2026' (evaluationId=4) y retrocediendo 14 de ellos a nivel 11
-- ("No cumple", valor 1.00) para representar que en mayo aún no habían
-- alcanzado el estado que sí tienen en junio.
-- Criterio de selección de los 14: ítems que en junio están en nivel 10
-- ("Parcial") repartidos en distintas cláusulas (4.1, 4.4, 5.3, 6.1/6.5,
-- 7.1-7.5, 8.1-8.3, 9.1, 10.1), evitando las cláusulas que ya forman
-- parte de la historia de brechas de junio/julio (4.2.x, 6.2.x, 7.4.1).
-- El texto de justificación/mejora se reutiliza tal cual el de junio: ya
-- describe un estado parcial/incompleto, por lo que también sirve como
-- explicación de "aún no cumplido" un mes antes.
-- Requiere haber ejecutado antes seed_evaluation_mayo2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
),
regress AS (
  SELECT unnest(ARRAY[6,18,35,41,51,68,69,73,77,83,87,89,94,115]) AS req_id
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE",
  "N_RESPONSIBLE_ID_FK", "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS",
  "N_STANDARD_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "N_AUDITOR_STATUS"
)
SELECT
  ev.id,
  j."N_REQUIREMENT_ID_FK",
  CASE WHEN r.req_id IS NOT NULL THEN 11 ELSE j."N_MATURITY_LEVEL_ID_FK" END,
  CASE WHEN r.req_id IS NOT NULL THEN 1.00 ELSE j."N_VALUE" END,
  j."N_RESPONSIBLE_ID_FK", j."C_JUSTIFICATION", j."C_IMPROVEMENT_ACTIONS",
  4, 1, 1, now(), false, 1
FROM "MAE_REQUIREMENT_EVALUATION" j
CROSS JOIN ev
LEFT JOIN regress r ON r.req_id = j."N_REQUIREMENT_ID_FK"
WHERE j."N_EVALUATION_ID_FK" = 4;

COMMIT;
