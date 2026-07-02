-- =====================================================================
-- Seed: 3-4 controles (MAE_CONTROL_IMPLEMENTATION) por cada uno de los 6
-- riesgos 'En tratamiento' de la evaluación Junio 2026 (evaluationId=4)
-- Mezcla L_IS_IMPLEMENTED = true/false para que el resumen "Controles"
-- (X implementados, Y pendientes) de la lista 'Riesgos en tratamiento'
-- se vea realista, no todo implementado ni todo pendiente.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_CONTROL_IMPLEMENTATION" (
  "N_RISK_ID_FK", "C_ACTIVITIES", "D_START_DATE", "D_VERIFICATION_DATE", "N_RESPONSIBLE_ID_FK",
  "C_OBSERVATION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "L_IS_IMPLEMENTED"
)
VALUES
  (22, 'Elaborar registro formal de partes interesadas del SGSI (entes reguladores, ciudadanía, proveedores TI, otras entidades del Estado).', '2026-06-20', '2026-07-10', 9, 'Registro elaborado y remitido para revisión.', 1, 1, now(), false, true),
  (22, 'Levantar y documentar los requisitos pertinentes de cada parte interesada identificada.', '2026-06-20', '2026-07-15', 9, 'Matriz de requisitos completada.', 1, 1, now(), false, true),
  (22, 'Aprobar el registro y la matriz de requisitos con el Comité SGSI.', '2026-07-15', NULL, 12, NULL, 1, 1, now(), false, false),
  (22, 'Establecer una revisión periódica anual del registro de partes interesadas.', '2026-08-01', NULL, 9, NULL, 1, 1, now(), false, false),
  (30, 'Definir las actividades necesarias para el logro de cada objetivo de seguridad.', '2026-06-20', '2026-07-10', 12, 'Listado de actividades por objetivo elaborado.', 1, 1, now(), false, true),
  (30, 'Identificar y asignar los recursos (presupuesto, personal, herramientas) necesarios por objetivo.', '2026-07-10', NULL, 12, NULL, 1, 1, now(), false, false),
  (30, 'Asignar responsables formales por cada actividad definida.', '2026-07-15', NULL, 12, NULL, 1, 1, now(), false, false),
  (30, 'Definir un cronograma con plazos de cumplimiento para cada actividad.', '2026-08-01', NULL, 12, NULL, 1, 1, now(), false, false),
  (37, 'Definir un esquema formal de clasificación de la información (pública, interna, confidencial, restringida).', '2026-06-20', '2026-07-05', 1, 'Esquema elaborado.', 1, 1, now(), false, true),
  (37, 'Aprobar el esquema de clasificación con el Comité SGSI.', '2026-07-05', '2026-07-12', 12, 'Aprobado en sesión del Comité SGSI.', 1, 1, now(), false, true),
  (37, 'Aplicar la clasificación a la Base de Datos del Padrón Electoral y demás activos críticos.', '2026-07-15', NULL, 1, NULL, 1, 1, now(), false, false),
  (38, 'Definir el procedimiento de etiquetado de información según el esquema de clasificación aprobado.', '2026-07-15', NULL, 1, NULL, 1, 1, now(), false, false),
  (38, 'Capacitar al personal sobre el etiquetado de información según su clasificación.', '2026-08-01', NULL, 14, NULL, 1, 1, now(), false, false),
  (38, 'Aplicar el etiquetado a los activos de información críticos.', '2026-08-15', NULL, 1, NULL, 1, 1, now(), false, false),
  (39, 'Implementar cifrado para la transferencia de información sensible.', '2026-06-20', '2026-07-08', 3, 'Cifrado habilitado en los canales críticos.', 1, 1, now(), false, true),
  (39, 'Configurar VPN para las comunicaciones con proveedores y entidades externas.', '2026-06-25', '2026-07-10', 3, 'VPN configurada y en operación.', 1, 1, now(), false, true),
  (39, 'Documentar los canales autorizados de transferencia de información.', '2026-07-15', NULL, 1, NULL, 1, 1, now(), false, false),
  (39, 'Auditar los canales de transferencia de información existentes.', '2026-08-01', NULL, 15, NULL, 1, 1, now(), false, false),
  (40, 'Implementar una política técnica (GPO) que restrinja el uso de utilidades con privilegios elevados.', '2026-06-20', '2026-07-05', 3, 'Política GPO desplegada en los servidores críticos.', 1, 1, now(), false, true),
  (40, 'Revisar y documentar el uso histórico de utilidades con privilegios elevados.', '2026-07-10', NULL, 3, NULL, 1, 1, now(), false, false),
  (40, 'Capacitar a los administradores de sistemas sobre el uso restringido de utilidades privilegiadas.', '2026-08-01', NULL, 3, NULL, 1, 1, now(), false, false);

COMMIT;
