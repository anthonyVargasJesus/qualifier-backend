-- =====================================================================
-- Agrega N_IS_READ a MAE_NOTIFICATION (leído/no leído), para la bandeja de
-- notificaciones en la app. Si ya creaste MAE_NOTIFICATION con
-- create_notification_table.sql ANTES de este cambio, corre este script
-- para agregarle la columna que falta; si la tabla no existe todavía, usa
-- directamente create_notification_table.sql (ya la incluye).
-- =====================================================================

ALTER TABLE "MAE_NOTIFICATION"
  ADD COLUMN IF NOT EXISTS "N_IS_READ" BOOLEAN NOT NULL DEFAULT FALSE;
