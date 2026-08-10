-- =====================================================================
-- Seed: evaluación de riesgo (MAE_RISK_ASSESSMENT) para NTP-ISO/IEC
-- 42001. Requiere seed_risks_ntp42001.sql ya ejecutado.
--
-- Solo para los 13 riesgos de JUNIO que se cierran en julio (mismo
-- criterio que ISO 27001: no todos los riesgos llegan a evaluación
-- formal). N_VALUATION_CID = valoración del activo asociado (4.00 en
-- los 13 casos: activos 26 "Documentación del SGSI" o 34 "Servidor de
-- Aplicaciones", ver seed_risks_ntp42001.sql). Amenaza=2.00,
-- vulnerabilidad=2.00 (escala 1-3) -> valor = 4.00*2*2 = 16.00 ->
-- nivel MEDIO (rango 9-19 de MAE_RISK_LEVEL).
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
INSERT INTO "MAE_RISK_ASSESSMENT" (
  "N_RISK_ID", "N_VALUATION_CID", "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE",
  "C_EXISTING_IMPLEMENTED_CONTROLS", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID", "N_COMPANY_ID",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
SELECT
  r."N_RISK_ID_PK", a."N_VALUATION", 2.00, 2.00,
  'Controles existentes insuficientes: implementación parcial sin formalizar ni documentar.',
  a."N_VALUATION" * 2.00 * 2.00, 2, 1, 1, now(), false
FROM "MAE_RISK" r
JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
JOIN "MAE_ACTIVES_INVENTORY" a ON a."N_ACTIVES_INVENTORY_ID_PK" = r."N_ACTIVES_INVENTORY_ID_FK"
CROSS JOIN junio
WHERE b."N_EVALUATION_ID_FK" = junio.id AND (b."C_TYPE", b."C_NUMERATION_TO_SHOW") IN (SELECT type, code FROM closed_in_julio);

COMMIT;
