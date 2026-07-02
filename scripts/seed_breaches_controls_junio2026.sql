-- =====================================================================
-- Seed: una brecha (MAE_BREACH) por cada control 'NO CUMPLIDO' en la
-- evaluación 'Análisis de Brechas ISO 27001 - Junio 2026' (evaluationId=4)
-- Alcance: los 6 controles con calificación IN (Inicial), que coinciden
--          exactamente con el conteo "NO CUMPLIDOS (6)" mostrado en la UI
--          para Controles en esta evaluación.
-- Todas de tipo Control (C_TYPE=2), estado inicial "Abierta".
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_BREACH" (
  "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK",
  "C_TITLE", "C_DESCRIPTION", "N_BREACH_SEVERITY_ID_FK", "N_BREACH_STATUS_ID_FK", "N_RESPONSIBLE_ID_FK",
  "C_EVIDENCE_DESCRIPTION", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "C_NUMERATION_TO_SHOW"
)
VALUES
  (4, 4, '2', 0, 10, 'El control 5.8 no se cumple: Seguridad de la información en gestión de proyectos', 'La seguridad de la información se considera solo de forma puntual en algunos proyectos. No existe un requisito formal ni un checklist de seguridad aplicado a toda la metodología de gestión de proyectos de la organización.', 2, 1, 16, 'No se encontró una metodología de proyectos que incluya requisitos de seguridad de la información como paso obligatorio.', 1, 1, now(), false, '5.8'),
  (4, 4, '2', 0, 14, 'El control 5.12 no se cumple: Clasificación de información', 'Se ha iniciado la clasificación de información crítica, pero no existe un esquema formal de clasificación aplicado de manera consistente a todos los activos de información de la organización.', 3, 1, 1, 'No existe un esquema de clasificación de información documentado y aprobado.', 1, 1, now(), false, '5.12'),
  (4, 4, '2', 0, 15, 'El control 5.13 no se cumple: Etiquetado de información', 'Al no existir un esquema de clasificación consolidado, el etiquetado de la información según su nivel de clasificación no se aplica de forma consistente.', 2, 1, 1, 'No se encontraron muestras de información etiquetada según su nivel de clasificación.', 1, 1, now(), false, '5.13'),
  (4, 4, '2', 0, 16, 'El control 5.14 no se cumple: Transferencia de información', 'No se aplican controles consistentes para la transferencia segura de información (interna, con proveedores o entidades externas), lo que expone la información a interceptación o divulgación no autorizada.', 3, 1, 1, 'No existe un procedimiento de transferencia segura de información ni registro de canales autorizados.', 1, 1, now(), false, '5.14'),
  (4, 4, '2', 0, 77, 'El control 8.18 no se cumple: Uso de programas de utilidad privilegiados', 'El uso de programas de utilidad con privilegios elevados se realiza de forma manual, sin un control centralizado ni restricciones formales sobre quién puede ejecutarlos.', 3, 1, 3, 'No existe una política ni un control técnico (GPO o similar) que restrinja el uso de utilidades con privilegios elevados.', 1, 1, now(), false, '8.18'),
  (4, 4, '2', 0, 78, 'El control 8.19 no se cumple: Instalación de software en sistemas operativos', 'La instalación de software en los sistemas operativos no cuenta con un control centralizado formal, permitiendo instalaciones no autorizadas por parte de los usuarios.', 2, 1, 3, 'No existe una política de restricción de instalación de software ni un mecanismo técnico que la haga cumplir.', 1, 1, now(), false, '8.19');

COMMIT;
