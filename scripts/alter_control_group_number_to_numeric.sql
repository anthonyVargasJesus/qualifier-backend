-- =====================================================================
-- MAE_CONTROL_GROUP.N_NUMBER: INTEGER -> NUMERIC (sin precisión/escala
-- fija). Habilita cargar normas cuyo Anexo A tiene sub-grupos anidados
-- que el propio documento numera con decimales (ej. NTP-ISO/IEC 42001,
-- Anexo A.6 dividido en A.6.1 "Ciclo de vida del sistema de IA" y A.6.2
-- "Datos para sistemas de IA") sin tener que inventar una renumeración
-- artificial (ver ControlGroupEntity.cs / seed_controls_ntp42001.sql).
--
-- Los grupos existentes (ISO 27001: 5, 6, 7...) no cambian de valor —
-- Postgres convierte INTEGER -> NUMERIC sin pérdida y sin agregar
-- ceros decimales (5 sigue siendo 5, no 5.00), porque la columna nueva
-- no tiene escala fija.
--
-- Este proyecto no usa migraciones de EF Core (no hay carpeta
-- Migrations); el esquema se actualiza con scripts manuales como este.
-- Ejecutar manualmente contra Railway/Postgres ANTES de desplegar el
-- backend con el cambio de tipo en ControlGroupEntity.number (de int a
-- decimal) — si se despliega el backend primero, las lecturas seguirán
-- funcionando (el mapeo EF tolera leer un INTEGER en una prop decimal),
-- pero cualquier escritura con número no entero (ej. cargar NTP 42001)
-- fallaría contra la columna vieja hasta correr este script.
-- =====================================================================

BEGIN;

ALTER TABLE "MAE_CONTROL_GROUP"
  ALTER COLUMN "N_NUMBER" TYPE NUMERIC;

COMMIT;
