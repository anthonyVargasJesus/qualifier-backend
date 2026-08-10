-- =====================================================================
-- Regresiona a mano un puñado de ítems entre Junio→Julio (en las dos
-- normas) para que la nueva sección "¿En qué mejoramos o empeoramos?"
-- de /gap/compliance-evolution tenga casos reales del lado "Empeoraron"
-- — con el diseño original, julio solo cerraba brechas o las dejaba
-- igual, nunca las empeoraba, así que esa columna siempre salía vacía
-- en el par actual/anterior (Junio→Julio) que muestra el reporte.
--
-- Retrocede 3 ítems por norma de Parcial (10) a No cumple (11) en la
-- evaluación de JULIO — historia: "esto venía avanzando pero hubo un
-- retroceso real" (ej. cambio de personal, hallazgo de auditoría
-- nuevo). Actualiza también la severidad de la brecha correspondiente
-- (Alta -> Crítica) para que quede consistente con el nuevo nivel.
--
-- ISO 27001: requisitos 7, 58 + control 14.
-- NTP 42001: requisitos (id) 145, 152 + controles (id) 152, 143.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- Requisitos que retroceden (Parcial -> No cumple)
UPDATE "MAE_REQUIREMENT_EVALUATION"
SET "N_MATURITY_LEVEL_ID_FK" = 11,
    "N_VALUE" = 1.00,
    "C_JUSTIFICATION" = 'Retroceso respecto a junio: el avance logrado no se sostuvo (cambio de responsable / hallazgo posterior) y el ítem volvió a un estado no conforme.',
    "C_IMPROVEMENT_ACTIONS" = 'Reevaluar la causa del retroceso, reasignar responsable y reforzar el seguimiento antes de la próxima evaluación.'
WHERE "N_REQUIREMENT_EVALUATION_ID_PK" IN (267, 268, 454, 458);

-- Controles que retroceden (Parcial -> No cumple)
UPDATE "MAE_CONTROL_EVALUATION"
SET "N_MATURITY_LEVEL_ID_FK" = 11,
    "N_VALUE" = 1.00,
    "C_JUSTIFICATION" = 'Retroceso respecto a junio: el avance logrado no se sostuvo (cambio de responsable / hallazgo posterior) y el ítem volvió a un estado no conforme.',
    "C_IMPROVEMENT_ACTIONS" = 'Reevaluar la causa del retroceso, reasignar responsable y reforzar el seguimiento antes de la próxima evaluación.'
WHERE "N_CONTROL_EVALUATION_ID_PK" IN (300, 486, 475);

-- Brechas correspondientes: severidad Alta(3) -> Crítica(4), evidencia parcial ya no aplica
UPDATE "MAE_BREACH" b
SET "N_BREACH_SEVERITY_ID_FK" = 4,
    "C_DESCRIPTION" = 'No se ha implementado este punto de manera formal. No existe evidencia de un proceso, política o control operativo vigente.',
    "C_EVIDENCE_DESCRIPTION" = NULL
FROM "MAE_EVALUATION" ev
WHERE b."N_EVALUATION_ID_FK" = ev."N_EVALUATION_ID_PK"
  AND ev."C_DESCRIPTION" = 'Evaluación - Julio 2026'
  AND (
    (b."C_TYPE" = '1' AND b."N_REQUIREMENT_ID_FK" IN (
      SELECT "N_REQUIREMENT_ID_FK" FROM "MAE_REQUIREMENT_EVALUATION" WHERE "N_REQUIREMENT_EVALUATION_ID_PK" IN (267, 268, 454, 458)
    ))
    OR
    (b."C_TYPE" = '2' AND b."N_CONTROL_ID_FK" IN (
      SELECT "N_CONTROL_ID_FK" FROM "MAE_CONTROL_EVALUATION" WHERE "N_CONTROL_EVALUATION_ID_PK" IN (300, 486, 475)
    ))
  );

COMMIT;
