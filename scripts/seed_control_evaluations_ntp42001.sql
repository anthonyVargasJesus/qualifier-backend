-- =====================================================================
-- Seed: evaluaciones de control (MAE_CONTROL_EVALUATION) para las 3
-- evaluaciones de NTP-ISO/IEC 42001 (Mayo/Junio/Julio 2026). Requiere
-- seed_evaluations_ntp42001.sql y seed_responsibles_ntp42001.sql ya
-- ejecutados. Mismo diseño/criterio que seed_requirement_evaluations_
-- ntp42001.sql (leer ese README de cabecera primero) aplicado a los
-- 38 controles del Anexo A.
--
-- C_JUSTIFICATION/C_IMPROVEMENT_ACTIONS/C_CONTROL_DESCRIPTION son
-- NOT NULL en esta tabla (a diferencia de requirement_evaluation) —
-- se usa '' en vez de NULL donde no aplica (mismo criterio que ya
-- usan las filas reales de ISO 27001, ver ejemplos en vivo).
--
-- Julio: SOLO re-evalúa los 22 controles en brecha en junio — 8 se
-- cierran a Cumple, 14 quedan igual.
-- Mayo: copia junio con 10 controles retrocedidos un nivel (8 de
-- Cumple a Parcial, 2 de Parcial a No cumple).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- ---------------------------------------------------------------------
-- JUNIO 2026 — línea base, los 38 controles
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Junio 2026'
),
ctrl_codes AS (
  SELECT c."N_CONTROL_ID_PK" AS id, c."C_NAME" AS name, c."C_DESCRIPTION" AS description,
    g."N_NUMBER"::text || '.' || c."N_NUMBER"::text AS code, g."N_NUMBER" AS group_number
  FROM "MAE_CONTROL" c JOIN "MAE_CONTROL_GROUP" g ON g."N_CONTROL_GROUP_ID_PK" = c."N_CONTROL_GROUP_ID"
  WHERE c."N_STANDARD_ID" = (SELECT id FROM std)
),
resp AS (
  SELECT "N_RESPONSIBLE_ID_PK" AS id, "C_NAME" AS name FROM "MAE_RESPONSIBLE"
  WHERE "N_STANDARD_ID" = (SELECT id FROM std)
),
items(code, level_id) AS (VALUES
  ('2.1', 9), ('2.2', 9), ('2.3', 10),
  ('3.1', 9), ('3.2', 10),
  ('4.1', 9), ('4.2', 10), ('4.3', 10), ('4.4', 9), ('4.5', 9),
  ('5.1', 10), ('5.2', 11), ('5.3', 11), ('5.4', 11),
  ('6.1', 9), ('6.2', 9), ('6.3', 10), ('6.4', 10), ('6.5', 11), ('6.6', 10), ('6.7', 9), ('6.8', 10), ('6.9', 11),
  ('7.1', 10), ('7.2', 10), ('7.3', 11), ('7.4', 11), ('7.5', 12),
  ('8.1', 9), ('8.2', 10), ('8.3', 9), ('8.4', 10),
  ('9.1', 9), ('9.2', 10), ('9.3', 9),
  ('10.1', 9), ('10.2', 10), ('10.3', 12)
)
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, cc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE cc.group_number
      WHEN 2 THEN 'Área Legal y Cumplimiento' WHEN 3 THEN 'Comité de Gobernanza de IA'
      WHEN 4 THEN 'Equipo de Desarrollo de IA' WHEN 5 THEN 'Oficina de Seguridad de la Información'
      WHEN 6 THEN 'Equipo de Desarrollo de IA' WHEN 7 THEN 'Equipo de Datos e IA'
      WHEN 8 THEN 'Área Legal y Cumplimiento' WHEN 9 THEN 'Equipo de Desarrollo de IA'
      WHEN 10 THEN 'Gestión de Proveedores y Terceros' END),
  CASE i.level_id
    WHEN 9 THEN 'Control ' || cc.code || ' (' || cc.name || ') implementado y documentado. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Control ' || cc.code || ' con implementación parcial: existen avances iniciales pero falta formalizar el proceso completo, su documentación de respaldo y la evidencia asociada.'
    WHEN 11 THEN 'Control ' || cc.code || ' no implementado de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
    WHEN 12 THEN 'Control ' || cc.code || ' no aplica al alcance actual del sistema de gestión de IA de la organización.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE ''
  END,
  '', std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN ctrl_codes cc ON cc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

