-- =====================================================================
-- Seed: brechas (MAE_BREACH) para las 3 evaluaciones de NTP-ISO/IEC
-- 42001 (Mayo/Junio/Julio 2026). Requiere seed_{requirement,control}_
-- evaluations_ntp42001.sql ya ejecutados (reutiliza exactamente los
-- mismos conjuntos de ítems en brecha: nivel Parcial/No cumple).
--
-- C_TYPE: '1' = requisito, '2' = control (confirmado contra filas
-- reales de ISO 27001). Para requisito, N_CONTROL_ID_FK=0 (sentinel,
-- NOT NULL); para control, N_REQUIREMENT_ID_FK=0. Severidad = default
-- del catálogo de niveles de madurez (Parcial->Alta, No cumple->
-- Crítica, ver MAE_MATURITY_LEVEL.N_BREACH_SEVERITY_ID_FK). Estado =
-- "Abierta" en las 3 evaluaciones (el avance/cierre se refleja en el
-- plan de acción, no acá — separación de responsabilidad más simple
-- que tener que sincronizar el estado en dos tablas).
--
-- Totales: Mayo 49 (19 requisitos + 30 controles), Junio 37 (15+22),
-- Julio 24 (10+14, solo lo que sigue abierto tras la reevaluación
-- parcial — los 13 ítems cerrados en julio no generan brecha julio).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- ---------------------------------------------------------------------
-- Requisitos en brecha — MAYO / JUNIO / JULIO (union de los 3 ciclos
-- en un solo INSERT; cada fila resuelve su propia evaluación por mes)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
req_codes AS (
  SELECT r."N_REQUIREMENT_ID_PK" AS id, r."C_NAME" AS name,
    CASE WHEN r."N_LEVEL" = 2 THEN p1."N_NUMERATION"::text || '.' || r."N_NUMERATION"::text
         WHEN r."N_LEVEL" = 3 THEN p1."N_NUMERATION"::text || '.' || p2."N_NUMERATION"::text || '.' || r."N_NUMERATION"::text
    END AS code,
    p1."N_NUMERATION" AS clause
  FROM "MAE_REQUIREMENT" r
  LEFT JOIN "MAE_REQUIREMENT" p2 ON p2."N_REQUIREMENT_ID_PK" = r."N_PARENT_ID" AND r."N_LEVEL" = 3
  LEFT JOIN "MAE_REQUIREMENT" p1 ON p1."N_REQUIREMENT_ID_PK" = COALESCE(p2."N_PARENT_ID", r."N_PARENT_ID")
  WHERE r."N_STANDARD_ID" = (SELECT id FROM std) AND r."N_LEVEL" > 1
),
resp AS (
  SELECT "N_RESPONSIBLE_ID_PK" AS id, "C_NAME" AS name FROM "MAE_RESPONSIBLE"
  WHERE "N_STANDARD_ID" = (SELECT id FROM std)
),
items(mes, code, level_id) AS (VALUES
  -- Mayo (19)
  ('Mayo','4.4',10), ('Mayo','5.2',10), ('Mayo','5.3',10), ('Mayo','6.2',10), ('Mayo','6.3',10),
  ('Mayo','7.2',10), ('Mayo','7.3',11), ('Mayo','7.4',10), ('Mayo','8.1',11), ('Mayo','8.2',11),
  ('Mayo','8.3',11), ('Mayo','8.4',11), ('Mayo','9.1',10), ('Mayo','10.1',10), ('Mayo','6.1.2',10),
  ('Mayo','6.1.3',10), ('Mayo','6.1.4',11), ('Mayo','7.5.3',10), ('Mayo','9.2.2',10),
  -- Junio (15)
  ('Junio','5.3',10), ('Junio','6.2',10), ('Junio','7.2',10), ('Junio','7.4',10), ('Junio','8.1',10),
  ('Junio','9.1',10), ('Junio','6.1.2',10), ('Junio','6.1.3',10), ('Junio','7.5.3',10), ('Junio','9.2.2',10),
  ('Junio','7.3',11), ('Junio','8.2',11), ('Junio','8.3',11), ('Junio','8.4',11), ('Junio','6.1.4',11),
  -- Julio (10 — solo lo que sigue abierto tras la reevaluación parcial)
  ('Julio','6.2',10), ('Julio','8.1',10), ('Julio','6.1.2',10), ('Julio','6.1.3',10), ('Julio','9.2.2',10),
  ('Julio','7.3',11), ('Julio','8.2',11), ('Julio','8.3',11), ('Julio','8.4',11), ('Julio','6.1.4',11)
)
INSERT INTO "MAE_BREACH" (
  "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK",
  "C_TITLE", "C_DESCRIPTION", "N_BREACH_SEVERITY_ID_FK", "N_BREACH_STATUS_ID_FK", "N_RESPONSIBLE_ID_FK",
  "C_EVIDENCE_DESCRIPTION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED",
  "C_NUMERATION_TO_SHOW"
)
SELECT
  ev."N_EVALUATION_ID_PK", std.id, '1', rc.id, 0,
  'El requisito ' || rc.code || ' no se cumple: ' || rc.name,
  CASE i.level_id
    WHEN 10 THEN 'Implementación parcial: existen avances iniciales pero falta formalizar el proceso completo y su documentación de respaldo.'
    WHEN 11 THEN 'No se ha implementado este punto de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
  END,
  CASE i.level_id WHEN 10 THEN 3 WHEN 11 THEN 4 END,
  1,
  (SELECT resp.id FROM resp WHERE resp.name = CASE rc.clause
      WHEN 4 THEN 'Comité de Gobernanza de IA' WHEN 5 THEN 'Dirección Ejecutiva'
      WHEN 6 THEN 'Oficial de Gobernanza de IA' WHEN 7 THEN 'Equipo de Desarrollo de IA'
      WHEN 8 THEN 'Equipo de Desarrollo de IA' WHEN 9 THEN 'Oficina de Seguridad de la Información'
      WHEN 10 THEN 'Oficial de Gobernanza de IA' END),
  CASE i.level_id
    WHEN 10 THEN 'Evidencia parcial disponible (borradores o avances iniciales), sin aprobación formal.'
    ELSE NULL
  END,
  1, 1, now(), false, rc.code
