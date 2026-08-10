-- =====================================================================
-- Seed: re-evaluación de los 6 controles del Anexo A que quedaron NO
-- CUMPLIDOS en 'Análisis de Brechas ISO 27001 - Junio 2026' (evaluationId=4),
-- ahora para 'Evaluación - Julio 2026'.
-- Carga: MAE_CONTROL_EVALUATION solo para esos 6 controles (no se
-- re-evalúan los otros 87 que ya estaban conformes en Junio).
-- Resultado del avance: 1 control cierra (5.12 Clasificación de
-- información, nivel 11 -> 10); 5 continúan NO CUMPLIDOS (3 con avance
-- parcial documentado, 2 sin cambios desde Junio).
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
INSERT INTO "MAE_CONTROL_EVALUATION" (
  "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK", "N_VALUE",
  "N_RESPONSIBLE_ID_FK", "C_JUSTIFICATION", "C_IMPROVEMENT_ACTIONS", "C_CONTROL_DESCRIPTION",
  "N_STANDARD_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT ev.id, v.control_id, v.maturity_level_id, v.value, v.responsible_id,
       v.justification, v.improvement_actions, v.control_description, 4, 1, 1, now(), false
FROM ev, (VALUES
  -- ===== CIERRA (nivel 11 -> 10, valor 1.00 -> 3.00) =====
  (14, 10, 3.00, 1, 'Se ha definido y aprobado un esquema formal de clasificación de la información (pública, interna, confidencial, restringida) con el Comité SGSI.', 'Aplicar la clasificación aprobada a la totalidad de los activos de información críticos, en particular la Base de Datos del Padrón Electoral.', 'Clasificación de información.'),
  -- ===== CONTINÚAN NO CUMPLIDOS, con avance parcial documentado =====
  (10, 11, 1.00, 16, 'El checklist de requisitos de seguridad de la información para la gestión de proyectos se encuentra en elaboración; aún no ha sido incorporado formalmente a la metodología de gestión de proyectos.', 'Aprobar el checklist e incorporarlo como paso obligatorio en la metodología de gestión de proyectos vigente.', 'Integración de la seguridad de la información en la gestión de proyectos.'),
  (16, 11, 1.00, 1, 'Se han definido los canales autorizados y el uso de cifrado/VPN para la transferencia segura de información con terceros y entre áreas; falta difundir el procedimiento y capacitar al personal que transfiere información crítica.', 'Difundir el procedimiento aprobado y capacitar al personal que transfiere información crítica de forma habitual.', 'Transferencia de información.'),
  (77, 11, 1.00, 3, 'Se ha desplegado una política técnica (GPO) que restringe el uso de programas de utilidad con privilegios elevados en los servidores críticos; falta extenderla a la totalidad de los sistemas y auditar el uso histórico.', 'Extender la política GPO a todos los sistemas restantes y completar la auditoría de uso histórico de utilidades privilegiadas.', 'Uso de programas de utilidad privilegiados.'),
  -- ===== CONTINÚAN NO CUMPLIDOS, sin cambios desde Junio =====
  (15, 11, 1.00, 1, 'Al no existir un esquema de clasificación consolidado y aplicado, el etiquetado de la información según su nivel de clasificación no se aplica de forma consistente.', 'Definir e implementar un procedimiento de etiquetado de información acorde al esquema de clasificación aprobado y aplicarlo a los activos críticos.', 'Etiquetado de información.'),
  (78, 11, 1.00, 3, 'La instalación de software en los sistemas operativos no cuenta con un control centralizado formal, permitiendo instalaciones no autorizadas por parte de los usuarios.', 'Implementar un control técnico (GPO o whitelisting) que impida la instalación de software no autorizado en los sistemas operativos.', 'Instalación de software en sistemas operativos.')
) AS v(control_id, maturity_level_id, value, responsible_id, justification, improvement_actions, control_description);

COMMIT;
