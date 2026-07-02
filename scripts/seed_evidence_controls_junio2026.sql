-- =====================================================================
-- Seed: una evidencia por control evaluado en 'Análisis de Brechas ISO 27001
-- - Junio 2026' (evaluationId=4)
-- Carga: MAE_DOCUMENTATION (nuevos documentos para temas del Anexo A sin
--        catálogo previo), MAE_REFERENCE_DOCUMENTATION (93 evidencias, una
--        por control_evaluation)
-- URL: se reutiliza la misma URL real de Firebase Storage que ya se dejó en
-- todas las evidencias existentes, en vez de un dominio ficticio.
-- Requiere que ya existan las 93 filas en MAE_CONTROL_EVALUATION para
-- evaluationId=4 (creadas por seed_control_evaluation_junio2026.sql).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- 1) Documentos nuevos para temas del Anexo A sin entrada previa en MAE_DOCUMENTATION
WITH new_docs AS (
  INSERT INTO "MAE_DOCUMENTATION" ("C_NAME", "N_DOCUMENT_TYPE_ID", "N_STANDARD_ID", "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED")
  VALUES
    ('Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 2, 4, 1, 1, now(), false),
    ('Metodología de Seguridad en Gestión de Proyectos', 2, 4, 1, 1, now(), false),
    ('Inventario y Gestión de Activos de Información', 2, 4, 1, 1, now(), false),
    ('Esquema de Clasificación y Etiquetado de la Información', 1, 4, 1, 1, now(), false),
    ('Política de Control de Accesos', 1, 4, 1, 1, now(), false),
    ('Política de Seguridad para Proveedores', 1, 4, 1, 1, now(), false),
    ('Procedimiento de Gestión de Incidentes de Seguridad', 2, 4, 1, 1, now(), false),
    ('Plan de Continuidad de TIC', 2, 4, 1, 1, now(), false),
    ('Registro de Requisitos Legales y Contractuales', 2, 4, 1, 1, now(), false),
    ('Procedimiento de Selección de Personal', 2, 4, 1, 1, now(), false),
    ('Acuerdo de Confidencialidad y Procedimiento de Terminación', 2, 4, 1, 1, now(), false),
    ('Política de Trabajo Remoto', 1, 4, 1, 1, now(), false),
    ('Procedimiento de Seguridad Física Perimetral', 2, 4, 1, 1, now(), false),
    ('Procedimiento de Monitoreo de Seguridad Física', 2, 4, 1, 1, now(), false),
    ('Evaluación de Riesgos Físicos y Ambientales', 2, 4, 1, 1, now(), false),
    ('Procedimiento de Protección de Equipos y Medios de Almacenamiento', 2, 4, 1, 1, now(), false),
    ('Plan de Mantenimiento de Utilidades y Cableado', 2, 4, 1, 1, now(), false),
    ('Procedimiento de Eliminación Segura de Equipos', 2, 4, 1, 1, now(), false),
    ('Política de Gestión de Accesos Privilegiados', 1, 4, 1, 1, now(), false),
    ('Política de Autenticación Segura', 1, 4, 1, 1, now(), false),
    ('Procedimiento de Gestión de Vulnerabilidades y Antimalware', 2, 4, 1, 1, now(), false),
    ('Política de Copias de Seguridad y Protección de Datos', 1, 4, 1, 1, now(), false),
    ('Procedimiento de Registro y Monitoreo de Eventos', 2, 4, 1, 1, now(), false),
    ('Política de Control de Software e Instalaciones', 1, 4, 1, 1, now(), false),
    ('Política de Seguridad de Redes', 1, 4, 1, 1, now(), false),
    ('Política de Uso de Criptografía', 1, 4, 1, 1, now(), false),
    ('Ciclo de Vida de Desarrollo Seguro (SDLC)', 2, 4, 1, 1, now(), false),
    ('Procedimiento de Gestión de Cambios y Separación de Entornos', 2, 4, 1, 1, now(), false)
  RETURNING "N_DOCUMENTATION_ID_PK", "C_NAME"
),

-- 2) Lookup unificado: documentos nuevos + documentos ya existentes en el catálogo
doc_lookup AS (
  SELECT "N_DOCUMENTATION_ID_PK", "C_NAME" FROM new_docs
  UNION ALL
  SELECT "N_DOCUMENTATION_ID_PK", "C_NAME" FROM "MAE_DOCUMENTATION"
  WHERE "C_NAME" IN ('Roles y Responsabilidades en Seguridad de la Información', 'Matriz de Segregación de Funciones', 'Programa de Auditoría Interna del SGSI', 'Programa de Concientización en Seguridad de la Información')
),

