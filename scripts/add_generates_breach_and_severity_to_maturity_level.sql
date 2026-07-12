-- =====================================================================
-- Niveles de madurez (MAE_MATURITY_LEVEL): reemplaza la lógica hardcodeada
-- de generación de brechas (que hacía match contra el nombre del nivel,
-- "Parcial"/"No cumple") por dos campos configurables por nivel:
--
-- 1) L_GENERATES_BREACH: si en true, evaluar un control/requisito con este
--    nivel de madurez genera (o actualiza) una brecha abierta.
-- 2) N_BREACH_SEVERITY_ID_FK: severidad con la que se genera esa brecha,
--    solo relevante cuando L_GENERATES_BREACH = true.
--
-- No inserta ni actualiza filas: los 7 niveles nuevos del catálogo
-- (nombres/valores) y su configuración de generatesBreach/breachSeverityId
-- se cargan manualmente desde el CRUD de Angular una vez desplegado esto.
--
-- Este proyecto no usa migraciones de EF Core (no hay carpeta Migrations);
-- el esquema se actualiza con scripts manuales como este.
-- Ejecutar manualmente contra Railway/Postgres antes de desplegar el backend.
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_MATURITY_LEVEL"
  ADD COLUMN IF NOT EXISTS "L_GENERATES_BREACH" BOOLEAN NOT NULL DEFAULT FALSE;

ALTER TABLE "MAE_MATURITY_LEVEL"
  ADD COLUMN IF NOT EXISTS "N_BREACH_SEVERITY_ID_FK" INTEGER NULL;

ALTER TABLE "MAE_MATURITY_LEVEL"
  ADD CONSTRAINT "CST_MAE_MATURITY_LEVEL_BREACH_SEVERITY_FK"
  FOREIGN KEY ("N_BREACH_SEVERITY_ID_FK")
  REFERENCES "MAE_BREACH_SEVERITY" ("N_BREACH_SEVERITY_ID_PK");

COMMIT;
