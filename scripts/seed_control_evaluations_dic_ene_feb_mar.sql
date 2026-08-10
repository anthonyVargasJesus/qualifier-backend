-- =====================================================================
-- Seed: evaluaciones de control para Diciembre/Enero/Febrero/Marzo,
-- ISO 27001 y NTP-ISO/IEC 42001. Mismo mecanismo y umbrales que
-- seed_requirement_evaluations_dic_ene_feb_mar.sql (leer su cabecera
-- para el detalle de la curva buscada) — retrocede un subconjunto de
-- controles un nivel de madurez a partir del estado real de "Mayo
-- 2026" de cada norma, con umbral distinto por mes para lograr una
-- tendencia con subidas y bajadas real.
-- Requiere seed_evaluations_dic_ene_feb_mar.sql ya ejecutado.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH targets(standard_name, month_desc, threshold) AS (VALUES
  ('ISO 27001',          'ANÁLISIS DE BRECHAS - DICIEMBRE', 6),
  ('ISO 27001',          'Evaluación - Enero 2026',          4),
  ('ISO 27001',          'Evaluación - Febrero 2026',        5),
  ('ISO 27001',          'Evaluación - Marzo 2026',          2),
  ('NTP-ISO/IEC 42001',  'ANÁLISIS DE BRECHAS - DICIEMBRE',  6),
  ('NTP-ISO/IEC 42001',  'Evaluación - Enero 2026',          4),
  ('NTP-ISO/IEC 42001',  'Evaluación - Febrero 2026',        5),
  ('NTP-ISO/IEC 42001',  'Evaluación - Marzo 2026',          2)
),
mayo_ctrl AS (
  SELECT ce.*, s."C_NAME" AS standard_name
  FROM "MAE_CONTROL_EVALUATION" ce
  JOIN "MAE_EVALUATION" ev ON ev."N_EVALUATION_ID_PK" = ce."N_EVALUATION_ID_FK"
  JOIN "MAE_STANDARD" s ON s."N_STANDARD_ID_PK" = ev."N_STANDARD_ID_FK"
  WHERE ev."C_DESCRIPTION" = 'Evaluación - Mayo 2026'
),
regressed AS (
  SELECT
    t.standard_name, t.month_desc,
    m."N_CONTROL_ID_FK" AS control_id,
    m."N_RESPONSIBLE_ID_FK" AS responsible_id,
    m."C_JUSTIFICATION" AS justification,
    m."C_IMPROVEMENT_ACTIONS" AS improvement_actions,
    m."C_CONTROL_DESCRIPTION" AS control_description,
    m."C_CONTROL_TYPE" AS control_type,
    m."N_STANDARD_ID_FK" AS standard_id,
    m."N_COMPANY_ID_FK" AS company_id,
    CASE
      WHEN MOD(m."N_CONTROL_ID_FK", 10) < t.threshold AND m."N_MATURITY_LEVEL_ID_FK" = 9 THEN 10
      WHEN MOD(m."N_CONTROL_ID_FK", 10) < t.threshold AND m."N_MATURITY_LEVEL_ID_FK" = 10 THEN 11
      ELSE m."N_MATURITY_LEVEL_ID_FK"
    END AS new_level_id
  FROM targets t
  JOIN mayo_ctrl m ON m.standard_name = t.standard_name
)
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "C_CONTROL_TYPE"
)
SELECT
  tgt_ev."N_EVALUATION_ID_PK", r.control_id, r.new_level_id, ml."N_VALUE", r.responsible_id,
  r.justification, r.improvement_actions, r.control_description, r.standard_id, r.company_id,
  1, now(), false, r.control_type
FROM regressed r
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = r.new_level_id
JOIN "MAE_STANDARD" s ON s."C_NAME" = r.standard_name
JOIN "MAE_EVALUATION" tgt_ev ON tgt_ev."N_STANDARD_ID_FK" = s."N_STANDARD_ID_PK" AND tgt_ev."C_DESCRIPTION" = r.month_desc;

COMMIT;
