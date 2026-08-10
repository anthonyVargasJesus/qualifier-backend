-- =====================================================================
-- Seed: brechas de requisitos para 'Evaluación - Mayo 2026'.
-- Dos grupos:
--  1) Las 14 brechas que YA existían en junio (mismo texto que
--     seed_breaches_junio2026.sql) — siguen sin cumplirse en mayo, un mes
--     antes.
--  2) 14 brechas NUEVAS para los requisitos que en junio ya estaban
--     conformes (nivel 10) pero que en mayo se retrocedieron a nivel 11
--     (ver seed_requirement_evaluation_mayo2026.sql). El texto de
--     descripción reutiliza la justificación real de junio (ya describe
--     un estado parcial/incompleto).
-- Todas de tipo Requerimiento (C_TYPE=1), estado inicial "Abierta".
-- Requiere haber ejecutado antes seed_evaluation_mayo2026.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH ev AS (
  SELECT "N_EVALUATION_ID_PK" AS id
  FROM "MAE_EVALUATION"
  WHERE "C_DESCRIPTION" = 'Evaluación - Mayo 2026'
    AND "N_COMPANY_ID_FK" = 1
    AND ("N_IS_DELETED" IS NULL OR "N_IS_DELETED" = false)
)
INSERT INTO "MAE_BREACH" (
  "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK",
  "C_TITLE", "C_DESCRIPTION", "N_BREACH_SEVERITY_ID_FK", "N_BREACH_STATUS_ID_FK", "N_RESPONSIBLE_ID_FK",
  "C_EVIDENCE_DESCRIPTION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "C_NUMERATION_TO_SHOW"
)
SELECT ev.id, 4, '1', v.requirement_id, 0, v.title, v.description, v.severity_id, 1, v.responsible_id,
       v.evidence_description, 1, 1, now(), false, v.numeration
