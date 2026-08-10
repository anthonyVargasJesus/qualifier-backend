-- =====================================================================
-- Seed: árbol de requisitos (cláusulas 4-10) de NTP-ISO/IEC 42001
-- (MAE_REQUIREMENT). Requiere que seed_standard_ntp42001.sql ya se
-- haya ejecutado (resuelve N_STANDARD_ID por C_NAME, no por PK fijo).
--
-- Estructura (43 filas):
--   Nivel 1 (7):  encabezados de cláusula 4-10.
--   Nivel 2 (24): subcláusulas. 20 son hojas (4.1-4.4, 5.1-5.3, 6.2,
--                 6.3, 7.1-7.4, 8.1-8.4, 9.1, 10.1, 10.2). 4 son
--                 contenedores porque el documento real las subdivide
--                 más (6.1, 7.5, 9.2, 9.3).
--   Nivel 3 (12): hojas reales bajo esos 4 contenedores (6.1.1-6.1.4,
--                 7.5.1-7.5.3, 9.2.1-9.2.2, 9.3.1-9.3.3).
--
-- TODAS las 43 filas llevan N_IS_EVALUABLE=true (incluidos los 7
-- encabezados de nivel 1 y los 4 contenedores de nivel 2 que SÍ tienen
-- hijos) — NO false como tenía la primera versión de este script.
-- Corregido en vivo: GetRequirementsByStandardIdQuery (la pestaña
-- admin "Requisitos") filtra por isEvaluable ANTES de armar el árbol,
-- así que con isEvaluable=false en los contenedores no sobrevivía
-- ningún nodo raíz y la pestaña quedaba vacía sin error visible.
-- Confirmado contra el dato real de ISO 27001 (standardId=4): sus
-- filas de nivel 1/2/3 con hijos también están en true (solo 3
-- cláusulas de referencia sin contenido propio —tipo "Alcance"— están
-- en false, caso que NTP 42001 no tiene). El filtrado real de qué
-- cuenta como "hoja evaluada" en Análisis del GAP no depende de este
-- flag por sí solo sino de si el ítem tiene hijos (ver GapItemsBuilder/
-- StandardEntity) — los 11 contenedores de acá (7 nivel1 + 4 nivel2)
-- nunca van a recibir una evaluación propia, igual que ya pasa con los
-- encabezados de nivel 1 de ISO 27001 hoy (0 evaluaciones registradas
-- para ellos pese a isEvaluable=true).
--
-- N_NUMERATION es el sufijo dentro de su propio nivel (int), no el
-- código completo: numerationToShow se arma en runtime concatenando
-- con el punto los N_NUMERATION de todos los ancestros (StandardEntity.
-- setNumeration), igual que ya hace ISO 27001. Así, la fila nivel-1 de
-- la cláusula 6 tiene N_NUMERATION=6, y su hijo nivel-2 "6.1" tiene
-- N_NUMERATION=1 (que junto al padre compone "6.1"); el nieto nivel-3
-- "6.1.1" tiene N_NUMERATION=1 (compone "6.1.1").
--
-- C_NAME = el enunciado normativo completo de la cláusula ("La
-- organización debe..."), igual que hoy lo tiene ISO 27001 (name no es
-- un título corto sino el requisito en sí).
-- Generado para revisión manual antes de ejecutar contra Railway/Postgres
-- =====================================================================

BEGIN;

-- ---------------------------------------------------------------------
-- Nivel 1: encabezados de cláusula (7 filas, N_IS_EVALUABLE=true)
--
-- N_PARENT_ID = 0 (NO NULL) para "sin padre": igual que en MAE_STANDARD,
-- RequirementEntity.parentId es un int no-nullable en C#; EF Core
-- lanza InvalidCastException ("Column 'parentId' is null") al leer una
-- fila con NULL ahí. Confirmado contra las cláusulas raíz reales de
-- ISO 27001 (standardId=4, nivel 1), que usan 0 (se detectó en vivo: la
-- primera versión de este script insertó NULL y la pestaña "Requisitos"
-- quedaba vacía sin ningún error visible en el frontend).
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_REQUIREMENT" (
  "N_NUMERATION", "C_NAME", "C_DESCRIPTION", "N_STANDARD_ID", "N_LEVEL",
  "N_COMPANY_ID", "N_PARENT_ID", "N_IS_EVALUABLE",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT v.numeration, v.name, NULL, std.id, 1, 1, 0, true, 1, now(), false
