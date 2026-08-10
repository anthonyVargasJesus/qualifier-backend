# Seed "NTP-ISO/IEC 42001" — carga de norma nueva

Carga la NTP-ISO/IEC 42001 (versión peruana de ISO/IEC 42001:2023, sistema de
gestión de IA) como una **norma nueva e independiente** — mismo mecanismo genérico
(`MAE_STANDARD` / `MAE_REQUIREMENT` / `MAE_CONTROL_GROUP` / `MAE_CONTROL`) que ya
usa ISO 27001 (id=4), sin cambios de código: es puramente carga de contenido.

No toca ninguna evaluación existente ni las tablas `MAE_EVALUATION*` — es
contenido de catálogo, no un ciclo evaluado. Para evaluarla luego haría falta un
`INSERT` en `MAE_EVALUATION` con `N_STANDARD_ID_FK` apuntando a esta norma (fuera
de alcance de este seed).

## Qué se carga

- **1 norma** (`MAE_STANDARD`): "NTP-ISO/IEC 42001", sin norma padre
  (`N_PARENT_ID = 0`, ver nota abajo).
- **43 requisitos** (`MAE_REQUIREMENT`, cláusulas 4-10 del cuerpo normativo),
  **todos con `N_IS_EVALUABLE = true`** (ver nota abajo):
  - Nivel 1 (7): encabezados de cláusula 4-10.
  - Nivel 2 (24): 20 hojas (4.1-4.4, 5.1-5.3, 6.2, 6.3, 7.1-7.4, 8.1-8.4, 9.1,
    10.1, 10.2) + 4 contenedores que el documento real subdivide más (6.1,
    7.5, 9.2, 9.3).
  - Nivel 3 (12): hojas reales bajo esos 4 contenedores (6.1.1-6.1.4,
    7.5.1-7.5.3, 9.2.1-9.2.2, 9.3.1-9.3.3).
  - De estas 43, **32 son hojas reales** (20 de nivel 2 + 12 de nivel 3) — las
    11 restantes (7 de nivel 1 + 4 contenedores de nivel 2) tienen hijos y en
    la práctica nunca reciben una evaluación propia (mismo comportamiento que
    ya tienen hoy los encabezados de nivel 1 de ISO 27001).
- **9 grupos de control + 38 controles** (`MAE_CONTROL_GROUP` / `MAE_CONTROL`),
  del Anexo A (A.1 es solo la introducción del anexo, sin controles propios,
  no se carga como grupo), **planos, numeración 2-10** (A.2 Políticas...,
  A.3 Organización interna, A.4 Recursos, A.5 Evaluación de impactos, A.6
  Ciclo de vida, A.7 Datos, A.8 Información a partes interesadas, A.9
  Utilización, A.10 Terceros y clientes) — confirmado contra la tabla de
  contenidos real del documento: A.6 y A.7 son secciones de primer nivel
  independientes, no hay sub-grupos anidados en esta norma.

## Decisiones de diseño y su porqué

**Numeración de requisitos = numeración real del documento, sin sufijos
sintéticos.** A diferencia de cómo hoy aparenta estar cargada ISO 27001 (donde
cláusulas atómicas como "4.2" llevan un ".1" agregado, ej. "4.2.1"), acá se
preservó la numeración NTP tal cual (4.1, 4.2... 6.1.1... 9.3.3) — no hacía
falta forzar un nivel extra artificial para que un ítem cuente como hoja real.

**`N_PARENT_ID = 0` (no `NULL`) para "sin padre", en `MAE_STANDARD` y en las
filas de nivel 1 de `MAE_REQUIREMENT`.** Descubierto en vivo: `parentId` es un
`int` no-nullable tanto en `StandardEntity` como en `RequirementEntity` — EF
Core lanza `InvalidCastException: Column 'parentId' is null` al leer una fila
con `NULL` ahí. La convención real de la app (confirmada contra ISO 27000/
27001 y sus cláusulas raíz) es `0`, no `NULL`. Los scripts ya están corregidos
con esto.

**`N_IS_EVALUABLE = true` en las 43 filas, incluidos los 11 contenedores con
hijos (7 de nivel 1 + 4 de nivel 2).** También descubierto en vivo: la
pestaña admin "Requisitos" (`GetRequirementsByStandardIdQuery`) filtra por
`isEvaluable` **antes** de armar el árbol — con `false` en los contenedores,
ningún nodo sobrevivía al filtro y la pestaña quedaba vacía sin error visible
(a diferencia del bug de `parentId`, que sí tira una excepción capturable).
Se verificó contra el dato real de ISO 27001: sus filas de nivel 1/2/3 con
hijos también están en `true` — el campo no distingue "hoja evaluable" de
"contenedor", solo excluye clausulas de referencia sin contenido propio (tipo
"Alcance", que NTP 42001 no tiene en el rango 4-10). La distinción real de
qué cuenta como ítem evaluado en Análisis del GAP vive en otro lado
(`GapItemsBuilder` filtra solo por `isEvaluable`, así que en la práctica
cuenta las 43 filas, no 32 — mismo comportamiento que ISO 27001 tiene hoy con
sus 8 encabezados de nivel 1: siempre aparecen "Pendiente" porque nunca se
les carga una evaluación real, pero suman al total). No se intentó corregir
ese comportamiento de fondo — está fuera del alcance de este seed y ya existe
igual en la norma existente.

