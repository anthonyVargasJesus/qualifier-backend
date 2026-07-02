-- =====================================================================
-- Fix: MAE_INDICATOR (Avanzado/Medio/Básico) tenía las 3 filas con
-- N_MINIMUM = 0.00 y N_MAXIMUM = 0.00, por lo que ningún valor promedio
-- de madurez calculado caía dentro de un rango real. Esto dejaba la
-- columna "Calificación" vacía en la tabla resumen por cláusula
-- (Requisitos/Controles) para prácticamente todas las filas.
-- Escala de madurez usada: 0 (No implementado) a 5 (Optimizado).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

UPDATE "MAE_INDICATOR" SET "N_MINIMUM" = 0.00, "N_MAXIMUM" = 1.99 WHERE "N_INDICATOR_ID_PK" = 3; -- Básico
UPDATE "MAE_INDICATOR" SET "N_MINIMUM" = 2.00, "N_MAXIMUM" = 3.49 WHERE "N_INDICATOR_ID_PK" = 2; -- Medio
UPDATE "MAE_INDICATOR" SET "N_MINIMUM" = 3.50, "N_MAXIMUM" = 5.00 WHERE "N_INDICATOR_ID_PK" = 1; -- Avanzado

COMMIT;