FROM std, (VALUES
  (4,  'Contexto de la organización'),
  (5,  'Liderazgo'),
  (6,  'Planificación'),
  (7,  'Apoyo'),
  (8,  'Operación'),
  (9,  'Evaluación del desempeño'),
  (10, 'Mejora')
) AS v(numeration, name);

-- ---------------------------------------------------------------------
-- Nivel 2: subcláusulas (24 filas, N_IS_EVALUABLE=true en todas: 20 hojas + 4 contenedores)
-- v(clause, numeration, evaluable, name, description)
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_REQUIREMENT" (
  "N_NUMERATION", "C_NAME", "C_DESCRIPTION", "N_STANDARD_ID", "N_LEVEL",
  "N_COMPANY_ID", "N_PARENT_ID", "N_IS_EVALUABLE",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  v.numeration, v.name, v.description, std.id, 2, 1,
  (SELECT r1."N_REQUIREMENT_ID_PK" FROM "MAE_REQUIREMENT" r1
   WHERE r1."N_STANDARD_ID" = std.id AND r1."N_LEVEL" = 1 AND r1."N_NUMERATION" = v.clause),
  v.evaluable, 1, now(), false
FROM std, (VALUES
  -- 4. Contexto de la organización
  (4, 1, true,  '4.1 Comprensión de la organización y su contexto',
      'La organización debe determinar las cuestiones externas e internas pertinentes para su propósito y que afecten su capacidad para alcanzar los resultados previstos de su sistema de gestión de IA, incluyendo si el cambio climático es un tema relevante y el propósito previsto de los sistemas de IA que desarrolla, proporciona o utiliza.'),
  (4, 2, true,  '4.2 Comprensión de las necesidades y expectativas de las partes interesadas',
      'La organización debe determinar las partes interesadas pertinentes para el sistema de gestión de IA, los requisitos pertinentes de estas partes interesadas y cuáles de estos requisitos serán abordados a través del sistema de gestión de IA. Las partes interesadas pertinentes pueden tener requisitos relacionados con el cambio climático.'),
  (4, 3, true,  '4.3 Determinación del alcance del sistema de gestión de IA',
      'La organización debe determinar los límites y la aplicabilidad del sistema de gestión de IA para establecer su alcance, considerando las cuestiones externas e internas y los requisitos de las partes interesadas pertinentes. El alcance debe estar disponible como información documentada.'),
  (4, 4, true,  '4.4 Sistema de gestión de IA',
      'La organización debe establecer, implementar, mantener, mejorar continuamente y documentar un sistema de gestión de IA, incluidos los procesos necesarios y sus interacciones, de acuerdo con los requisitos de esta norma.'),
  -- 5. Liderazgo
  (5, 1, true,  '5.1 Liderazgo y compromiso',
      'La alta dirección debe demostrar liderazgo y compromiso con respecto al sistema de gestión de IA, asegurando que se establezcan la política y los objetivos de IA compatibles con la dirección estratégica, la integración de los requisitos del sistema en los procesos de negocio, la disponibilidad de recursos, la comunicación de la importancia de una gestión de IA eficaz, el logro de los resultados previstos y la promoción de la mejora continua.'),
  (5, 2, true,  '5.2 Política de IA',
      'La alta dirección debe establecer una política de IA apropiada para el propósito de la organización, que proporcione un marco de referencia para el establecimiento de los objetivos de IA e incluya el compromiso de cumplir los requisitos aplicables y de mejora continua del sistema de gestión de IA. La política debe estar disponible como información documentada y comunicarse dentro de la organización.'),
  (5, 3, true,  '5.3 Roles, responsabilidades y autoridades en la organización',
      'La alta dirección debe asegurarse de que las responsabilidades y autoridades para los roles pertinentes se asignen y comuniquen en toda la organización, incluyendo la responsabilidad de asegurar la conformidad del sistema de gestión de IA con los requisitos de esta norma y de informar a la alta dirección sobre su desempeño.'),
  -- 6. Planificación
  (6, 1, true, '6.1 Acciones para abordar riesgos y oportunidades', NULL),
  (6, 2, true,  '6.2 Objetivos de IA y planificación para lograrlos',
      'La organización debe establecer objetivos de IA en las funciones y niveles pertinentes, coherentes con la política de IA, medibles cuando sea posible, que tengan en cuenta los requisitos aplicables, sean objeto de seguimiento, se comuniquen y se actualicen según corresponda. Al planificar cómo lograrlos, debe determinar qué se hará, qué recursos se requerirán, quién será responsable, cuándo se finalizará y cómo se evaluarán los resultados.'),
  (6, 3, true,  '6.3 Planificación de los cambios',
      'Cuando la organización determine la necesidad de cambios en el sistema de gestión de IA, estos cambios se deben llevar a cabo de manera planificada.'),
  -- 7. Apoyo
  (7, 1, true,  '7.1 Recursos',
      'La organización debe determinar y proporcionar los recursos necesarios para el establecimiento, implementación, mantenimiento y mejora continua del sistema de gestión de IA.'),
  (7, 2, true,  '7.2 Competencia',
      'La organización debe determinar la competencia necesaria de las personas que realizan, bajo su control, un trabajo que afecte su desempeño de IA, asegurarse de que sean competentes con base en educación, formación o experiencia apropiadas, y tomar acciones para adquirir la competencia necesaria cuando sea aplicable, conservando información documentada como evidencia.'),
  (7, 3, true,  '7.3 Toma de conciencia',
      'Las personas que realicen trabajos bajo el control de la organización deben tomar conciencia de la política de IA, de su contribución a la eficacia del sistema de gestión de IA (incluidos los beneficios de mejorar el desempeño de IA) y de las implicaciones de no cumplir los requisitos del sistema de gestión de IA.'),
  (7, 4, true,  '7.4 Comunicación',
      'La organización debe determinar las comunicaciones internas y externas pertinentes al sistema de gestión de IA, incluyendo qué comunicar, cuándo comunicar, a quién comunicar y cómo comunicar.'),
  (7, 5, true, '7.5 Información documentada', NULL),
  -- 8. Operación
  (8, 1, true,  '8.1 Planificación y control operacional',
      'La organización debe planificar, implementar y controlar los procesos necesarios para cumplir los requisitos y para implementar las acciones determinadas en la planificación, mediante el establecimiento de criterios para los procesos y la implementación del control de dichos procesos, incluyendo los controles relacionados con el desarrollo y el ciclo de vida del sistema de IA. Los procesos, productos o servicios provistos externamente pertinentes al sistema deben controlarse.'),
  (8, 2, true,  '8.2 Evaluación del riesgo de IA',
      'La organización debe realizar evaluaciones del riesgo de IA a intervalos planificados o cuando se propongan o produzcan cambios significativos, conservando información documentada de los resultados de todas las evaluaciones.'),
  (8, 3, true,  '8.3 Tratamiento del riesgo de IA',
      'La organización debe implementar el plan de tratamiento del riesgo de IA y verificar su eficacia. Cuando se identifiquen nuevos riesgos que requieran tratamiento, o cuando las opciones de tratamiento definidas no sean eficaces, se debe repetir el proceso de tratamiento y actualizar el plan, conservando información documentada de los resultados.'),
  (8, 4, true,  '8.4 Evaluación del impacto del sistema de IA',
      'La organización debe realizar evaluaciones de impacto del sistema de IA a intervalos planificados o cuando se propongan o produzcan cambios significativos, conservando información documentada de los resultados de todas las evaluaciones.'),
  -- 9. Evaluación del desempeño
  (9, 1, true,  '9.1 Seguimiento, medición, análisis y evaluación',
      'La organización debe determinar qué necesita seguimiento y medición, los métodos de seguimiento, medición, análisis y evaluación para asegurar resultados válidos, y cuándo se deben realizar y analizar, evaluando el desempeño y la eficacia del sistema de gestión de IA, con información documentada disponible como evidencia de los resultados.'),
  (9, 2, true, '9.2 Auditoría interna', NULL),
  (9, 3, true, '9.3 Revisión por la dirección', NULL),
  -- 10. Mejora
  (10, 1, true, '10.1 Mejora continua',
      'La organización debe mejorar continuamente la conveniencia, adecuación y eficacia del sistema de gestión de IA.'),
  (10, 2, true, '10.2 No conformidad y acción correctiva',
      'Cuando ocurra una no conformidad, la organización debe reaccionar ante ella tomando acciones para controlarla y corregirla, evaluar la necesidad de acciones para eliminar sus causas, implementar las acciones necesarias y revisar su eficacia, haciendo cambios al sistema de gestión de IA si fuera necesario. Información documentada debe estar disponible como evidencia de la naturaleza de las no conformidades, las acciones tomadas y sus resultados.')
) AS v(clause, numeration, evaluable, name, description);

