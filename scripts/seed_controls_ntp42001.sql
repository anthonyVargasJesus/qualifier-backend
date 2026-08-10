-- =====================================================================
-- Seed: grupos de control y controles del Anexo A de NTP-ISO/IEC 42001
-- (MAE_CONTROL_GROUP, MAE_CONTROL). Requiere que
-- seed_standard_ntp42001.sql ya se haya ejecutado (resuelve
-- N_STANDARD_ID por C_NAME, no por PK fijo).
--
-- 9 grupos, PLANOS, con la numeración real del índice del documento
-- (A.1 es solo la introducción del anexo, sin controles propios, no se
-- carga como grupo):
--   A.2  Políticas relacionadas con la IA
--   A.3  Organización interna
--   A.4  Recursos para los sistemas de IA
--   A.5  Evaluación de los impactos de los sistemas de IA
--   A.6  Ciclo de vida del sistema de IA
--   A.7  Datos para sistemas de IA
--   A.8  Información para las partes interesadas de los sistemas de IA
--   A.9  Utilización de sistemas de IA
--   A.10 Relaciones con terceros y clientes
-- 38 controles en total.
--
-- CORREGIDO: una versión anterior de este script asumía que A.6 se
-- subdividía en "A.6.1 Ciclo de vida" / "A.6.2 Datos" y los cargaba
-- como grupos "6.1"/"6.2" (motivo por el que se agregó soporte
-- decimal a MAE_CONTROL_GROUP.N_NUMBER — ver
-- alter_control_group_number_to_numeric.sql). El índice real del
-- documento (confirmado contra la tabla de contenidos) muestra que
-- "Ciclo de vida del sistema de IA" (A.6) y "Datos para sistemas de
-- IA" (A.7) son DOS secciones de primer nivel independientes, cada
-- una con su propia página — no hay anidamiento real. Este script
-- vuelve a usar números enteros simples (2-10) para esta norma; el
-- soporte decimal en el modelo queda disponible para alguna futura
-- norma que sí tenga sub-grupos genuinamente anidados, pero no hace
-- falta para NTP 42001.
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- ---------------------------------------------------------------------
-- Grupos de control (9 filas)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_CONTROL_GROUP" (
  "N_NUMBER", "C_NAME", "C_DESCRIPTION", "N_STANDARD_ID", "N_COMPANY_ID",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT v.number, v.name, v.description, std.id, 1, 1, now(), false
FROM std, (VALUES
  (2,  'Políticas relacionadas con la IA',                          'Anexo A.2 de NTP-ISO/IEC 42001.'),
  (3,  'Organización interna',                                      'Anexo A.3 de NTP-ISO/IEC 42001.'),
  (4,  'Recursos para los sistemas de IA',                          'Anexo A.4 de NTP-ISO/IEC 42001.'),
  (5,  'Evaluación de los impactos de los sistemas de IA',          'Anexo A.5 de NTP-ISO/IEC 42001.'),
  (6,  'Ciclo de vida del sistema de IA',                           'Anexo A.6 de NTP-ISO/IEC 42001.'),
  (7,  'Datos para sistemas de IA',                                 'Anexo A.7 de NTP-ISO/IEC 42001.'),
  (8,  'Información para las partes interesadas de los sistemas de IA', 'Anexo A.8 de NTP-ISO/IEC 42001.'),
  (9,  'Utilización de sistemas de IA',                             'Anexo A.9 de NTP-ISO/IEC 42001.'),
  (10, 'Relaciones con terceros y clientes',                        'Anexo A.10 de NTP-ISO/IEC 42001.')
) AS v(number, name, description);

-- ---------------------------------------------------------------------
-- Controles (38 filas). v(group_number, control_number, name, description)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_CONTROL" (
  "N_NUMBER", "C_NAME", "C_DESCRIPTION", "N_CONTROL_GROUP_ID", "N_STANDARD_ID",
  "N_COMPANY_ID", "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  v.control_number, v.name, v.description,
  (SELECT g."N_CONTROL_GROUP_ID_PK" FROM "MAE_CONTROL_GROUP" g
   WHERE g."N_STANDARD_ID" = std.id AND g."N_NUMBER" = v.group_number),
  std.id, 1, 1, now(), false
