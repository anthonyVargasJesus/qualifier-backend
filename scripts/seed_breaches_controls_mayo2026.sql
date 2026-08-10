-- =====================================================================
-- Seed: brechas de controles para 'Evaluación - Mayo 2026'.
-- Dos grupos:
--  1) Las 6 brechas que YA existían en junio (mismo texto que
--     seed_breaches_controls_junio2026.sql).
--  2) 6 brechas NUEVAS para los controles que en junio ya estaban
--     conformes (nivel 10) pero que en mayo se retrocedieron a nivel 11
--     (ver seed_control_evaluation_mayo2026.sql).
-- Todas de tipo Control (C_TYPE=2), estado inicial "Abierta".
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
SELECT ev.id, 4, '2', 0, v.control_id, v.title, v.description, v.severity_id, 1, v.responsible_id,
       v.evidence_description, 1, 1, now(), false, v.numeration
FROM ev, (VALUES
  -- ===== Grupo 1: las 6 que ya existían en junio (mismo texto) =====
  (10, 'El control 5.8 no se cumple: Seguridad de la información en gestión de proyectos', 'La seguridad de la información se considera solo de forma puntual en algunos proyectos. No existe un requisito formal ni un checklist de seguridad aplicado a toda la metodología de gestión de proyectos de la organización.', 2, 16, 'No se encontró una metodología de proyectos que incluya requisitos de seguridad de la información como paso obligatorio.', '5.8'),
  (14, 'El control 5.12 no se cumple: Clasificación de información', 'Se ha iniciado la clasificación de información crítica, pero no existe un esquema formal de clasificación aplicado de manera consistente a todos los activos de información de la organización.', 3, 1, 'No existe un esquema de clasificación de información documentado y aprobado.', '5.12'),
  (15, 'El control 5.13 no se cumple: Etiquetado de información', 'Al no existir un esquema de clasificación consolidado, el etiquetado de la información según su nivel de clasificación no se aplica de forma consistente.', 2, 1, 'No se encontraron muestras de información etiquetada según su nivel de clasificación.', '5.13'),
  (16, 'El control 5.14 no se cumple: Transferencia de información', 'No se aplican controles consistentes para la transferencia segura de información (interna, con proveedores o entidades externas), lo que expone la información a interceptación o divulgación no autorizada.', 3, 1, 'No existe un procedimiento de transferencia segura de información ni registro de canales autorizados.', '5.14'),
  (77, 'El control 8.18 no se cumple: Uso de programas de utilidad privilegiados', 'El uso de programas de utilidad con privilegios elevados se realiza de forma manual, sin un control centralizado ni restricciones formales sobre quién puede ejecutarlos.', 3, 3, 'No existe una política ni un control técnico (GPO o similar) que restrinja el uso de utilidades con privilegios elevados.', '8.18'),
  (78, 'El control 8.19 no se cumple: Instalación de software en sistemas operativos', 'La instalación de software en los sistemas operativos no cuenta con un control centralizado formal, permitiendo instalaciones no autorizadas por parte de los usuarios.', 2, 3, 'No existe una política de restricción de instalación de software ni un mecanismo técnico que la haga cumplir.', '8.19'),
  -- ===== Grupo 2: 6 nuevas (regresadas desde nivel 10 de junio) =====
  (11, 'El control 5.9 no se cumple: Inventario de información y otros activos asociados', 'El inventario de activos de información y las reglas de uso aceptable aún no están consolidados de forma sistemática.', 2, 3, 'No se encontró un inventario de activos de información completo y actualizado.', '5.9'),
  (21, 'El control 5.19 no se cumple: Seguridad de la información en relación con proveedores', 'Aún no se incluyen cláusulas de seguridad en los acuerdos con proveedores relevantes ni se monitorean formalmente los servicios prestados.', 2, 16, 'No se encontraron acuerdos con cláusulas de seguridad de la información vigentes con proveedores.', '5.19'),
  (33, 'El control 5.31 no se cumple: Identificación de requisitos legales, estatutarios, reglamentarios y contractuales', 'Los requisitos legales, de propiedad intelectual, protección de registros y datos personales aplicables aún no están formalmente identificados ni gestionados.', 2, 10, 'No existe un registro de requisitos legales aplicables al SGSI.', '5.31'),
  (42, 'El control 6.5 no se cumple: Responsabilidades después de la terminación o cambio de empleo', 'Aún no existen acuerdos de confidencialidad firmados por el personal ni un procedimiento formal de responsabilidades tras la terminación del empleo.', 3, 13, 'No se encontraron acuerdos de confidencialidad firmados ni un procedimiento de terminación de empleo documentado.', '6.5'),
  (46, 'El control 7.1 no se cumple: Perímetro de seguridad física', 'El perímetro de seguridad física aún no está formalmente definido ni los controles de entrada (credenciales, registro de visitantes) aplicados de forma establecida.', 3, 5, 'No se encontró documentación del perímetro de seguridad física ni registro de controles de entrada.', '7.1'),
  (73, 'El control 8.14 no se cumple: Redundancia de las instalaciones de procesamiento de información', 'Aún no se cuenta con redundancia en las instalaciones críticas de procesamiento de información.', 3, 1, 'No se encontró evidencia de instalaciones de procesamiento redundantes.', '8.14')
) AS v(control_id, title, description, severity_id, responsible_id, evidence_description, numeration);

COMMIT;
