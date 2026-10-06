-- 005_rename_gap_menu.sql
--
-- Renombra el menú "GAP" (N_MENU_ID_PK = 13) a "Evaluación", y su opción
-- "/gap/gap-home" (antes "Análisis GAP", después "Evaluación de controles")
-- a "Cumplimiento" -- para no usar "GAP" (jerga en inglés) en el sidebar,
-- y evitar el choque con el menú viejo "BRECHAS" (que ya existe con ese
-- nombre para otras pantallas legacy) si se hubiera usado "Brechas" acá.
--
-- El resto de renombres discutidos (Análisis GAP (Tabla) -> Vista tabla,
-- Plan de Acción (Calendario) -> Implementación, etc.) quedan pendientes de
-- confirmar -- no se tocan en este script.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-13. Pendiente de aplicar en producción.

BEGIN;

UPDATE "MAE_MENU" SET "C_NAME" = 'Evaluación' WHERE "N_MENU_ID_PK" = 13;
UPDATE "MAE_OPTION" SET "C_NAME" = 'Cumplimiento' WHERE "C_URL" = '/gap/gap-home';

COMMIT;

-- Verificación post-ejecución
SELECT "N_MENU_ID_PK", "C_NAME" FROM "MAE_MENU" WHERE "N_MENU_ID_PK" = 13;
SELECT "N_OPTION_ID_PK", "C_NAME", "C_URL" FROM "MAE_OPTION" WHERE "C_URL" = '/gap/gap-home';
