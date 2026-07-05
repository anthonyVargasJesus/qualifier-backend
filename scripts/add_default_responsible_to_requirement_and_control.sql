-- =====================================================================
-- Agrega el "responsable por defecto" a nivel de cláusula (MAE_REQUIREMENT)
-- y control (MAE_CONTROL), para precargar RequirementEvaluation.responsibleId
-- / ControlEvaluation.responsibleId cuando se evalúa por primera vez en un
-- ciclo nuevo. No reemplaza el responsable guardado por evaluación — cada
-- evaluación sigue teniendo su propio responsableId, editable e
-- independiente del valor por defecto de la cláusula/control.
-- Este proyecto no usa migraciones de EF Core (no hay carpeta Migrations);
-- el esquema se actualiza con scripts manuales como este.
-- Ejecutar manualmente contra Railway/Postgres antes de desplegar el backend.
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_REQUIREMENT"
  ADD COLUMN "N_DEFAULT_RESPONSIBLE_ID_FK" INTEGER NULL;

ALTER TABLE "MAE_REQUIREMENT"
  ADD CONSTRAINT "CST_MAE_REQUIREMENT_DEFAULT_RESPONSIBLE_FK"
  FOREIGN KEY ("N_DEFAULT_RESPONSIBLE_ID_FK")
  REFERENCES "MAE_RESPONSIBLE" ("N_RESPONSIBLE_ID_PK");

ALTER TABLE "MAE_CONTROL"
  ADD COLUMN "N_DEFAULT_RESPONSIBLE_ID_FK" INTEGER NULL;

ALTER TABLE "MAE_CONTROL"
  ADD CONSTRAINT "CST_MAE_CONTROL_DEFAULT_RESPONSIBLE_FK"
  FOREIGN KEY ("N_DEFAULT_RESPONSIBLE_ID_FK")
  REFERENCES "MAE_RESPONSIBLE" ("N_RESPONSIBLE_ID_PK");

COMMIT;
