-- =====================================================================
-- Agrega la marca manual "Obsoleta" a las evidencias (MAE_REFERENCE_DOCUMENTATION).
-- El responsable la activa cuando una evidencia ya no aplica (ej. una política
-- reemplazada), sin necesidad de borrarla. No se calcula automáticamente por fecha.
--
-- Este proyecto no usa migraciones de EF Core (no hay carpeta Migrations);
-- el esquema se actualiza con scripts manuales como este.
-- Ejecutar manualmente contra Railway/Postgres antes de desplegar el backend.
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_REFERENCE_DOCUMENTATION"
  ADD COLUMN "N_IS_OBSOLETE" BOOLEAN NOT NULL DEFAULT FALSE;

COMMIT;
