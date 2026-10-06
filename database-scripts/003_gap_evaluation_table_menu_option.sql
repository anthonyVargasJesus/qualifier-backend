-- 003_gap_evaluation_table_menu_option.sql
--
-- Agrega la opción de menú para la nueva pantalla "Análisis del GAP · Tabla"
-- (/gap/evaluacion-tabla, GapEvaluationTableComponent) al menú "GAP" (N_MENU_ID_PK = 13),
-- al final de la lista (orden 6, después de "Reporte SOA") y con el mismo rol que ya tiene
-- acceso a "Análisis GAP" (N_ROLE_ID = 13) -- ajustar si en producción hay más roles con
-- acceso a ese menú.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-12. Pendiente de aplicar en producción.

BEGIN;

INSERT INTO "MAE_OPTION" ("C_NAME", "C_IMAGE", "C_URL", "N_IS_MOBILE", "D_CREATION_DATE", "N_CREATION_USER_ID", "N_COMPANY_ID")
VALUES ('Análisis GAP (Tabla)', '', '/gap/evaluacion-tabla', false, now(), 1, 1);

INSERT INTO "MAE_OPTION_IN_MENU" ("N_MENU_ID", "N_OPTION_ID", "N_ORDER", "N_COMPANY_ID", "D_CREATION_DATE", "N_CREATION_USER_ID")
SELECT 13, "N_OPTION_ID_PK", 6, 1, now(), 1
FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla';

INSERT INTO "MAE_OPTION_IN_MENU_IN_ROLE" ("N_MENU_ID", "N_OPTION_ID", "N_ROLE_ID", "N_ORDER", "N_COMPANY_ID", "D_CREATION_DATE", "N_CREATION_USER_ID")
SELECT 13, "N_OPTION_ID_PK", 13, 6, 1, now(), 1
FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla';

COMMIT;

-- Verificación post-ejecución
SELECT o."C_NAME", o."C_URL", oim."N_ORDER"
FROM "MAE_OPTION_IN_MENU" oim
JOIN "MAE_OPTION" o ON o."N_OPTION_ID_PK" = oim."N_OPTION_ID"
WHERE oim."N_MENU_ID" = 13
ORDER BY oim."N_ORDER";
