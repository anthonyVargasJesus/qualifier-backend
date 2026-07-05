-- =====================================================================
-- Plan de acción (MAE_ACTION_PLAN): el responsable pasa de ser un cargo
-- del catálogo MAE_RESPONSIBLE a ser un usuario real del sistema
-- (MAE_USER), para que quede trazable quién ejecuta cada acción
-- correctiva.
--
-- 1) N_USER_ID_FK (nuevo, nullable): usuario real asignado. Nullable
--    porque las filas existentes no tienen usuario asignado todavía.
-- 2) N_RESPONSIBLE_ID_FK deja de ser obligatorio y de usarse desde la
--    app, pero NO se borra la columna ni su dato — solo se le quita la
--    restricción NOT NULL para no perder el historial de qué cargo
--    tenía asignado cada plan de acción antes de este cambio.
--
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_ACTION_PLAN"
  ADD COLUMN IF NOT EXISTS "N_USER_ID_FK" INTEGER NULL REFERENCES "MAE_USER"("N_USER_ID_PK");

ALTER TABLE "MAE_ACTION_PLAN"
  ALTER COLUMN "N_RESPONSIBLE_ID_FK" DROP NOT NULL;

COMMIT;