FROM std, items i
JOIN req_codes rc ON rc.code = i.code
JOIN "MAE_EVALUATION" ev ON ev."N_STANDARD_ID_FK" = (SELECT id FROM std) AND ev."C_DESCRIPTION" = 'Evaluación - ' || i.mes || ' 2026';

-- ---------------------------------------------------------------------
-- Controles en brecha — MAYO / JUNIO / JULIO
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ctrl_codes AS (
  SELECT c."N_CONTROL_ID_PK" AS id, c."C_NAME" AS name,
    g."N_NUMBER"::text || '.' || c."N_NUMBER"::text AS code, g."N_NUMBER" AS group_number
  FROM "MAE_CONTROL" c JOIN "MAE_CONTROL_GROUP" g ON g."N_CONTROL_GROUP_ID_PK" = c."N_CONTROL_GROUP_ID"
  WHERE c."N_STANDARD_ID" = (SELECT id FROM std)
),
resp AS (
  SELECT "N_RESPONSIBLE_ID_PK" AS id, "C_NAME" AS name FROM "MAE_RESPONSIBLE"
  WHERE "N_STANDARD_ID" = (SELECT id FROM std)
),
items(mes, code, level_id) AS (VALUES
  -- Mayo (30)
  ('Mayo','2.2',10), ('Mayo','2.3',10), ('Mayo','3.2',10), ('Mayo','4.2',10), ('Mayo','4.3',10),
  ('Mayo','4.5',10), ('Mayo','5.1',11), ('Mayo','5.2',11), ('Mayo','5.3',11), ('Mayo','5.4',11),
  ('Mayo','6.2',10), ('Mayo','6.3',10), ('Mayo','6.4',10), ('Mayo','6.5',11), ('Mayo','6.6',10),
  ('Mayo','6.7',10), ('Mayo','6.8',10), ('Mayo','6.9',11), ('Mayo','7.1',11), ('Mayo','7.2',10),
  ('Mayo','7.3',11), ('Mayo','7.4',11), ('Mayo','8.1',10), ('Mayo','8.2',10), ('Mayo','8.3',10),
  ('Mayo','8.4',10), ('Mayo','9.2',10), ('Mayo','9.3',10), ('Mayo','10.1',10), ('Mayo','10.2',10),
  -- Junio (22)
  ('Junio','2.3',10), ('Junio','3.2',10), ('Junio','4.2',10), ('Junio','4.3',10), ('Junio','5.1',10),
  ('Junio','6.3',10), ('Junio','6.4',10), ('Junio','6.6',10), ('Junio','6.8',10), ('Junio','7.1',10),
  ('Junio','7.2',10), ('Junio','8.2',10), ('Junio','8.4',10), ('Junio','9.2',10), ('Junio','10.2',10),
  ('Junio','5.2',11), ('Junio','5.3',11), ('Junio','5.4',11), ('Junio','6.5',11), ('Junio','6.9',11),
  ('Junio','7.3',11), ('Junio','7.4',11),
  -- Julio (14 — solo lo que sigue abierto tras la reevaluación parcial)
  ('Julio','5.1',10), ('Julio','6.6',10), ('Julio','6.8',10), ('Julio','7.1',10), ('Julio','7.2',10),
  ('Julio','8.4',10), ('Julio','10.2',10),
  ('Julio','5.2',11), ('Julio','5.3',11), ('Julio','5.4',11), ('Julio','6.5',11), ('Julio','6.9',11),
  ('Julio','7.3',11), ('Julio','7.4',11)
)
INSERT INTO "MAE_BREACH" (
  "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK",
  "C_TITLE", "C_DESCRIPTION", "N_BREACH_SEVERITY_ID_FK", "N_BREACH_STATUS_ID_FK", "N_RESPONSIBLE_ID_FK",
  "C_EVIDENCE_DESCRIPTION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED",
  "C_NUMERATION_TO_SHOW"
)
SELECT
  ev."N_EVALUATION_ID_PK", std.id, '2', 0, cc.id,
  'El control ' || cc.code || ' no se cumple: ' || cc.name,
  CASE i.level_id
    WHEN 10 THEN 'Implementación parcial: existen avances iniciales pero falta formalizar el proceso completo y su documentación de respaldo.'
    WHEN 11 THEN 'No se ha implementado este punto de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
  END,
  CASE i.level_id WHEN 10 THEN 3 WHEN 11 THEN 4 END,
  1,
  (SELECT resp.id FROM resp WHERE resp.name = CASE cc.group_number
      WHEN 2 THEN 'Área Legal y Cumplimiento' WHEN 3 THEN 'Comité de Gobernanza de IA'
      WHEN 4 THEN 'Equipo de Desarrollo de IA' WHEN 5 THEN 'Oficina de Seguridad de la Información'
      WHEN 6 THEN 'Equipo de Desarrollo de IA' WHEN 7 THEN 'Equipo de Datos e IA'
      WHEN 8 THEN 'Área Legal y Cumplimiento' WHEN 9 THEN 'Equipo de Desarrollo de IA'
      WHEN 10 THEN 'Gestión de Proveedores y Terceros' END),
  CASE i.level_id
    WHEN 10 THEN 'Evidencia parcial disponible (borradores o avances iniciales), sin aprobación formal.'
    ELSE NULL
  END,
  1, 1, now(), false, cc.code
FROM std, items i
JOIN ctrl_codes cc ON cc.code = i.code
JOIN "MAE_EVALUATION" ev ON ev."N_STANDARD_ID_FK" = (SELECT id FROM std) AND ev."C_DESCRIPTION" = 'Evaluación - ' || i.mes || ' 2026';

COMMIT;
