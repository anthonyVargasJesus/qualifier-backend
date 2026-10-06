-- 007_iso27001_control_descriptions.sql
--
-- Carga las 93 descripciones de controles de ISO 27001 (MAE_CONTROL.C_DESCRIPTION),
-- extraidas del propio Excel de ONPE ("FM03-GPP_GC Declaracion de aplicabilidad del SGSI
-- (SoA) V01.xlsx", hoja SoA_V02) -- coinciden casi palabra por palabra con el texto oficial
-- de la norma (Anexo A ISO/IEC 27001:2022), con "servidores" en vez de "empleados" en algunos
-- casos (terminologia propia de ONPE). Antes de este script, 92 de los 93 controles de
-- ISO 27001 no tenian descripcion cargada (NTP-42001 ya estaba completo).
--
-- Generado automaticamente a partir del Excel -- no fue tipeado a mano.
-- Probado en DB de desarrollo (Railway) el 2026-08-13. Pendiente de aplicar en producción.

BEGIN;

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir un conjunto de políticas para la seguridad de la información, aprobada por la alta dirección, publicada y comunicada a los servidores y partes externas pertinentes; y revisadas a intervalos planificados y si ocurren cambios significativos.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 1
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben definir  y asignar de acuerdo con las necesidades de la organización  todas los roles y responsabilidades de la seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 2
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe separar Los deberes y áreas de responsabilidad en conflicto para reducir las posibilidades de modificación no autorizada o no intencional, o el uso indebido de los activos de la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 3
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'La Alta Dirección debe exigir a todos los empleados y contratistas la aplicación de la seguridad de la información de acuerdo con las políticas y procedimientos establecidos por la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 4
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe mantener los contactos apropiados con las autoridades pertinentes.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 5
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe mantener contactos apropiados con grupos de interés especial u otros foros y asociaciones profesionales especializadas en seguridad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 6
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe recopilar y analizar información relacionada con las amenazas a la seguridad de la información'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 7
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe integrar La seguridad de la información en la gestión de proyectos para garantizar que los riesgos de seguridad de la información se abordan como parte de la gestión del proyecto.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 8
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe desarrollar y mantener un inventario de información y otros activos asociados, incluidos los propietarios.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 9
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar, documentar e implementar reglas para el uso aceptable y procedimientos para el manejo de la información y otros activos asociados.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 10
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Todos los empleados y usuarios de partes externas deben devolver todos los activos de la organización que se encuentren a su cargo, al terminar su empleo, contrato o acuerdo.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 11
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe clasificar la información de acuerdo con las necesidades de seguridad de la información de la ONPE en función de la confidencialidad, integridad, disponibilidad y los requisitos de las partes interesadas pertinentes.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 12
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe desarrollar e implementar un conjunto adecuado de procedimientos para el etiquetado de la información, de acuerdo con el esquema de clasificación de información adoptado por la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 13
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe contar con políticas, procedimientos o acuerdos de transferencia  de información en todo tipo de instalaciones de transferencia dentro de la organización  y entre la organización y otras partes.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 14
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer e implementar reglas para controlar el acceso físico y lógico a la información y otros activos asociados en función de los requisitos de negocio y de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 15
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe gestionar el ciclo de vida completo de las identidades'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 16
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe controlar la asignación y gestión de la información de autenticación mediante un proceso de gestión, incluido el asesoramiento al personal sobre el manejo adecuado de la información de autenticación.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 17
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proporcionar, revisar, modificar y eliminar de acuerdo con las políticas y procedimientos de control de acceso de la organización los derechos de acceso a la información y otros activos asociados.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 18
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir e implementar procesos y procedimientos para gestionar los riesgos de seguridad de la información asociados con el uso de productos o servicios de terceros'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 19
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer y acordar con cada proveedor en función del tipo de relación con el proveedor los requisitos de seguridad de la información pertinentes'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 20
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir e implementar procesos y procedimientos para gestionar los riesgos de seguridad de la información asociados con la cadena de suministros de productos y servicios de TIC.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 21
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe monitorear, revisar, evaluar y gestionar periódicamente los cambios en las practicas de seguridad de la información del proveedor y la prestación de servicios'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 22
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer procesos de adquisición, uso, gestión y salida de los servicios en nube de acuerdo con los requisitos de seguridad de la información de la Entidad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 23
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe planificar y preparar la gestión de incidentes de seguridad de la información definiendo, estableciendo y comunicando procesos, funciones y responsabilidades de gestión de incidentes de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 24
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe evaluar los incidentes de seguridad de la información y decidir si se van a clasificar como incidentes de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 25
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe responder  a los incidentes de seguridad de la información de acuerdo con procedimientos documentados.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 26
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe utilizar el conocimiento obtenido de los incidentes de seguridad de la información para fortalecer y mejorar los controles de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 27
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer e implementar  procedimientos para la identificación, recopilación, adquisición y preservación de evidencia relacionada con eventos de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 28
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe planificar como mantener la seguridad de la información a un nivel adecuado durante la interrupción.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 29
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'La preparación de las TIC se  debe planificar, implementar, mantener y probar en función de los objetivos de continuidad de negocio y requisito de continuidad de las TIC.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 30
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar, documentar y mantener actualizados los requisitos legales, estatutarios, reglamentarios y contractuales relevantes para la seguridad de la información y el enfoque de la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 31
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe implementar procedimientos apropiados para proteger los derechos de propiedad intelectual.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 32
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proteger los registros contra perdida, destrucción, falsificación, acceso no autorizado y publicación no autorizada.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 33
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar y cumplir los requisitos relacionados con la preservación de la privacidad  y la protección de la PII de acuerdo con las leyes y reglamentos aplicables y los requisitos contractuales.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 34
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe revisar de forma independiente  a intervalos planificados o cuando ocurran cambios significativos el enfoque de la organización para gestionar la seguridad de la información y su implementación.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 35
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe revisar periódicamente el cumplimiento de la política y normas de seguridad de la información de la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 36
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Los procedimientos  operativos para las instalaciones de procesamiento de información se deben documentar y  poner a disposición del personal que los necesita.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 5
  AND c."N_NUMBER" = 37
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Las verificaciones de los antecedentes de todos los candidatos a un empleo en la organización, se deben llevar a cabo de acuerdo con las leyes, reglamentos y ética pertinentes, y deben ser proporcionales a los requisitos de negocio, a la clasificación de la información a que se va a tener acceso, y a los riesgos percibidos.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 1
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer en los acuerdos contractuales de empleo las responsabilidades del personal y de la organización para la seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 2
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Todos los empleados de la organización y las partes interesadas relevantes  deben recibir la formación en toma de conciencia, educación y capacitación adecuadas  en seguridad de la información y actualizaciones regulares sobre las políticas y procedimientos pertinentes para su cargo.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 3
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe contar con un proceso disciplinario formal el cual debería ser comunicado, para emprender acciones contra empleados que hayan cometido una violación a la seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 4
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben definir, aplicarse y comunicarse al personal pertinente y otras partes interesadas las responsabilidades y los deberes de seguridad de la información que permanecen validos después de la terminación o cambio de contrato o empleo.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 5
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar,  documentar, revisar regularmente los requisitos para los acuerdos de confidencialidad o no divulgación que reflejen las necesidades de la organización para la protección de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 6
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe implementar  medidas de seguridad cuando el personal trabaja de forma remota para proteger la información a la que se accede o almacena fuera de las instalaciones de la Entidad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 7
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proporcionar un mecanismo para que el personal informe eventos de seguridad de la información observados o sospechados a través de los canales apropiados de manera oportuna'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 6
  AND c."N_NUMBER" = 8
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir perímetros de seguridad  y usarlos para proteger áreas que contengan información sensible o critica y otros activos asociados'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 1
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben proteger las áreas seguras con controles de entrada y puntos de acceso apropiados'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 2
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe diseñar y aplicar seguridad física a oficinas, salas e instalaciones.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 3
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe monitorear continuamente las instalaciones para detectar accesos físicos no autorizados'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 4
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe diseñar e implementar la protección contra amenazas físicas y ambientales, como  desastres naturales, y otras amenazas físicas intencionales o no intencionales a la infraestructura.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 5
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe diseñar y aplicar medidas de seguridad para trabajar en áreas seguras.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 6
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe  adoptar una política de escritorio limpio para los documentos y medios de almacenamiento removibles, y una política de pantalla limpia en las instalaciones de procesamiento de información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 7
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe ubicar y proteger los equipos de forma segura'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 8
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe aplicar medidas de seguridad a los activos que se encuentran fuera de las instalaciones de la organización, teniendo en cuenta los diferentes riesgos de trabajar fuera de  instalaciones.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 9
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe gestionar los medios de almacenamiento a lo largo de su ciclo de vida de adquisición, uso, transporte y eliminación de acuerdo con el esquema de clasificación y los requisitos de manipulación de la entidad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 10
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proteger las instalaciones de procesamiento de información contra cortes de energía y otras interrupciones causadas por falla en los servicios de suministro'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 11
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proteger los cables que transportan energía, datos o servicios de información de apoyo contra intercepciones, interferencias o daños.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 12
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe mantener correctamente los equipos  para garantizar su disponibilidad, integridad y confidencialidad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 13
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe verificar todos los elementos de equipos que contengan medios de almacenamiento, para garantizar que cualquier dato sensible o software con licencia haya sido retirado o sobrescrito en forma segura antes de su disposición o reutilización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 7
  AND c."N_NUMBER" = 14
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proteger la información almacenada,  procesada o accesible a través de los dispositivos finales de los usuarios.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 1
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe restringir y controlar la asignación y uso de derechos de acceso privilegiado.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 2
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe restringir el acceso a la información y otros activos asociados de acuerdo con la política de control de acceso'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 3
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debería Controlar el acceso de lectura y escritura  a los códigos fuente de los programas, las herramientas de desarrollo y las bibliotecas de software.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 4
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe implementar tecnologías y procedimientos de autenticación seguros en función de las restricciones de acceso a la información y la política de control de acceso.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 5
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe monitorear y ajustar el uso de recursos de acuerdo con los requisitos de capacidad actuales y esperados'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 6
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe implementar controles de detección, de prevención y de recuperación, combinados con la toma de conciencia apropiada de los usuarios, para proteger contra códigos maliciosos.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 7
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe obtener oportunamente información acerca de las vulnerabilidades técnicas de los sistemas de información que se usen; evaluar la exposición de la organización a estas vulnerabilidades, y tomar las medidas apropiadas para tratar el riesgo asociado.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 8
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer, documentar, implementar, monitorear y revisar las configuraciones, incluidas las configuraciones de seguridad, hardware, software, servicios y redes.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 9
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe eliminar cuando ya no sea necesaria la información almacenada  en sistemas de información, dispositivos o en cualquier otro medio'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 10
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Debe utilizarse de acuerdo con la política específica de la organización sobre control de acceso, otras políticas específicas relacionadas y los requisitos del negocio, teniendo en cuenta la legislación aplicable.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 11
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe aplicar medidas de prevención de fuga de datos a los sistemas, redes y cualquier otro dispositivo que procese, almacene o transmita información confidencial.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 12
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe mantener y probar regularmente las copias de respaldo de la información, el software y los sistemas de acuerdo con la política de respaldo establecida.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 13
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Las instalaciones de procesamiento de información se deben implementar con redundancia suficiente para cumplir los requisitos de disponibilidad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 14
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe producir, almacenar, proteger, y analizar registros (eventos) que registren actividades, excepciones, fallas y otros eventos relevantes.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 15
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben monitorear las redes, los sistemas y las aplicaciones para detectar comportamientos anómales y deben tomarse las medidas apropiadas para evaluar posibles incidentes de seguridad de la información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 16
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben sincronizar con las fuentes de tiempo aprobadas  todos los relojes de los sistemas de procesamiento de información utilizados por la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 17
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe restringir y controlar estrictamente el uso de programas utilitarios que podrían ser capaces de anular los controles del sistema y de las aplicaciones.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 18
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe implementar procedimientos y medidas para gestionar de forma segura las instalación de software en los sistemas operativos.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 19
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe proteger, administrar y controlar las redes y los dispositivos de red para salvaguardar la información en los sistemas y aplicaciones.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 20
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar, implementar y monitorear  los mecanismos de seguridad, los niveles de servicio y los requisitos de gestión de todos los servicios de red.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 21
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben segregar en las redes de la organización los grupos de servicios de información, usuarios y sistemas de información.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 22
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe gestionar el acceso a sitios web externos para reducir la exposición a contenido malicioso.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 23
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir e implementar las reglas para el uso eficaz de la criptografía, incluida la gestión de claves  criptográficas.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 24
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe establecer y aplicar reglas para el desarrollo seguro de software y de sistemas dentro de la organización.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 25
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe identificar, especificar  y aprobar los requisitos de seguridad de la información al desarrollar o adquirir aplicaciones.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 26
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben establecer, documentar, mantener y aplicar a cualquier actividad de desarrollo de sistemas de información  principios para el diseño de sistemas seguros .'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 27
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe aplicar al desarrollo de software principios de codificación segura.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 28
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se deben definir e implementar en el ciclo de vida del desarrollo procesos de prueba de seguridad.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 29
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe dirigir, monitorear y revisar las actividades relacionadas con el desarrollo de sistemas contratados externamente.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 30
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe separar y proteger los entornos de desarrollo, prueba y producción.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 31
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe definir, establecer e implementar procedimientos de gestión de cambios para controlar los cambios en las instalaciones de procesamiento de información y los sistemas de información .'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 32
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'La información de prueba se debe seleccionar, proteger y controlar cuidadosamente.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 33
  AND (c."N_IS_DELETED" IS NOT TRUE);

UPDATE "MAE_CONTROL" c
SET "C_DESCRIPTION" = 'Se debe planificar y acordar entre el evaluador y la gerencia correspondiente las pruebas de auditoria y otras actividades de aseguramiento que involucren la evaluación de los sistemas operativos.'
FROM "MAE_CONTROL_GROUP" g
WHERE c."N_CONTROL_GROUP_ID" = g."N_CONTROL_GROUP_ID_PK"
  AND g."N_STANDARD_ID" = 4
  AND g."N_NUMBER" = 8
  AND c."N_NUMBER" = 34
  AND (c."N_IS_DELETED" IS NOT TRUE);

COMMIT;

-- Verificación post-ejecución: deberían quedar 0 controles de ISO 27001 sin descripción
SELECT count(*) as sin_descripcion
FROM "MAE_CONTROL" c
JOIN "MAE_STANDARD" s ON s."N_STANDARD_ID_PK" = c."N_STANDARD_ID"
WHERE s."C_NAME" = 'ISO 27001'
  AND (c."N_IS_DELETED" IS NOT TRUE)
  AND (c."C_DESCRIPTION" IS NULL OR c."C_DESCRIPTION" = '');
