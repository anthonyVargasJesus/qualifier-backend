-- =====================================================================
-- Seed: evaluaciones de requisito (MAE_REQUIREMENT_EVALUATION) para
-- las 3 evaluaciones de NTP-ISO/IEC 42001 (Mayo/Junio/Julio 2026).
-- Requiere seed_evaluations_ntp42001.sql y seed_responsibles_ntp42001.sql
-- ya ejecutados.
--
-- Solo se evalúan las 32 hojas reales (no los 11 contenedores con
-- hijos) — mismo comportamiento real que ISO 27001, donde sus
-- encabezados de nivel 1 nunca reciben una evaluación propia (ver
-- README_ntp42001.md).
--
-- Contenido TEMPLATIZADO por nivel de madurez (no prosa única por
-- ítem, dado el volumen: 32 requisitos x 3 ciclos): la justificación/
-- acciones de mejora son genéricas según el nivel (Cumple/Parcial/No
-- cumple), con el código y nombre del requisito insertados en el
-- texto para variar un poco. El responsable se asigna por cláusula
-- (área temática), no por ítem individual — ver mapeo en el CASE de
-- abajo, coincide con seed_responsibles_ntp42001.sql.
--
-- Julio: SOLO re-evalúa los 15 requisitos que estaban en brecha en
-- junio (Parcial/No cumple) — de esos, 5 se cierran a Cumple y 10
-- quedan igual. Los otros 17 requisitos que ya estaban Cumple en junio
-- NO se tocan en julio (mismo patrón usado para ISO 27001: julio es
-- una REEVALUACIÓN PARCIAL, no una vuelta completa).
-- Mayo: copia el estado completo de junio y retrocede 5 requisitos un
-- nivel (4 de Cumple a Parcial, 1 de Parcial a No cumple), simulando
-- un punto de control anterior con más brechas.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- ---------------------------------------------------------------------
-- JUNIO 2026 — línea base, los 32 requisitos evaluables
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Junio 2026'
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
items(code, level_id) AS (VALUES
  ('4.1', 9), ('4.2', 9), ('4.3', 9), ('4.4', 9),
  ('5.1', 9), ('5.2', 9), ('5.3', 10),
  ('6.2', 10), ('6.3', 9),
  ('7.1', 9), ('7.2', 10), ('7.3', 11), ('7.4', 10),
  ('8.1', 10), ('8.2', 11), ('8.3', 11), ('8.4', 11),
  ('9.1', 10),
  ('10.1', 9), ('10.2', 9),
  ('6.1.1', 9), ('6.1.2', 10), ('6.1.3', 10), ('6.1.4', 11),
  ('7.5.1', 9), ('7.5.2', 9), ('7.5.3', 10),
  ('9.2.1', 9), ('9.2.2', 10),
  ('9.3.1', 9), ('9.3.2', 9), ('9.3.3', 9)
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, rc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE rc.clause
      WHEN 4 THEN 'Comité de Gobernanza de IA' WHEN 5 THEN 'Dirección Ejecutiva'
      WHEN 6 THEN 'Oficial de Gobernanza de IA' WHEN 7 THEN 'Equipo de Desarrollo de IA'
      WHEN 8 THEN 'Equipo de Desarrollo de IA' WHEN 9 THEN 'Oficina de Seguridad de la Información'
      WHEN 10 THEN 'Oficial de Gobernanza de IA' END),
  CASE i.level_id
    WHEN 9 THEN 'Requisito ' || rc.code || ' (' || rc.name || ') implementado y documentado. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Requisito ' || rc.code || ' con implementación parcial: existen avances iniciales pero falta formalizar el proceso completo, su documentación de respaldo y la evidencia asociada.'
    WHEN 11 THEN 'Requisito ' || rc.code || ' no implementado de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE NULL
  END,
  std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN req_codes rc ON rc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

