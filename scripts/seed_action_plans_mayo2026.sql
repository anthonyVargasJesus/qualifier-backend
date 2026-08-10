-- =====================================================================
-- Seed: planes de acción (MAE_ACTION_PLAN) para 'Evaluación - Mayo 2026'.
-- Dos grupos:
--  1) Los 28 planes que ya existían en junio (mismo texto que
--     seed_action_plans_junio2026.sql), con fechas retrocedidas un mes
--     (inicio 15/05/2026 en vez de 15/06/2026, vencimientos -1 mes).
--  2) 20 planes NUEVOS (uno por brecha nueva), reutilizando como
--     descripción la acción de mejora ya redactada en junio para ese
--     mismo ítem — tiene sentido: es exactamente lo que había que hacer
--     para que el ítem llegara al estado que ya tiene en junio. Vencen
--     el 15/06/2026 (deben resolverse antes de la evaluación de junio).
-- Estado inicial "Pendiente" en ambos grupos.
-- Requiere haber ejecutado antes seed_evaluation_mayo2026.sql y los dos
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
)
INSERT INTO "MAE_ACTION_PLAN" (
  "N_BREACH_ID_FK", "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TITLE", "C_DESCRIPTION",
  "N_RESPONSIBLE_ID_FK", "D_START_DATE", "D_DUE_DATE", "N_ACTION_PLAN_STATUS_ID_FK", "N_ACTION_PLAN_PRIORITY_ID_FK",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  (SELECT b."N_BREACH_ID_PK" FROM "MAE_BREACH" b
   WHERE b."N_EVALUATION_ID_FK" = ev.id AND b."C_TYPE" = v.breach_type
     AND b."N_REQUIREMENT_ID_FK" = v.requirement_id AND b."N_CONTROL_ID_FK" = v.control_id),
  ev.id, 4, v.title, v.description, v.responsible_id, v.start_date::date, v.due_date::date, 1, v.priority_id, 1, 1, now(), false
FROM ev, (VALUES
  -- ===== Grupo 1: los 28 que ya existían en junio (mismo texto, fechas -1 mes) =====
  ('1', 7, 0, 'Elaborar registro preliminar de partes interesadas', 'Identificar y documentar en una lista inicial las partes interesadas relevantes al SGSI (reguladores, ciudadanía, proveedores TI, otras entidades del Estado).', 9, '2026-05-15', '2026-06-15', 1),
  ('1', 7, 0, 'Formalizar procedimiento de gestión de partes interesadas', 'Documentar y aprobar un procedimiento que defina cómo se identifican, actualizan y revisan las partes interesadas de forma recurrente.', 9, '2026-05-15', '2026-08-30', 1),
  ('1', 52, 0, 'Levantar requisitos de partes interesadas identificadas', 'A partir del registro de partes interesadas, documentar los requisitos pertinentes de cada una en una matriz.', 9, '2026-05-15', '2026-06-30', 1),
  ('1', 52, 0, 'Aprobar matriz de requisitos de partes interesadas', 'Revisar y aprobar formalmente la matriz de requisitos con el Comité SGSI, dejando evidencia documentada.', 9, '2026-05-15', '2026-08-30', 1),
  ('1', 54, 0, 'Definir qué requisitos abordará el SGSI', 'Con la matriz de requisitos aprobada, determinar y documentar cuáles serán atendidos por el SGSI y cuáles quedan fuera de alcance.', 9, '2026-05-15', '2026-07-15', 1),
  ('1', 55, 0, 'Vincular objetivos de seguridad con política y riesgos', 'Actualizar el documento de objetivos de seguridad para referenciar explícitamente la política vigente y los resultados de la última evaluación de riesgos.', 12, '2026-05-15', '2026-07-15', 2),
  ('1', 56, 0, 'Definir indicadores de cumplimiento de objetivos', 'Establecer indicadores medibles para cada objetivo de seguridad de la información y un mecanismo de reporte periódico.', 12, '2026-05-15', '2026-07-31', 2),
  ('1', 57, 0, 'Incorporar resultados de riesgos en la planificación', 'Actualizar la formulación de objetivos para que cite el registro de riesgos evaluados y tratados vigente.', 12, '2026-05-15', '2026-07-31', 2),
  ('1', 58, 0, 'Comunicar objetivos y planificación a la organización', 'Difundir formalmente los objetivos de seguridad y su planificación a las partes pertinentes mediante correo institucional y reunión de kickoff.', 12, '2026-05-15', '2026-06-30', 2),
  ('1', 59, 0, 'Establecer control de versiones del documento de objetivos', 'Definir una periodicidad de actualización y aplicar control de versiones al documento de objetivos de seguridad.', 12, '2026-05-15', '2026-06-30', 2),
  ('1', 60, 0, 'Definir actividades para cada objetivo de seguridad', 'Elaborar un listado de actividades concretas necesarias para alcanzar cada objetivo de seguridad definido.', 12, '2026-05-15', '2026-06-30', 1),
  ('1', 60, 0, 'Aprobar plan de actividades por objetivo', 'Validar y aprobar formalmente el plan de actividades con el Comité SGSI, dejando evidencia documentada.', 12, '2026-05-15', '2026-08-15', 1),
  ('1', 61, 0, 'Identificar recursos necesarios por objetivo', 'Determinar el presupuesto, personal y herramientas requeridos para el cumplimiento de cada objetivo de seguridad.', 12, '2026-05-15', '2026-07-15', 1),
  ('1', 61, 0, 'Gestionar aprobación de recursos ante la Alta Dirección', 'Presentar la solicitud de recursos identificada a la Alta Dirección para su aprobación y asignación formal.', 12, '2026-05-15', '2026-08-30', 1),
  ('1', 62, 0, 'Asignar responsables por actividad de objetivos', 'Documentar en el plan de objetivos quién es responsable de cada actividad definida.', 12, '2026-05-15', '2026-06-30', 1),
  ('1', 76, 0, 'Definir plazos de cumplimiento por objetivo', 'Establecer un cronograma con fechas de culminación para cada actividad asociada a los objetivos de seguridad.', 12, '2026-05-15', '2026-06-30', 1),
  ('1', 127, 0, 'Definir criterio de evaluación de resultados', 'Establecer el método y los criterios con los que se evaluarán los resultados obtenidos frente a los objetivos planificados.', 12, '2026-05-15', '2026-07-31', 1),
  ('1', 127, 0, 'Documentar procedimiento de evaluación de objetivos', 'Formalizar el procedimiento de evaluación de resultados como parte del ciclo de planificación de objetivos del SGSI.', 12, '2026-05-15', '2026-08-30', 1),
  ('1', 128, 0, 'Documentar procedimiento de comunicación del SGSI', 'Elaborar y aprobar un procedimiento que defina qué, cuándo, a quién y cómo comunicar interna y externamente asuntos relevantes del SGSI.', 1, '2026-05-15', '2026-07-31', 2),
  ('2', 0, 10, 'Incorporar checklist de seguridad en gestión de proyectos', 'Añadir un checklist de requisitos de seguridad de la información obligatorio en la metodología de gestión de proyectos.', 16, '2026-05-15', '2026-07-31', 2),
  ('2', 0, 14, 'Definir esquema de clasificación de información', 'Elaborar y aprobar un esquema formal de clasificación de la información (pública, interna, confidencial, restringida).', 1, '2026-05-15', '2026-06-30', 1),
  ('2', 0, 14, 'Aplicar clasificación a los activos de información críticos', 'Ejecutar la clasificación sobre los activos de información identificados como críticos según el esquema aprobado.', 1, '2026-05-15', '2026-08-30', 1),
  ('2', 0, 15, 'Definir e implementar etiquetado según clasificación', 'Establecer un procedimiento de etiquetado de información acorde al esquema de clasificación aprobado y aplicarlo a los activos críticos.', 1, '2026-05-15', '2026-08-15', 2),
  ('2', 0, 16, 'Definir controles de transferencia segura de información', 'Documentar los canales autorizados y controles técnicos (cifrado, VPN) para la transferencia de información con terceros y entre áreas.', 1, '2026-05-15', '2026-07-15', 1),
  ('2', 0, 16, 'Difundir y capacitar sobre transferencia segura', 'Comunicar el procedimiento aprobado y capacitar al personal que transfiere información crítica de forma habitual.', 1, '2026-05-15', '2026-08-30', 1),
  ('2', 0, 77, 'Restringir uso de utilidades con privilegios elevados', 'Implementar una política técnica (GPO o similar) que restrinja el uso de programas de utilidad con privilegios elevados a personal autorizado.', 3, '2026-05-15', '2026-07-15', 1),
  ('2', 0, 77, 'Auditar uso histórico de utilidades privilegiadas', 'Revisar registros existentes para identificar usos indebidos previos y ajustar la política según hallazgos.', 3, '2026-05-15', '2026-08-30', 1),
  ('2', 0, 78, 'Restringir instalación de software no autorizado', 'Implementar un control técnico (GPO o whitelisting) que impida la instalación de software no autorizado en los sistemas operativos.', 3, '2026-05-15', '2026-07-31', 2),
  -- ===== Grupo 2: 20 nuevos (uno por brecha nueva), vencen antes de junio =====
  ('1', 6, 0, 'Formalizar periodicidad del análisis de contexto', 'Definir una periodicidad semestral de revisión del análisis de contexto y formalizarla en el procedimiento del SGSI, incorporando los resultados a las actas del Comité SGSI.', 2, '2026-05-20', '2026-06-15', 2),
  ('1', 18, 0, 'Institucionalizar el ciclo de mejora continua del SGSI', 'Formalizar el ciclo PHVA del SGSI en un procedimiento único que integre planificación, revisión por la dirección y acciones de mejora.', 12, '2026-05-20', '2026-06-15', 2),
  ('1', 35, 0, 'Aprobar matriz de roles y responsabilidades del SGSI', 'Revisar anualmente la matriz de roles y responsabilidades para reflejar cambios organizacionales.', 2, '2026-05-20', '2026-06-15', 2),
  ('1', 41, 0, 'Aprobar metodología de evaluación de riesgos', 'Automatizar el registro de evaluaciones de riesgo mediante el módulo de Riesgos de la herramienta, reduciendo el uso de hojas de cálculo externas.', 1, '2026-05-20', '2026-06-15', 1),
  ('1', 51, 0, 'Poner en ejecución el plan de tratamiento de riesgos', 'Establecer revisiones trimestrales del avance del plan de tratamiento de riesgos con registro documentado de resultados.', 1, '2026-05-20', '2026-06-15', 1),
  ('1', 68, 0, 'Asignar recursos formales para el SGSI', 'Incluir la revisión de recursos del SGSI como punto fijo de la revisión anual por la dirección.', 11, '2026-05-20', '2026-06-15', 2),
  ('1', 69, 0, 'Determinar competencias y capacitar roles críticos', 'Formalizar un plan anual de capacitación en seguridad de la información con evaluación de efectividad posterior.', 13, '2026-05-20', '2026-06-15', 2),
  ('1', 73, 0, 'Iniciar programa de concientización en seguridad', 'Establecer un programa anual de concientización con cobertura a toda la organización y registro de asistencia/evaluación.', 14, '2026-05-20', '2026-06-15', 2),
  ('1', 77, 0, 'Establecer control documental del SGSI', 'Migrar el control de versiones de la documentación del SGSI a un repositorio centralizado con control de acceso.', 1, '2026-05-20', '2026-06-15', 2),
  ('1', 83, 0, 'Planificar y controlar procesos operativos del SGSI', 'Formalizar la evaluación de seguridad de nuevos proveedores tercerizados previo a su incorporación operativa.', 11, '2026-05-20', '2026-06-15', 2),
  ('1', 87, 0, 'Aprobar metodología de evaluación de riesgos operativa', 'Automatizar el registro de evaluaciones de riesgo mediante el módulo de Riesgos de la herramienta, reduciendo el uso de hojas de cálculo externas.', 1, '2026-05-20', '2026-06-15', 1),
  ('1', 89, 0, 'Poner en ejecución el plan de tratamiento de riesgos operativo', 'Establecer revisiones trimestrales del avance del plan de tratamiento de riesgos con registro documentado de resultados.', 1, '2026-05-20', '2026-06-15', 1),
  ('1', 94, 0, 'Definir plan de monitoreo del SGSI', 'Consolidar los indicadores de desempeño del SGSI en un tablero único con periodicidad de análisis definida.', 1, '2026-05-20', '2026-06-15', 2),
  ('1', 115, 0, 'Implementar registro de no conformidades y acciones correctivas', 'Implementar un repositorio único de no conformidades y acciones correctivas para facilitar el análisis de recurrencia.', 12, '2026-05-20', '2026-06-15', 2),
  ('2', 0, 11, 'Consolidar inventario de activos de información', 'Implementar un checklist de devolución de activos vinculado al proceso de salida de personal.', 3, '2026-05-20', '2026-06-15', 2),
  ('2', 0, 21, 'Incorporar cláusulas de seguridad en acuerdos con proveedores', 'Incorporar una evaluación formal de riesgos de la cadena de suministro TIC antes de la contratación de nuevos proveedores tecnológicos.', 16, '2026-05-20', '2026-06-15', 2),
  ('2', 0, 33, 'Registrar requisitos legales aplicables al SGSI', 'Establecer una revisión anual conjunta con Asesoría Jurídica para actualizar el registro de requisitos legales aplicables.', 10, '2026-05-20', '2026-06-15', 2),
  ('2', 0, 42, 'Formalizar acuerdos de confidencialidad y procedimiento de terminación', 'Incluir en el procedimiento de terminación la revocación inmediata de accesos como paso obligatorio verificable.', 13, '2026-05-20', '2026-06-15', 1),
  ('2', 0, 46, 'Definir perímetro de seguridad física', 'Evaluar la incorporación de control biométrico en los puntos de acceso más críticos.', 5, '2026-05-20', '2026-06-15', 1),
  ('2', 0, 73, 'Implementar redundancia en instalaciones críticas', 'Centralizar la sincronización de reloj (NTP) de todos los sistemas críticos y validarla periódicamente.', 1, '2026-05-20', '2026-06-15', 1)
) AS v(breach_type, requirement_id, control_id, title, description, responsible_id, start_date, due_date, priority_id);

COMMIT;
