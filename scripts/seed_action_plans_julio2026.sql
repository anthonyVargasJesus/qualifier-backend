-- =====================================================================
-- Seed: planes de acción (MAE_ACTION_PLAN) para las 13 brechas que
-- continúan abiertas en 'Evaluación - Julio 2026' (8 de requisitos + 5 de
-- controles — ver seed_breaches_julio2026.sql y
-- seed_breaches_controls_julio2026.sql).
-- Criterio: brechas de severidad Alta reciben 2 planes (una acción de
-- corto plazo + una de cierre/difusión); brechas de severidad Media
-- reciben 1. Prioridad heredada de la severidad (Alta->1, Media->2).
-- Estado inicial "Pendiente" (recién creados para este ciclo), fecha de
-- inicio 20/07/2026.
-- Requiere haber ejecutado antes seed_evaluation_julio2026.sql y los dos
-- scripts de brechas de julio.
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
INSERT INTO "MAE_ACTION_PLAN" (
  "N_BREACH_ID_FK", "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TITLE", "C_DESCRIPTION",
  "N_RESPONSIBLE_ID_FK", "D_START_DATE", "D_DUE_DATE", "N_ACTION_PLAN_STATUS_ID_FK", "N_ACTION_PLAN_PRIORITY_ID_FK",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  (SELECT b."N_BREACH_ID_PK" FROM "MAE_BREACH" b
   WHERE b."N_EVALUATION_ID_FK" = ev.id AND b."C_TYPE" = v.breach_type
     AND b."N_REQUIREMENT_ID_FK" = v.requirement_id AND b."N_CONTROL_ID_FK" = v.control_id),
  ev.id, 4, v.title, v.description, v.responsible_id, DATE '2026-07-20', v.due_date::date, 1, v.priority_id, 1, 1, now(), false
FROM ev, (VALUES
  -- Requisitos
  ('1', 52, 0, 'Aprobar matriz de vinculación entre objetivos, política y riesgos', 'Presentar y aprobar formalmente ante el Comité SGSI la matriz que vincula los objetivos de seguridad con la política vigente y los resultados de la evaluación de riesgos.', 12, '2026-08-31', 2),
  ('1', 54, 0, 'Definir indicadores de cumplimiento de objetivos', 'Establecer indicadores medibles para cada objetivo de seguridad de la información y un mecanismo de reporte periódico.', 12, '2026-09-15', 2),
  ('1', 55, 0, 'Aprobar actualización de objetivos con resultados de riesgos', 'Revisar y aprobar la versión actualizada del documento de objetivos que referencia el registro de riesgos evaluados y tratados.', 12, '2026-08-31', 2),
  ('1', 56, 0, 'Poner en operación el reporte de indicadores de objetivos', 'Habilitar el canal de reporte periódico de los indicadores de cumplimiento de objetivos a las partes pertinentes.', 12, '2026-09-15', 2),
  ('1', 57, 0, 'Establecer control de versiones del documento de objetivos', 'Definir una periodicidad de actualización y aplicar control de versiones al documento de objetivos de seguridad.', 1, '2026-08-15', 2),
  ('1', 61, 0, 'Validar cronograma de plazos por objetivo', 'Validar con el Comité SGSI el cronograma de plazos de cumplimiento elaborado para cada objetivo de seguridad.', 12, '2026-08-15', 1),
  ('1', 61, 0, 'Comunicar cronograma de plazos a responsables', 'Difundir formalmente el cronograma validado a todos los responsables asignados por actividad.', 12, '2026-08-31', 1),
  ('1', 127, 0, 'Levantar matriz de requisitos de partes interesadas', 'A partir del registro de partes interesadas ya aprobado, documentar los requisitos pertinentes de cada una en una matriz.', 9, '2026-08-31', 1),
  ('1', 128, 0, 'Determinar requisitos que abordará el SGSI', 'Con la matriz de requisitos aprobada, determinar y documentar cuáles serán atendidos por el SGSI y cuáles quedan fuera de alcance.', 9, '2026-09-15', 1),
  ('1', 128, 0, 'Aprobar alcance de requisitos abordados por el SGSI', 'Presentar y aprobar formalmente con el Comité SGSI el listado de requisitos de partes interesadas que serán abordados por el SGSI.', 9, '2026-09-30', 1),
  -- Controles
  ('2', 0, 10, 'Incorporar checklist de seguridad a la metodología de proyectos', 'Aprobar el checklist de requisitos de seguridad de la información e incorporarlo como paso obligatorio en la metodología de gestión de proyectos.', 16, '2026-08-31', 2),
  ('2', 0, 15, 'Definir e implementar etiquetado según clasificación', 'Establecer un procedimiento de etiquetado de información acorde al esquema de clasificación ya aprobado y aplicarlo a los activos críticos.', 1, '2026-09-15', 2),
  ('2', 0, 16, 'Difundir procedimiento de transferencia segura', 'Comunicar formalmente el procedimiento de transferencia segura de información (canales autorizados, cifrado, VPN) a toda la organización.', 1, '2026-08-15', 1),
  ('2', 0, 16, 'Capacitar al personal en transferencia segura', 'Capacitar al personal que transfiere información crítica de forma habitual en el uso del procedimiento aprobado.', 1, '2026-08-31', 1),
  ('2', 0, 77, 'Extender política GPO a la totalidad de los sistemas', 'Extender la política técnica de restricción de utilidades con privilegios elevados a todos los sistemas críticos restantes.', 3, '2026-08-31', 1),
  ('2', 0, 77, 'Auditar uso histórico de utilidades privilegiadas', 'Completar la revisión de registros históricos para identificar usos indebidos previos y ajustar la política según los hallazgos.', 3, '2026-09-15', 1),
  ('2', 0, 78, 'Restringir instalación de software no autorizado', 'Implementar un control técnico (GPO o whitelisting) que impida la instalación de software no autorizado en los sistemas operativos.', 3, '2026-09-15', 2)
) AS v(breach_type, requirement_id, control_id, title, description, responsible_id, due_date, priority_id);

COMMIT;