-- ---------------------------------------------------------------------
-- MAYO 2026 — copia de junio, con 5 requisitos retrocedidos un nivel
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
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
-- Mismo nivel que junio salvo 5 excepciones retrocedidas (ver comentario arriba)
items(code, level_id) AS (VALUES
  ('4.1', 9), ('4.2', 9), ('4.3', 9), ('4.4', 10),
  ('5.1', 9), ('5.2', 10), ('5.3', 10),
  ('6.2', 10), ('6.3', 10),
  ('7.1', 9), ('7.2', 10), ('7.3', 11), ('7.4', 10),
  ('8.1', 11), ('8.2', 11), ('8.3', 11), ('8.4', 11),
  ('9.1', 10),
  ('10.1', 10), ('10.2', 9),
  ('6.1.1', 9), ('6.1.2', 10), ('6.1.3', 10), ('6.1.4', 11),
  ('7.5.1', 9), ('7.5.2', 9), ('7.5.3', 10),
  ('9.2.1', 9), ('9.2.2', 10),
  ('9.3.1', 9), ('9.3.2', 9), ('9.3.3', 9)
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, rc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE rc.clause
      WHEN 4 THEN 'Comité de Gobernanza de IA' WHEN 5 THEN 'Dirección Ejecutiva'
      WHEN 6 THEN 'Oficial de Gobernanza de IA' WHEN 7 THEN 'Equipo de Desarrollo de IA'
      WHEN 8 THEN 'Equipo de Desarrollo de IA' WHEN 9 THEN 'Oficina de Seguridad de la Información'
      WHEN 10 THEN 'Oficial de Gobernanza de IA' END),
  CASE i.level_id
    WHEN 9 THEN 'Requisito ' || rc.code || ' (' || rc.name || ') implementado y documentado. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Requisito ' || rc.code || ' con implementación parcial: existen avances iniciales pero falta formalizar el proceso completo, su documentación de respaldo y la evidencia asociada.'
    WHEN 11 THEN 'Requisito ' || rc.code || ' no implementado de manera formal. No existe evidencia de un proceso, política o control operativo vigente.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE NULL
  END,
  std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN req_codes rc ON rc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

-- ---------------------------------------------------------------------
-- JULIO 2026 — reevaluación PARCIAL: solo los 15 requisitos en brecha
-- en junio (5 se cierran a Cumple, 10 quedan igual)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
),
ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id FROM "MAE_EVALUATION"
  WHERE "N_STANDARD_ID_FK" = (SELECT id FROM std) AND "C_DESCRIPTION" = 'Evaluación - Julio 2026'
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
items(code, level_id) AS (VALUES
  -- cerrados (Parcial -> Cumple)
  ('5.3', 9), ('7.2', 9), ('7.4', 9), ('9.1', 9), ('7.5.3', 9),
  -- siguen igual
  ('6.2', 10), ('8.1', 10), ('6.1.2', 10), ('6.1.3', 10), ('9.2.2', 10),
  ('7.3', 11), ('8.2', 11), ('8.3', 11), ('8.4', 11), ('6.1.4', 11)
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE", "N_RESPONSIBLE_ID_FK",
  "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "N_STANDARD_ID_FK", "N_COMPANY_ID_FK",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  ev.id, rc.id, i.level_id, ml."N_VALUE",
  (SELECT resp.id FROM resp WHERE resp.name = CASE rc.clause
      WHEN 4 THEN 'Comité de Gobernanza de IA' WHEN 5 THEN 'Dirección Ejecutiva'
      WHEN 6 THEN 'Oficial de Gobernanza de IA' WHEN 7 THEN 'Equipo de Desarrollo de IA'
      WHEN 8 THEN 'Equipo de Desarrollo de IA' WHEN 9 THEN 'Oficina de Seguridad de la Información'
      WHEN 10 THEN 'Oficial de Gobernanza de IA' END),
  CASE i.level_id
    WHEN 9 THEN 'Requisito ' || rc.code || ' (' || rc.name || ') cerrado en julio: se completó la implementación y documentación pendiente de junio. Evidencia disponible en el repositorio de gestión documental del SGIA.'
    WHEN 10 THEN 'Requisito ' || rc.code || ' se mantiene con implementación parcial: continúan pendientes la formalización del proceso y su documentación de respaldo.'
    WHEN 11 THEN 'Requisito ' || rc.code || ' se mantiene sin implementar de manera formal.'
  END,
  CASE i.level_id
    WHEN 10 THEN 'Formalizar y documentar el proceso, y reunir la evidencia pendiente.'
    WHEN 11 THEN 'Definir e implementar el proceso correspondiente, asignar responsable y establecer un cronograma de implementación.'
    ELSE NULL
  END,
  std.id, 1, 1, now(), false
FROM std, ev, items i
JOIN req_codes rc ON rc.code = i.code
JOIN "MAE_MATURITY_LEVEL" ml ON ml."N_MATURITY_LEVEL_ID_PK" = i.level_id;

COMMIT;
