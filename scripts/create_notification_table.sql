-- =====================================================================
-- Historial de notificaciones push enviadas (MAE_NOTIFICATION). Cada vez
-- que el backend dispara una notificación (ej. "plan de acción asignado"),
-- queda un registro acá con el usuario, título, cuerpo y fecha — para
-- auditoría/debug. No reemplaza el envío push en sí (FCM), solo lo deja
-- trazado en la base de datos.
--
-- Este proyecto no usa migraciones de EF Core (no hay carpeta Migrations);
-- el esquema se actualiza con scripts manuales como este.
-- Ejecutar manualmente contra Railway/Postgres antes de desplegar el backend.
-- =====================================================================

BEGIN;

CREATE TABLE "MAE_NOTIFICATION" (
  "N_NOTIFICATION_ID_PK" SERIAL PRIMARY KEY,
  "N_USER_ID_FK" INTEGER NOT NULL,
  "C_TITLE" VARCHAR(200) NOT NULL,
  "C_BODY" VARCHAR(1000) NOT NULL,
  "C_TYPE" VARCHAR(100),
  "N_ACTION_PLAN_ID_FK" INTEGER,
  "N_BREACH_ID_FK" INTEGER,
  "N_COMPANY_ID_FK" INTEGER,
  "N_IS_READ" BOOLEAN NOT NULL DEFAULT FALSE,
  "D_CREATION_DATE" TIMESTAMP,
  "D_UPDATE_DATE" TIMESTAMP,
  "N_CREATION_USER_ID" INTEGER,
  "N_UPDATE_USER_ID" INTEGER,
  "N_IS_DELETED" BOOLEAN
);

ALTER TABLE "MAE_NOTIFICATION"
  ADD CONSTRAINT "CST_MAE_NOTIFICATION_USER_FK"
  FOREIGN KEY ("N_USER_ID_FK")
  REFERENCES "MAE_USER" ("N_USER_ID_PK");

ALTER TABLE "MAE_NOTIFICATION"
  ADD CONSTRAINT "CST_MAE_NOTIFICATION_ACTION_PLAN_FK"
  FOREIGN KEY ("N_ACTION_PLAN_ID_FK")
  REFERENCES "MAE_ACTION_PLAN" ("N_ACTION_PLAN_ID_PK");

ALTER TABLE "MAE_NOTIFICATION"
  ADD CONSTRAINT "CST_MAE_NOTIFICATION_BREACH_FK"
  FOREIGN KEY ("N_BREACH_ID_FK")
  REFERENCES "MAE_BREACH" ("N_BREACH_ID_PK");

COMMIT;
