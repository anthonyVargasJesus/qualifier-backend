# Seed de evaluaciones Mayo/Junio/Julio 2026 — NTP-ISO/IEC 42001

Carga 3 ciclos de evaluación completos (con brechas, planes de acción,
riesgos, evaluación/tratamiento de riesgo e implementación de control)
para la norma NTP-ISO/IEC 42001, con la misma historia narrativa usada
para ISO 27001: **Junio** es la línea base (evaluación completa),
**Mayo** es un punto de control anterior con más brechas, y **Julio**
es una reevaluación parcial que solo toca los ítems en brecha de junio.

**Ninguna de las 3 se marca como evaluación "actual"** — decisión
explícita: `N_IS_CURRENT` es una columna **global** (no por norma;
`GetCurrentEvaluationQuery` trae la única fila `isCurrent=true` sin
filtrar por `standardId`, y de ahí depende toda la pantalla "Análisis
del GAP"). Hoy esa fila es "Evaluación - Julio 2026" de ISO 27001;
marcar alguna de estas la reemplazaría. Estas 3 evaluaciones quedan
como histórico/borrador, visibles desde el administrador de
evaluaciones, pero `/gap/gap-home` sigue mostrando ISO 27001.

## Alcance evaluado

Solo las **32 hojas reales de requisitos** (20 de nivel 2 + 12 de
nivel 3) y los **38 controles** — 70 ítems por ciclo. Los 11
"contenedores" de requisitos (7 de nivel 1 + 4 de nivel 2 con hijos)
**no** reciben evaluación propia, igual que hoy pasa con los
encabezados de nivel 1 de ISO 27001 (confirmado: 0 evaluaciones
registradas para ellos pese a `N_IS_EVALUABLE=true`).

## Contenido templatizado, no prosa única por ítem

Dado el volumen (70 ítems × 3 ciclos = 210 evaluaciones, más 110
brechas y su pipeline completo), el texto de justificación/evidencia/
acciones de mejora es **genérico por nivel de madurez** (Cumple/
Parcial/No cumple/No aplica), con el código y nombre del ítem
insertado para variar un poco — no una redacción bespoke por ítem como
se hizo para las evaluaciones de ISO 27001 originales. Es una
diferencia de estilo deliberada frente a esos scripts, documentada acá
para que no sorprenda al revisar.

## La historia: quién cambia y cuándo

**Línea base en junio** (70 ítems evaluados): 31 Cumple, 25 Parcial,
12 No cumple, 2 No aplica → **37 en brecha** (15 requisitos + 22
controles).

**Julio — reevaluación PARCIAL**: solo se re-evalúan esos 37 ítems en
brecha (no se toca nada que ya estaba Cumple en junio). De esos, 13 se
cierran a Cumple (5 requisitos: 5.3, 7.2, 7.4, 9.1, 7.5.3; 8 controles:
2.3, 3.2, 4.2, 4.3, 6.3, 6.4, 8.2, 9.2) y 24 quedan igual. Los otros 33
ítems (los que ya eran Cumple/No aplica en junio) **no tienen fila de
evaluación en julio** — mismo patrón ya usado para ISO 27001: en la
pantalla de julio esos ítems se ven "Pendiente" al no tener evaluación
de ese ciclo, una limitación conocida y aceptada del diseño, no un bug.

**Mayo — punto de control anterior**: copia el estado de junio y
retrocede 15 ítems un nivel de madurez (12 de Cumple a Parcial: 4
requisitos + 8 controles; 3 de Parcial a No cumple: 1 requisito + 2
controles), simulando que en mayo la situación era peor. Esto agrega
12 brechas nuevas sobre las 37 de junio → **49 brechas en mayo**.

## Riesgos: activos/amenazas/vulnerabilidades reutilizados

`MAE_RISK` exige activo + amenaza + vulnerabilidad reales (las 3 FK
son NOT NULL). NTP 42001 no tiene activos propios en
`MAE_ACTIVES_INVENTORY` (norma nueva, y esa tabla exige además 8 FKs
más — macroproceso, subproceso, tipo de activo, dueño, custodio,
clasificación de uso, tipo de soporte, ubicación — que habría que
catalogar desde cero). En vez de construir ese catálogo completo, se
**reutilizan los 12 activos reales ya cargados para ISO 27001** (son
activos genéricos de la organización — servidores, backups, personal,
documentación — igual de válidos para un riesgo de gestión de IA que
para uno de seguridad de la información).

Amenazas/vulnerabilidades: el catálogo compartido de la empresa (117
de cada una) resultó estar organizado por tema de cláusula de gestión
(contexto, liderazgo, riesgos, objetivos, competencia...) de forma
casi genérica a cualquier norma de sistema de gestión — se reutilizan
directamente por cláusula/grupo temático (ver el `CASE` en
`seed_risks_ntp42001.sql`) sin crear entradas nuevas.

## Riesgo → evaluación → tratamiento: solo el subconjunto que se cierra

Se genera un riesgo (`MAE_RISK`) por cada una de las 110 brechas. Pero
`MAE_RISK_ASSESSMENT` / `MAE_RISK_TREATMENT` / `MAE_CONTROL_IMPLEMENTATION`
solo se cargan para los **13 riesgos de junio que se cierran en
julio** — mismo criterio que ISO 27001 originalmente (no todos los
riesgos identificados llegan a evaluación/tratamiento formal; ahí se
muestra el ciclo completo de principio a fin para esos 13 casos
concretos, como ejemplo representativo del flujo).

## Responsables

8 roles nuevos (`seed_responsibles_ntp42001.sql`), asignados por área
temática — cláusula para requisitos, grupo de control para controles
— no por ítem individual. Ver el mapeo exacto en el `CASE` de cada
script de evaluación.

## Orden de ejecución

1. `seed_responsibles_ntp42001.sql`
2. `seed_evaluations_ntp42001.sql`
3. `seed_requirement_evaluations_ntp42001.sql`
4. `seed_control_evaluations_ntp42001.sql`
5. `seed_breaches_ntp42001.sql`
6. `seed_action_plans_ntp42001.sql`
7. `seed_risks_ntp42001.sql`
8. `seed_risk_assessment_ntp42001.sql`
9. `seed_risk_treatment_ntp42001.sql`
10. `seed_control_implementation_ntp42001.sql`

Cada script depende del anterior (1→2→...→10), en ese orden estricto.

## Resultado esperado

| Tabla | Mayo | Junio | Julio | Total |
|---|---|---|---|---|
| `MAE_EVALUATION` | 1 | 1 | 1 | 3 |
| `MAE_REQUIREMENT_EVALUATION` | 32 | 32 | 15 | 79 |
| `MAE_CONTROL_EVALUATION` | 38 | 38 | 22 | 98 |
| `MAE_BREACH` | 49 | 37 | 24 | 110 |
| `MAE_ACTION_PLAN` | 49 | 37 | 24 | 110 |
| `MAE_RISK` | 49 | 37 | 24 | 110 |
| `MAE_RISK_ASSESSMENT` | — | 13 | — | 13 |
| `MAE_RISK_TREATMENT` | — | 13 | — | 13 |
| `MAE_CONTROL_IMPLEMENTATION` | — | 13 | — | 13 |
| `MAE_RESPONSIBLE` | +8 (una sola vez) | | | 8 |

## Nota sobre re-ejecución

Ninguno de los 10 scripts es idempotente. Correrlos dos veces duplica
todo. Para revertir, borrar en orden inverso filtrando por
`N_STANDARD_ID`/evaluaciones cuya descripción empiece con
`'Evaluación - '` y esté en `('Mayo 2026','Junio 2026','Julio 2026')`
para `N_STANDARD_ID = <id de NTP-ISO/IEC 42001>` — no tocar las
evaluaciones homónimas de ISO 27001 (mismo nombre de ciclo, distinto
`N_STANDARD_ID_FK`).
