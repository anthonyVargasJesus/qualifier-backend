-- =====================================================================
-- Seed: evaluación de 10 de los 40 riesgos registrados en 'Evaluación -
-- Mayo 2026' (mismos valores que seed_risk_assessment_junio2026.sql,
-- para los 10 riesgos que ya venían de junio), y actualiza su estado de
-- 'Registrado' a 'En evaluación' en MAE_RISK.
-- Los 20 riesgos nuevos de mayo quedan en 'Registrado' (recién
-- identificados este ciclo, aún sin evaluar).
-- Requiere haber ejecutado antes seed_risks_mayo2026.sql.
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
INSERT INTO "MAE_RISK_ASSESSMENT" (
  "N_RISK_ID", "N_VALUATION_CID", "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE",
  "C_EXISTING_IMPLEMENTED_CONTROLS", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID",
  "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
SELECT rl.risk_id, v.cid, v.menace_level, v.vuln_level, v.existing_controls, v.assessment_value, v.risk_level_id,
       1, 1, now(), false
FROM risk_lookup rl
JOIN (VALUES
  ('1', 7, 0, 5.33, 2.00, 2.00, 'No se han implementado controles formales; solo existe una identificación inicial de partes interesadas sin procedimiento documentado.', 21.32, 3),
  ('1', 52, 0, 5.33, 1.00, 2.00, 'Existe una política de seguridad aprobada que sirve de referencia, pero los objetivos no están formalmente vinculados a ella.', 10.66, 2),
  ('1', 54, 0, 5.33, 1.00, 1.00, 'Los objetivos de seguridad están planteados y se revisan de forma informal en el Comité SGSI.', 5.33, 1),
  ('1', 58, 0, 5.33, 2.00, 2.00, 'No existen controles de planificación (recursos, responsables, plazos) implementados para los objetivos de seguridad.', 21.32, 3),
  ('1', 76, 0, 4.67, 1.00, 1.00, 'Se realizan comunicaciones puntuales por correo institucional, sin un procedimiento formal de comunicación del SGSI.', 4.67, 1),
  ('2', 0, 10, 5.33, 1.00, 2.00, 'La seguridad de la información se considera de forma puntual en algunos proyectos, sin checklist obligatorio en la metodología.', 10.66, 2),
  ('2', 0, 14, 8.00, 2.00, 2.00, 'No existe un esquema de clasificación de información formalmente aprobado; solo se ha iniciado la clasificación de información crítica.', 32.00, 3),
  ('2', 0, 15, 8.00, 1.00, 2.00, 'No se aplica un procedimiento de etiquetado consistente, al depender de un esquema de clasificación aún incompleto.', 16.00, 2),
  ('2', 0, 16, 5.67, 2.00, 2.00, 'No existen controles de transferencia segura (cifrado, VPN, canales autorizados) implementados de forma consistente.', 22.68, 3),
  ('2', 0, 77, 7.33, 2.00, 2.00, 'El uso de programas de utilidad con privilegios elevados se realiza de forma manual, sin control técnico centralizado.', 29.32, 3)
) AS v(breach_type, requirement_id, control_id, cid, menace_level, vuln_level, existing_controls, assessment_value, risk_level_id)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

-- Pasar estos 10 riesgos de 'Registrado' a 'En evaluación'
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
SET "N_RISK_STATUS_ID_FK" = 2, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_RISK_ID_PK" IN (
  SELECT rl.risk_id
  FROM risk_lookup rl
  JOIN (VALUES ('1',7,0), ('1',52,0), ('1',54,0), ('1',58,0), ('1',76,0), ('2',0,10), ('2',0,14), ('2',0,15), ('2',0,16), ('2',0,77))
    AS v(breach_type, requirement_id, control_id)
    ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id
);

COMMIT;
