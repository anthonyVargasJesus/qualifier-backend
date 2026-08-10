-- =====================================================================
-- Seed: controles de implementación (MAE_CONTROL_IMPLEMENTATION) para
-- los 6 riesgos 'En tratamiento' de 'Evaluación - Mayo 2026' (mismo
-- contenido que seed_control_implementation_junio2026.sql, fechas
-- retrocedidas un mes).
-- Requiere haber ejecutado antes seed_risk_treatment_mayo2026.sql.
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
risk_lookup AS (
  SELECT r."N_RISK_ID_PK" AS risk_id, b."C_TYPE" AS breach_type,
         b."N_REQUIREMENT_ID_FK" AS requirement_id, b."N_CONTROL_ID_FK" AS control_id
  FROM "MAE_RISK" r
  JOIN "MAE_BREACH" b ON b."N_BREACH_ID_PK" = r."N_BREACH_ID_FK"
  WHERE r."N_EVALUATION_ID_FK" = (SELECT id FROM ev)
)
INSERT INTO "MAE_CONTROL_IMPLEMENTATION" (
  "N_RISK_ID_FK", "C_ACTIVITIES", "D_START_DATE", "D_VERIFICATION_DATE", "N_RESPONSIBLE_ID_FK",
  "C_OBSERVATION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "L_IS_IMPLEMENTED"
)
SELECT rl.risk_id, v.activity, v.start_date, v.verification_date, v.responsible_id,
       v.observation, 1, 1, now(), false, v.is_implemented
FROM risk_lookup rl
JOIN (VALUES
  -- Riesgo: req 4.2.1 - partes interesadas
  ('1', 7, 0, 'Elaborar registro formal de partes interesadas del SGSI (entes reguladores, ciudadanía, proveedores TI, otras entidades del Estado).', DATE '2026-05-20', DATE '2026-06-10', 9, 'Registro elaborado y remitido para revisión.', true),
  ('1', 7, 0, 'Levantar y documentar los requisitos pertinentes de cada parte interesada identificada.', DATE '2026-05-20', DATE '2026-06-15', 9, 'Matriz de requisitos completada.', true),
  ('1', 7, 0, 'Aprobar el registro y la matriz de requisitos con el Comité SGSI.', DATE '2026-06-15', NULL, 12, NULL, false),
  ('1', 7, 0, 'Establecer una revisión periódica anual del registro de partes interesadas.', DATE '2026-07-01', NULL, 9, NULL, false),
  -- Riesgo: req 6.2.6 - planificación de actividades por objetivo
  ('1', 58, 0, 'Definir las actividades necesarias para el logro de cada objetivo de seguridad.', DATE '2026-05-20', DATE '2026-06-10', 12, 'Listado de actividades por objetivo elaborado.', true),
  ('1', 58, 0, 'Identificar y asignar los recursos (presupuesto, personal, herramientas) necesarios por objetivo.', DATE '2026-06-10', NULL, 12, NULL, false),
  ('1', 58, 0, 'Asignar responsables formales por cada actividad definida.', DATE '2026-06-15', NULL, 12, NULL, false),
  ('1', 58, 0, 'Definir un cronograma con plazos de cumplimiento para cada actividad.', DATE '2026-07-01', NULL, 12, NULL, false),
  -- Riesgo: control 5.12 - clasificación de información
  ('2', 0, 14, 'Definir un esquema formal de clasificación de la información (pública, interna, confidencial, restringida).', DATE '2026-05-20', DATE '2026-06-05', 1, 'Esquema elaborado.', true),
  ('2', 0, 14, 'Aprobar el esquema de clasificación con el Comité SGSI.', DATE '2026-06-05', DATE '2026-06-12', 12, 'Aprobado en sesión del Comité SGSI.', true),
  ('2', 0, 14, 'Aplicar la clasificación a la Base de Datos del Padrón Electoral y demás activos críticos.', DATE '2026-06-15', NULL, 1, NULL, false),
  -- Riesgo: control 5.13 - etiquetado de información
  ('2', 0, 15, 'Definir el procedimiento de etiquetado de información según el esquema de clasificación aprobado.', DATE '2026-06-15', NULL, 1, NULL, false),
  ('2', 0, 15, 'Capacitar al personal sobre el etiquetado de información según su clasificación.', DATE '2026-07-01', NULL, 14, NULL, false),
  ('2', 0, 15, 'Aplicar el etiquetado a los activos de información críticos.', DATE '2026-07-15', NULL, 1, NULL, false),
  -- Riesgo: control 5.14 - transferencia de información
  ('2', 0, 16, 'Implementar cifrado para la transferencia de información sensible.', DATE '2026-05-20', DATE '2026-06-08', 3, 'Cifrado habilitado en los canales críticos.', true),
  ('2', 0, 16, 'Configurar VPN para las comunicaciones con proveedores y entidades externas.', DATE '2026-05-25', DATE '2026-06-10', 3, 'VPN configurada y en operación.', true),
  ('2', 0, 16, 'Documentar los canales autorizados de transferencia de información.', DATE '2026-06-15', NULL, 1, NULL, false),
  ('2', 0, 16, 'Auditar los canales de transferencia de información existentes.', DATE '2026-07-01', NULL, 15, NULL, false),
  -- Riesgo: control 8.18 - utilidades privilegiadas
  ('2', 0, 77, 'Implementar una política técnica (GPO) que restrinja el uso de utilidades con privilegios elevados.', DATE '2026-05-20', DATE '2026-06-05', 3, 'Política GPO desplegada en los servidores críticos.', true),
  ('2', 0, 77, 'Revisar y documentar el uso histórico de utilidades con privilegios elevados.', DATE '2026-06-10', NULL, 3, NULL, false),
  ('2', 0, 77, 'Capacitar a los administradores de sistemas sobre el uso restringido de utilidades privilegiadas.', DATE '2026-07-01', NULL, 3, NULL, false)
) AS v(breach_type, requirement_id, control_id, activity, start_date, verification_date, responsible_id, observation, is_implemented)
  ON rl.breach_type = v.breach_type AND rl.requirement_id = v.requirement_id AND rl.control_id = v.control_id;

COMMIT;