FROM ev, (VALUES
  -- ===== Grupo 1: las 14 que ya existían en junio (mismo texto) =====
  (7, 'El requisito 4.2.1 no se cumple: Las partes interesadas pertinentes al sistema de gestión de seguridad de la información', 'No se ha formalizado un registro de partes interesadas del SGSI (entes reguladores, ciudadanía, proveedores TI, otras entidades del Estado). Solo existe una identificación inicial sin análisis sistemático.', 3, 9, 'No existe un documento formal de identificación de partes interesadas; solo notas preliminares dispersas.', '4.2.1'),
  (127, 'El requisito 4.2.2 no se cumple: Los requisitos pertinentes de estas partes interesadas', 'Al no existir un registro formal de partes interesadas, tampoco se han determinado ni documentado sus requisitos pertinentes de forma sistemática.', 3, 9, 'No se encontró una matriz de requisitos de partes interesadas vinculada al SGSI.', '4.2.2'),
  (128, 'El requisito 4.2.3 no se cumple: Cuáles de estos requisitos se abordarán mediante el sistema de gestión de seguridad de la información.', 'No se ha determinado qué requisitos de partes interesadas serán abordados por el SGSI, al faltar el insumo previo (registro y análisis de requisitos).', 3, 9, 'No existe documento que vincule requisitos de partes interesadas con el alcance del SGSI.', '4.2.3'),
  (52, 'El requisito 6.2.1 no se cumple: Asegurar la consistencia de los objetivos y la planificación con las políticas de seguridad de la información', 'Los objetivos de seguridad de la información definidos no cuentan con una vinculación formal y documentada con la política de seguridad ni con el contexto de riesgo evaluado.', 2, 12, 'El plan de objetivos vigente no referencia explícitamente la política de seguridad ni la evaluación de riesgos.', '6.2.1'),
  (54, 'El requisito 6.2.2 no se cumple: Realizar  la medición de los objetivos en cuanto a su consecución', 'No se ha implementado un mecanismo de medición del cumplimiento de los objetivos de seguridad de la información planteados.', 2, 12, 'No existen indicadores ni reportes de seguimiento de cumplimiento de objetivos.', '6.2.2'),
  (55, 'El requisito 6.2.3 no se cumple: Tomar en cuenta los requisitos aplicables de seguridad de la información, resultados de la valoración y tratamiento de riesgos para formular los objetivos y la planificación ', 'La formulación de los objetivos de seguridad no incorpora de manera documentada los resultados de la evaluación y tratamiento de riesgos vigente.', 2, 12, 'El plan de objetivos no cita ni referencia el registro de riesgos evaluados.', '6.2.3'),
  (56, 'El requisito 6.2.4 no se cumple: Comunicar a las partes pertinentes en la organización los objetivos y la planificación ', 'Los objetivos de seguridad de la información y su planificación no han sido comunicados formalmente a las partes pertinentes dentro de la organización.', 2, 12, 'No hay evidencia de comunicados, correos o actas donde se difundan los objetivos de seguridad.', '6.2.4'),
  (57, 'El requisito 6.2.5 no se cumple: Actualizar y mantener documentado según sea apropiado', 'Los objetivos de seguridad de la información no cuentan con un procedimiento de actualización periódica ni versión documentada vigente.', 2, 12, 'El documento de objetivos disponible no tiene control de versiones ni fecha de última actualización.', '6.2.5'),
  (58, 'El requisito 6.2.6 no se cumple: La planificación debe prever las actividades necesarias para lograr los objetivos', 'No existe una planificación formal que detalle las actividades necesarias para el logro de cada objetivo de seguridad de la información definido.', 3, 12, 'No se encontró un plan de actividades asociado a los objetivos de seguridad.', '6.2.6'),
  (59, 'El requisito 6.2.7 no se cumple: La planificación debe prever los recursos que serán requeridos para lograr los objetivos', 'La planificación de objetivos de seguridad no identifica ni asigna los recursos (presupuesto, personal, herramientas) necesarios para su cumplimiento.', 3, 12, 'No existe un presupuesto o asignación de recursos vinculada a los objetivos de seguridad.', '6.2.7'),
  (60, 'El requisito 6.2.8 no se cumple: La planificación debe prever la asignación de responsabilidades para su ejecución', 'No se han asignado responsables formales para la ejecución de las actividades que permitirían el cumplimiento de los objetivos de seguridad.', 3, 12, 'El plan de objetivos no contiene una columna o registro de responsables por actividad.', '6.2.8'),
  (61, 'El requisito 6.2.9 no se cumple: La planificación y los objetivos deben prever plazos para su culminación', 'La planificación de objetivos de seguridad no establece plazos ni fechas límite para la culminación de las actividades asociadas.', 3, 12, 'No se encontró un cronograma con fechas de cumplimiento para los objetivos de seguridad.', '6.2.9'),
  (62, 'El requisito 6.2.10 no se cumple: La planificación debe prever cómo se evaluarán los resultado', 'No se ha definido un método o criterio para evaluar los resultados obtenidos respecto a los objetivos de seguridad de la información planificados.', 3, 12, 'No existe un procedimiento o criterio documentado de evaluación de resultados de los objetivos.', '6.2.10'),
  (76, 'El requisito 7.4.1 no se cumple: Determinar la necesidad de la comunicación interna y externa relevante al SGSI, incluyendo el qué, cuándo, a quién, quién y procesos para la comunicación ', 'No existe un procedimiento formal que determine qué, cuándo, a quién, quién y cómo comunicar interna y externamente asuntos relevantes del SGSI; solo se realizan comunicaciones puntuales.', 2, 12, 'No se encontró un procedimiento de comunicación del SGSI ni un registro de comunicaciones realizadas.', '7.4.1'),
  -- ===== Grupo 2: 14 nuevas (regresadas desde nivel 10 de junio) =====
  (6, 'El requisito 4.1.1 no se cumple: Determinar los aspectos externos e internos relevantes para el SGSI y  que afectan la capacidad de lograr los resultados', 'El análisis de contexto interno y externo (marco legal electoral, normativa de protección de datos, infraestructura TI, partes interesadas) que sustenta la planificación del SGSI aún no está formalizado ni actualizado con una periodicidad definida.', 2, 2, 'No se encontró un documento de análisis de contexto vigente con fecha de última actualización.', '4.1.1'),
  (18, 'El requisito 4.4.1 no se cumple: Establecer, implementar, mantener y mejorar continuamente un sistema de gestión de seguridad de la información', 'El SGSI se encuentra en fase de establecimiento; los procesos de gestión, operación y mejora aún no están todos en ejecución de forma institucionalizada.', 2, 12, 'No se encontró evidencia de un ciclo PHVA documentado y en funcionamiento para el SGSI.', '4.4.1'),
  (35, 'El requisito 5.3.1 no se cumple: Asignar responsabilidades y autoridades para asegurar que el sistema de gestión de seguridad de la información esté conforme a los requisitos ', 'Los roles y responsabilidades de seguridad de la información aún no están formalmente asignados ni documentados.', 2, 2, 'No existe una matriz de roles y responsabilidades del SGSI aprobada.', '5.3.1'),
  (41, 'El requisito 6.5.1 no se cumple: Establecer y mantener criterios de riesgo de seguridad de la información que incluyan los criterios de aceptación de los riesgos y para realizar valoraciones ', 'No existe aún una metodología formal de evaluación de riesgos de seguridad de la información con criterios de aceptación definidos.', 3, 1, 'No se encontró un documento de metodología de evaluación de riesgos aprobado.', '6.5.1'),
  (51, 'El requisito 6.1.6 no se cumple: Tratamiento de riesgos de seguridad de la información', 'No se cuenta aún con un plan de tratamiento de riesgos en ejecución.', 3, 1, 'No existe un plan de tratamiento de riesgos vigente ni evidencia de su ejecución.', '6.1.6'),
  (68, 'El requisito 7.1.1 no se cumple: Determinar y proporcionar los recursos necesarios para el establecimiento, implementación, mantenimiento y mejora continua del SGSI', 'Aún no se determinan ni asignan formalmente los recursos necesarios para el establecimiento y mantenimiento del SGSI.', 2, 11, 'No existe un documento de asignación de recursos (presupuesto, personal, herramientas) para el SGSI.', '7.1.1'),
  (69, 'El requisito 7.2.1 no se cumple: Determinar las competencias necesaria de las personas en materia de seguridad de la información para los roles críticos en el SGSI', 'No se han determinado formalmente las competencias necesarias para los roles críticos del SGSI ni existe evidencia de capacitación del personal asignado.', 2, 13, 'No se encontró un documento de competencias requeridas ni registros de capacitación.', '7.2.1'),
  (73, 'El requisito 7.3.1 no se cumple: Concientizar a las personas que trabajan en la organización sobre política de seguridad de información', 'Aún no se ejecutan actividades de concientización en seguridad de la información dirigidas al personal.', 2, 14, 'No existe evidencia de campañas o sesiones de concientización realizadas.', '7.3.1'),
  (77, 'El requisito 7.5.1 no se cumple: Documentar y mantener la información necesaria para la efectividad del SGSI', 'La información documentada del SGSI aún no se mantiene ni actualiza con una frecuencia establecida, ni cuenta con control de versiones.', 2, 1, 'No se encontró un procedimiento de control documental ni evidencia de control de versiones.', '7.5.1'),
  (83, 'El requisito 8.1.1 no se cumple: Planificar, implementar y controlar los procesos necesarios para cumplir los requisitos de seguridad de la información, gestionar los  riesgos y lograr los objetivos', 'Los procesos necesarios para cumplir los requisitos de seguridad de la información, incluyendo el control de procesos tercerizados, aún no están formalmente planificados.', 2, 11, 'No existe documentación de planificación operativa del SGSI ni control de procesos tercerizados.', '8.1.1'),
  (87, 'El requisito 8.2.1 no se cumple: Realizar evaluaciones de riesgos de seguridad de la información en intervalos planificados o cuando cambios significativos se propongan u ocurran', 'No existe aún una metodología formal de evaluación de riesgos de seguridad de la información con criterios de aceptación definidos.', 3, 1, 'No se encontró un documento de metodología de evaluación de riesgos aprobado.', '8.2.1'),
  (89, 'El requisito 8.3.1 no se cumple: Implementar el plan de tratamiento de riesgos de seguridad de la información', 'No se cuenta aún con un plan de tratamiento de riesgos en ejecución.', 3, 1, 'No existe un plan de tratamiento de riesgos vigente ni evidencia de su ejecución.', '8.3.1'),
  (94, 'El requisito 9.1.1 no se cumple: Determinar lo que necesita ser monitoreado y medido, incluyendo procesos y controles de seguridad de la información', 'Aún no se han determinado los procesos y controles a monitorear, ni los métodos y responsables asignados para el seguimiento.', 2, 1, 'No se encontró un plan de monitoreo del SGSI con procesos, métodos y responsables definidos.', '9.1.1'),
  (115, 'El requisito 10.1.1 no se cumple: Realizar las correciones ante las no conformidades detectadas', 'Las no conformidades detectadas aún no se corrigen ni documentan de forma sistemática, ni se ejecutan acciones correctivas con seguimiento.', 2, 12, 'No se encontró un registro de no conformidades ni acciones correctivas documentadas.', '10.1.1')
) AS v(requirement_id, title, description, severity_id, responsible_id, evidence_description, numeration);

COMMIT;
