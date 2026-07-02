-- =====================================================================
-- Seed: riesgos por defecto sugeridos para cada requerimiento ISO 27001
-- Carga: MAE_VULNERABILITY_TYPE (1 nuevo tipo), MAE_MENACE, MAE_VULNERABILITY,
--        MAE_DEFAULT_RISK, MAE_REQUIREMENT_IN_DEFAULT_RISK
-- Alcance: standardId = 4 (ISO 27001), companyId = 1 (ONPE),
--          87 requerimientos evaluables de nivel 3
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- 1) Nuevo tipo de vulnerabilidad para brechas organizacionales/de proceso
WITH new_vuln_type AS (
  INSERT INTO "MAE_VULNERABILITY_TYPE" ("C_NAME", "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Organización', 1, 1, now(), false)
  RETURNING "N_VULNERABILITY_TYPE_ID_PK"
),

-- 2) Amenazas (menaces)
new_menaces AS (
  INSERT INTO "MAE_MENACE" ("C_NAME", "N_MENACE_TYPE_ID_FK", "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES
    ('Toma de decisiones estratégicas sin considerar el contexto interno o externo', 6, 1, 1, now(), false),
    ('Incumplimiento de requisitos legales, regulatorios o contractuales de partes interesadas', 6, 1, 1, now(), false),
    ('Alcance del SGSI desactualizado frente a cambios organizacionales', 6, 1, 1, now(), false),
    ('Interrupción o degradación del ciclo de mejora continua del SGSI', 6, 1, 1, now(), false),
    ('Falta de compromiso o soporte visible de la alta dirección', 6, 1, 1, now(), false),
    ('Uso de recursos insuficientes para sostener el SGSI', 6, 1, 1, now(), false),
    ('Política de seguridad desalineada con los objetivos y contexto de la organización', 6, 1, 1, now(), false),
    ('Desconocimiento del personal y partes interesadas sobre la política de seguridad', 4, 1, 1, now(), false),
    ('Asignación inadecuada de roles y responsabilidades de seguridad', 6, 1, 1, now(), false),
    ('Materialización de riesgos no identificados ni tratados oportunamente', 6, 1, 1, now(), false),
    ('Toma de decisiones de seguridad basada en riesgos mal evaluados', 6, 1, 1, now(), false),
    ('Persistencia de riesgos de seguridad sin mitigar', 6, 1, 1, now(), false),
    ('Objetivos de seguridad desalineados con la política y el contexto de riesgo', 6, 1, 1, now(), false),
    ('Desviación no detectada del cumplimiento de los objetivos de seguridad', 6, 1, 1, now(), false),
    ('Incumplimiento de los objetivos de seguridad por planificación incompleta', 6, 1, 1, now(), false),
    ('Desconocimiento del personal sobre sus obligaciones de seguridad por falta de competencia', 4, 1, 1, now(), false),
    ('Error humano por falta de concientización en seguridad de la información', 4, 1, 1, now(), false),
    ('Fuga o divulgación no autorizada de información por comunicación mal gestionada', 6, 1, 1, now(), false),
    ('Falta de trazabilidad y evidencia documental del SGSI', 6, 1, 1, now(), false),
    ('Cambios no controlados o procesos tercerizados sin supervisión', 6, 1, 1, now(), false),
    ('Recurrencia de no conformidades no detectadas oportunamente', 6, 1, 1, now(), false),
    ('Desviación no detectada del desempeño del SGSI', 6, 1, 1, now(), false),
    ('Recurrencia de no conformidades no corregidas', 6, 1, 1, now(), false),
    ('Toma de decisiones directivas sin información objetiva del desempeño del SGSI', 6, 1, 1, now(), false),
    ('Resistencia al cambio o estancamiento en la mejora del SGSI', 6, 1, 1, now(), false)
  RETURNING "N_MENACE_ID_PK", "C_NAME"
),

-- 3) Vulnerabilidades
new_vulns AS (
  INSERT INTO "MAE_VULNERABILITY" ("C_NAME", "N_VULNERABILITY_TYPE_ID_FK", "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  SELECT v.name, t."N_VULNERABILITY_TYPE_ID_PK", 1, 1, now(), false
  FROM new_vuln_type t,
  (VALUES
    ('Ausencia de un procedimiento formal para determinar el contexto interno y externo del SGSI'),
    ('Ausencia de un procedimiento formal para identificar partes interesadas y sus requisitos'),
    ('Ausencia de un procedimiento para definir y revisar el alcance del SGSI'),
    ('Ausencia de un SGSI formalmente establecido, implementado y mantenido'),
    ('Ausencia de liderazgo y participación activa de la alta dirección en el SGSI'),
    ('Falta de asignación de recursos (tiempo, presupuesto, personal) para el SGSI'),
    ('Falta de una política de seguridad documentada, aprobada y alineada a la organización'),
    ('Política de seguridad no comunicada ni disponible a las partes interesadas'),
    ('Roles y responsabilidades de seguridad no definidos ni asignados formalmente'),
    ('Ausencia de un procedimiento para determinar y planificar acciones frente a riesgos y oportunidades'),
    ('Ausencia de una metodología formal de evaluación de riesgos de seguridad de la información'),
    ('Ausencia de un plan de tratamiento de riesgos de seguridad de la información'),
    ('Objetivos de seguridad no medibles ni alineados a la política de seguridad'),
    ('Falta de medición, comunicación o actualización de los objetivos de seguridad'),
    ('Planificación de objetivos sin recursos, responsables o plazos definidos'),
    ('Ausencia de un programa de gestión de competencias en seguridad de la información'),
    ('Ausencia de un programa de concientización en seguridad de la información'),
    ('Ausencia de canales y procedimientos definidos de comunicación interna y externa'),
    ('Información documentada incompleta, desactualizada o sin control de versiones'),
    ('Ausencia de control de cambios y de supervisión de procesos tercerizados'),
    ('Ausencia de un programa de auditoría interna planificado y ejecutado'),
    ('Falta de indicadores y metodología de monitoreo, medición, análisis y evaluación'),
    ('Ausencia de un procedimiento de gestión de no conformidades y acciones correctivas'),
    ('Revisiones por la dirección no realizadas o sin insumos suficientes'),
    ('Ausencia de mecanismos formales de mejora continua')
  ) AS v(name)
  RETURNING "N_VULNERABILITY_ID_PK", "C_NAME"
),

-- 4) Riesgos por defecto (combinación amenaza + vulnerabilidad)
risk_pairs (risk_name, menace_name, vuln_name) AS (
  VALUES
    ('Riesgo por deficiente identificación del contexto interno y externo del SGSI', 'Toma de decisiones estratégicas sin considerar el contexto interno o externo', 'Ausencia de un procedimiento formal para determinar el contexto interno y externo del SGSI'),
    ('Riesgo por identificación incompleta de partes interesadas y sus requisitos', 'Incumplimiento de requisitos legales, regulatorios o contractuales de partes interesadas', 'Ausencia de un procedimiento formal para identificar partes interesadas y sus requisitos'),
    ('Riesgo por alcance del SGSI mal definido o desactualizado', 'Alcance del SGSI desactualizado frente a cambios organizacionales', 'Ausencia de un procedimiento para definir y revisar el alcance del SGSI'),
    ('Riesgo por SGSI no establecido, implementado o mejorado de forma continua', 'Interrupción o degradación del ciclo de mejora continua del SGSI', 'Ausencia de un SGSI formalmente establecido, implementado y mantenido'),
    ('Riesgo por falta de compromiso visible de la alta dirección con el SGSI', 'Falta de compromiso o soporte visible de la alta dirección', 'Ausencia de liderazgo y participación activa de la alta dirección en el SGSI'),
    ('Riesgo por asignación insuficiente de recursos para el SGSI', 'Uso de recursos insuficientes para sostener el SGSI', 'Falta de asignación de recursos (tiempo, presupuesto, personal) para el SGSI'),
    ('Riesgo por política de seguridad inadecuada o desalineada con la organización', 'Política de seguridad desalineada con los objetivos y contexto de la organización', 'Falta de una política de seguridad documentada, aprobada y alineada a la organización'),
    ('Riesgo por política de seguridad no comunicada ni disponible a las partes interesadas', 'Desconocimiento del personal y partes interesadas sobre la política de seguridad', 'Política de seguridad no comunicada ni disponible a las partes interesadas'),
    ('Riesgo por roles y responsabilidades de seguridad no asignados formalmente', 'Asignación inadecuada de roles y responsabilidades de seguridad', 'Roles y responsabilidades de seguridad no definidos ni asignados formalmente'),
    ('Riesgo por no determinar ni tratar oportunamente los riesgos y oportunidades del SGSI', 'Materialización de riesgos no identificados ni tratados oportunamente', 'Ausencia de un procedimiento para determinar y planificar acciones frente a riesgos y oportunidades'),
    ('Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información', 'Toma de decisiones de seguridad basada en riesgos mal evaluados', 'Ausencia de una metodología formal de evaluación de riesgos de seguridad de la información'),
    ('Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información', 'Persistencia de riesgos de seguridad sin mitigar', 'Ausencia de un plan de tratamiento de riesgos de seguridad de la información'),
    ('Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 'Objetivos de seguridad desalineados con la política y el contexto de riesgo', 'Objetivos de seguridad no medibles ni alineados a la política de seguridad'),
    ('Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 'Desviación no detectada del cumplimiento de los objetivos de seguridad', 'Falta de medición, comunicación o actualización de los objetivos de seguridad'),
    ('Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 'Incumplimiento de los objetivos de seguridad por planificación incompleta', 'Planificación de objetivos sin recursos, responsables o plazos definidos'),
    ('Riesgo por personal no competente en seguridad de la información', 'Desconocimiento del personal sobre sus obligaciones de seguridad por falta de competencia', 'Ausencia de un programa de gestión de competencias en seguridad de la información'),
    ('Riesgo por falta de concientización del personal en seguridad de la información', 'Error humano por falta de concientización en seguridad de la información', 'Ausencia de un programa de concientización en seguridad de la información'),
    ('Riesgo por comunicación interna o externa de seguridad de la información no gestionada', 'Fuga o divulgación no autorizada de información por comunicación mal gestionada', 'Ausencia de canales y procedimientos definidos de comunicación interna y externa'),
    ('Riesgo por información documentada incompleta, desactualizada o sin control', 'Falta de trazabilidad y evidencia documental del SGSI', 'Información documentada incompleta, desactualizada o sin control de versiones'),
    ('Riesgo por planificación y control operacional deficiente, incluyendo procesos tercerizados', 'Cambios no controlados o procesos tercerizados sin supervisión', 'Ausencia de control de cambios y de supervisión de procesos tercerizados'),
    ('Riesgo por ausencia de un programa de auditoría interna efectivo', 'Recurrencia de no conformidades no detectadas oportunamente', 'Ausencia de un programa de auditoría interna planificado y ejecutado'),
    ('Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI', 'Desviación no detectada del desempeño del SGSI', 'Falta de indicadores y metodología de monitoreo, medición, análisis y evaluación'),
    ('Riesgo por no conformidades no corregidas ni documentadas adecuadamente', 'Recurrencia de no conformidades no corregidas', 'Ausencia de un procedimiento de gestión de no conformidades y acciones correctivas'),
    ('Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes', 'Toma de decisiones directivas sin información objetiva del desempeño del SGSI', 'Revisiones por la dirección no realizadas o sin insumos suficientes'),
    ('Riesgo por ausencia de mejora continua del SGSI', 'Resistencia al cambio o estancamiento en la mejora del SGSI', 'Ausencia de mecanismos formales de mejora continua')
),
new_risks AS (
  INSERT INTO "MAE_DEFAULT_RISK" ("C_NAME", "N_STANDARD_ID_FK", "N_MENACE_ID_FK", "N_VULNERABILITY_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  SELECT rp.risk_name, 4, nm."N_MENACE_ID_PK", nv."N_VULNERABILITY_ID_PK", 1, 1, now(), false
  FROM risk_pairs rp
  JOIN new_menaces nm ON nm."C_NAME" = rp.menace_name
  JOIN new_vulns nv ON nv."C_NAME" = rp.vuln_name
  RETURNING "N_DEFAULT_RISK_ID_PK", "C_NAME"
)

-- 5) Asociación requerimiento -> riesgo sugerido (MAE_REQUIREMENT_IN_DEFAULT_RISK)
INSERT INTO "MAE_REQUIREMENT_IN_DEFAULT_RISK" ("N_REQUIREMENT_ID_FK", "N_DEFAULT_RISK_ID_FK", "L_IS_ACTIVE", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
SELECT l.requirement_id, nr."N_DEFAULT_RISK_ID_PK", true, 1, 1, now(), false
FROM (VALUES
    (6, 'Riesgo por deficiente identificación del contexto interno y externo del SGSI'),
    (7, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos'),
    (11, 'Riesgo por alcance del SGSI mal definido o desactualizado'),
    (12, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (18, 'Riesgo por SGSI no establecido, implementado o mejorado de forma continua'),
    (19, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (20, 'Riesgo por asignación insuficiente de recursos para el SGSI'),
    (21, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (22, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (23, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (24, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (25, 'Riesgo por falta de compromiso visible de la alta dirección con el SGSI'),
    (27, 'Riesgo por política de seguridad inadecuada o desalineada con la organización'),
    (28, 'Riesgo por política de seguridad inadecuada o desalineada con la organización'),
    (29, 'Riesgo por política de seguridad inadecuada o desalineada con la organización'),
    (30, 'Riesgo por política de seguridad inadecuada o desalineada con la organización'),
    (31, 'Riesgo por política de seguridad no comunicada ni disponible a las partes interesadas'),
    (32, 'Riesgo por política de seguridad no comunicada ni disponible a las partes interesadas'),
    (33, 'Riesgo por política de seguridad no comunicada ni disponible a las partes interesadas'),
    (35, 'Riesgo por roles y responsabilidades de seguridad no asignados formalmente'),
    (36, 'Riesgo por roles y responsabilidades de seguridad no asignados formalmente'),
    (41, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información'),
    (42, 'Riesgo por no determinar ni tratar oportunamente los riesgos y oportunidades del SGSI'),
    (43, 'Riesgo por no determinar ni tratar oportunamente los riesgos y oportunidades del SGSI'),
    (47, 'Riesgo por no determinar ni tratar oportunamente los riesgos y oportunidades del SGSI'),
    (48, 'Riesgo por no determinar ni tratar oportunamente los riesgos y oportunidades del SGSI'),
    (49, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información'),
    (51, 'Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información'),
    (52, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo'),
    (54, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados'),
    (55, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo'),
    (56, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados'),
    (57, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados'),
    (58, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos'),
    (59, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos'),
    (60, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos'),
    (61, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos'),
    (62, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos'),
    (68, 'Riesgo por asignación insuficiente de recursos para el SGSI'),
    (69, 'Riesgo por personal no competente en seguridad de la información'),
    (70, 'Riesgo por personal no competente en seguridad de la información'),
    (71, 'Riesgo por personal no competente en seguridad de la información'),
    (72, 'Riesgo por personal no competente en seguridad de la información'),
    (73, 'Riesgo por falta de concientización del personal en seguridad de la información'),
    (74, 'Riesgo por falta de concientización del personal en seguridad de la información'),
    (75, 'Riesgo por falta de concientización del personal en seguridad de la información'),
    (76, 'Riesgo por comunicación interna o externa de seguridad de la información no gestionada'),
    (77, 'Riesgo por información documentada incompleta, desactualizada o sin control'),
    (78, 'Riesgo por información documentada incompleta, desactualizada o sin control'),
    (79, 'Riesgo por información documentada incompleta, desactualizada o sin control'),
    (83, 'Riesgo por planificación y control operacional deficiente, incluyendo procesos tercerizados'),
    (84, 'Riesgo por información documentada incompleta, desactualizada o sin control'),
    (85, 'Riesgo por planificación y control operacional deficiente, incluyendo procesos tercerizados'),
    (86, 'Riesgo por planificación y control operacional deficiente, incluyendo procesos tercerizados'),
    (87, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información'),
    (88, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información'),
    (89, 'Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información'),
    (90, 'Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información'),
    (94, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (95, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (96, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (97, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (98, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (99, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI'),
    (100, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (101, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (102, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (103, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (104, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (105, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (106, 'Riesgo por ausencia de un programa de auditoría interna efectivo'),
    (107, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (108, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (109, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (110, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (111, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (112, 'Riesgo por revisiones por la dirección no realizadas o sin insumos suficientes'),
    (115, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (116, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (117, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (119, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (120, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (121, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (122, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente'),
    (123, 'Riesgo por ausencia de mejora continua del SGSI'),
    (127, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos'),
    (128, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos')
) AS l(requirement_id, risk_name)
JOIN new_risks nr ON nr."C_NAME" = l.risk_name;

COMMIT;
