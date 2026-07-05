-- Agrega la columna para guardar el FCM token (push notifications) de cada
-- usuario. Nullable: no todos los usuarios tendrán la app móvil instalada.
ALTER TABLE "MAE_USER" ADD COLUMN IF NOT EXISTS "C_FCM_TOKEN" character varying(500);
