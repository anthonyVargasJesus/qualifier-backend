-- =====================================================================
-- Seed: evaluación de 10 de los 20 riesgos registrados en la evaluación
-- 'Análisis de Brechas ISO 27001 - Junio 2026' (evaluationId=4)
-- Carga: MAE_RISK_ASSESSMENT (fórmula confirmada contra un registro
-- existente: valor = CID * nivel de amenaza * nivel de vulnerabilidad;
-- nivel de riesgo según los rangos del catálogo MAE_RISK_LEVEL:
-- BAJO 1-8, MEDIO 9-19, ALTO 20-45), y actualiza el estado de esos 10
-- riesgos de 'Registrado' a 'En evaluación' en MAE_RISK.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_RISK_ASSESSMENT" (
  "N_RISK_ID", "N_VALUATION_CID", "N_MENACE_LEVEL_VALUE", "N_VULNERABILITY_LEVEL_VALUE",
  "C_EXISTING_IMPLEMENTED_CONTROLS", "N_RISK_ASSESSMENT_VALUE", "N_RISK_LEVEL_ID",
  "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
VALUES
  (22, 5.33, 2.00, 2.00, 'No se han implementado controles formales; solo existe una identificación inicial de partes interesadas sin procedimiento documentado.', 21.32, 3, 1, 1, now(), false),
  (25, 5.33, 1.00, 2.00, 'Existe una política de seguridad aprobada que sirve de referencia, pero los objetivos no están formalmente vinculados a ella.', 10.66, 2, 1, 1, now(), false),
  (26, 5.33, 1.00, 1.00, 'Los objetivos de seguridad están planteados y se revisan de forma informal en el Comité SGSI.', 5.33, 1, 1, 1, now(), false),
  (30, 5.33, 2.00, 2.00, 'No existen controles de planificación (recursos, responsables, plazos) implementados para los objetivos de seguridad.', 21.32, 3, 1, 1, now(), false),
  (35, 4.67, 1.00, 1.00, 'Se realizan comunicaciones puntuales por correo institucional, sin un procedimiento formal de comunicación del SGSI.', 4.67, 1, 1, 1, now(), false),
  (36, 5.33, 1.00, 2.00, 'La seguridad de la información se considera de forma puntual en algunos proyectos, sin checklist obligatorio en la metodología.', 10.66, 2, 1, 1, now(), false),
  (37, 8.00, 2.00, 2.00, 'No existe un esquema de clasificación de información formalmente aprobado; solo se ha iniciado la clasificación de información crítica.', 32.00, 3, 1, 1, now(), false),
  (38, 8.00, 1.00, 2.00, 'No se aplica un procedimiento de etiquetado consistente, al depender de un esquema de clasificación aún incompleto.', 16.00, 2, 1, 1, now(), false),
  (39, 5.67, 2.00, 2.00, 'No existen controles de transferencia segura (cifrado, VPN, canales autorizados) implementados de forma consistente.', 22.68, 3, 1, 1, now(), false),
  (40, 7.33, 2.00, 2.00, 'El uso de programas de utilidad con privilegios elevados se realiza de forma manual, sin control técnico centralizado.', 29.32, 3, 1, 1, now(), false);

-- Pasar estos 10 riesgos de 'Registrado' a 'En evaluación'
UPDATE "MAE_RISK"
SET "N_RISK_STATUS_ID_FK" = 2, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_RISK_ID_PK" IN (22, 25, 26, 30, 35, 36, 37, 38, 39, 40);

COMMIT;
