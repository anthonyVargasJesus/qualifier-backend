-- =====================================================================
-- Seed: inventario de activos de TI (MAE_ACTIVES_INVENTORY) y catálogos
-- de soporte (macroproceso/subproceso, tipos de activo, propietario,
-- custodio, clasificación de uso, tipo de soporte, ubicación).
-- El único activo existente ("HGJH") pertenece a un macroproceso de
-- abastecimiento genérico de prueba; este script agrega un macroproceso
-- de TI y 12 activos reales alineados al alcance del SGSI (infraestructura
-- tecnológica de la sede central de ONPE).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- 1) Nuevos tipos de activo (categorías ISO 27005 Anexo B)
WITH new_active_types AS (
  INSERT INTO "MAE_ACTIVE_TYPE" ("C_NAME", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Información', 1, 1, now(), false), ('Software', 1, 1, now(), false), ('Hardware', 1, 1, now(), false), ('Servicio', 1, 1, now(), false)
  RETURNING "N_ACTIVE_TYPE_ID_PK", "C_NAME"
),
active_type_lookup AS (
  SELECT "N_ACTIVE_TYPE_ID_PK", "C_NAME" FROM new_active_types
  UNION ALL
  SELECT "N_ACTIVE_TYPE_ID_PK", "C_NAME" FROM "MAE_ACTIVE_TYPE" WHERE "C_NAME" = 'Personal'
),

-- 2) Nuevo macroproceso y subproceso de TI
new_macroprocess AS (
  INSERT INTO "MAE_MACROPROCESS" ("C_CODE", "C_NAME", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('S.05', 'Gestión de Tecnologías de la Información', 1, 1, now(), false)
  RETURNING "N_MACROPROCESS_ID_PK", "C_NAME"
),
new_subprocess AS (
  INSERT INTO "MAE_SUBPROCESS" ("C_CODE", "C_NAME", "N_MACROPROCESS_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  SELECT 'S.05.01', 'Gestión de Infraestructura y Seguridad TI', "N_MACROPROCESS_ID_PK", 1, 1, now(), false FROM new_macroprocess
  RETURNING "N_SUBPROCESS_ID_PK", "C_NAME"
),

-- 3) Nuevo propietario y custodio
new_owner AS (
  INSERT INTO "MAE_OWNER" ("C_NAME", "C_CODE", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Dirección TI', 'DTI', 1, 1, now(), false)
  RETURNING "N_OWNER_ID_PK", "C_NAME"
),
new_custodian AS (
  INSERT INTO "MAE_CUSTODIAN" ("C_NAME", "C_CODE", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Especialista en Infraestructura', 'EI', 1, 1, now(), false)
  RETURNING "N_CUSTODIAN_ID_PK", "C_NAME"
),

-- 4) Nuevas clasificaciones de uso y tipos de soporte
new_usage_classifications AS (
  INSERT INTO "MAE_USAGE_CLASSIFICATION" ("C_NAME", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Confidencial', 1, 1, now(), false), ('Restringido', 1, 1, now(), false)
  RETURNING "N_USAGE_CLASSIFICATION_ID_PK", "C_NAME"
),
usage_classification_lookup AS (
  SELECT "N_USAGE_CLASSIFICATION_ID_PK", "C_NAME" FROM new_usage_classifications
  UNION ALL
  SELECT "N_USAGE_CLASSIFICATION_ID_PK", "C_NAME" FROM "MAE_USAGE_CLASSIFICATION" WHERE "C_NAME" = 'Interno'
),
new_support_types AS (
  INSERT INTO "MAE_SUPPORT_TYPE" ("C_NAME", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('Digital', 1, 1, now(), false), ('Lógico', 1, 1, now(), false)
  RETURNING "N_SUPPORT_TYPE_ID_PK", "C_NAME"
),
support_type_lookup AS (
  SELECT "N_SUPPORT_TYPE_ID_PK", "C_NAME" FROM new_support_types
  UNION ALL
  SELECT "N_SUPPORT_TYPE_ID_PK", "C_NAME" FROM "MAE_SUPPORT_TYPE" WHERE "C_NAME" = 'Físico'
),

-- 5) Nueva ubicación
new_location AS (
  INSERT INTO "MAE_LOCATION" ("C_ABBREVIATION", "C_NAME", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES ('CD', 'Sede Central - Centro de Datos', 1, 1, now(), false)
  RETURNING "N_LOCATION_ID_PK"
)

-- 6) Los 12 activos de TI
INSERT INTO "MAE_ACTIVES_INVENTORY" (
  "C_NUMBER", "N_MACROPROCESS_ID_FK", "N_SUBPROCESS_ID_FK", "N_ACTIVE_TYPE_ID_FK", "C_NAME", "C_DESCRIPTION",
  "N_OWNER_ID_FK", "N_CUSTODIAN_ID_FK", "N_USAGE_CLASSIFICATION_ID_FK", "N_SUPPORT_TYPE_ID_FK", "N_LOCATION_ID_FK",
  "N_VALUATION", "N_COMPANY_ID_FK", "N_STANDARD_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "L_IS_DELETED"
)
SELECT a.number, mp."N_MACROPROCESS_ID_PK", sp."N_SUBPROCESS_ID_PK", at."N_ACTIVE_TYPE_ID_PK", a.name, a.description,
       ow."N_OWNER_ID_PK", cu."N_CUSTODIAN_ID_PK", uc."N_USAGE_CLASSIFICATION_ID_PK", st."N_SUPPORT_TYPE_ID_PK", loc."N_LOCATION_ID_PK",
       a.valuation, 1, 4, 1, now(), false
FROM (VALUES
    ('002', 'Información', 'Sistema de Gestión Documental del SGSI', 'Repositorio digital de políticas, procedimientos y registros del SGSI.', 'Confidencial', 'Digital', 5),
    ('003', 'Información', 'Base de Datos del Padrón Electoral', 'Base de datos con información de electores utilizada en los procesos electorales.', 'Confidencial', 'Digital', 5),
    ('004', 'Hardware', 'Servidor de Aplicaciones', 'Servidor que aloja las aplicaciones institucionales de ONPE.', 'Interno', 'Físico', 4),
    ('005', 'Hardware', 'Servidor de Base de Datos', 'Servidor de bases de datos que soporta los sistemas de información de la institución.', 'Confidencial', 'Físico', 5),
    ('006', 'Servicio', 'Red Corporativa (LAN/WAN)', 'Infraestructura de red de la sede central, incluyendo enlaces LAN y WAN.', 'Interno', 'Lógico', 4),
    ('007', 'Software', 'Sistema de Correo Institucional', 'Plataforma de correo electrónico utilizada para comunicación interna y externa.', 'Interno', 'Digital', 3),
    ('008', 'Hardware', 'Estaciones de Trabajo del Personal', 'Equipos de cómputo asignados al personal de la sede central.', 'Interno', 'Físico', 3),
    ('009', 'Software', 'Sistema de Control de Accesos e Identidad', 'Plataforma de gestión de identidades y control de accesos a los sistemas.', 'Restringido', 'Digital', 5),
    ('010', 'Información', 'Copias de Seguridad (Backups)', 'Respaldos periódicos de la información crítica de los sistemas institucionales.', 'Confidencial', 'Digital', 5),
    ('011', 'Información', 'Documentación del SGSI (Políticas y Procedimientos)', 'Conjunto de políticas, procedimientos, planes y registros documentados del SGSI.', 'Interno', 'Digital', 4),
    ('012', 'Personal', 'Personal de la Oficina de Seguridad de la Información', 'Personal responsable de la operación y mantenimiento del SGSI.', 'Interno', 'Físico', 4),
    ('013', 'Servicio', 'Servicios de Terceros / Proveedores TI', 'Servicios tecnológicos prestados por proveedores externos a la institución.', 'Interno', 'Lógico', 3)
) AS a(number, type_name, name, description, usage_name, support_name, valuation)
CROSS JOIN new_macroprocess mp
CROSS JOIN new_subprocess sp
CROSS JOIN new_owner ow
CROSS JOIN new_custodian cu
CROSS JOIN new_location loc
JOIN active_type_lookup at ON at."C_NAME" = a.type_name
JOIN usage_classification_lookup uc ON uc."C_NAME" = a.usage_name
JOIN support_type_lookup st ON st."C_NAME" = a.support_name;

COMMIT;