-- ---------------------------------------------------------------------
-- Nivel 3: hojas bajo 6.1, 7.5, 9.2 y 9.3 (12 filas, todas evaluables)
-- v(clause, subclause, numeration, name, description)
-- El padre se resuelve encadenando dos joins (nivel 2 cuyo propio padre
-- de nivel 1 coincide en N_NUMERATION) porque "1" como N_NUMERATION de
-- nivel 2 se repite en cada cláusula (6.1, 7.1, 9.1...) y no alcanza
-- para desambiguar sin mirar también al abuelo.
-- ---------------------------------------------------------------------
WITH std AS (
  SELECT "N_STANDARD_ID_PK" AS id FROM "MAE_STANDARD"
  WHERE "C_NAME" = 'NTP-ISO/IEC 42001' AND "N_COMPANY_ID" = 1
)
INSERT INTO "MAE_REQUIREMENT" (
  "N_NUMERATION", "C_NAME", "C_DESCRIPTION", "N_STANDARD_ID", "N_LEVEL",
  "N_COMPANY_ID", "N_PARENT_ID", "N_IS_EVALUABLE",
  "N_CREATION_USER_ID", "D_CREATION_DATE", "N_IS_DELETED"
)
SELECT
  v.numeration, v.name, v.description, std.id, 3, 1,
  (SELECT r2."N_REQUIREMENT_ID_PK" FROM "MAE_REQUIREMENT" r2
   JOIN "MAE_REQUIREMENT" r1 ON r1."N_REQUIREMENT_ID_PK" = r2."N_PARENT_ID"
   WHERE r2."N_STANDARD_ID" = std.id AND r2."N_LEVEL" = 2 AND r2."N_NUMERATION" = v.subclause
     AND r1."N_LEVEL" = 1 AND r1."N_NUMERATION" = v.clause),
  true, 1, now(), false
