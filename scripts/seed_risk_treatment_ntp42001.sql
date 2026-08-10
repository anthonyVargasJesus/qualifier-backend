-- =====================================================================
-- Seed: tratamiento de riesgo (MAE_RISK_TREATMENT) para NTP-ISO/IEC
-- 42001. Requiere seed_risk_assessment_ntp42001.sql ya ejecutado —
-- mismos 13 riesgos de junio que se cierran en julio.
--
-- N_RISK_TREATMENT_METHOD_ID=1 ("Mitigar, reducir o modificar el
-- riesgo"). Los valores de amenaza/vulnerabilidad/nivel acá
-- representan el riesgo RESIDUAL tras el tratamiento (menor que el
-- original de MAE_RISK_ASSESSMENT: amenaza baja de 2.00 a 1.00),
-- consistente con que estos 13 ítems terminan Cumple en julio ->
-- valor residual = 4.00*1*2 = 8.00 -> nivel BAJO.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
closed_in_julio(type, code) AS (VALUES
  ('1','5.3'), ('1','7.2'), ('1','7.4'), ('1','9.1'), ('1','7.5.3'),
  ('2','2.3'), ('2','3.2'), ('2','4.2'), ('2','4.3'), ('2','6.3'), ('2','6.4'), ('2','8.2'), ('2','9.2')
),
junio AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Junio 2026'
)
INSERT INTO "MAE_RISK_TREATMENT" (
  "N_RISK_ID_FK", "N_RISK_TREATMENT_METHOD_ID", "C_CONTROL_TYPE", "C_CONTROLS_TO_IMPLEMENT",
  "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID_FK",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  r."N_RISK_ID_PK", 1, 'Preventivo - Administrativo',
  CASE b."C_TYPE"
    WHEN '1' THEN 'Formalizar, documentar e implementar el proceso correspondiente al requisito ' || b."C_NUMERATION_TO_SHOW" || '.'
    ELSE 'Formalizar, documentar e implementar el control ' || b."C_NUMERATION_TO_SHOW" || '.'
  END,
  1.00, 2.00, a."N_VALUATION" * 1.00 * 2.00, 1,
  1, 1, now(), false
FROM "MAE_RISK" r
JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
JOIN "MAE_ACTIVES_INVENTORY" a ON a."N_ACTIVES_INVENTORY_ID_PK" = r."N_ACTIVES_INVENTORY_ID_FK"
CROSS JOIN junio
WHERE b."N_EVALUATION_ID_FK" = junio.id AND (b."C_TYPE", b."C_NUMERATION_TO_SHOW") IN (SELECT type, code FROM closed_in_julio);

COMMIT;
