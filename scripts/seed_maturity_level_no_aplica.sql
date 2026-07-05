-- =====================================================================
-- Reinserta "No aplica" en MAE_MATURITY_LEVEL (se eliminó la fila original,
-- ID=7). Mismo valor (0.00) que tenía antes, para que siga siendo la
-- categoría más baja del set: No aplica(0) < No cumple(1) < Parcial(3) <
-- Cumple(5). Color en hex (antes era 'lightgray'), para que quede
-- consistente con las otras 3 filas de seed_maturity_level_simple_categories.sql.
--
-- OJO: esta fila nace con un N_MATURITY_LEVEL_ID_PK nuevo (no vuelve a ser
-- el 7) — si alguna evaluación existente todavía apunta al id 7 viejo,
-- va a seguir rota y hay que reasignarla aparte; este script no lo hace.
-- =====================================================================

BEGIN;

INSERT INTO "MAE_MATURITY_LEVEL" (
  "C_NAME", "C_DESCRIPTION", "C_ABBREVIATION", "N_VALUE", "C_COLOR", "N_FACTOR",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
VALUES
  ('No aplica', 'No aplica.', 'NA', 0.00, '#9E9E9E', 1.00, 1, 1, now(), false);

COMMIT;