**`C_NAME` de requisito = el enunciado normativo completo** ("La organización
debe..."), no un título corto — así es como ya está cargado ISO 27001 (confirmado
con ejemplos reales, ej. req. 6 → "Determinar los aspectos externos e internos
relevantes para el SGSI..."). `C_DESCRIPTION` se dejó `NULL` salvo donde el texto
normativo tenía contenido adicional claramente separable (notas, alcance del
listado de entradas de la revisión por la dirección, etc.).

**Controles: `C_NAME` = tópico de la Tabla A.1 (columna corta), `C_DESCRIPTION`
= el enunciado del control** — mismo patrón esperado por el formulario
`add-control` (`name` ≤100 caract., `description` ≤500).

**Grupos de control: numeración plana 2-10, sin decimales — corregido en
vivo tras revisar la tabla de contenidos real del documento.** Una versión
anterior de este seed asumió (sin haber visto el índice del documento) que
el Anexo A subdividía A.6 en "A.6.1 Ciclo de vida del sistema de IA" y "A.6.2
Datos para sistemas de IA", y los cargó como grupos decimales "6.1"/"6.2" —
para lo cual se había cambiado `MAE_CONTROL_GROUP.N_NUMBER` de `int` a
`decimal` en todo el flujo (`ControlGroupEntity`, su configuración EF, los
DTOs de `ControlGroup`/`Control`/Gap/Excel/Dashboard, y los 5 lugares del
backend que arman `numerationToShow = grupo.número + "." + control.número`,
centralizados en `ControlGroupEntity.numberToShow`). Al revisar la tabla de
contenidos real se confirmó que **"Ciclo de vida del sistema de IA" (A.6) y
"Datos para sistemas de IA" (A.7) son dos secciones de primer nivel
independientes**, cada una con su propia página — no hay anidamiento. Este
seed volvió a usar enteros simples (2-10), igual que ISO 27001.

El soporte decimal en `ControlGroupEntity.number` (y la migración
`alter_control_group_number_to_numeric.sql`, ya aplicada en Railway) **no se
revirtió** — no hace daño (ISO 27001 y esta norma siguen usando enteros
simples, que siguen mostrándose igual, ej. "5" no "5.00") y queda disponible
por si alguna futura norma sí tiene sub-grupos genuinamente anidados en su
Anexo A. Si se prefiere revertir ese cambio de tipo por completo (volver
`number` a `int`), es un cambio aparte, no incluido acá.

**`N_DEFAULT_RESPONSIBLE_ID_FK` se dejó `NULL`** en todos los requisitos y
controles — es un catálogo nuevo sin responsables definidos aún para esta norma
en la compañía; asignarlos es un paso posterior vía UI (`current-standard`),
fuera de alcance de este seed de contenido.

## Orden de ejecución

0. `alter_control_group_number_to_numeric.sql` — cambio de esquema puro
   (`MAE_CONTROL_GROUP.N_NUMBER` de `INTEGER` a `NUMERIC`), independiente del
   contenido de esta norma. Ya no es estrictamente necesario para esta carga
   (los grupos de NTP 42001 volvieron a ser enteros), pero sigue siendo
   necesario para desplegar el backend con `ControlGroupEntity.number` como
   `decimal` — si el backend nuevo se despliega antes de correr esto,
   cualquier escritura contra la columna vieja fallará.
1. `seed_standard_ntp42001.sql` — crea la norma (los otros 2 scripts resuelven
   su `N_STANDARD_ID` por `C_NAME` + `N_COMPANY_ID`, así que este va primero).
2. `seed_requirements_ntp42001.sql` — árbol de requisitos (3 pasadas: nivel 1,
   luego nivel 2 resolviendo el padre nivel 1, luego nivel 3 resolviendo el
   padre nivel 2 vía join con el abuelo nivel 1 para desambiguar).
3. `seed_controls_ntp42001.sql` — grupos de control (numeración plana 2-10),
   luego los 38 controles resolviendo su grupo por número.

Los pasos 2 y 3 son independientes entre sí (tablas distintas), pero ambos
dependen de que 1 ya haya corrido.

## Resultado esperado tras ejecutar los 4

- `MAE_CONTROL_GROUP.N_NUMBER`: pasa a `NUMERIC` (sin cambiar los valores
  existentes de ISO 27001).
- `MAE_STANDARD`: +1 fila ("NTP-ISO/IEC 42001").
- `MAE_REQUIREMENT`: +43 filas (7 nivel 1 + 24 nivel 2 + 12 nivel 3; todas
  `N_IS_EVALUABLE=true`; 32 son hojas reales, 11 son contenedores con hijos).
- `MAE_CONTROL_GROUP`: +9 filas (números 2 a 10, planos).
- `MAE_CONTROL`: +38 filas.

## Nota sobre re-ejecución

`alter_control_group_number_to_numeric.sql` es seguro de re-ejecutar (cambiar
un `NUMERIC` a `NUMERIC` otra vez no hace nada). Los 3 `seed_*` NO son
idempotentes (no hay `WHERE NOT EXISTS`/`ON CONFLICT`) — igual que el resto de
los scripts `seed_*` de este directorio. Correrlos dos veces duplica la norma
completa. Si se necesita revertir el contenido, borrar en orden inverso
(`MAE_CONTROL` → `MAE_CONTROL_GROUP` → `MAE_REQUIREMENT` → `MAE_STANDARD`)
filtrando por `N_STANDARD_ID`/`C_NAME = 'NTP-ISO/IEC 42001'` (la alteración de
esquema del paso 0 no hace falta revertirla: es compatible con ISO 27001).
