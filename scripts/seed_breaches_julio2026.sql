-- =====================================================================
-- Seed: una brecha (MAE_BREACH) por cada requisito que continúa 'NO
-- CUMPLIDO' en 'Evaluación - Julio 2026' (8 de los 14 requisitos que
-- estaban en brecha en Junio 2026; los otros 6 cerraron — ver
-- seed_requirement_evaluation_julio2026.sql).
-- Todas de tipo Requerimiento (C_TYPE=1), estado inicial "Abierta".
-- Severidad heredada de la brecha equivalente de Junio (mismo tema, mismo
-- riesgo residual).
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
INSERT INTO "MAE_BREACH" (
  "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK",
  "C_TITLE", "C_DESCRIPTION", "N_BREACH_SEVERITY_ID_FK", "N_BREACH_STATUS_ID_FK", "N_RESPONSIBLE_ID_FK",
  "C_EVIDENCE_DESCRIPTION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "C_NUMERATION_TO_SHOW"
)
SELECT ev.id, 4, '1', v.requirement_id, 0, v.title, v.description, v.severity_id, 1, v.responsible_id,
       v.evidence_description, 1, 1, now(), false, v.numeration
FROM ev, (VALUES
  (52, 'El requisito 6.2.1 no se cumple: Asegurar la consistencia de los objetivos y la planificación con las políticas de seguridad de la información', 'Los objetivos de seguridad de la información definidos no cuentan aún con una vinculación formal y aprobada con la política de seguridad ni con el contexto de riesgo evaluado.', 2, 12, 'La matriz que vincula objetivos, política y evaluación de riesgos está en elaboración desde junio, pero todavía no ha sido aprobada por el Comité SGSI.', '6.2.1'),
  (54, 'El requisito 6.2.2 no se cumple: Realizar  la medición de los objetivos en cuanto a su consecución', 'No se ha implementado un mecanismo de medición del cumplimiento de los objetivos de seguridad de la información planteados.', 2, 12, 'No existen indicadores ni reportes de seguimiento de cumplimiento de objetivos; sin cambios desde la evaluación de junio.', '6.2.2'),
  (55, 'El requisito 6.2.3 no se cumple: Tomar en cuenta los requisitos aplicables de seguridad de la información, resultados de la valoración y tratamiento de riesgos para formular los objetivos y la planificación ', 'La formulación de los objetivos de seguridad no incorpora aún de manera aprobada los resultados de la evaluación y tratamiento de riesgos vigente.', 2, 12, 'La actualización del documento de objetivos con las referencias al registro de riesgos está en elaboración, pendiente de aprobación.', '6.2.3'),
  (56, 'El requisito 6.2.4 no se cumple: Comunicar a las partes pertinentes en la organización los objetivos y la planificación ', 'Los objetivos de seguridad de la información y su planificación no han sido comunicados formalmente a las partes pertinentes dentro de la organización.', 2, 12, 'Los indicadores de cumplimiento están en definición, pero el canal de reporte periódico a las partes pertinentes aún no está operativo.', '6.2.4'),
  (57, 'El requisito 6.2.5 no se cumple: Actualizar y mantener documentado según sea apropiado', 'Los objetivos de seguridad de la información no cuentan con un procedimiento de actualización periódica ni versión documentada vigente.', 2, 12, 'El documento de objetivos disponible no tiene control de versiones ni fecha de última actualización; sin cambios desde junio.', '6.2.5'),
  (61, 'El requisito 6.2.9 no se cumple: La planificación y los objetivos deben prever plazos para su culminación', 'La planificación de objetivos de seguridad no establece aún plazos ni fechas límite validados y comunicados para la culminación de las actividades asociadas.', 3, 12, 'El cronograma de plazos por objetivo está en elaboración, pendiente de validación y de comunicación a los responsables.', '6.2.9'),
  (127, 'El requisito 4.2.2 no se cumple: Los requisitos pertinentes de estas partes interesadas', 'Aunque ya existe un registro formal de partes interesadas (aprobado en julio), aún no se ha realizado el análisis sistemático de sus requisitos pertinentes.', 3, 9, 'No se encontró una matriz de requisitos de partes interesadas vinculada al SGSI; pendiente de iniciar ahora que el registro ya está aprobado.', '4.2.2'),
  (128, 'El requisito 4.2.3 no se cumple: Cuáles de estos requisitos se abordarán mediante el sistema de gestión de seguridad de la información.', 'No se ha determinado qué requisitos de partes interesadas serán abordados por el SGSI, al faltar el insumo previo (matriz de requisitos aprobada).', 3, 9, 'No existe documento que vincule requisitos de partes interesadas con el alcance del SGSI; depende de que primero se apruebe la matriz de requisitos (req. 4.2.2).', '4.2.3')
) AS v(requirement_id, title, description, severity_id, responsible_id, evidence_description, numeration);

COMMIT;
