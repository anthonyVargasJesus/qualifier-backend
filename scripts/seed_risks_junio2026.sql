-- =====================================================================
-- Seed: un riesgo (MAE_RISK) creado a partir del 'riesgo sugerido' de
-- cada una de las 20 brechas de la evaluación Junio 2026 (evaluationId=4)
-- Replica exactamente lo que el diálogo "Nuevo Riesgo" enviaría: nombre,
-- amenaza y vulnerabilidad copiados del MAE_DEFAULT_RISK vinculado a cada
-- requisito/control, más el activo y la brecha de origen.
-- Requiere haber ejecutado antes seed_actives_inventory.sql.
-- Estado inicial: "Registrado".
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- Lookup de activos por nombre
WITH asset_lookup AS (
  SELECT "N_ACTIVES_INVENTORY_ID_PK", "C_NUMBER", "C_NAME" FROM "MAE_ACTIVES_INVENTORY"
  WHERE "C_NAME" IN ('Documentación del SGSI (Políticas y Procedimientos)', 'Sistema de Correo Institucional', 'Base de Datos del Padrón Electoral', 'Red Corporativa (LAN/WAN)', 'Sistema de Control de Accesos e Identidad', 'Estaciones de Trabajo del Personal')
)

INSERT INTO "MAE_RISK" (
  "N_ACTIVES_INVENTORY_ID_FK", "C_ACTIVES_INVENTORY_NUMBER", "C_ACTIVES_INVENTORY_NAME", "N_MENACE_ID_FK", "N_VULNERABILITY_ID_FK",
  "N_COMPANY_ID_FK", "N_EVALUATION_ID_FK", "C_NAME", "N_RISK_STATUS_ID_FK", "N_BREACH_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT al."N_ACTIVES_INVENTORY_ID_PK", al."C_NUMBER", al."C_NAME", l.menace_id, l.vuln_id,
       1, 4, l.risk_name, 1, l.breach_id, 1, now(), false
FROM (VALUES
    (19, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (20, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (21, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (22, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (23, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (24, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (25, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (26, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (27, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (28, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (29, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (30, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (31, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (32, 'Riesgo por comunicación interna o externa de seguridad de la información no gestionada', 45, 45, 'Sistema de Correo Institucional'),
    (39, 'Riesgo por proyectos ejecutados sin considerar requisitos de seguridad de la información', 150, 150, 'Documentación del SGSI (Políticas y Procedimientos)'),
    (40, 'Riesgo por divulgación de información sensible por ausencia de clasificación', 154, 154, 'Base de Datos del Padrón Electoral'),
    (41, 'Riesgo por manejo inadecuado de información por falta de etiquetado', 155, 155, 'Base de Datos del Padrón Electoral'),
    (42, 'Riesgo por interceptación o pérdida de información durante su transferencia', 156, 156, 'Red Corporativa (LAN/WAN)'),
    (43, 'Riesgo por uso indebido de herramientas de administración con privilegios elevados', 216, 216, 'Sistema de Control de Accesos e Identidad'),
    (44, 'Riesgo por instalación de software no autorizado o malicioso en los sistemas operativos', 217, 217, 'Estaciones de Trabajo del Personal')
) AS l(breach_id, risk_name, menace_id, vuln_id, asset_name)
JOIN asset_lookup al ON al."C_NAME" = l.asset_name;

COMMIT;