-- 3) Lookup de control_evaluation ya cargados para evaluationId=4
control_eval_lookup AS (
  SELECT "N_CONTROL_EVALUATION_ID_PK", "N_CONTROL_ID_FK" FROM "MAE_CONTROL_EVALUATION" WHERE "N_EVALUATION_ID_FK"=4
)

-- 4) Una evidencia por control evaluado
INSERT INTO "MAE_REFERENCE_DOCUMENTATION" (
  "C_NAME", "C_DESCRIPTION", "N_DOCUMENTATION_ID_FK", "N_CONTROL_EVALUATION_ID_FK",
  "N_EVALUATION_ID_FK", "N_COMPANY_ID_FK", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED", "C_URL"
)
SELECT l.evidence_name, l.evidence_desc, dl."N_DOCUMENTATION_ID_PK", cel."N_CONTROL_EVALUATION_ID_PK",
       4, 1, 1, now(), false, 'https://firebasestorage.googleapis.com/v0/b/qualifier-a9a5c.firebasestorage.app/o/evidences%2F1%2Fundefined-1764776143751.pdf?alt=media&token=d3b6fb75-8087-407b-abf3-f78b2a91a64a'
FROM (VALUES
    (1, 'Procedimiento de Selección de Personal', 'Evidencia - Procedimiento de Selección de Personal', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Selección de Personal.'),
    (2, 'Roles y Responsabilidades en Seguridad de la Información', 'Evidencia - Roles y Responsabilidades en Seguridad de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Roles y Responsabilidades en Seguridad de la Información.'),
    (3, 'Roles y Responsabilidades en Seguridad de la Información', 'Evidencia - Roles y Responsabilidades en Seguridad de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Roles y Responsabilidades en Seguridad de la Información.'),
    (4, 'Matriz de Segregación de Funciones', 'Evidencia - Matriz de Segregación de Funciones', 'Evidencia documental que sustenta la evaluación del control, referida a: Matriz de Segregación de Funciones.'),
    (5, 'Matriz de Segregación de Funciones', 'Evidencia - Matriz de Segregación de Funciones', 'Evidencia documental que sustenta la evaluación del control, referida a: Matriz de Segregación de Funciones.'),
    (6, 'Procedimiento de Selección de Personal', 'Evidencia - Procedimiento de Selección de Personal', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Selección de Personal.'),
    (7, 'Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia - Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas.'),
    (8, 'Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia - Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas.'),
    (9, 'Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia - Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Contacto con Autoridades e Inteligencia de Amenazas.'),
    (10, 'Metodología de Seguridad en Gestión de Proyectos', 'Evidencia - Metodología de Seguridad en Gestión de Proyectos', 'Evidencia documental que sustenta la evaluación del control, referida a: Metodología de Seguridad en Gestión de Proyectos.'),
    (11, 'Inventario y Gestión de Activos de Información', 'Evidencia - Inventario y Gestión de Activos de Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Inventario y Gestión de Activos de Información.'),
    (12, 'Inventario y Gestión de Activos de Información', 'Evidencia - Inventario y Gestión de Activos de Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Inventario y Gestión de Activos de Información.'),
    (13, 'Inventario y Gestión de Activos de Información', 'Evidencia - Inventario y Gestión de Activos de Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Inventario y Gestión de Activos de Información.'),
    (14, 'Esquema de Clasificación y Etiquetado de la Información', 'Evidencia - Esquema de Clasificación y Etiquetado de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Esquema de Clasificación y Etiquetado de la Información.'),
    (15, 'Esquema de Clasificación y Etiquetado de la Información', 'Evidencia - Esquema de Clasificación y Etiquetado de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Esquema de Clasificación y Etiquetado de la Información.'),
    (16, 'Esquema de Clasificación y Etiquetado de la Información', 'Evidencia - Esquema de Clasificación y Etiquetado de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Esquema de Clasificación y Etiquetado de la Información.'),
    (17, 'Política de Control de Accesos', 'Evidencia - Política de Control de Accesos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Accesos.'),
    (18, 'Política de Control de Accesos', 'Evidencia - Política de Control de Accesos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Accesos.'),
    (19, 'Política de Control de Accesos', 'Evidencia - Política de Control de Accesos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Accesos.'),
    (20, 'Política de Control de Accesos', 'Evidencia - Política de Control de Accesos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Accesos.'),
    (21, 'Política de Seguridad para Proveedores', 'Evidencia - Política de Seguridad para Proveedores', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad para Proveedores.'),
    (22, 'Política de Seguridad para Proveedores', 'Evidencia - Política de Seguridad para Proveedores', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad para Proveedores.'),
    (23, 'Política de Seguridad para Proveedores', 'Evidencia - Política de Seguridad para Proveedores', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad para Proveedores.'),
    (24, 'Política de Seguridad para Proveedores', 'Evidencia - Política de Seguridad para Proveedores', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad para Proveedores.'),
    (25, 'Política de Seguridad para Proveedores', 'Evidencia - Política de Seguridad para Proveedores', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad para Proveedores.'),
    (26, 'Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia - Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Incidentes de Seguridad.'),
    (27, 'Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia - Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Incidentes de Seguridad.'),
    (28, 'Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia - Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Incidentes de Seguridad.'),
    (29, 'Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia - Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Incidentes de Seguridad.'),
    (30, 'Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia - Procedimiento de Gestión de Incidentes de Seguridad', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Incidentes de Seguridad.'),
    (31, 'Plan de Continuidad de TIC', 'Evidencia - Plan de Continuidad de TIC', 'Evidencia documental que sustenta la evaluación del control, referida a: Plan de Continuidad de TIC.'),
    (32, 'Plan de Continuidad de TIC', 'Evidencia - Plan de Continuidad de TIC', 'Evidencia documental que sustenta la evaluación del control, referida a: Plan de Continuidad de TIC.'),
    (33, 'Registro de Requisitos Legales y Contractuales', 'Evidencia - Registro de Requisitos Legales y Contractuales', 'Evidencia documental que sustenta la evaluación del control, referida a: Registro de Requisitos Legales y Contractuales.'),
    (34, 'Registro de Requisitos Legales y Contractuales', 'Evidencia - Registro de Requisitos Legales y Contractuales', 'Evidencia documental que sustenta la evaluación del control, referida a: Registro de Requisitos Legales y Contractuales.'),
    (35, 'Registro de Requisitos Legales y Contractuales', 'Evidencia - Registro de Requisitos Legales y Contractuales', 'Evidencia documental que sustenta la evaluación del control, referida a: Registro de Requisitos Legales y Contractuales.'),
    (36, 'Registro de Requisitos Legales y Contractuales', 'Evidencia - Registro de Requisitos Legales y Contractuales', 'Evidencia documental que sustenta la evaluación del control, referida a: Registro de Requisitos Legales y Contractuales.'),
    (37, 'Programa de Auditoría Interna del SGSI', 'Evidencia - Programa de Auditoría Interna del SGSI', 'Evidencia documental que sustenta la evaluación del control, referida a: Programa de Auditoría Interna del SGSI.'),
    (38, 'Programa de Auditoría Interna del SGSI', 'Evidencia - Programa de Auditoría Interna del SGSI', 'Evidencia documental que sustenta la evaluación del control, referida a: Programa de Auditoría Interna del SGSI.'),
    (39, 'Programa de Auditoría Interna del SGSI', 'Evidencia - Programa de Auditoría Interna del SGSI', 'Evidencia documental que sustenta la evaluación del control, referida a: Programa de Auditoría Interna del SGSI.'),
    (40, 'Programa de Concientización en Seguridad de la Información', 'Evidencia - Programa de Concientización en Seguridad de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Programa de Concientización en Seguridad de la Información.'),
    (41, 'Programa de Concientización en Seguridad de la Información', 'Evidencia - Programa de Concientización en Seguridad de la Información', 'Evidencia documental que sustenta la evaluación del control, referida a: Programa de Concientización en Seguridad de la Información.'),
    (42, 'Acuerdo de Confidencialidad y Procedimiento de Terminación', 'Evidencia - Acuerdo de Confidencialidad y Procedimiento de Terminación', 'Evidencia documental que sustenta la evaluación del control, referida a: Acuerdo de Confidencialidad y Procedimiento de Terminación.'),
    (43, 'Acuerdo de Confidencialidad y Procedimiento de Terminación', 'Evidencia - Acuerdo de Confidencialidad y Procedimiento de Terminación', 'Evidencia documental que sustenta la evaluación del control, referida a: Acuerdo de Confidencialidad y Procedimiento de Terminación.'),
    (44, 'Política de Trabajo Remoto', 'Evidencia - Política de Trabajo Remoto', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Trabajo Remoto.'),
    (45, 'Política de Trabajo Remoto', 'Evidencia - Política de Trabajo Remoto', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Trabajo Remoto.'),
    (46, 'Procedimiento de Seguridad Física Perimetral', 'Evidencia - Procedimiento de Seguridad Física Perimetral', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Seguridad Física Perimetral.'),
    (47, 'Procedimiento de Seguridad Física Perimetral', 'Evidencia - Procedimiento de Seguridad Física Perimetral', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Seguridad Física Perimetral.'),
    (48, 'Procedimiento de Monitoreo de Seguridad Física', 'Evidencia - Procedimiento de Monitoreo de Seguridad Física', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Monitoreo de Seguridad Física.'),
    (49, 'Procedimiento de Monitoreo de Seguridad Física', 'Evidencia - Procedimiento de Monitoreo de Seguridad Física', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Monitoreo de Seguridad Física.'),
    (50, 'Evaluación de Riesgos Físicos y Ambientales', 'Evidencia - Evaluación de Riesgos Físicos y Ambientales', 'Evidencia documental que sustenta la evaluación del control, referida a: Evaluación de Riesgos Físicos y Ambientales.'),
    (51, 'Evaluación de Riesgos Físicos y Ambientales', 'Evidencia - Evaluación de Riesgos Físicos y Ambientales', 'Evidencia documental que sustenta la evaluación del control, referida a: Evaluación de Riesgos Físicos y Ambientales.'),
    (52, 'Evaluación de Riesgos Físicos y Ambientales', 'Evidencia - Evaluación de Riesgos Físicos y Ambientales', 'Evidencia documental que sustenta la evaluación del control, referida a: Evaluación de Riesgos Físicos y Ambientales.'),
    (53, 'Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia - Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Protección de Equipos y Medios de Almacenamiento.'),
    (54, 'Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia - Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Protección de Equipos y Medios de Almacenamiento.'),
    (55, 'Plan de Mantenimiento de Utilidades y Cableado', 'Evidencia - Plan de Mantenimiento de Utilidades y Cableado', 'Evidencia documental que sustenta la evaluación del control, referida a: Plan de Mantenimiento de Utilidades y Cableado.'),
    (56, 'Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia - Procedimiento de Protección de Equipos y Medios de Almacenamiento', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Protección de Equipos y Medios de Almacenamiento.'),
    (57, 'Plan de Mantenimiento de Utilidades y Cableado', 'Evidencia - Plan de Mantenimiento de Utilidades y Cableado', 'Evidencia documental que sustenta la evaluación del control, referida a: Plan de Mantenimiento de Utilidades y Cableado.'),
    (58, 'Procedimiento de Eliminación Segura de Equipos', 'Evidencia - Procedimiento de Eliminación Segura de Equipos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Eliminación Segura de Equipos.'),
    (59, 'Procedimiento de Eliminación Segura de Equipos', 'Evidencia - Procedimiento de Eliminación Segura de Equipos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Eliminación Segura de Equipos.'),
    (60, 'Política de Gestión de Accesos Privilegiados', 'Evidencia - Política de Gestión de Accesos Privilegiados', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Gestión de Accesos Privilegiados.'),
    (61, 'Política de Gestión de Accesos Privilegiados', 'Evidencia - Política de Gestión de Accesos Privilegiados', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Gestión de Accesos Privilegiados.'),
    (62, 'Política de Gestión de Accesos Privilegiados', 'Evidencia - Política de Gestión de Accesos Privilegiados', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Gestión de Accesos Privilegiados.'),
    (63, 'Política de Gestión de Accesos Privilegiados', 'Evidencia - Política de Gestión de Accesos Privilegiados', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Gestión de Accesos Privilegiados.'),
    (64, 'Política de Autenticación Segura', 'Evidencia - Política de Autenticación Segura', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Autenticación Segura.'),
    (65, 'Política de Autenticación Segura', 'Evidencia - Política de Autenticación Segura', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Autenticación Segura.'),
    (66, 'Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia - Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Vulnerabilidades y Antimalware.'),
    (67, 'Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia - Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Vulnerabilidades y Antimalware.'),
    (68, 'Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia - Procedimiento de Gestión de Vulnerabilidades y Antimalware', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Vulnerabilidades y Antimalware.'),
    (69, 'Política de Copias de Seguridad y Protección de Datos', 'Evidencia - Política de Copias de Seguridad y Protección de Datos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Copias de Seguridad y Protección de Datos.'),
    (70, 'Política de Copias de Seguridad y Protección de Datos', 'Evidencia - Política de Copias de Seguridad y Protección de Datos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Copias de Seguridad y Protección de Datos.'),
    (71, 'Política de Copias de Seguridad y Protección de Datos', 'Evidencia - Política de Copias de Seguridad y Protección de Datos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Copias de Seguridad y Protección de Datos.'),
    (72, 'Política de Copias de Seguridad y Protección de Datos', 'Evidencia - Política de Copias de Seguridad y Protección de Datos', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Copias de Seguridad y Protección de Datos.'),
    (73, 'Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia - Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Registro y Monitoreo de Eventos.'),
    (74, 'Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia - Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Registro y Monitoreo de Eventos.'),
    (75, 'Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia - Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Registro y Monitoreo de Eventos.'),
    (76, 'Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia - Procedimiento de Registro y Monitoreo de Eventos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Registro y Monitoreo de Eventos.'),
    (77, 'Política de Control de Software e Instalaciones', 'Evidencia - Política de Control de Software e Instalaciones', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Software e Instalaciones.'),
    (78, 'Política de Control de Software e Instalaciones', 'Evidencia - Política de Control de Software e Instalaciones', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Control de Software e Instalaciones.'),
    (79, 'Política de Seguridad de Redes', 'Evidencia - Política de Seguridad de Redes', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad de Redes.'),
    (80, 'Política de Seguridad de Redes', 'Evidencia - Política de Seguridad de Redes', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad de Redes.'),
    (81, 'Política de Seguridad de Redes', 'Evidencia - Política de Seguridad de Redes', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad de Redes.'),
    (82, 'Política de Seguridad de Redes', 'Evidencia - Política de Seguridad de Redes', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Seguridad de Redes.'),
    (83, 'Política de Uso de Criptografía', 'Evidencia - Política de Uso de Criptografía', 'Evidencia documental que sustenta la evaluación del control, referida a: Política de Uso de Criptografía.'),
    (84, 'Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia - Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia documental que sustenta la evaluación del control, referida a: Ciclo de Vida de Desarrollo Seguro (SDLC).'),
    (85, 'Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia - Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia documental que sustenta la evaluación del control, referida a: Ciclo de Vida de Desarrollo Seguro (SDLC).'),
    (86, 'Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia - Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia documental que sustenta la evaluación del control, referida a: Ciclo de Vida de Desarrollo Seguro (SDLC).'),
    (87, 'Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia - Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia documental que sustenta la evaluación del control, referida a: Ciclo de Vida de Desarrollo Seguro (SDLC).'),
    (88, 'Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia - Ciclo de Vida de Desarrollo Seguro (SDLC)', 'Evidencia documental que sustenta la evaluación del control, referida a: Ciclo de Vida de Desarrollo Seguro (SDLC).'),
    (89, 'Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia - Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Cambios y Separación de Entornos.'),
    (90, 'Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia - Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Cambios y Separación de Entornos.'),
    (91, 'Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia - Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Cambios y Separación de Entornos.'),
    (92, 'Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia - Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Cambios y Separación de Entornos.'),
    (93, 'Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia - Procedimiento de Gestión de Cambios y Separación de Entornos', 'Evidencia documental que sustenta la evaluación del control, referida a: Procedimiento de Gestión de Cambios y Separación de Entornos.')
) AS l(control_id, doc_name, evidence_name, evidence_desc)
JOIN doc_lookup dl ON dl."C_NAME" = l.doc_name
JOIN control_eval_lookup cel ON cel."N_CONTROL_ID_FK" = l.control_id;

COMMIT;
