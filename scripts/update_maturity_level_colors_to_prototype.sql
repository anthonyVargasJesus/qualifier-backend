-- =====================================================================
-- Ajusta C_COLOR de Cumple/Parcial/No cumple/No aplica para que coincida
-- exactamente con la paleta del prototipo de Fable (STATUSES en el JSX):
--   Cumple     -> #2F6F5E
--   Parcial    -> #B07A1F
--   No cumple  -> #A8443B
--   No aplica  -> #6B7480
-- Reemplaza los colores puestos en los scripts anteriores
-- (seed_maturity_level_simple_categories.sql / seed_maturity_level_no_aplica.sql).
-- El fondo pastel de cada chip (cuando NO está seleccionado) no vive en esta
-- tabla — se resuelve en el frontend (gap-evaluation-item.component.ts).
-- =====================================================================

BEGIN;

UPDATE "MAE_MATURITY_LEVEL" SET "C_COLOR" = '#2F6F5E' WHERE "C_ABBREVIATION" = 'CU' AND ("L_IS_DELETED" IS NULL OR "L_IS_DELETED" = false);
UPDATE "MAE_MATURITY_LEVEL" SET "C_COLOR" = '#B07A1F' WHERE "C_ABBREVIATION" = 'PA' AND ("L_IS_DELETED" IS NULL OR "L_IS_DELETED" = false);
UPDATE "MAE_MATURITY_LEVEL" SET "C_COLOR" = '#A8443B' WHERE "C_ABBREVIATION" = 'NC' AND ("L_IS_DELETED" IS NULL OR "L_IS_DELETED" = false);
UPDATE "MAE_MATURITY_LEVEL" SET "C_COLOR" = '#6B7480' WHERE "C_ABBREVIATION" = 'NA' AND ("L_IS_DELETED" IS NULL OR "L_IS_DELETED" = false);

COMMIT;
