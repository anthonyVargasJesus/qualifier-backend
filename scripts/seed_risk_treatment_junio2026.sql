-- =====================================================================
-- Seed: 6 de los 10 riesgos 'En evaluación' pasan a tratamiento
-- (MAE_RISK_TREATMENT) en la evaluación Junio 2026 (evaluationId=4).
-- Selección: los 5 riesgos de nivel ALTO + 1 de nivel MEDIO. Los otros 4
-- (2 MEDIO + 2 BAJO) quedan en 'En evaluación'.
-- Reutiliza el mismo nivel de amenaza/vulnerabilidad y CID ya registrados
-- en MAE_RISK_ASSESSMENT para consistencia.
-- N_RESIDUAL_RISK_ID_FK queda NULL: el catálogo MAE_RESIDUAL_RISK solo
-- tiene una categoría (ACEPTAR, rango 1-3) y ningún valor de estos 6
-- riesgos cae en ese rango (igual que haría la app).
-- Actualiza el estado de estos 6 riesgos de 'En evaluación' a 'En
-- tratamiento' en MAE_RISK.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_RISK_TREATMENT" (
  "N_RISK_ID_FK", "N_RISK_TREATMENT_METHOD_ID", "C_CONTROL_TYPE", "C_CONTROLS_TO_IMPLEMENT",
  "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID_FK",
  "N_RESIDUAL_RISK_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
VALUES
  (22, 1, 'Preventivo - Administrativo', 'Elaborar y aprobar un registro formal de partes interesadas del SGSI, con sus requisitos identificados y su relación con los controles aplicables.', 2.00, 2.00, 21.32, 3, NULL, 1, 1, now(), false),
  (30, 1, 'Preventivo - Administrativo', 'Elaborar un plan de acción por cada objetivo de seguridad que incluya actividades, recursos necesarios, responsables y plazos de cumplimiento.', 2.00, 2.00, 21.32, 3, NULL, 1, 1, now(), false),
  (37, 1, 'Preventivo - Administrativo', 'Definir y aprobar un esquema formal de clasificación de la información, y aplicarlo a los activos críticos, en particular la Base de Datos del Padrón Electoral.', 2.00, 2.00, 32.00, 3, NULL, 1, 1, now(), false),
  (38, 1, 'Preventivo - Administrativo', 'Definir e implementar un procedimiento de etiquetado de información acorde al esquema de clasificación aprobado.', 1.00, 2.00, 16.00, 2, NULL, 1, 1, now(), false),
  (39, 1, 'Preventivo - Técnico', 'Implementar controles técnicos de transferencia segura de información (cifrado, VPN, canales autorizados) para las comunicaciones con terceros y entre áreas.', 2.00, 2.00, 22.68, 3, NULL, 1, 1, now(), false),
  (40, 1, 'Preventivo - Técnico', 'Implementar una política técnica (GPO o herramienta de gestión de accesos privilegiados) que restrinja el uso de programas de utilidad con privilegios elevados a personal autorizado.', 2.00, 2.00, 29.32, 3, NULL, 1, 1, now(), false);

-- Pasar estos 6 riesgos de 'En evaluación' a 'En tratamiento'
UPDATE "MAE_RISK"
SET "N_RISK_STATUS_ID_FK" = 3, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_RISK_ID_PK" IN (22, 30, 37, 38, 39, 40);

COMMIT;