FROM std, (VALUES
  -- 6.1 Acciones para abordar riesgos y oportunidades
  (6, 1, 1, '6.1.1 Generalidades',
      'Al planificar el sistema de gestión de IA, la organización debe considerar su contexto y los requisitos de las partes interesadas y determinar los riesgos y oportunidades que es necesario abordar para asegurar que el sistema alcance sus resultados previstos, prevenir o reducir efectos no deseados y lograr la mejora continua, estableciendo y manteniendo criterios de riesgo de IA. La organización debe planificar las acciones para abordarlos, integrarlas en sus procesos y evaluar su eficacia.'),
  (6, 1, 2, '6.1.2 Evaluación del riesgo de IA',
      'La organización debe definir y establecer un proceso de evaluación del riesgo de IA, alineado con la política y los objetivos de IA, que identifique, analice (consecuencias, probabilidad, nivel de riesgo) y evalúe los riesgos de IA (comparación con criterios de riesgo y priorización para tratamiento), produciendo resultados coherentes, válidos y comparables en evaluaciones repetidas. Debe conservarse información documentada del proceso.'),
  (6, 1, 3, '6.1.3 Tratamiento del riesgo de IA',
      'Tomando en cuenta los resultados de la evaluación del riesgo, la organización debe definir un proceso de tratamiento del riesgo de IA que seleccione las opciones de tratamiento, determine los controles necesarios (comparándolos con el Anexo A), elabore una declaración de aplicabilidad y formule un plan de tratamiento del riesgo de IA, obteniendo la aprobación de la dirección para dicho plan y para la aceptación de los riesgos residuales.'),
  (6, 1, 4, '6.1.4 Evaluación del impacto del sistema de IA',
      'La organización debe definir un proceso para evaluar las posibles consecuencias del desarrollo, provisión o uso de sistemas de IA para individuos, grupos de individuos y sociedades, teniendo en cuenta el contexto técnico y social específico y las jurisdicciones aplicables, y considerar los resultados de esta evaluación en la evaluación del riesgo de IA. El resultado debe documentarse y, cuando sea apropiado, estar disponible para las partes interesadas pertinentes.'),
  -- 7.5 Información documentada
  (7, 5, 1, '7.5.1 Generalidades',
      'El sistema de gestión de IA de la organización debe incluir la información documentada requerida por esta norma y la que la organización determine como necesaria para la eficacia del sistema, cuya extensión puede variar según el tamaño de la organización, la complejidad de sus procesos y la competencia de las personas.'),
  (7, 5, 2, '7.5.2 Creación y actualización',
      'Al crear y actualizar la información documentada, la organización debe asegurarse de que sean apropiados la identificación y descripción (título, fecha, autor o número de referencia), el formato y medio de soporte, y la revisión y aprobación con respecto a la conveniencia y adecuación.'),
  (7, 5, 3, '7.5.3 Control de la información documentada',
      'La información documentada requerida por el sistema de gestión de IA debe controlarse para asegurar que esté disponible, sea idónea para su uso donde y cuando se necesite, y esté protegida adecuadamente, abordando su distribución, acceso, almacenamiento, preservación, control de cambios, conservación y disposición. La de origen externo que se determine necesaria también debe identificarse y controlarse.'),
  -- 9.2 Auditoría interna
  (9, 2, 1, '9.2.1 Generalidades',
      'La organización debe llevar a cabo auditorías internas a intervalos planificados para proporcionar información acerca de si el sistema de gestión de IA es conforme con los requisitos propios de la organización y con los de esta norma, y si se implementa y mantiene eficazmente.'),
  (9, 2, 2, '9.2.2 Programa de auditoría interna',
      'La organización debe planificar, establecer, implementar y mantener uno o varios programas de auditoría (frecuencia, métodos, responsabilidades, planificación e informes), considerando la importancia de los procesos y los resultados de auditorías previas, definiendo objetivos, criterios y alcance para cada auditoría, y asegurando la objetividad e imparcialidad del proceso y que los resultados se informen a la dirección pertinente. Debe conservarse información documentada como evidencia.'),
  -- 9.3 Revisión por la dirección
  (9, 3, 1, '9.3.1 Generalidades',
      'La alta dirección debe revisar el sistema de gestión de IA de la organización a intervalos planificados, para asegurarse de su conveniencia, adecuación y eficacia continuas.'),
  (9, 3, 2, '9.3.2 Entradas de la revisión por la dirección',
      'La revisión por la dirección debe incluir el estado de las acciones de revisiones previas, los cambios en cuestiones externas e internas y en las necesidades y expectativas de las partes interesadas pertinentes, la información sobre el desempeño del sistema (no conformidades y acciones correctivas, resultados de seguimiento y medición, resultados de auditorías) y las oportunidades de mejora continua.'),
  (9, 3, 3, '9.3.3 Resultados de la revisión por la dirección',
      'Los resultados de la revisión por la dirección deben incluir las decisiones relacionadas con las oportunidades de mejora continua y cualquier necesidad de cambios en el sistema de gestión de IA, disponibles como información documentada.')
) AS v(clause, subclause, numeration, name, description);

COMMIT;
