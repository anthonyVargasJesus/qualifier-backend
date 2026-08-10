-- =====================================================================
-- Seed: una brecha (MAE_BREACH) por cada control del Anexo A que continúa
-- 'NO CUMPLIDO' en 'Evaluación - Julio 2026' (5 de los 6 controles que
-- estaban en brecha en Junio 2026; 5.12 Clasificación de información
-- cerró — ver seed_control_evaluation_julio2026.sql).
-- Todas de tipo Control (C_TYPE=2), estado inicial "Abierta".
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
SELECT ev.id, 4, '2', 0, v.control_id, v.title, v.description, v.severity_id, 1, v.responsible_id,
       v.evidence_description, 1, 1, now(), false, v.numeration
FROM ev, (VALUES
  (10, 'El control 5.8 no se cumple: Seguridad de la información en gestión de proyectos', 'La seguridad de la información se considera solo de forma puntual en algunos proyectos. El checklist de requisitos de seguridad está en elaboración, pero aún no ha sido incorporado formalmente a la metodología de gestión de proyectos.', 2, 16, 'El checklist está redactado y en revisión; no se ha encontrado evidencia de su incorporación efectiva a la metodología de proyectos.', '5.8'),
  (15, 'El control 5.13 no se cumple: Etiquetado de información', 'Al no haberse aplicado aún el esquema de clasificación (recién aprobado en julio) a los activos de información, el etiquetado según su nivel de clasificación no se aplica de forma consistente.', 2, 1, 'No se encontraron muestras de información etiquetada según su nivel de clasificación; sin cambios desde junio.', '5.13'),
  (16, 'El control 5.14 no se cumple: Transferencia de información', 'Se han definido los canales autorizados y el uso de cifrado/VPN para la transferencia segura de información, pero el procedimiento aún no ha sido difundido ni el personal capacitado en su aplicación.', 3, 1, 'Existe un documento de canales autorizados en borrador; no hay evidencia de difusión ni de capacitación al personal que transfiere información crítica.', '5.14'),
  (77, 'El control 8.18 no se cumple: Uso de programas de utilidad privilegiados', 'Se ha desplegado una política técnica (GPO) en los servidores críticos que restringe el uso de programas de utilidad con privilegios elevados, pero aún no cubre la totalidad de los sistemas ni se ha auditado el uso histórico.', 3, 3, 'GPO desplegada y verificada en servidores críticos; pendiente extenderla al resto de sistemas y completar la auditoría de uso histórico.', '8.18'),
  (78, 'El control 8.19 no se cumple: Instalación de software en sistemas operativos', 'La instalación de software en los sistemas operativos no cuenta con un control centralizado formal, permitiendo instalaciones no autorizadas por parte de los usuarios.', 2, 3, 'No existe una política de restricción de instalación de software ni un mecanismo técnico que la haga cumplir; sin cambios desde junio.', '8.19')
) AS v(control_id, title, description, severity_id, responsible_id, evidence_description, numeration);

COMMIT;
