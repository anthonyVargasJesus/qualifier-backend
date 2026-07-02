-- =====================================================================
-- Update: marca algunos planes de acción de la evaluación 'Junio 2026'
-- (evaluationId=4) como "Completo" o "En progreso", para que el tablero
-- de Avance de Planes de Acción muestre una distribución realista en vez
-- de que los 28 planes aparezcan como "Pendiente".
-- Criterio: los planes con fecha de vencimiento más próxima (corto plazo)
-- se marcan Completo; los de plazo intermedio, En progreso; el resto
-- (plazo más lejano, estructural) queda Pendiente.
-- Resultado esperado: 7 Completo / 7 En progreso / 14 Pendiente.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- Completo (5 Comité SGSI + 1 Oficial de Seguridad de la Información + 1 Oficina de Planeamiento)
UPDATE "MAE_ACTION_PLAN"
SET "N_ACTION_PLAN_STATUS_ID_FK" = 4, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_ACTION_PLAN_ID_PK" IN (38, 39, 44, 45, 40, 50, 30);

-- En progreso (3 Comité SGSI + 1 Especialista en Infraestructura + 1 Oficial de Seguridad
-- de la Información + 1 Oficina de Planeamiento + 1 Oficina de Proyectos / Seguridad)
UPDATE "MAE_ACTION_PLAN"
SET "N_ACTION_PLAN_STATUS_ID_FK" = 2, "D_UPDATE_DATE" = now(), "N_UPDATE_USER_ID" = 1
WHERE "N_ACTION_PLAN_ID_PK" IN (42, 35, 36, 55, 53, 32, 49);

COMMIT;
