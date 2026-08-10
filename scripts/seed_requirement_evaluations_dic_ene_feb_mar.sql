-- =====================================================================
-- Seed: evaluaciones de requisito para Diciembre/Enero/Febrero/Marzo,
-- ISO 27001 y NTP-ISO/IEC 42001. Requiere seed_evaluations_dic_ene_feb_mar.sql
-- ya ejecutado.
--
-- Construidas copiando el estado real de "Evaluación - Mayo 2026" de
-- cada norma (ya cargado) y retrocediendo un subconjunto de ítems un
-- nivel de madurez (Cumple->Parcial, Parcial->No cumple; No cumple y
-- No aplica quedan como piso, no bajan más) — mismo mecanismo que ya
-- se usó para construir Mayo a partir de Junio. El subconjunto se
-- elige por MOD(requirementId, 10) < umbral, un umbral distinto por
-- mes para lograr una tendencia con SUBIDAS Y BAJADAS real (pedido
-- explícito), no una recta:
--
--   Diciembre (umbral 6/10 = 60% retrocede) -> el peor punto
--   Enero     (umbral 4/10 = 40% retrocede) -> mejora vs. diciembre
--   Febrero   (umbral 5/10 = 50% retrocede) -> retrocede vs. enero (bajón real)
--   Marzo     (umbral 2/10 = 20% retrocede) -> recupera fuerte, ya cerca de mayo
--   Mayo      (dato real ya cargado)        -> continúa la mejora hacia junio/julio
--
-- Con esto, ISO 27001 pasa aprox. 32% -> 40% -> 36% -> 48% -> 56% (mayo)
-- -> 60% (junio) -> 62% (julio); NTP 42001 aprox. 37% -> 45% -> 41% ->
-- 53% -> 61% (mayo) en adelante. No son valores exactos (dependen de
-- cuántos ítems de cada bucket ya estaban en Parcial/No cumple en
-- mayo, ese piso no baja más), pero si la curva resultante queda
-- demasiado plana se puede ajustar el umbral y volver a correr.
--
-- Solo requisitos/controles (ver alcance reducido en seed_evaluations_
-- dic_ene_feb_mar.sql) — sin brechas/planes/riesgos para estos 4 meses.
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
mayo_req AS (
  SELECT re.*, s."C_NAME" AS standard_name
  FROM "MAE_REQUIREMENT_EVALUATION" re
  JOIN "MAE_EVALUATION" ev ON ev."N_EVALUATION_ID_PK" = re."N_EVALUATION_ID_FK"
  JOIN "MAE_STANDARD" s ON s."N_STANDARD_ID_PK" = ev."N_STANDARD_ID_FK"
  WHERE ev."C_DESCRIPTION" = 'Evaluación - Mayo 2026'
),
regressed AS (
  SELECT
    t.standard_name, t.month_desc,
    m."N_REQUIREMENT_ID_FK" AS requirement_id,
    m."N_RESPONSIBLE_ID_FK" AS responsible_id,
    m."C_JUSTIFICATION" AS justification,
    m."C_IMPROVEMENT_ACTIONS" AS improvement_actions,
    m."N_STANDARD_ID_FK" AS standard_id,
    m."N_COMPANY_ID_FK" AS company_id,
    CASE
      WHEN MOD(m."N_REQUIREMENT_ID_FK", 10) < t.threshold AND m."N_MATURITY_LEVEL_ID_FK" = 9 THEN 10
      WHEN MOD(m."N_REQUIREMENT_ID_FK", 10) < t.threshold AND m."N_MATURITY_LEVEL_ID_FK" = 10 THEN 11
      ELSE m."N_MATURITY_LEVEL_ID_FK"
    END AS new_level_id
  FROM targets t
  JOIN mayo_req m ON m.standard_name = t.standard_name
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  tgt_ev."N_EVALUATION_ID_PK", r.requirement_id, r.new_level_id, ml."N_VALUE", r.responsible_id,
  r.justification, r.improvement_actions, r.standard_id, r.company_id, 1, now(), false
FROM regressed r
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = r.new_level_id
JOIN "MAE_STANDARD" s ON s."C_NAME" = r.standard_name
JOIN "MAE_EVALUATION" tgt_ev ON tgt_ev."N_STANDARD_ID_FK" = s."N_STANDARD_ID_PK" AND tgt_ev."C_DESCRIPTION" = r.month_desc;

COMMIT;
