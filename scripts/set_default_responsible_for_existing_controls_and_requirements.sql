-- =====================================================================
-- Asigna un responsable por defecto a los controles (MAE_CONTROL) y
-- cláusulas (MAE_REQUIREMENT) que todavía no tienen uno configurado
-- (N_DEFAULT_RESPONSIBLE_ID_FK IS NULL).
--
-- Para cada control/cláusula, toma el primer responsable (por id) que
-- pertenezca al mismo estándar (N_STANDARD_ID) y compañía (N_COMPANY_ID).
-- Si un estándar no tiene NINGÚN responsable creado todavía, esos
-- controles/cláusulas quedan sin cambios (no hay de dónde asignar) — la
-- consulta de verificación al final los lista para que crees al menos un
-- responsable en ese estándar y vuelvas a correr el script.
--
-- Ejecutar manualmente contra Railway/Postgres. No afecta evaluaciones ya
-- guardadas (eso vive en MAE_CONTROL_EVALUATION/MAE_REQUIREMENT_EVALUATION,
-- tablas distintas) — solo precarga el valor por defecto para próximas
-- evaluaciones nuevas.
-- =====================================================================

BEGIN;

-- 1) Vista previa: cuántos quedarían sin asignar por falta de responsables
--    en su estándar (correr antes del UPDATE para revisar).
SELECT c."N_CONTROL_ID_PK", c."C_NAME", c."N_STANDARD_ID", c."N_COMPANY_ID"
FROM "MAE_CONTROL" c
WHERE (c."N_IS_DELETED" IS NULL OR c."N_IS_DELETED" = false)
  AND c."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL
  AND NOT EXISTS (
      SELECT 1 FROM "MAE_RESPONSIBLE" r
      WHERE r."N_STANDARD_ID" = c."N_STANDARD_ID"
        AND r."N_COMPANY_ID" = c."N_COMPANY_ID"
        AND (r."N_IS_DELETED" IS NULL OR r."N_IS_DELETED" = false)
  );

SELECT req."N_REQUIREMENT_ID_PK", req."C_NAME", req."N_STANDARD_ID", req."N_COMPANY_ID"
FROM "MAE_REQUIREMENT" req
WHERE (req."N_IS_DELETED" IS NULL OR req."N_IS_DELETED" = false)
  AND req."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL
  AND NOT EXISTS (
      SELECT 1 FROM "MAE_RESPONSIBLE" r
      WHERE r."N_STANDARD_ID" = req."N_STANDARD_ID"
        AND r."N_COMPANY_ID" = req."N_COMPANY_ID"
        AND (r."N_IS_DELETED" IS NULL OR r."N_IS_DELETED" = false)
  );

-- 2) Asignación real.
UPDATE "MAE_CONTROL" c
SET "N_DEFAULT_RESPONSIBLE_ID_FK" = (
    SELECT r."N_RESPONSIBLE_ID_PK"
    FROM "MAE_RESPONSIBLE" r
    WHERE r."N_STANDARD_ID" = c."N_STANDARD_ID"
      AND r."N_COMPANY_ID" = c."N_COMPANY_ID"
      AND (r."N_IS_DELETED" IS NULL OR r."N_IS_DELETED" = false)
    ORDER BY r."N_RESPONSIBLE_ID_PK"
    LIMIT 1
)
WHERE (c."N_IS_DELETED" IS NULL OR c."N_IS_DELETED" = false)
  AND c."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL;

UPDATE "MAE_REQUIREMENT" req
SET "N_DEFAULT_RESPONSIBLE_ID_FK" = (
    SELECT r."N_RESPONSIBLE_ID_PK"
    FROM "MAE_RESPONSIBLE" r
    WHERE r."N_STANDARD_ID" = req."N_STANDARD_ID"
      AND r."N_COMPANY_ID" = req."N_COMPANY_ID"
      AND (r."N_IS_DELETED" IS NULL OR r."N_IS_DELETED" = false)
    ORDER BY r."N_RESPONSIBLE_ID_PK"
    LIMIT 1
)
WHERE (req."N_IS_DELETED" IS NULL OR req."N_IS_DELETED" = false)
  AND req."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL;

COMMIT;

-- 3) Verificación posterior: deberían quedar solo los que ya salieron en la
--    vista previa del paso 1 (estándares sin ningún responsable creado).
SELECT c."N_CONTROL_ID_PK", c."C_NAME", c."N_STANDARD_ID"
FROM "MAE_CONTROL" c
WHERE (c."N_IS_DELETED" IS NULL OR c."N_IS_DELETED" = false)
  AND c."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL;

SELECT req."N_REQUIREMENT_ID_PK", req."C_NAME", req."N_STANDARD_ID"
FROM "MAE_REQUIREMENT" req
WHERE (req."N_IS_DELETED" IS NULL OR req."N_IS_DELETED" = false)
  AND req."N_DEFAULT_RESPONSIBLE_ID_FK" IS NULL;
