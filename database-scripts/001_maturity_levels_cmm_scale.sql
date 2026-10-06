-- 001_maturity_levels_cmm_scale.sql
--
-- Reconfigura MAE_MATURITY_LEVEL (company_id = 1) para usar la escala CMM de 6 niveles
-- (No aplicable + No implementado/Inicial/Repetible/Definido/Gestionado/Optimizado, 0-5)
-- en lugar de la escala anterior de 4 niveles (No aplica/No cumple/Parcial/Cumple).
--
-- Origen: FM03-GPP_GC Declaración de aplicabilidad del SGSI (SoA) V01.xlsx, hoja "Métricas madurez".
--
-- Los 4 registros existentes (IDs 9, 10, 11, 12) se actualizan in-place para no romper las
-- evaluaciones ya guardadas que apuntan a esos IDs por FK. Se agregan 3 registros nuevos
-- (Inicial, Repetible, Gestionado) para completar la escala.
--
-- Probado en DB de desarrollo (Railway) el 2026-08-12. Pendiente de aplicar en producción.
--
-- IMPORTANTE antes de correr en producción:
--   1. Confirmar que los IDs 9/10/11/12 en prod correspondan a los mismos registros
--      (Cumple/Parcial/No cumple/No aplica respectivamente) -- verificar con el SELECT
--      del final antes de correr el UPDATE/INSERT.
--   2. Hacer un backup de MAE_MATURITY_LEVEL antes de ejecutar.
--   3. Company_id = 1 está hardcodeado -- ajustar si en producción es otro.

BEGIN;

-- Renombra/reconfigura los 4 niveles existentes para que encajen en la escala CMM de 6 niveles
UPDATE "MAE_MATURITY_LEVEL" SET
  "C_NAME" = 'No aplicable',
  "C_DESCRIPTION" = 'No aplica el control en el proceso analizado u en otros procesos.',
  "C_ABBREVIATION" = 'NA',
  "N_VALUE" = 0.00,
  "C_COLOR" = '#6B7480',
  "N_FACTOR" = 1.00,
  "L_GENERATES_BREACH" = false,
  "N_BREACH_SEVERITY_ID_FK" = NULL,
  "D_UPDATE_DATE" = now()
WHERE "N_MATURITY_LEVEL_ID_PK" = 12;

UPDATE "MAE_MATURITY_LEVEL" SET
  "C_NAME" = 'No implementado',
  "C_DESCRIPTION" = 'No se ha identificado ningún esfuerzo por implementar el control. No hay evidencia de intención, planificación ni acción.',
  "C_ABBREVIATION" = 'NI',
  "N_VALUE" = 0.00,
  "C_COLOR" = '#A8443B',
  "N_FACTOR" = 1.00,
  "L_GENERATES_BREACH" = true,
  "N_BREACH_SEVERITY_ID_FK" = 4, -- Crítica
  "D_UPDATE_DATE" = now()
WHERE "N_MATURITY_LEVEL_ID_PK" = 11;

UPDATE "MAE_MATURITY_LEVEL" SET
  "C_NAME" = 'Definido',
  "C_DESCRIPTION" = 'El control está formalmente documentado, implementado y operativo con roles y responsabilidades definidos y es parte de los procesos institucionales. Se aplica consistentemente por el personal involucrado.',
  "C_ABBREVIATION" = 'DE',
  "N_VALUE" = 3.00,
  "C_COLOR" = '#8FA83E',
  "N_FACTOR" = 1.00,
  "L_GENERATES_BREACH" = true,
  "N_BREACH_SEVERITY_ID_FK" = 1, -- Baja
  "D_UPDATE_DATE" = now()
WHERE "N_MATURITY_LEVEL_ID_PK" = 10;

UPDATE "MAE_MATURITY_LEVEL" SET
  "C_NAME" = 'Optimizado',
  "C_DESCRIPTION" = 'El control está integrado en la cultura institucional. Se mejora continuamente, se automatiza donde es posible, se utiliza para retroalimentar decisiones estratégicas y lecciones aprendidas documentadas.',
  "C_ABBREVIATION" = 'OP',
  "N_VALUE" = 5.00,
  "C_COLOR" = '#2F6F5E',
  "N_FACTOR" = 1.00,
  "L_GENERATES_BREACH" = false,
  "N_BREACH_SEVERITY_ID_FK" = NULL,
  "D_UPDATE_DATE" = now()
WHERE "N_MATURITY_LEVEL_ID_PK" = 9;

-- Agrega los 3 niveles intermedios que faltan
INSERT INTO "MAE_MATURITY_LEVEL"
  ("C_NAME", "C_DESCRIPTION", "C_ABBREVIATION", "N_VALUE", "C_COLOR", "N_COMPANY_ID_FK",
   "D_CREATION_DATE", "N_CREATION_USER_ID", "L_IS_DELETED", "N_FACTOR", "L_GENERATES_BREACH", "N_BREACH_SEVERITY_ID_FK")
VALUES
  ('Inicial',
   'El control se aplica de forma ad-hoc y reactiva o informal. No hay proceso definido ni consistente. Depende de esfuerzos individuales.',
   'IN', 1.00, '#C97A3A', 1, now(), 1, false, 1.00, true, 3), -- Alta
  ('Repetible',
   'Hay un enfoque común emergente. Algunas acciones están documentadas. El control se implementa de forma repetitiva, pero aún sin sistematización.',
   'RE', 2.00, '#D1A62E', 1, now(), 1, false, 1.00, true, 2), -- Media
  ('Gestionado',
   'El control es monitoreado, medido y evaluado regularmente. Hay evidencia de seguimiento, análisis y acciones correctivas cuando se requiere.',
   'GE', 4.00, '#4F8F5B', 1, now(), 1, false, 1.00, false, NULL);

COMMIT;

-- Verificación post-ejecución
SELECT "N_MATURITY_LEVEL_ID_PK", "C_NAME", "N_VALUE", "L_GENERATES_BREACH", "N_BREACH_SEVERITY_ID_FK"
FROM "MAE_MATURITY_LEVEL" ORDER BY "N_VALUE", "C_NAME";
