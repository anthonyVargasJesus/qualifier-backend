-- =====================================================================
-- Seed: evaluación de 6 de los 13 riesgos registrados en 'Evaluación -
-- Julio 2026' (los mismos activo/amenaza/vulnerabilidad que su
-- equivalente en Junio, al persistir el mismo problema — se reutiliza el
-- mismo CID y niveles de amenaza/vulnerabilidad que Junio calculó para
-- ese mismo activo).
-- Carga: MAE_RISK_ASSESSMENT (valor = CID * nivel de amenaza * nivel de
-- vulnerabilidad; nivel de riesgo según MAE_RISK_LEVEL: BAJO 1-8,
-- MEDIO 9-19, ALTO 20-45), y actualiza el estado de esos 6 riesgos de
-- 'Registrado' a 'En evaluación' en MAE_RISK.
-- Los otros 7 riesgos quedan en 'Registrado' (sin evaluar este ciclo).
-- Requiere haber ejecutado antes seed_risks_julio2026.sql.
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
INSERT INTO "MAE_RISK_ASSESSMENT" (
  "N_RISK_ID", "N_VALUATION_CID", "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE",
  "C_EXISTING_IMPLEMENTED_CONTROLS", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID",
  "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
SELECT rl.risk_id, v.cid, v.menace_level, v.vuln_level, v.existing_controls, v.assessment_value, v.risk_level_id,
       1, 1, now(), false
FROM risk_lookup rl
JOIN (VALUES
  ('1', 127, 0, 5.33, 2.00, 2.00, 'Existe ya un registro de partes interesadas aprobado, pero aún no se ha iniciado el levantamiento formal de sus requisitos.', 21.32, 3),
  ('1', 52, 0, 5.33, 1.00, 2.00, 'La matriz de vinculación entre objetivos, política y riesgos está en elaboración, pendiente de aprobación por el Comité SGSI.', 10.66, 2),
  ('1', 55, 0, 5.33, 1.00, 1.00, 'La actualización del documento de objetivos con las referencias al registro de riesgos está en elaboración, pendiente de aprobación.', 5.33, 1),
  ('2', 0, 15, 8.00, 1.00, 2.00, 'El esquema de clasificación de información ya fue aprobado en julio; el procedimiento de etiquetado aún no ha sido definido ni aplicado.', 16.00, 2),
  ('2', 0, 16, 5.67, 2.00, 2.00, 'Se han definido los canales autorizados y el uso de cifrado/VPN, pero el procedimiento aún no ha sido difundido ni el personal capacitado.', 22.68, 3),
  ('2', 0, 77, 7.33, 2.00, 2.00, 'La política GPO de restricción de utilidades privilegiadas ya está desplegada en los servidores críticos; falta extenderla al resto de sistemas y auditar el uso histórico.', 29.32, 3)
) AS v(breach_type, requirement_id, control_id, cid, menace_level, vuln_level, existing_controls, assessment_value, risk_level_id)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

-- Pasar estos 6 riesgos de 'Registrado' a 'En evaluación'
-- (cada sentencia necesita su propio WITH; las CTEs de arriba no se
-- heredan de la sentencia INSERT anterior)
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
SET "N_RISK_STATUS_ID_FK" = 2, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_RISK_ID_PK" IN (
  SELECT rl.risk_id
  FROM risk_lookup rl
  JOIN (VALUES ('1',127,0), ('1',52,0), ('1',55,0), ('2',0,15), ('2',0,16), ('2',0,77))
    AS v(breach_type, requirement_id, control_id)
    ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id
);

COMMIT;
