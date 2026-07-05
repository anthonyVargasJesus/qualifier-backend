-- Diagnóstico de solo lectura: por qué el control 8.18 muestra evidencias en
-- "Mis acciones" pero gap-panel dice que no tiene evaluación guardada.
-- No modifica nada. (Postgres: nombres entre comillas dobles para respetar
-- las mayúsculas exactas con las que EF Core creó las tablas/columnas.)

-- 1) Cuál evaluación está marcada como "actual" ahora mismo
SELECT "N_EVALUATION_ID_PK", "C_DESCRIPTION", "N_IS_CURRENT", "N_STANDARD_ID_FK"
FROM "MAE_EVALUATION"
WHERE "N_IS_CURRENT" = true;

-- 2) El breach del control 8.18 (ajusta el LIKE si tienes varios "8.18")
SELECT "N_BREACH_ID_PK", "N_EVALUATION_ID_FK", "N_STANDARD_ID_FK", "C_TYPE", "N_REQUIREMENT_ID_FK", "N_CONTROL_ID_FK", "C_TITLE"
FROM "MAE_BREACH"
WHERE "C_TITLE" LIKE '%8.18%' OR "C_TITLE" LIKE '%programas de utilidad%';

-- 3) El/los plan(es) de acción de ese breach
SELECT "N_ACTION_PLAN_ID_PK", "N_BREACH_ID_FK", "N_EVALUATION_ID_FK", "N_USER_ID_FK", "C_TITLE"
FROM "MAE_ACTION_PLAN"
WHERE "N_BREACH_ID_FK" IN (SELECT "N_BREACH_ID_PK" FROM "MAE_BREACH" WHERE "C_TITLE" LIKE '%8.18%' OR "C_TITLE" LIKE '%programas de utilidad%');

-- 4) Evaluaciones de control existentes para ese control (control 8.18), en CUALQUIER ciclo
SELECT "N_CONTROL_EVALUATION_ID_PK", "N_EVALUATION_ID_FK", "N_CONTROL_ID_FK", "N_MATURITY_LEVEL_ID_FK"
FROM "MAE_CONTROL_EVALUATION"
WHERE "N_CONTROL_ID_FK" IN (SELECT "N_CONTROL_ID_FK" FROM "MAE_BREACH" WHERE "C_TITLE" LIKE '%8.18%' OR "C_TITLE" LIKE '%programas de utilidad%');

-- 5) Evidencias registradas, y a qué controlEvaluationId/evaluationId apuntan
SELECT "N_REFERENCE_DOCUMENTATION_ID_PK", "N_CONTROL_EVALUATION_ID_FK", "N_REQUIREMENT_EVALUATION_ID_FK", "N_EVALUATION_ID_FK", "C_NAME"
FROM "MAE_REFERENCE_DOCUMENTATION"
WHERE "N_CONTROL_EVALUATION_ID_FK" IN (
    SELECT "N_CONTROL_EVALUATION_ID_PK" FROM "MAE_CONTROL_EVALUATION"
    WHERE "N_CONTROL_ID_FK" IN (SELECT "N_CONTROL_ID_FK" FROM "MAE_BREACH" WHERE "C_TITLE" LIKE '%8.18%' OR "C_TITLE" LIKE '%programas de utilidad%')
);
