-- 006_rename_action_plan_calendar_to_implementacion.sql
--
-- Renombra la opción "/gap/plan-de-accion-calendario" (antes "Plan de Acción
-- (Calendario)") a "Implementación" -- coincide con el nombre real de la
-- hoja del Excel de ONPE ("Declaración de aplicabilidad del SGSI (SoA)")
-- que esta pantalla replica (calendario mensual de implementación de
-- controles). Confirmado visualmente contra esa hoja el 2026-08-13.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-13. Pendiente de aplicar en producción.

BEGIN;

UPDATE "MAE_OPTION" SET "C_NAME" = 'Implementación' WHERE "C_URL" = '/gap/plan-de-accion-calendario';

COMMIT;

-- Verificación post-ejecución
SELECT "N_OPTION_ID_PK", "C_NAME", "C_URL" FROM "MAE_OPTION" WHERE "C_URL" = '/gap/plan-de-accion-calendario';
