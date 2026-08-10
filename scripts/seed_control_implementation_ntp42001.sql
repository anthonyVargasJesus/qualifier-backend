-- =====================================================================
-- Seed: implementación de control (MAE_CONTROL_IMPLEMENTATION) para
-- NTP-ISO/IEC 42001. Requiere seed_risk_treatment_ntp42001.sql ya
-- ejecutado — mismos 13 riesgos de junio que se cierran en julio,
-- documentando la actividad que efectivamente los cerró.
-- D_START_DATE = inicio de junio, D_VERIFICATION_DATE = inicio de
-- julio (cuando se reevaluó y se confirmó el cierre).
-- L_IS_IMPLEMENTED=true, L_IS_EFFECTIVE=true: son los 13 casos que sí
-- se completaron y verificaron.
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
  SELECT "N_EVALUATION_ID_PK" AS id, "D_START_DATE" AS start_date FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Junio 2026'
),
julio AS (
  SELECT "D_START_DATE" AS start_date FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Julio 2026'
)
INSERT INTO "MAE_CONTROL_IMPLEMENTATION" (
  "N_RISK_ID_FK", "C_ACTIVITIES", "D_START_DATE", "D_VERIFICATION_DATE", "N_RESPONSIBLE_ID_FK",
  "C_OBSERVATION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED",
  "L_IS_IMPLEMENTED", "L_IS_EFFECTIVE"
)
SELECT
  r."N_RISK_ID_PK",
  CASE b."C_TYPE"
    WHEN '1' THEN 'Formalización y documentación del proceso correspondiente al requisito ' || b."C_NUMERATION_TO_SHOW" || '.'
    ELSE 'Formalización y documentación del control ' || b."C_NUMERATION_TO_SHOW" || '.'
  END,
  junio.start_date, julio.start_date, b."N_RESPONSIBLE_ID_FK",
  'Implementación verificada en la reevaluación de julio 2026; el ítem pasó a estado Cumple.',
  1, 1, now(), false, true, true
FROM "MAE_RISK" r
JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
CROSS JOIN junio
CROSS JOIN julio
WHERE b."N_EVALUATION_ID_FK" = junio.id AND (b."C_TYPE", b."C_NUMERATION_TO_SHOW") IN (SELECT type, code FROM closed_in_julio);

COMMIT;
