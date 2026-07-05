-- =====================================================================
-- Agrega "Cumple", "Parcial" y "No cumple" como niveles de madurez
-- adicionales (catálogo simplificado tipo gap analysis), para standardId=4
-- (ISO 27001), companyId=1 (ONPE) — ajustar esos dos valores si aplica a
-- otro estándar/empresa.
--
-- OJO — dos cosas a tener en cuenta antes de correr esto:
-- 1) "No aplica" NO se inserta aquí: ya existe como fila
--    N_MATURITY_LEVEL_ID_PK=7 (C_ABBREVIATION='NA', N_VALUE=0.00) en tu
--    catálogo actual. Insertar una segunda "No aplica" duplicaría el
--    concepto con dos IDs distintos en el mismo dropdown.
-- 2) Esto SOLO agrega filas al catálogo (aparecen como chips seleccionables
--    en la pantalla de evaluación). NO cambia la lógica que hoy deriva
--    "Cumplido/No cumplido/Pendiente" en el backend (que sigue mirando
--    value >= 2 => Cumplido, value 0-1 => No cumplido). Con los valores de
--    abajo, "Parcial" (value=3.00) caería hoy dentro del rango "Cumplido"
--    en el badge de estado — para que "Parcial" se vea como su propia
--    categoría en el badge hace falta el cambio de código que ya habíamos
--    conversado (y revertido) antes.
-- =====================================================================

BEGIN;

INSERT INTO "MAE_MATURITY_LEVEL" (
  "C_NAME", "C_DESCRIPTION", "C_ABBREVIATION", "N_VALUE", "C_COLOR", "N_FACTOR",
  "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
VALUES
  ('Cumple',     'El requisito o control se cumple en su totalidad, con evidencia que lo sustenta.', 'CU', 5.00, '#2E7D32', 1.00, 1, 1, now(), false),
  ('Parcial',    'El requisito o control se cumple de forma parcial; existen brechas pendientes de cerrar.', 'PA', 3.00, '#B8860B', 1.00, 1, 1, now(), false),
  ('No cumple',  'El requisito o control no se cumple.', 'NC', 1.00, '#B71C1C', 1.00, 1, 1, now(), false);

COMMIT;
