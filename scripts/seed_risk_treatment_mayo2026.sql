-- =====================================================================
-- Seed: 6 de los 10 riesgos 'En evaluación' pasan a tratamiento en
-- 'Evaluación - Mayo 2026' (mismos valores que
-- seed_risk_treatment_junio2026.sql). Los otros 4 quedan en
-- 'En evaluación'.
-- N_RESIDUAL_RISK_ID_FK queda NULL (igual que en junio).
-- Actualiza el estado de estos 6 riesgos a 'En tratamiento' en MAE_RISK.
-- Requiere haber ejecutado antes seed_risk_assessment_mayo2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
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
  ('1', 7, 0, 'Preventivo - Administrativo', 'Elaborar y aprobar un registro formal de partes interesadas del SGSI, con sus requisitos identificados y su relación con los controles aplicables.', 2.00, 2.00, 21.32, 3),
  ('1', 58, 0, 'Preventivo - Administrativo', 'Elaborar un plan de acción por cada objetivo de seguridad que incluya actividades, recursos necesarios, responsables y plazos de cumplimiento.', 2.00, 2.00, 21.32, 3),
  ('2', 0, 14, 'Preventivo - Administrativo', 'Definir y aprobar un esquema formal de clasificación de la información, y aplicarlo a los activos críticos, en particular la Base de Datos del Padrón Electoral.', 2.00, 2.00, 32.00, 3),
  ('2', 0, 15, 'Preventivo - Administrativo', 'Definir e implementar un procedimiento de etiquetado de información acorde al esquema de clasificación aprobado.', 1.00, 2.00, 16.00, 2),
  ('2', 0, 16, 'Preventivo - Técnico', 'Implementar controles técnicos de transferencia segura de información (cifrado, VPN, canales autorizados) para las comunicaciones con terceros y entre áreas.', 2.00, 2.00, 22.68, 3),
  ('2', 0, 77, 'Preventivo - Técnico', 'Implementar una política técnica (GPO o herramienta de gestión de accesos privilegiados) que restrinja el uso de programas de utilidad con privilegios elevados a personal autorizado.', 2.00, 2.00, 29.32, 3)
) AS v(breach_type, requirement_id, control_id, control_type, controls_to_implement, menace_level, vuln_level, assessment_value, risk_level_id)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

-- Pasar estos 6 riesgos de 'En evaluación' a 'En tratamiento'
WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
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
  JOIN (VALUES ('1',7,0), ('1',58,0), ('2',0,14), ('2',0,15), ('2',0,16), ('2',0,77))
    AS v(breach_type, requirement_id, control_id)
    ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id
);

COMMIT;
