-- =====================================================================
-- Seed: valoración de impacto (Confidencialidad/Integridad/Disponibilidad)
-- para cada uno de los 12 activos de TI creados por seed_actives_inventory.sql
-- (MAE_VALUATION_IN_ACTIVE, catálogo MAE_IMPACT_VALUATION: 1=Confidencialidad,
-- 2=Integridad, 3=Disponibilidad). No incluye el activo de prueba "HGJH",
-- que ya tiene sus 3 valoraciones cargadas.
-- Escala: Disponibilidad se mantiene en el rango declarado en el catálogo
-- (2-4); Confidencialidad/Integridad no tienen rango declarado, se usa una
-- escala de 1 a 10 ponderada según la criticidad real de cada activo.
-- Requiere haber ejecutado antes seed_actives_inventory.sql.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH asset_lookup AS (
  SELECT "N_ACTIVES_INVENTORY_ID_PK", "C_NAME" FROM "MAE_ACTIVES_INVENTORY"
  WHERE "C_NAME" IN ('Sistema de Gestión Documental del SGSI', 'Base de Datos del Padrón Electoral', 'Servidor de Aplicaciones', 'Servidor de Base de Datos', 'Red Corporativa (LAN/WAN)', 'Sistema de Correo Institucional', 'Estaciones de Trabajo del Personal', 'Sistema de Control de Accesos e Identidad', 'Copias de Seguridad (Backups)', 'Documentación del SGSI (Políticas y Procedimientos)', 'Personal de la Oficina de Seguridad de la Información', 'Servicios de Terceros / Proveedores TI')
)

INSERT INTO "MAE_VALUATION_IN_ACTIVE" (
  "N_ACTIVES_INVENTORY_ID", "N_IMPACT_VALUATION_ID", "N_VALUE", "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
SELECT al."N_ACTIVES_INVENTORY_ID_PK", l.impact_valuation_id, l.value, 1, 1, now(), false
FROM (VALUES
    ('Sistema de Gestión Documental del SGSI', 1, 7.00),
    ('Sistema de Gestión Documental del SGSI', 2, 8.00),
    ('Sistema de Gestión Documental del SGSI', 3, 3.00),
    ('Base de Datos del Padrón Electoral', 1, 10.00),
    ('Base de Datos del Padrón Electoral', 2, 10.00),
    ('Base de Datos del Padrón Electoral', 3, 4.00),
    ('Servidor de Aplicaciones', 1, 6.00),
    ('Servidor de Aplicaciones', 2, 8.00),
    ('Servidor de Aplicaciones', 3, 4.00),
    ('Servidor de Base de Datos', 1, 8.00),
    ('Servidor de Base de Datos', 2, 9.00),
    ('Servidor de Base de Datos', 3, 4.00),
    ('Red Corporativa (LAN/WAN)', 1, 6.00),
    ('Red Corporativa (LAN/WAN)', 2, 7.00),
    ('Red Corporativa (LAN/WAN)', 3, 4.00),
    ('Sistema de Correo Institucional', 1, 6.00),
    ('Sistema de Correo Institucional', 2, 5.00),
    ('Sistema de Correo Institucional', 3, 3.00),
    ('Estaciones de Trabajo del Personal', 1, 5.00),
    ('Estaciones de Trabajo del Personal', 2, 4.00),
    ('Estaciones de Trabajo del Personal', 3, 2.00),
    ('Sistema de Control de Accesos e Identidad', 1, 9.00),
    ('Sistema de Control de Accesos e Identidad', 2, 9.00),
    ('Sistema de Control de Accesos e Identidad', 3, 4.00),
    ('Copias de Seguridad (Backups)', 1, 9.00),
    ('Copias de Seguridad (Backups)', 2, 10.00),
    ('Copias de Seguridad (Backups)', 3, 3.00),
    ('Documentación del SGSI (Políticas y Procedimientos)', 1, 6.00),
    ('Documentación del SGSI (Políticas y Procedimientos)', 2, 7.00),
    ('Documentación del SGSI (Políticas y Procedimientos)', 3, 3.00),
    ('Personal de la Oficina de Seguridad de la Información', 1, 7.00),
    ('Personal de la Oficina de Seguridad de la Información', 2, 6.00),
    ('Personal de la Oficina de Seguridad de la Información', 3, 3.00),
    ('Servicios de Terceros / Proveedores TI', 1, 6.00),
    ('Servicios de Terceros / Proveedores TI', 2, 6.00),
    ('Servicios de Terceros / Proveedores TI', 3, 3.00)
) AS l(asset_name, impact_valuation_id, value)
JOIN asset_lookup al ON al."C_NAME" = l.asset_name;

COMMIT;
