-- =====================================================================
-- Seed: planes de acción (MAE_ACTION_PLAN) para las 20 brechas creadas
-- en la evaluación 'Análisis de Brechas ISO 27001 - Junio 2026'
-- (evaluationId=4): 14 de requisitos (breachId 19-32) + 6 de controles
-- (breachId 39-44).
-- Criterio: brechas de severidad Alta reciben 2 planes (una acción de
-- corto plazo + una estructural); brechas de severidad Media reciben 1.
-- Estado inicial "Pendiente", prioridad heredada de la severidad de la
-- brecha (Alta->Alta, Media->Media), fecha de inicio 15/06/2026.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_ACTION_PLAN" (
  "N_BREACH_ID_FK", "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TITLE", "C_DESCRIPTION",
  "N_RESPONSIBLE_ID_FK", "D_START_DATE", "D_DUE_DATE", "N_ACTION_PLAN_STATUS_ID_FK", "N_ACTION_PLAN_PRIORITY_ID_FK",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
VALUES
  (19, 4, 4, 'Elaborar registro preliminar de partes interesadas', 'Identificar y documentar en una lista inicial las partes interesadas relevantes al SGSI (reguladores, ciudadanía, proveedores TI, otras entidades del Estado).', 9, '2026-06-15', '2026-07-15', 1, 1, 1, 1, now(), false),
  (19, 4, 4, 'Formalizar procedimiento de gestión de partes interesadas', 'Documentar y aprobar un procedimiento que defina cómo se identifican, actualizan y revisan las partes interesadas de forma recurrente.', 9, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (20, 4, 4, 'Levantar requisitos de partes interesadas identificadas', 'A partir del registro de partes interesadas, documentar los requisitos pertinentes de cada una en una matriz.', 9, '2026-06-15', '2026-07-31', 1, 1, 1, 1, now(), false),
  (20, 4, 4, 'Aprobar matriz de requisitos de partes interesadas', 'Revisar y aprobar formalmente la matriz de requisitos con el Comité SGSI, dejando evidencia documentada.', 9, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (21, 4, 4, 'Definir qué requisitos abordará el SGSI', 'Con la matriz de requisitos aprobada, determinar y documentar cuáles serán atendidos por el SGSI y cuáles quedan fuera de alcance.', 9, '2026-06-15', '2026-08-15', 1, 1, 1, 1, now(), false),
  (22, 4, 4, 'Vincular objetivos de seguridad con política y riesgos', 'Actualizar el documento de objetivos de seguridad para referenciar explícitamente la política vigente y los resultados de la última evaluación de riesgos.', 12, '2026-06-15', '2026-08-15', 1, 2, 1, 1, now(), false),
  (23, 4, 4, 'Definir indicadores de cumplimiento de objetivos', 'Establecer indicadores medibles para cada objetivo de seguridad de la información y un mecanismo de reporte periódico.', 12, '2026-06-15', '2026-08-31', 1, 2, 1, 1, now(), false),
  (24, 4, 4, 'Incorporar resultados de riesgos en la planificación', 'Actualizar la formulación de objetivos para que cite el registro de riesgos evaluados y tratados vigente.', 12, '2026-06-15', '2026-08-31', 1, 2, 1, 1, now(), false),
  (25, 4, 4, 'Comunicar objetivos y planificación a la organización', 'Difundir formalmente los objetivos de seguridad y su planificación a las partes pertinentes mediante correo institucional y reunión de kickoff.', 12, '2026-06-15', '2026-07-31', 1, 2, 1, 1, now(), false),
  (26, 4, 4, 'Establecer control de versiones del documento de objetivos', 'Definir una periodicidad de actualización y aplicar control de versiones al documento de objetivos de seguridad.', 12, '2026-06-15', '2026-07-31', 1, 2, 1, 1, now(), false),
  (27, 4, 4, 'Definir actividades para cada objetivo de seguridad', 'Elaborar un listado de actividades concretas necesarias para alcanzar cada objetivo de seguridad definido.', 12, '2026-06-15', '2026-07-31', 1, 1, 1, 1, now(), false),
  (27, 4, 4, 'Aprobar plan de actividades por objetivo', 'Validar y aprobar formalmente el plan de actividades con el Comité SGSI, dejando evidencia documentada.', 12, '2026-06-15', '2026-09-15', 1, 1, 1, 1, now(), false),
  (28, 4, 4, 'Identificar recursos necesarios por objetivo', 'Determinar el presupuesto, personal y herramientas requeridos para el cumplimiento de cada objetivo de seguridad.', 12, '2026-06-15', '2026-08-15', 1, 1, 1, 1, now(), false),
  (28, 4, 4, 'Gestionar aprobación de recursos ante la Alta Dirección', 'Presentar la solicitud de recursos identificada a la Alta Dirección para su aprobación y asignación formal.', 12, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (29, 4, 4, 'Asignar responsables por actividad de objetivos', 'Documentar en el plan de objetivos quién es responsable de cada actividad definida.', 12, '2026-06-15', '2026-07-31', 1, 1, 1, 1, now(), false),
  (30, 4, 4, 'Definir plazos de cumplimiento por objetivo', 'Establecer un cronograma con fechas de culminación para cada actividad asociada a los objetivos de seguridad.', 12, '2026-06-15', '2026-07-31', 1, 1, 1, 1, now(), false),
  (31, 4, 4, 'Definir criterio de evaluación de resultados', 'Establecer el método y los criterios con los que se evaluarán los resultados obtenidos frente a los objetivos planificados.', 12, '2026-06-15', '2026-08-31', 1, 1, 1, 1, now(), false),
  (31, 4, 4, 'Documentar procedimiento de evaluación de objetivos', 'Formalizar el procedimiento de evaluación de resultados como parte del ciclo de planificación de objetivos del SGSI.', 12, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (32, 4, 4, 'Documentar procedimiento de comunicación del SGSI', 'Elaborar y aprobar un procedimiento que defina qué, cuándo, a quién y cómo comunicar interna y externamente asuntos relevantes del SGSI.', 1, '2026-06-15', '2026-08-31', 1, 2, 1, 1, now(), false),
  (39, 4, 4, 'Incorporar checklist de seguridad en gestión de proyectos', 'Añadir un checklist de requisitos de seguridad de la información obligatorio en la metodología de gestión de proyectos.', 16, '2026-06-15', '2026-08-31', 1, 2, 1, 1, now(), false),
  (40, 4, 4, 'Definir esquema de clasificación de información', 'Elaborar y aprobar un esquema formal de clasificación de la información (pública, interna, confidencial, restringida).', 1, '2026-06-15', '2026-07-31', 1, 1, 1, 1, now(), false),
  (40, 4, 4, 'Aplicar clasificación a los activos de información críticos', 'Ejecutar la clasificación sobre los activos de información identificados como críticos según el esquema aprobado.', 1, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (41, 4, 4, 'Definir e implementar etiquetado según clasificación', 'Establecer un procedimiento de etiquetado de información acorde al esquema de clasificación aprobado y aplicarlo a los activos críticos.', 1, '2026-06-15', '2026-09-15', 1, 2, 1, 1, now(), false),
  (42, 4, 4, 'Definir controles de transferencia segura de información', 'Documentar los canales autorizados y controles técnicos (cifrado, VPN) para la transferencia de información con terceros y entre áreas.', 1, '2026-06-15', '2026-08-15', 1, 1, 1, 1, now(), false),
  (42, 4, 4, 'Difundir y capacitar sobre transferencia segura', 'Comunicar el procedimiento aprobado y capacitar al personal que transfiere información crítica de forma habitual.', 1, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (43, 4, 4, 'Restringir uso de utilidades con privilegios elevados', 'Implementar una política técnica (GPO o similar) que restrinja el uso de programas de utilidad con privilegios elevados a personal autorizado.', 3, '2026-06-15', '2026-08-15', 1, 1, 1, 1, now(), false),
  (43, 4, 4, 'Auditar uso histórico de utilidades privilegiadas', 'Revisar registros existentes para identificar usos indebidos previos y ajustar la política según hallazgos.', 3, '2026-06-15', '2026-09-30', 1, 1, 1, 1, now(), false),
  (44, 4, 4, 'Restringir instalación de software no autorizado', 'Implementar un control técnico (GPO o whitelisting) que impida la instalación de software no autorizado en los sistemas operativos.', 3, '2026-06-15', '2026-08-31', 1, 2, 1, 1, now(), false);

COMMIT;
