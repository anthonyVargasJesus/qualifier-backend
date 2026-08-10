-- =====================================================================
-- Seed: riesgos (MAE_RISK) para las 110 brechas de NTP-ISO/IEC 42001
-- (Mayo 49 + Junio 37 + Julio 24). Requiere seed_breaches_ntp42001.sql
-- ya ejecutado — lee las brechas directo de MAE_BREACH (no repite los
-- listados de ítems).
--
-- MAE_RISK exige activo+amenaza+vulnerabilidad reales (N_ACTIVES_
-- INVENTORY_ID_FK/N_MENACE_ID_FK/N_VULNERABILITY_ID_FK son NOT NULL).
-- NTP 42001 no tiene activos propios en MAE_ACTIVES_INVENTORY (norma
-- nueva) — se reutilizan los 12 activos reales ya cargados para
-- ISO 27001 (catálogo de la empresa, no exclusivo de una norma:
-- servidores, backups, personal, etc. aplican igual a un riesgo de
-- gestión de IA). Amenazas/vulnerabilidades: se reutiliza el catálogo
-- compartido (117 de cada uno) — resultó estar organizado por tema de
-- cláusula de gestión (contexto, liderazgo, riesgos, objetivos...) de
-- forma casi genérica a cualquier norma de sistema de gestión, así que
-- se reutilizan directamente por cláusula/grupo temático (ver los CASE
-- de abajo) sin necesidad de crear entradas nuevas.
--
-- N_RISK_STATUS_ID_FK: "Registrado" (1) para casi todos; "En
-- tratamiento" (3) solo para los 13 riesgos de JUNIO que se cierran en
-- julio (los que van a tener evaluación y tratamiento de riesgo — ver
-- seed_risk_assessment_ntp42001.sql / seed_risk_treatment_ntp42001.sql
-- / seed_control_implementation_ntp42001.sql, que solo cubren ese
-- subconjunto de 13, igual que se hizo para ISO 27001 originalmente
-- (no todos los riesgos llegan a evaluación/tratamiento completo).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
-- Los 13 ítems (5 requisitos + 8 controles) que se cierran en julio —
-- sus riesgos de JUNIO son los que reciben evaluación/tratamiento completo.
closed_in_julio(type, code) AS (VALUES
  ('1','5.3'), ('1','7.2'), ('1','7.4'), ('1','9.1'), ('1','7.5.3'),
  ('2','2.3'), ('2','3.2'), ('2','4.2'), ('2','4.3'), ('2','6.3'), ('2','6.4'), ('2','8.2'), ('2','9.2')
),
junio AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Junio 2026'
)
INSERT INTO "MAE_RISK" (
  "N_ACTIVES_INVENTORY_ID_FK", "N_MENACE_ID_FK", "N_VULNERABILITY_ID_FK", "N_COMPANY_ID_FK",
  "N_EVALUATION_ID_FK", "C_NAME", "N_RISK_STATUS_ID_FK", "N_BREACH_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  CASE
    WHEN b."C_TYPE" = '2' AND g."N_NUMBER" IN (4,5,6,7) THEN
      CASE WHEN g."N_NUMBER" = 7 THEN 33 ELSE 34 END          -- técnico/datos: servidor BD/aplicaciones
    ELSE 26                                                    -- gobernanza/documental: Documentación del SGSI
  END,
  CASE
    WHEN b."C_TYPE" = '1' THEN
      CASE p1."N_NUMERATION"
        WHEN 4 THEN 28 WHEN 5 THEN 32 WHEN 6 THEN 40 WHEN 7 THEN 43 WHEN 8 THEN 47 WHEN 9 THEN 49 WHEN 10 THEN 52
      END
    ELSE
      CASE g."N_NUMBER"
        WHEN 2 THEN 143 WHEN 3 THEN 144 WHEN 4 THEN 151 WHEN 5 THEN 150 WHEN 6 THEN 145
        WHEN 7 THEN 152 WHEN 8 THEN 148 WHEN 9 THEN 149 WHEN 10 THEN 147
      END
  END,
  CASE
    WHEN b."C_TYPE" = '1' THEN
      CASE p1."N_NUMERATION"
        WHEN 4 THEN 28 WHEN 5 THEN 34 WHEN 6 THEN 38 WHEN 7 THEN 44 WHEN 8 THEN 47 WHEN 9 THEN 48 WHEN 10 THEN 50
      END
    ELSE
      CASE g."N_NUMBER"
        WHEN 2 THEN 143 WHEN 3 THEN 144 WHEN 4 THEN 151 WHEN 5 THEN 150 WHEN 6 THEN 145
        WHEN 7 THEN 154 WHEN 8 THEN 148 WHEN 9 THEN 149 WHEN 10 THEN 153
      END
  END,
  1, b."N_EVALUATION_ID_FK", b."C_TITLE",
  CASE WHEN b."N_EVALUATION_ID_FK" = junio.id AND (b."C_TYPE", b."C_NUMERATION_TO_SHOW") IN (SELECT type, code FROM closed_in_julio)
       THEN 3 ELSE 1 END,
  b."N_BREACH_ID_PK", 1, now(), false
FROM "MAE_BREACH" b
CROSS JOIN std
CROSS JOIN junio
LEFT JOIN "MAE_REQUIREMENT" req ON b."C_TYPE" = '1' AND req."N_REQUIREMENT_ID_PK" = b."N_REQUIREMENT_ID_FK"
LEFT JOIN "MAE_REQUIREMENT" p2 ON p2."N_REQUIREMENT_ID_PK" = req."N_PARENT_ID" AND req."N_LEVEL" = 3
LEFT JOIN "MAE_REQUIREMENT" p1 ON p1."N_REQUIREMENT_ID_PK" = COALESCE(p2."N_PARENT_ID", req."N_PARENT_ID")
LEFT JOIN "MAE_CONTROL" ctrl ON b."C_TYPE" = '2' AND ctrl."N_CONTROL_ID_PK" = b."N_CONTROL_ID_FK"
LEFT JOIN "MAE_CONTROL_GROUP" g ON g."N_CONTROL_GROUP_ID_PK" = ctrl."N_CONTROL_GROUP_ID"
WHERE b."N_STANDARD_ID_FK" = (SELECT id FROM std);

COMMIT;
