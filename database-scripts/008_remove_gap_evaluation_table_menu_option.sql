-- 008_remove_gap_evaluation_table_menu_option.sql
--
-- Elimina la opción de menú "Análisis GAP (Tabla)" (/gap/evaluacion-tabla, agregada en
-- 003_gap_evaluation_table_menu_option.sql) junto con el componente Angular
-- (GapEvaluationTableComponent) y su fila (GapEvaluationRowComponent) -- se decidió no
-- mantener esta vista alternativa.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-13. Pendiente de aplicar en producción.

BEGIN;

DELETE FROM "MAE_OPTION_IN_MENU_IN_ROLE" WHERE "N_OPTION_ID" IN (SELECT "N_OPTION_ID_PK" FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla');
DELETE FROM "MAE_OPTION_IN_MENU" WHERE "N_OPTION_ID" IN (SELECT "N_OPTION_ID_PK" FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla');
DELETE FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla';

COMMIT;

-- Verificación post-ejecución (debe devolver 0 filas)
SELECT * FROM "MAE_OPTION" WHERE "C_URL" = '/gap/evaluacion-tabla';
