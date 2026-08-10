-- =====================================================================
-- Seed: nueva norma 'NTP-ISO/IEC 42001' (MAE_STANDARD)
-- Versión peruana (NTP) de ISO/IEC 42001:2023 - Sistema de gestión de IA.
-- companyId=1 (ONPE), sin norma padre (es una norma raíz, igual que
-- ISO 27001 id=4).
-- Debe ejecutarse ANTES de seed_requirements_ntp42001.sql y
-- seed_controls_ntp42001.sql (ambos resuelven N_STANDARD_ID por
-- C_NAME + N_COMPANY_ID vía subquery, no por PK fijo).
--
-- N_STANDARD_ID_PK NO es una columna identity/serial en esta base
-- (a diferencia de N_REQUIREMENT_ID_PK/N_CONTROL_GROUP_ID_PK/
-- N_CONTROL_ID_PK, que sí lo son — confirmado vía information_schema.
-- columns.is_identity antes de ejecutar). Por eso acá se calcula el
-- siguiente ID a mano (MAX+1) en vez de omitir la columna.
--
-- N_PARENT_ID = 0 (NO NULL) para "sin norma padre": StandardEntity.
-- parentId es un int no-nullable en C#; EF Core lanza
-- InvalidCastException ("Column 'parentId' is null") al materializar
-- una fila con NULL ahí. ISO 27000 (norma raíz real) ya usa 0 con este
-- mismo propósito — confirmado contra la fila real antes de corregir
-- esto (se detectó en vivo: la primera versión de este script insertó
-- NULL y rompió GET /api/standard/{id} para esta norma).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

INSERT INTO "MAE_STANDARD" (
  "N_STANDARD_ID_PK", "C_NAME", "C_DESCRIPTION", "N_PARENT_ID", "N_COMPANY_ID",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  (SELECT COALESCE(MAX("N_STANDARD_ID_PK"), 0) + 1 FROM "MAE_STANDARD"),
  'NTP-ISO/IEC 42001',
  'Tecnología de la información. Inteligencia artificial. Sistema de gestión. Requisitos. Versión peruana (NTP) de la norma ISO/IEC 42001:2023.',
  0, 1,
  1, now(), false;

COMMIT;
