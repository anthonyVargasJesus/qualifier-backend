# Scripts de base de datos

Scripts SQL manuales para cambios de datos/catálogos que no pasan por un ORM/migración
automática (el proyecto no usa EF Core Migrations). Sirven como registro de qué se corrió,
cuándo y por qué, y como base para replicar el mismo cambio en producción cuando corresponda.

## Convención

- Numerados en orden (`001_`, `002_`, ...), un archivo por cambio lógico.
- Cada script debe poder ejecutarse de forma idempotente o dejar claro en un comentario si no
  lo es (por ejemplo, un `INSERT` que fallaría si se corre dos veces).
- Encabezado con: qué hace, de dónde sale el requerimiento, en qué ambiente se probó, y
  cualquier verificación previa necesaria antes de correrlo en producción.
- Terminar con un `SELECT` de verificación que confirme el resultado.

## Estado de los scripts

| Script | Descripción | Dev | Producción |
|---|---|---|---|
| `001_maturity_levels_cmm_scale.sql` | Migra `MAE_MATURITY_LEVEL` de 4 a 6 niveles (escala CMM) | ✅ Aplicado 2026-08-12 | ⏳ Pendiente |
| `002_maturity_level_is_not_applicable.sql` | Agrega `L_IS_NOT_APPLICABLE`, corrige bug de "aplica"/%cumplimiento que dejó 001 | ✅ Aplicado 2026-08-12 | ⏳ Pendiente |
| `003_gap_evaluation_table_menu_option.sql` | Opción de menú para la pantalla "Análisis GAP (Tabla)" | ✅ Aplicado 2026-08-12 | ⏳ Pendiente |
| `004_action_plan_calendar_menu_option.sql` | Opción de menú para "Plan de Acción (Calendario)" | ✅ Aplicado 2026-08-12 | ⏳ Pendiente |
| `005_rename_gap_menu.sql` | Renombra menú "GAP" → "Evaluación" y opción `/gap/gap-home` → "Cumplimiento" | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `006_rename_action_plan_calendar_to_implementacion.sql` | Renombra `/gap/plan-de-accion-calendario` → "Implementación" (nombre real de la hoja del Excel) | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `007_iso27001_control_descriptions.sql` | Carga las 93 descripciones de controles ISO 27001 (`MAE_CONTROL.C_DESCRIPTION`), extraídas del Excel SoA de ONPE | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `008_remove_gap_evaluation_table_menu_option.sql` | Elimina la opción de menú "Análisis GAP (Tabla)" -- se retiró el componente | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `009_iso27001_requirement_descriptions.sql` | Carga 96 descripciones de cláusulas ISO 27001 (4-10, `MAE_REQUIREMENT.C_DESCRIPTION`), extraídas del documento oficial en español, y corrige una descripción duplicada por error en la fila de la cláusula 4. NTP-ISO/IEC 42001 no necesitó script: sus 11 filas vacías son cláusulas contenedoras sin texto propio en la norma, ya verificado. | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `010_move_implementacion_to_reportes_menu.sql` | Mueve la opción "Implementación" (`/gap/plan-de-accion-calendario`) del menú EVALUACIÓN al menú REPORTES | ✅ Aplicado 2026-08-13 | ⏳ Pendiente |
| `011_soa_test_evaluation_iso27001_nov2025.sql` | Carga una evaluación ISO 27001 (93 controles con Aplicable/Implementado/Madurez/Justificación reales) desde el SoA `data de prueba` que pasó el usuario | ✅ Aplicado 2026-08-13 | ⏳ Pendiente -- ⚠️ revisar `N_RESPONSIBLE_ID_FK` (hardcodeado a la cuenta de dev, id 1) antes de aplicar |
| `012_soa_test_evaluation_referencia_implementacion.sql` | Complementa el 011: carga "Referencia de la implementación del control" (columna que se había omitido) en `C_IMPROVEMENT_ACTIONS` | ✅ Aplicado 2026-08-13 | ⏳ Pendiente (junto con 011) |
