-- =====================================================================
-- Asignación de grupos de control y familias de cláusula a usuarios, para
-- que /gap/panel solo muestre lo que le corresponde a cada usuario logeado.
--
-- MAE_USER_CONTROL_GROUP: qué grupos de control (Organizacional, Personas,
-- Físico, Tecnológico) ve un usuario.
-- MAE_USER_REQUIREMENT_FAMILY: qué familias de cláusula (nivel 1 de
-- MAE_REQUIREMENT, ej. "4. Contexto de la organización") ve un usuario.
--
-- Un usuario SIN ninguna fila en estas tablas no ve nada en /gap/panel
-- (se decidió así explícitamente, no "ve todo por defecto").
--
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

CREATE TABLE IF NOT EXISTS "MAE_USER_CONTROL_GROUP" (
  "N_USER_CONTROL_GROUP_ID_PK" SERIAL PRIMARY KEY,
  "N_USER_ID_FK" INTEGER NOT NULL REFERENCES "MAE_USER"("N_USER_ID_PK"),
  "N_CONTROL_GROUP_ID_FK" INTEGER NOT NULL REFERENCES "MAE_CONTROL_GROUP"("N_CONTROL_GROUP_ID_PK"),
  "N_STANDARD_ID_FK" INTEGER NOT NULL,
  "N_COMPANY_ID_FK" INTEGER NOT NULL,
  "D_CREATION_DATE" TIMESTAMP NULL,
  "D_UPDATE_DATE" TIMESTAMP NULL,
  "N_CREATION_USER_ID" INTEGER NULL,
  "N_UPDATE_USER_ID" INTEGER NULL,
  "N_IS_DELETED" BOOLEAN NULL
);

CREATE TABLE IF NOT EXISTS "MAE_USER_REQUIREMENT_FAMILY" (
  "N_USER_REQUIREMENT_FAMILY_ID_PK" SERIAL PRIMARY KEY,
  "N_USER_ID_FK" INTEGER NOT NULL REFERENCES "MAE_USER"("N_USER_ID_PK"),
  "N_REQUIREMENT_ID_FK" INTEGER NOT NULL REFERENCES "MAE_REQUIREMENT"("N_REQUIREMENT_ID_PK"),
  "N_STANDARD_ID_FK" INTEGER NOT NULL,
  "N_COMPANY_ID_FK" INTEGER NOT NULL,
  "D_CREATION_DATE" TIMESTAMP NULL,
  "D_UPDATE_DATE" TIMESTAMP NULL,
  "N_CREATION_USER_ID" INTEGER NULL,
  "N_UPDATE_USER_ID" INTEGER NULL,
  "N_IS_DELETED" BOOLEAN NULL
);

COMMIT;
