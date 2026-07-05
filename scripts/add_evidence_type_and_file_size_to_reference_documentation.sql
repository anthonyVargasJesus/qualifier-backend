-- =====================================================================
-- Evidencias (MAE_REFERENCE_DOCUMENTATION): soporte para el nuevo diseño
-- de alta rápida (botones "Adjuntar archivo" / "Agregar enlace").
--
-- 1) N_DOCUMENTATION_ID_FK deja de ser obligatorio: el alta rápida ya no
--    pide elegir un "Tipo de documento" del catálogo.
-- 2) C_EVIDENCE_TYPE ('ARCHIVO' | 'ENLACE'): distingue si la evidencia es
--    un archivo subido o un enlace externo, para mostrar el ícono/etiqueta
--    correctos en la tarjeta.
-- 3) N_FILE_SIZE_BYTES: tamaño del archivo subido a Firebase Storage (null
--    para evidencias de tipo enlace).
--
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_REFERENCE_DOCUMENTATION"
  ALTER COLUMN "N_DOCUMENTATION_ID_FK" DROP NOT NULL;

ALTER TABLE "MAE_REFERENCE_DOCUMENTATION"
  ADD COLUMN IF NOT EXISTS "C_EVIDENCE_TYPE" VARCHAR(10);

ALTER TABLE "MAE_REFERENCE_DOCUMENTATION"
  ADD COLUMN IF NOT EXISTS "N_FILE_SIZE_BYTES" BIGINT;

-- Evidencias existentes: como antes solo se podían subir archivos (la url
-- venía de Firebase Storage), se etiquetan como ARCHIVO por defecto.
UPDATE "MAE_REFERENCE_DOCUMENTATION"
SET "C_EVIDENCE_TYPE" = 'ARCHIVO'
WHERE "C_EVIDENCE_TYPE" IS NULL;

COMMIT;
