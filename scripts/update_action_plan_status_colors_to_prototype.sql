-- =====================================================================
-- Colores de MAE_ACTION_PLAN_STATUS: reemplaza los colores "de sistema"
-- (rojo/naranja/verde puros) por la misma paleta apagada que ya usa el
-- resto de la app (niveles de madurez, temas del gap-panel), para que
-- los chips de Estado en el plan de acción se vean consistentes con el
-- prototipo.
--
-- Match por C_ABBREVIATION, no por nombre, para no depender de mayúsculas/
-- tildes. Ajusta aquí si tu catálogo real usa otras abreviaturas.
--
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

UPDATE "MAE_ACTION_PLAN_STATUS"
SET "C_COLOR" = '#6B7480' -- gris azulado (mismo tono que "No aplica")
WHERE "C_ABBREVIATION" = 'PEN' AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false);

UPDATE "MAE_ACTION_PLAN_STATUS"
SET "C_COLOR" = '#B07A1F' -- ámbar (mismo tono que "Parcial")
WHERE "C_ABBREVIATION" = 'PROG' AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false);

UPDATE "MAE_ACTION_PLAN_STATUS"
SET "C_COLOR" = '#2F6F5E' -- verde (mismo tono que "Cumple")
WHERE "C_ABBREVIATION" = 'COMP' AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false);

COMMIT;
