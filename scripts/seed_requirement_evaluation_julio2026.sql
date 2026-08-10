-- =====================================================================
-- Seed: re-evaluación de los 14 requisitos que quedaron NO CUMPLIDOS en
-- 'Análisis de Brechas ISO 27001 - Junio 2026' (evaluationId=4), ahora
-- para 'Evaluación - Julio 2026'.
-- Carga: MAE_REQUIREMENT_EVALUATION solo para esos 14 requisitos (no se
-- re-evalúan los otros 73 que ya estaban conformes en Junio — decisión
-- explícita: este ciclo de un mes revisa únicamente lo que estaba en
-- brecha).
-- Resultado del avance: 6 requisitos cierran (pasan de nivel 11 "No
-- cumple" a nivel 10 "Parcial/Gestionado"); 8 continúan NO CUMPLIDOS
-- (4 con avance parcial documentado, 4 sin cambios desde Junio).
-- Alcance: standardId=4 (ISO 27001), companyId=1 (ONPE).
-- Requiere haber ejecutado antes seed_evaluation_julio2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Julio 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
)
INSERT INTO "MAE_REQUIREMENT_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_REQUIREMENT_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE",
  "N_RESPONSIBLE_ID_FK", "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS",
  "N_STANDARD_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "N_AUDITOR_STATUS"
)
SELECT ev.id, v.requirement_id, v.maturity_level_id, v.value, v.responsible_id,
       v.justification, v.improvement_actions, 4, 1, 1, now(), false, 1
FROM ev, (VALUES
  -- ===== CIERRAN (nivel 11 -> 10, valor 1.00 -> 3.00) =====
  (7, 10, 3.00, 9, 'Se ha formalizado y aprobado el registro de partes interesadas del SGSI (entes reguladores, ciudadanía, proveedores TI, otras entidades del Estado), documentado en una matriz revisada por el Comité SGSI.', 'Aprobar y documentar el procedimiento de actualización periódica del registro de partes interesadas, con periodicidad semestral.'),
  (58, 10, 3.00, 12, 'Se ha elaborado y aprobado el listado de actividades necesarias para el logro de cada objetivo de seguridad de la información definido.', 'Revisar trimestralmente el cumplimiento de las actividades definidas por objetivo.'),
  (59, 10, 3.00, 12, 'Se han identificado y gestionado ante la Alta Dirección los recursos (presupuesto, personal, herramientas) necesarios para el cumplimiento de los objetivos de seguridad definidos.', 'Formalizar la asignación de recursos aprobada en el presupuesto anual del SGSI.'),
  (60, 10, 3.00, 12, 'Se han asignado y documentado los responsables de la ejecución de cada actividad vinculada a los objetivos de seguridad de la información.', 'Incluir la asignación de responsables como paso obligatorio en la plantilla de planificación de objetivos.'),
  (62, 10, 3.00, 12, 'Se ha definido el método y los criterios con los que se evaluarán los resultados obtenidos frente a los objetivos de seguridad planificados.', 'Documentar formalmente el procedimiento de evaluación de resultados como parte del ciclo de planificación del SGSI.'),
  (76, 10, 3.00, 1, 'Se ha documentado y aprobado un procedimiento de comunicación interna y externa del SGSI que define qué, cuándo, a quién y cómo comunicar asuntos relevantes de seguridad de la información.', 'Difundir el procedimiento aprobado a todo el personal mediante la intranet institucional.'),
  -- ===== CONTINÚAN NO CUMPLIDOS, con avance parcial documentado =====
  (52, 11, 1.00, 12, 'Se ha iniciado la vinculación formal de los objetivos de seguridad con la política vigente; la matriz que sustenta esta consistencia está en elaboración pero aún no ha sido aprobada por el Comité SGSI.', 'Culminar y aprobar la matriz de vinculación entre objetivos, política y contexto de riesgo en la próxima sesión del Comité SGSI.'),
  (55, 11, 1.00, 12, 'Se encuentra en elaboración la actualización del documento de objetivos de seguridad para referenciar explícitamente los resultados de la última evaluación de riesgos; la versión aún no ha sido aprobada.', 'Aprobar la versión actualizada del documento de objetivos con las referencias a los riesgos evaluados y tratados.'),
  (56, 11, 1.00, 12, 'Se están definiendo los indicadores de cumplimiento de los objetivos de seguridad de la información; el mecanismo de reporte periódico a las partes pertinentes todavía no está operativo.', 'Poner en operación el reporte periódico de indicadores de cumplimiento de objetivos, con la periodicidad ya definida.'),
  (61, 11, 1.00, 12, 'Se está elaborando el cronograma de plazos de cumplimiento por objetivo de seguridad; el cronograma aún no ha sido validado ni comunicado a los responsables.', 'Validar y comunicar formalmente el cronograma de plazos por objetivo a todos los responsables asignados.'),
  -- ===== CONTINÚAN NO CUMPLIDOS, sin cambios desde Junio =====
  (54, 11, 1.00, 12, 'Los objetivos de seguridad de la información se han planteado, pero aún no se mide su cumplimiento ni se comunican formalmente a las partes pertinentes.', 'Definir indicadores de cumplimiento para cada objetivo de seguridad y establecer un canal de comunicación periódico de resultados.'),
  (57, 11, 1.00, 12, 'Los objetivos de seguridad de la información se han planteado, pero aún no se mide su cumplimiento ni se comunican formalmente a las partes pertinentes.', 'Definir indicadores de cumplimiento para cada objetivo de seguridad y establecer un canal de comunicación periódico de resultados.'),
  (127, 11, 1.00, 9, 'Se ha iniciado la identificación de partes interesadas (entes reguladores, ciudadanía, proveedores TI, otras entidades del Estado), pero aún no existe un análisis sistemático de sus requisitos.', 'Elaborar y mantener una matriz formal de requisitos por parte interesada, vinculada a los controles aplicables.'),
  (128, 11, 1.00, 9, 'No se ha determinado qué requisitos de partes interesadas serán abordados por el SGSI, al faltar el insumo previo (matriz de requisitos aprobada).', 'Determinar y documentar qué requisitos de partes interesadas serán atendidos por el SGSI, una vez aprobada la matriz de requisitos.')
) AS v(requirement_id, maturity_level_id, value, responsible_id, justification, improvement_actions);

COMMIT;
