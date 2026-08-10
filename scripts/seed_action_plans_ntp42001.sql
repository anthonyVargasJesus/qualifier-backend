-- =====================================================================
-- Seed: planes de acción (MAE_ACTION_PLAN) para las 110 brechas de
-- NTP-ISO/IEC 42001 (Mayo 49 + Junio 37 + Julio 24). Requiere
-- seed_breaches_ntp42001.sql ya ejecutado — un plan de acción por
-- brecha, generado a partir de la brecha misma (no repite los
-- listados de ítems: los lee directo de MAE_BREACH ya insertado).
--
-- Prioridad: severidad Crítica (No cumple) -> Alta; severidad Alta
-- (Parcial) -> Media. Estado: Pendiente para brechas de "No cumple"
-- (nada iniciado), En curso para "Parcial" (ya hay avances, coincide
-- con la justificación de esos ítems). Fechas: inicio = primer día del
-- mes de la evaluación, vencimiento = +30 días.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_ACTION_PLAN" (
  "N_BREACH_ID_FK", "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TITLE", "C_DESCRIPTION",
  "N_RESPONSIBLE_ID_FK", "D_START_DATE", "D_DUE_DATE", "N_ACTION_PLAN_STATUS_ID_FK",
  "N_ACTION_PLAN_PRIORITY_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  b."N_BREACH_ID_PK", b."N_EVALUATION_ID_FK", b."N_STANDARD_ID_FK",
  'Cerrar brecha en ' || (CASE b."C_TYPE" WHEN '1' THEN 'el requisito ' ELSE 'el control ' END) || b."C_NUMERATION_TO_SHOW",
  CASE b."N_BREACH_SEVERITY_ID_FK"
    WHEN 4 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    WHEN 3 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
  END,
  b."N_RESPONSIBLE_ID_FK",
  ev."D_START_DATE",
  (ev."D_START_DATE" + INTERVAL '30 days')::date,
  CASE b."N_BREACH_SEVERITY_ID_FK" WHEN 4 THEN 1 WHEN 3 THEN 2 END,
  CASE b."N_BREACH_SEVERITY_ID_FK" WHEN 4 THEN 1 WHEN 3 THEN 2 END,
  1, 1, now(), false
FROM "MAE_BREACH" b
JOIN "MAE_EVALUATION" ev ON ev."N_EVALUATION_ID_PK" = b."N_EVALUATION_ID_FK"
WHERE b."N_STANDARD_ID_FK" = (SELECT id FROM std);

COMMIT;
