-- 002_maturity_level_is_not_applicable.sql
--
-- Agrega L_IS_NOT_APPLICABLE a MAE_MATURITY_LEVEL. Corrige un bug introducido por
-- 001_maturity_levels_cmm_scale.sql: el backend/frontend tenían el nombre "No aplica"
-- hardcodeado en varios lugares (GapItemsBuilder.NO_APLICA, PALE_BG_BY_NAME del frontend, etc.)
-- para saber si un nivel significa "no aplica". Al renombrarlo a "No aplicable" esas
-- comparaciones dejaron de coincidir en silencio:
--   - El reporte SOA mostraba "Aplica: Sí" en TODOS los ítems, incluso los marcados No aplicable.
--   - El % de "Cumple" (GapItemsBuilder.CUMPLE = "Cumple", nombre que ya no existe tras la
--     migración a la escala CMM) siempre daba 0 -- es la causa del "0% cumplimiento estimado"
--     que se veía en Análisis del GAP con ítems ya evaluados en niveles altos.
--
-- Esta columna reemplaza esa comparación por nombre. El "Cumple" también se corrige, pero sin
-- columna nueva: pasa a derivarse de L_GENERATES_BREACH = false AND N_VALUE > 0 (el nivel no
-- genera brecha y no es "no aplicable"), campos que 001 ya dejó bien configurados.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-12. Pendiente de aplicar en producción
-- (production no tiene los niveles CMM todavía, así que ahí se aplicaría junto con 001).

BEGIN;

ALTER TABLE "MAE_MATURITY_LEVEL"
  ADD COLUMN "L_IS_NOT_APPLICABLE" boolean NOT NULL DEFAULT false;

UPDATE "MAE_MATURITY_LEVEL" SET "L_IS_NOT_APPLICABLE" = true
WHERE "C_NAME" = 'No aplicable';

COMMIT;

-- Verificación post-ejecución
SELECT "N_MATURITY_LEVEL_ID_PK", "C_NAME", "N_VALUE", "L_GENERATES_BREACH", "L_IS_NOT_APPLICABLE"
FROM "MAE_MATURITY_LEVEL" ORDER BY "N_VALUE", "C_NAME";