-- ---------------------------------------------------------------------
-- MAYO 2026 — copia de junio, con 10 controles retrocedidos un nivel
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
),
ctrl_codes AS (
  SELECT c."N_CONTROL_ID_PK" AS id, c."C_NAME" AS name, c."C_DESCRIPTION" AS description,
    g."N_NUMBER"::text || '.' || c."N_NUMBER"::text AS code, g."N_NUMBER" AS group_number
  FROM "MAE_CONTROL" c JOIN "MAE_CONTROL_GROUP" g ON g."N_CONTROL_GROUP_ID_PK" = c."N_CONTROL_GROUP_ID"
  WHERE c."N_STANDARD_ID" = (SELECT id FROM std)
),
resp AS (
  SELECT "N_RESPONSIBLE_ID_PK" AS id, "C_NAME" AS name FROM "MAE_RESPONSIBLE"
  WHERE "N_STANDARD_ID" = (SELECT id FROM std)
),
-- Mismo nivel que junio salvo 10 excepciones retrocedidas (ver comentario arriba)
items(code, level_id) AS (VALUES
  ('2.1', 9), ('2.2', 10), ('2.3', 10),
  ('3.1', 9), ('3.2', 10),
  ('4.1', 9), ('4.2', 10), ('4.3', 10), ('4.4', 9), ('4.5', 10),
  ('5.1', 11), ('5.2', 11), ('5.3', 11), ('5.4', 11),
  ('6.1', 9), ('6.2', 10), ('6.3', 10), ('6.4', 10), ('6.5', 11), ('6.6', 10), ('6.7', 10), ('6.8', 10), ('6.9', 11),
  ('7.1', 11), ('7.2', 10), ('7.3', 11), ('7.4', 11), ('7.5', 12),
  ('8.1', 10), ('8.2', 10), ('8.3', 10), ('8.4', 10),
  ('9.1', 9), ('9.2', 10), ('9.3', 10),
  ('10.1', 10), ('10.2', 10), ('10.3', 12)
)
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, cc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE cc.group_number
      WHEN 2 THEN 'Área Legal y Cumplimiento' WHEN 3 THEN 'Comité de Gobernanza de IA'
      WHEN 4 THEN 'Equipo de Desarrollo de IA' WHEN 5 THEN 'Oficina de Seguridad de la Información'
      WHEN 6 THEN 'Equipo de Desarrollo de IA' WHEN 7 THEN 'Equipo de Datos e IA'
      WHEN 8 THEN 'Área Legal y Cumplimiento' WHEN 9 THEN 'Equipo de Desarrollo de IA'
      WHEN 10 THEN 'Gestión de Proveedores y Terceros' END),
  CASE i.level_id
    WHEN 9 THEN 'Control ' || cc.code || ' (' || cc.name || ') implementado y documentado. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Control ' || cc.code || ' con implementación parcial: existen avances iniciales pero falta formalizar el proceso completo, su documentación de respaldo y la evidencia asociada.'
    WHEN 11 THEN 'Control ' || cc.code || ' no implementado de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
    WHEN 12 THEN 'Control ' || cc.code || ' no aplica al alcance actual del sistema de gestión de IA de la organización.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE ''
  END,
  '', std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN ctrl_codes cc ON cc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

-- ---------------------------------------------------------------------
-- JULIO 2026 — reevaluación PARCIAL: solo los 22 controles en brecha
-- en junio (8 se cierran a Cumple, 14 quedan igual)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Julio 2026'
),
ctrl_codes AS (
  SELECT c."N_CONTROL_ID_PK" AS id, c."C_NAME" AS name, c."C_DESCRIPTION" AS description,
    g."N_NUMBER"::text || '.' || c."N_NUMBER"::text AS code, g."N_NUMBER" AS group_number
  FROM "MAE_CONTROL" c JOIN "MAE_CONTROL_GROUP" g ON g."N_CONTROL_GROUP_ID_PK" = c."N_CONTROL_GROUP_ID"
  WHERE c."N_STANDARD_ID" = (SELECT id FROM std)
),
resp AS (
  SELECT "N_RESPONSIBLE_ID_PK" AS id, "C_NAME" AS name FROM "MAE_RESPONSIBLE"
  WHERE "N_STANDARD_ID" = (SELECT id FROM std)
),
items(code, level_id) AS (VALUES
  -- cerrados (Parcial -> Cumple)
  ('2.3', 9), ('3.2', 9), ('4.2', 9), ('4.3', 9), ('6.3', 9), ('6.4', 9), ('8.2', 9), ('9.2', 9),
  -- siguen igual
  ('5.1', 10), ('6.6', 10), ('6.8', 10), ('7.1', 10), ('7.2', 10), ('8.4', 10), ('10.2', 10),
  ('5.2', 11), ('5.3', 11), ('5.4', 11), ('6.5', 11), ('6.9', 11), ('7.3', 11), ('7.4', 11)
)
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, cc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE cc.group_number
      WHEN 2 THEN 'Área Legal y Cumplimiento' WHEN 3 THEN 'Comité de Gobernanza de IA'
      WHEN 4 THEN 'Equipo de Desarrollo de IA' WHEN 5 THEN 'Oficina de Seguridad de la Información'
      WHEN 6 THEN 'Equipo de Desarrollo de IA' WHEN 7 THEN 'Equipo de Datos e IA'
      WHEN 8 THEN 'Área Legal y Cumplimiento' WHEN 9 THEN 'Equipo de Desarrollo de IA'
      WHEN 10 THEN 'Gestión de Proveedores y Terceros' END),
  CASE i.level_id
    WHEN 9 THEN 'Control ' || cc.code || ' (' || cc.name || ') cerrado en julio: se completó la implementación y documentación pendiente de junio. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Control ' || cc.code || ' se mantiene con implementación parcial: continúan pendientes la formalización del proceso y su documentación de respaldo.'
    WHEN 11 THEN 'Control ' || cc.code || ' se mantiene sin implementar de manera formal.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE ''
  END,
  '', std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN ctrl_codes cc ON cc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

COMMIT;
