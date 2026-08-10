-- =====================================================================
-- Seed: un riesgo (MAE_RISK) por cada una de las 13 brechas que
-- continúan abiertas en la evaluación 'Julio 2026'. Replica lo que el
-- diálogo "Nuevo Riesgo" enviaría: nombre, amenaza y vulnerabilidad
-- copiados del mismo MAE_DEFAULT_RISK ya usado en Junio para el mismo
-- requisito/control (persiste el mismo problema, mismo riesgo asociado),
-- más el activo y la brecha de origen (ahora la brecha de julio).
-- Estado inicial: "Registrado".
-- Requiere haber ejecutado antes seed_actives_inventory.sql,
-- seed_evaluation_julio2026.sql y los dos scripts de brechas de julio.
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
asset_lookup AS (
  SELECT "N_ACTIVES_INVENTORY_ID_PK", "C_NUMBER", "C_NAME" FROM "MAE_ACTIVES_INVENTORY"
  WHERE "C_NAME" IN ('Documentación del SGSI (Políticas y Procedimientos)', 'Base de Datos del Padrón Electoral', 'Red Corporativa (LAN/WAN)', 'Sistema de Control de Accesos e Identidad', 'Estaciones de Trabajo del Personal')
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
    ('1', 127, 0, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 128, 0, 'Riesgo por identificación incompleta de partes interesadas y sus requisitos', 29, 29, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 52, 0, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 54, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 55, 0, 'Riesgo por objetivos de seguridad desalineados con la política y el contexto de riesgo', 40, 40, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 56, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 57, 0, 'Riesgo por objetivos de seguridad no medidos, comunicados o actualizados', 41, 41, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('1', 61, 0, 'Riesgo por planificación de objetivos incompleta, sin recursos, responsables o plazos definidos', 42, 42, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('2', 0, 10, 'Riesgo por proyectos ejecutados sin considerar requisitos de seguridad de la información', 150, 150, 'Documentación del SGSI (Políticas y Procedimientos)'),
    ('2', 0, 15, 'Riesgo por manejo inadecuado de información por falta de etiquetado', 155, 155, 'Base de Datos del Padrón Electoral'),
    ('2', 0, 16, 'Riesgo por interceptación o pérdida de información durante su transferencia', 156, 156, 'Red Corporativa (LAN/WAN)'),
    ('2', 0, 77, 'Riesgo por uso indebido de herramientas de administración con privilegios elevados', 216, 216, 'Sistema de Control de Accesos e Identidad'),
    ('2', 0, 78, 'Riesgo por instalación de software no autorizado o malicioso en los sistemas operativos', 217, 217, 'Estaciones de Trabajo del Personal')
) AS v(breach_type, requirement_id, control_id, risk_name, menace_id, vuln_id, asset_name)
JOIN asset_lookup al ON al."C_NAME" = v.asset_name;

COMMIT;
