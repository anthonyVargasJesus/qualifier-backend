-- =====================================================================
-- Seed: responsables para NTP-ISO/IEC 42001 (MAE_RESPONSIBLE).
-- Norma nueva sin responsables configurados aún — MAE_REQUIREMENT_
-- EVALUATION/MAE_CONTROL_EVALUATION.N_RESPONSIBLE_ID_FK es NOT NULL,
-- así que hacen falta antes de poder seedear las evaluaciones de
-- mayo/junio/julio 2026.
-- 8 roles, asignados por área temática (ver seed_requirement_
-- evaluations_ntp42001.sql / seed_control_evaluations_ntp42001.sql
-- para el mapeo cláusula/grupo -> responsable).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_RESPONSIBLE" (
  "C_NAME", "C_DESCRIPTION", "N_STANDARD_ID", "N_COMPANY_ID",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT v.name, v.description, std.id, 1, 1, now(), false
FROM std, (VALUES
  ('Comité de Gobernanza de IA',              'Responsable del contexto organizacional y la organización interna del sistema de gestión de IA (cláusula 4, Anexo A.3).'),
  ('Dirección Ejecutiva',                     'Responsable del liderazgo y compromiso de la alta dirección (cláusula 5).'),
  ('Oficial de Gobernanza de IA',             'Responsable de la planificación, gestión de riesgos de IA y mejora continua (cláusula 6, 10).'),
  ('Equipo de Desarrollo de IA',               'Responsable del ciclo de vida técnico de los sistemas de IA y de los recursos de apoyo (cláusula 7-8, Anexo A.4, A.6, A.8).'),
  ('Equipo de Datos e IA',                     'Responsable de la gestión de datos usados en los sistemas de IA (Anexo A.7).'),
  ('Oficina de Seguridad de la Información',   'Responsable de la evaluación de desempeño, auditoría y evaluación de impactos (cláusula 9, Anexo A.5).'),
  ('Área Legal y Cumplimiento',                'Responsable de políticas de IA e información a partes interesadas (Anexo A.2, A.9).'),
  ('Gestión de Proveedores y Terceros',        'Responsable de las relaciones con terceros y proveedores de IA (Anexo A.10).')
) AS v(name, description);

COMMIT;
