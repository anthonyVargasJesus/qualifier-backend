-- =====================================================================
-- Seed: evaluación completa de los controles del Anexo A para
-- 'Evaluación - Mayo 2026', copiando en bloque el resultado real de
-- 'Evaluación - Junio 2026' (evaluationId=4) y retrocediendo 6 de ellos a
-- nivel 11 ("No cumple", valor 1.00).
-- Los controles 2, 60, 61 y 62 tienen en junio DOS filas de evaluación
-- cada uno (una "sucia" con texto de prueba tipo 'hyfhg'/'ok' creada
-- manualmente el 2026-07-05, y una limpia con el texto real del seed
-- creada el 2026-07-06 con un ID mayor) — MAE_CONTROL_EVALUATION no tiene
-- restricción de unicidad por (evaluación, control). Este script usa
-- DISTINCT ON + ORDER BY id DESC para quedarse solo con la fila más
-- reciente (la limpia) de cada control, evitando arrastrar el ruido de
-- pruebas a mayo.
-- Criterio de selección de los 6 a retroceder: controles en nivel 10 de
-- distintos grupos (organizacionales, personas, físicos, tecnológicos),
-- evitando los 6 controles que ya forman parte de la historia de
-- brechas de junio/julio (5.8, 5.12, 5.13, 5.14, 8.18, 8.19).
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
  SELECT unnest(ARRAY[11,21,33,42,46,73]) AS ctrl_id
),
canonical AS (
  SELECT DISTINCT ON ("N_CONTROL_ID_FK")
    "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
    "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION"
  FROM "MAE_CONTROL_EVALUATION"
  WHERE "N_EVALUATION_ID_FK" = 4
  ORDER BY "N_CONTROL_ID_FK", "N_CONTROL_EVALUATION_ID_PK" DESC
)
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE",
  "N_RESPONSIBLE_ID_FK", "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION",
  "N_STANDARD_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id,
  c."N_CONTROL_ID_FK",
  CASE WHEN r.ctrl_id IS NOT NULL THEN 11 ELSE c."N_MATURITY_LEVEL_ID_FK" END,
  CASE WHEN r.ctrl_id IS NOT NULL THEN 1.00 ELSE c."N_VALUE" END,
  c."N_RESPONSIBLE_ID_FK", c."C_JUSTIFICATION", c."C_IMPROVEMENT_ACTIONS", c."C_CONTROL_DESCRIPTION",
  4, 1, 1, now(), false
FROM canonical c
CROSS JOIN ev
LEFT JOIN regress r ON r.ctrl_id = c."N_CONTROL_ID_FK";

COMMIT;
