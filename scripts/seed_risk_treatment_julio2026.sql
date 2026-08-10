-- =====================================================================
-- Seed: 3 de los 6 riesgos 'En evaluación' pasan a tratamiento
-- (MAE_RISK_TREATMENT) en 'Evaluación - Julio 2026'.
-- Selección: los 3 riesgos de nivel ALTO (req. 4.2.2 partes interesadas,
-- control 5.14 transferencia de información, control 8.18 utilidades
-- privilegiadas). Los otros 3 (1 MEDIO + 1 MEDIO + 1 BAJO) quedan en
-- 'En evaluación'.
-- Reutiliza el mismo nivel de amenaza/vulnerabilidad y CID ya registrados
-- en seed_risk_assessment_julio2026.sql para consistencia.
-- N_RESIDUAL_RISK_ID_FK queda NULL: el catálogo MAE_RESIDUAL_RISK solo
-- tiene la categoría ACEPTAR (rango 1-3) y ningún valor de estos 3
-- riesgos cae en ese rango.
-- Actualiza el estado de estos 3 riesgos de 'En evaluación' a 'En
-- tratamiento' en MAE_RISK.
-- Requiere haber ejecutado antes seed_risk_assessment_julio2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Julio 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
),
risk_lookup AS (
  SELECT r."N_RISK_ID_PK" AS risk_id, b."C_TYPE" AS breach_type,
         b."N_REQUIREMENT_ID_FK" AS requirement_id, b."N_CONTROL_ID_FK" AS control_id
  FROM "MAE_RISK" r
  JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
  WHERE r."N_EVALUATION_ID_FK" = (SELECT id FROM ev)
)
INSERT INTO "MAE_RISK_TREATMENT" (
  "N_RISK_ID_FK", "N_RISK_TREATMENT_METHOD_ID", "C_CONTROL_TYPE", "C_CONTROLS_TO_IMPLEMENT",
  "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID_FK",
  "N_RESIDUAL_RISK_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT rl.risk_id, 1, v.control_type, v.controls_to_implement,
       v.menace_level, v.vuln_level, v.assessment_value, v.risk_level_id, NULL, 1, 1, now(), false
FROM risk_lookup rl
JOIN (VALUES
  ('1', 127, 0, 'Preventivo - Administrativo', 'Levantar y aprobar la matriz de requisitos de las partes interesadas ya identificadas, vinculándolos con los controles aplicables del SGSI.', 2.00, 2.00, 21.32, 3),
  ('2', 0, 16, 'Preventivo - Técnico', 'Difundir el procedimiento de transferencia segura ya definido (cifrado, VPN, canales autorizados) y capacitar al personal que transfiere información crítica.', 2.00, 2.00, 22.68, 3),
  ('2', 0, 77, 'Preventivo - Técnico', 'Extender la política técnica (GPO) de restricción de utilidades con privilegios elevados a la totalidad de los sistemas y completar la auditoría de uso histórico.', 2.00, 2.00, 29.32, 3)
) AS v(breach_type, requirement_id, control_id, control_type, controls_to_implement, menace_level, vuln_level, assessment_value, risk_level_id)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

-- Pasar estos 3 riesgos de 'En evaluación' a 'En tratamiento'
WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Julio 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
),
risk_lookup AS (
  SELECT r."N_RISK_ID_PK" AS risk_id, b."C_TYPE" AS breach_type,
         b."N_REQUIREMENT_ID_FK" AS requirement_id, b."N_CONTROL_ID_FK" AS control_id
  FROM "MAE_RISK" r
  JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
  WHERE r."N_EVALUATION_ID_FK" = (SELECT id FROM ev)
)
UPDATE "MAE_RISK"
SET "N_RISK_STATUS_ID_FK" = 3, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_RISK_ID_PK" IN (
  SELECT rl.risk_id
  FROM risk_lookup rl
  JOIN (VALUES ('1',127,0), ('2',0,16), ('2',0,77))
    AS v(breach_type, requirement_id, control_id)
    ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id
);

COMMIT;