FROM std, (VALUES
  -- Grupo 2: Políticas relacionadas con la IA
  (2, 1, 'Política de IA',
      'La organización debe documentar una política para el desarrollo o uso de sistemas de IA.'),
  (2, 2, 'Alineamiento con otras políticas organizacionales',
      'La organización debe determinar dónde otras políticas pueden verse afectadas o aplicarse a los objetivos de la organización con respecto a los sistemas de IA.'),
  (2, 3, 'Revisión de la política de IA',
      'La política de IA debe revisarse a intervalos planificados o adicionalmente según sea necesario para garantizar su idoneidad, adecuación y eficacia continuas.'),
  -- Grupo 3: Organización interna
  (3, 1, 'Roles y responsabilidades de IA',
      'Los roles y responsabilidades de IA deben definirse y asignarse según las necesidades de la organización.'),
  (3, 2, 'Informe de inquietudes',
      'La organización debe definir e implementar un proceso para informar inquietudes sobre el papel de la organización con respecto a un sistema de IA a lo largo de su ciclo de vida.'),
  -- Grupo 4: Recursos para los sistemas de IA
  (4, 1, 'Documentación de recursos',
      'La organización debe identificar y documentar los recursos relevantes necesarios para las actividades en determinadas etapas del ciclo de vida del sistema de IA y otras actividades relacionadas con la IA.'),
  (4, 2, 'Recursos de datos',
      'Como parte de la identificación de recursos, la organización debe documentar información sobre los recursos de datos utilizados para el sistema de IA.'),
  (4, 3, 'Recursos de herramientas',
      'Como parte de la identificación de recursos, la organización debe documentar información sobre los recursos de herramientas utilizados para el sistema de IA.'),
  (4, 4, 'Sistema y recursos de cómputo',
      'Como parte de la identificación de recursos, la organización debe documentar información sobre el sistema y los recursos de cómputo utilizados para el sistema de IA.'),
  (4, 5, 'Recursos humanos',
      'Como parte de la identificación de recursos, la organización debe documentar información sobre los recursos humanos y sus competencias utilizadas en el ciclo de vida del sistema de IA.'),
  -- Grupo 5: Evaluación de los impactos de los sistemas de IA
  (5, 1, 'Proceso de evaluación del impacto del sistema de IA',
      'La organización debe establecer un proceso para evaluar las posibles consecuencias para individuos, grupos de individuos y sociedades que pueden resultar del sistema de IA a lo largo de su ciclo de vida.'),
  (5, 2, 'Documentación de las evaluaciones de impacto',
      'La organización debe documentar los resultados de las evaluaciones de impacto del sistema de IA y conservar los resultados durante un período definido.'),
  (5, 3, 'Evaluación del impacto en individuos o grupos',
      'La organización debe evaluar y documentar los impactos potenciales de los sistemas de IA en individuos o grupos de individuos a lo largo del ciclo de vida del sistema.'),
  (5, 4, 'Evaluación de los impactos sociales',
      'La organización debe evaluar y documentar los posibles impactos sociales de sus sistemas de IA a lo largo de su ciclo de vida.'),
  -- Grupo 6: Ciclo de vida del sistema de IA
  (6, 1, 'Objetivos para el desarrollo responsable',
      'La organización debe identificar y documentar objetivos para guiar el desarrollo responsable de los sistemas de IA, e integrar medidas para lograrlos en el ciclo de vida del desarrollo.'),
  (6, 2, 'Procesos para el diseño y desarrollo responsable',
      'La organización debe definir y documentar los procesos específicos para el diseño y desarrollo responsable del sistema de IA.'),
  (6, 3, 'Requisitos y especificaciones del sistema',
      'La organización debe especificar y documentar los requisitos para nuevos sistemas de IA o mejoras materiales a los sistemas existentes.'),
  (6, 4, 'Documentación del diseño y desarrollo',
      'La organización debe documentar el diseño y desarrollo del sistema de IA en función de los objetivos organizacionales, los requisitos documentados y los criterios de especificación.'),
  (6, 5, 'Verificación y validación del sistema',
      'La organización debe definir y documentar medidas de verificación y validación para el sistema de IA y especificar criterios para su uso.'),
  (6, 6, 'Despliegue del sistema de IA',
      'La organización debe documentar un plan de despliegue y garantizar que se cumplan los requisitos adecuados antes de la implementación.'),
  (6, 7, 'Operación y monitoreo del sistema',
      'La organización debe definir y documentar los elementos necesarios para el funcionamiento continuo del sistema de IA, incluyendo monitoreo del sistema y rendimiento, reparaciones, actualizaciones y soporte.'),
  (6, 8, 'Documentación técnica del sistema',
      'La organización debe determinar qué documentación técnica del sistema de IA se necesita para cada categoría de partes interesadas y proporcionarla en la forma adecuada.'),
  (6, 9, 'Registros de eventos del sistema',
      'La organización debe determinar en qué fases del ciclo de vida del sistema de IA se debe habilitar el mantenimiento de registros de eventos, como mínimo cuando el sistema esté en uso.'),
  -- Grupo 7: Datos para sistemas de IA
  (7, 1, 'Datos para el desarrollo y mejora',
      'La organización debe definir, documentar e implementar procesos de gestión de datos relacionados con el desarrollo de sistemas de IA.'),
  (7, 2, 'Adquisición de datos',
      'La organización debe determinar y documentar los detalles sobre la adquisición y selección de los datos utilizados en los sistemas de IA.'),
  (7, 3, 'Calidad de los datos',
      'La organización debe definir y documentar los requisitos para la calidad de los datos y garantizar que los datos utilizados para desarrollar y operar el sistema de IA cumplan esos requisitos.'),
  (7, 4, 'Procedencia de los datos',
      'La organización debe definir y documentar un proceso para registrar la procedencia de los datos utilizados en sus sistemas de IA durante los ciclos de vida de los datos y del sistema.'),
  (7, 5, 'Preparación de datos',
      'La organización debe definir y documentar sus criterios para seleccionar la preparación de datos y los métodos de preparación de datos que se utilizarán.'),
  -- Grupo 8: Información para las partes interesadas de los sistemas de IA
  (8, 1, 'Documentación e información para usuarios',
      'La organización debe determinar y proporcionar la información necesaria a los usuarios del sistema de IA.'),
  (8, 2, 'Informes externos',
      'La organización debe proporcionar capacidades para que las partes interesadas informen sobre los impactos adversos del sistema.'),
  (8, 3, 'Comunicación de incidentes',
      'La organización debe determinar y documentar un plan para comunicar incidentes a los usuarios del sistema.'),
  (8, 4, 'Información para los interesados',
      'La organización debe determinar y documentar sus obligaciones de reportar información sobre el sistema de IA a las partes interesadas.'),
  -- Grupo 9: Utilización de sistemas de IA
  (9, 1, 'Procesos para el uso responsable',
      'La organización debe definir y documentar los procesos para el uso responsable de los sistemas de IA.'),
  (9, 2, 'Objetivos para el uso responsable',
      'La organización debe identificar y documentar objetivos para guiar el uso responsable de los sistemas de IA.'),
  (9, 3, 'Uso previsto del sistema de IA',
      'La organización debe garantizar que el sistema de IA se utilice de acuerdo con los usos previstos del sistema y la documentación que lo acompaña.'),
  -- Grupo 10: Relaciones con terceros y clientes
  (10, 1, 'Asignar responsabilidades',
      'La organización debe garantizar que las responsabilidades dentro del ciclo de vida de su sistema de IA se distribuyan entre la organización, sus socios, proveedores, clientes y terceros.'),
  (10, 2, 'Proveedores',
      'La organización debe establecer un proceso para garantizar que el uso de servicios, productos o materiales de proveedores se alinee con su enfoque de desarrollo y uso responsable de sistemas de IA.'),
  (10, 3, 'Clientes',
      'La organización debe garantizar que su enfoque responsable para el desarrollo y uso de sistemas de IA tenga en cuenta las expectativas y necesidades de sus clientes.')
) AS v(group_number, control_number, name, description);

COMMIT;
