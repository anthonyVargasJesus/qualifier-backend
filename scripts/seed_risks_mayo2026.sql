-- =====================================================================
-- Seed: riesgos (MAE_RISK) para 'Evaluación - Mayo 2026'.
-- Dos grupos:
--  1) Los 20 riesgos que ya existían en junio (mismo nombre/amenaza/
--     vulnerabilidad/activo que seed_risks_junio2026.sql), enlazados a
--     las brechas correspondientes de mayo.
--  2) 20 riesgos NUEVOS, uno por cada brecha nueva de mayo, usando el
--     'riesgo sugerido' real (MAE_DEFAULT_RISK) vinculado a cada
--     requisito/control en el catálogo.
-- Estado inicial: "Registrado".
-- Requiere haber ejecutado antes seed_actives_inventory.sql y los
-- scripts de brechas de mayo.
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
asset_lookup AS (
  SELECT "N_ACTIVES_INVENTORY_ID_PK", "C_NUMBER", "C_NAME" FROM "MAE_ACTIVES_INVENTORY"
  WHERE "C_NAME" IN (
    'Documentación del SGSI (Políticas y Procedimientos)', 'Sistema de Correo Institucional',
    'Base de Datos del Padrón Electoral', 'Red Corporativa (LAN/WAN)',
    'Sistema de Control de Accesos e Identidad', 'Estaciones de Trabajo del Personal',
    'Sistema de Gestión Documental del SGSI', 'Personal de la Oficina de Seguridad de la Información',
    'Servicios de Terceros / Proveedores TI', 'Servidor de Aplicaciones'
  )
)
INSERT INTO "MAE_RISK" (
  "N_ACTIVES_INVENTORY_ID_FK", "C_ACTIVES_INVENTORY_NUMBER", "C_ACTIVES_INVENTORY_NAME", "N_MENACE_ID_FK", "N_VULNERABILITY_ID_FK",
  "N_COMPANY_ID_FK", "N_EVALUATION_ID_FK", "C_NAME", "N_RISK_STATUS_ID_FK", "N_BREACH_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT al."N_ACTIVES_INVENTORY_ID_PK", al."C_NUMBER", al."C_NAME", v.menace_id, v.vuln_id,
       1, ev.id, v.risk_name, 1,
       (SELECT b."N_BREACH_ID_PK" FROM "MAE_BREACH" b
        WHERE b."N_EVALUATION_ID_FK" = ev.id AND b."C_TYPE" = v.breach_type
          AND b."N_REQUIREMENT_ID_FK" = v.requirement_id AND b."N_CONTROL_ID_FK" = v.control_id),
       1, now(), false
FROM ev, (VALUES
    -- ===== Grupo 1: 20 que ya existían en junio =====
    ('1', 7, 0, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 127, 0, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 128, 0, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 52, 0, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 54, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 55, 0, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 56, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 57, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 58, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 59, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 60, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 61, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 62, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 76, 0, 'Riesgo por comunicación interna o externa de seguridad de la información no gestionada', 45, 45, 'Sistema de Correo Institucional'),
    ('2', 0, 10, 'Riesgo por proyectos ejecutados sin considerar requisitos de seguridad de la información', 150, 150, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('2', 0, 14, 'Riesgo por divulgación de información sensible por ausencia de clasificación', 154, 154, 'Base de Datos del Padrón Electoral'),
    ('2', 0, 15, 'Riesgo por manejo inadecuado de información por falta de etiquetado', 155, 155, 'Base de Datos del Padrón Electoral'),
    ('2', 0, 16, 'Riesgo por interceptación o pérdida de información durante su transferencia', 156, 156, 'Red Corporativa (LAN/WAN)'),
    ('2', 0, 77, 'Riesgo por uso indebido de herramientas de administración con privilegios elevados', 216, 216, 'Sistema de Control de Accesos e Identidad'),
    ('2', 0, 78, 'Riesgo por instalación de software no autorizado o malicioso en los sistemas operativos', 217, 217, 'Estaciones de Trabajo del Personal'),
    -- ===== Grupo 2: 20 nuevos (uno por brecha nueva de mayo) =====
    ('1', 6, 0, 'Riesgo por deficiente identificación del contexto interno y externo del SGSI', 28, 28, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 18, 0, 'Riesgo por SGSI no establecido, implementado o mejorado de forma continua', 31, 31, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 35, 0, 'Riesgo por roles y responsabilidades de seguridad no asignados formalmente', 36, 36, 'Personal de la Oficina de Seguridad de la Información'),
    ('1', 41, 0, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información', 38, 38, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 51, 0, 'Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información', 39, 39, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 68, 0, 'Riesgo por asignación insuficiente de recursos para el SGSI', 33, 33, 'Personal de la Oficina de Seguridad de la Información'),
    ('1', 69, 0, 'Riesgo por personal no competente en seguridad de la información', 43, 43, 'Personal de la Oficina de Seguridad de la Información'),
    ('1', 73, 0, 'Riesgo por falta de concientización del personal en seguridad de la información', 44, 44, 'Personal de la Oficina de Seguridad de la Información'),
    ('1', 77, 0, 'Riesgo por información documentada incompleta, desactualizada o sin control', 46, 46, 'Sistema de Gestión Documental del SGSI'),
    ('1', 83, 0, 'Riesgo por planificación y control operacional deficiente, incluyendo procesos tercerizados', 47, 47, 'Servicios de Terceros / Proveedores TI'),
    ('1', 87, 0, 'Riesgo por ausencia de metodología formal de evaluación de riesgos de seguridad de la información', 38, 38, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 89, 0, 'Riesgo por ausencia de un plan de tratamiento de riesgos de seguridad de la información', 39, 39, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 94, 0, 'Riesgo por ausencia de indicadores y mecanismos de monitoreo, medición, análisis y evaluación del SGSI', 49, 49, 'Sistema de Gestión Documental del SGSI'),
    ('1', 115, 0, 'Riesgo por no conformidades no corregidas ni documentadas adecuadamente', 50, 50, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('2', 0, 11, 'Riesgo por activos de información no identificados ni inventariados', 151, 151, 'Sistema de Gestión Documental del SGSI'),
    ('2', 0, 21, 'Riesgo por brechas de seguridad originadas por proveedores', 161, 161, 'Servicios de Terceros / Proveedores TI'),
    ('2', 0, 33, 'Riesgo por incumplimiento de requisitos legales, reglamentarios o contractuales de seguridad', 170, 170, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('2', 0, 42, 'Riesgo por accesos o información retenida indebidamente tras la finalización del empleo', 181, 181, 'Sistema de Control de Accesos e Identidad'),
    ('2', 0, 46, 'Riesgo por acceso físico no autorizado a las instalaciones', 185, 185, 'Estaciones de Trabajo del Personal'),
    ('2', 0, 73, 'Riesgo por interrupción del servicio por falta de redundancia en las instalaciones de procesamiento', 212, 212, 'Servidor de Aplicaciones')
) AS v(breach_type, requirement_id, control_id, risk_name, menace_id, vuln_id, asset_name)
JOIN asset_lookup al ON al."C_NAME" = v.asset_name;

COMMIT;
