-- =====================================================================
-- Update: unifica la URL de todas las evidencias (MAE_REFERENCE_DOCUMENTATION)
-- a un único archivo real de Firebase Storage, sin tocar el nombre (C_NAME)
-- ni ningún otro campo.
-- Alcance: TODAS las filas de la tabla, sin filtrar por compañía/evaluación.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

UPDATE "MAE_REFERENCE_DOCUMENTATION"
SET
  "C_URL" = 'https://firebasestorage.googleapis.com/v0/b/qualifier-a9a5c.firebasestorage.app/o/evidences%2F1%2Fundefined-1764776143751.pdf?alt=media&token=d3b6fb75-8087-407b-abf3-f78b2a91a64a',
  "D_UPDATE_DATE" = now(),
  "N_UPDATE_USER_ID" = 1;

COMMIT;
