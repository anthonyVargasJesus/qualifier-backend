# Seed "Evaluación - Mayo 2026" — orden de ejecución

Punto de control intermedio entre `ANÁLISIS DE BRECHAS - DICIEMBRE` (id=3, sin datos
de detalle) y `Evaluación - Junio 2026` (id=4). NO es la evaluación "actual" — Julio
2026 (id=5) sigue siéndolo.

## Cómo se construyó

Diciembre 2025 resultó tener la evaluación creada pero **0 filas de detalle** (ni
requisitos ni controles evaluados) — no hay datos reales de referencia "anteriores" a
junio. En vez de inventar 180 ítems desde cero, Mayo se construyó copiando en bloque
el resultado real de Junio (evaluationId=4) y retrocediendo un subconjunto de 20 ítems
un nivel de madurez, para simular que en mayo aún no habían llegado al estado que sí
tienen en junio:

- **160 ítems** (73 requisitos + ~87 controles): idénticos a junio, mismo nivel/valor/
  justificación/responsable — no cambiaron entre mayo y junio.
- **20 ítems regresados** (14 requisitos + 6 controles, listados abajo): en junio están
  en nivel 10 "Parcial"; en mayo se bajan a nivel 11 "No cumple", generando una brecha
  nueva. El texto de justificación se reutiliza tal cual el de junio (ya describe un
  estado parcial/incompleto, sirve igual de bien como explicación de "aún no cumplido"
  un mes antes). Se evitaron a propósito las cláusulas/controles que ya son parte de la
  historia de brechas de junio/julio (4.2.x, 6.2.x, 7.4.1, 5.8, 5.12-5.14, 8.18-8.19).
- Los **20 ítems que ya estaban en brecha en junio** (los mismos de siempre: 7, 127,
  128, 52, 54-62, 76 + controles 10, 14, 15, 16, 77, 78) también estaban en brecha en
  mayo — se reutiliza el contenido real de `seed_breaches_junio2026.sql` /
  `seed_action_plans_junio2026.sql` / `seed_risks_junio2026.sql` /
  `seed_risk_assessment_junio2026.sql` / `seed_risk_treatment_junio2026.sql` /
  `seed_control_implementation_junio2026.sql`, con fechas retrocedidas ~1 mes.

**Importante — de dónde salió el texto**: para los 20 ítems "que ya existían en junio"
se reutilizó el contenido de los *scripts* de junio (ya revisados), no lo que hoy hay
en la tabla en vivo — las tablas reales de brechas/planes/riesgos de junio tienen
entradas de prueba añadidas después (`'crea backups'`, `'plplp'`, etc., ver
`README_julio2026.md`) que no se querían arrastrar a mayo.

**Controles 2, 60, 61 y 62** también tienen en Junio dos filas de evaluación cada uno
(una sucia de prueba + una limpia con ID mayor, sin restricción de unicidad en
`MAE_CONTROL_EVALUATION`). El script de mayo usa `DISTINCT ON` + `ORDER BY id DESC`
para quedarse solo con la fila limpia de cada uno al copiar los 160 ítems sin cambios.

## Los 20 ítems regresados en mayo

| Ítem | Numeración real (BD) | Tema |
|---|---|---|
| Req. 6 | 4.1.1 | Contexto interno/externo |
| Req. 18 | 4.4.1 | SGSI establecido/mejorado |
| Req. 35 | 5.3.1 | Roles y responsabilidades |
| Req. 41 | 6.5.1 | Metodología de evaluación de riesgos |
| Req. 51 | 6.1.6 | Tratamiento de riesgos |
| Req. 68 | 7.1.1 | Recursos del SGSI |
| Req. 69 | 7.2.1 | Competencias |
| Req. 73 | 7.3.1 | Concientización |
| Req. 77 | 7.5.1 | Información documentada |
| Req. 83 | 8.1.1 | Planificación operativa / tercerizados |
| Req. 87 | 8.2.1 | Evaluación de riesgos operativa |
| Req. 89 | 8.3.1 | Tratamiento de riesgos operativo |
| Req. 94 | 9.1.1 | Monitoreo y medición |
| Req. 115 | 10.1.1 | No conformidades |
| Control 11 | 5.9 | Inventario de activos |
| Control 21 | 5.19 | Seguridad con proveedores |
| Control 33 | 5.31 | Requisitos legales |
| Control 42 | 6.5 | Terminación de empleo |
| Control 46 | 7.1 | Perímetro físico |
| Control 73 | 8.14 | Redundancia de instalaciones |

La numeración de la tabla se reconstruyó recorriendo `N_PARENT_ID`/`N_NUMERATION` en
`MAE_REQUIREMENT` y `N_CONTROL_GROUP.N_NUMBER + MAE_CONTROL.N_NUMBER` para los
controles, calibrado y verificado contra ítems ya conocidos (req. 7 → "4.2.1", control
10 → "5.8").

## Orden de ejecución

1. `seed_evaluation_mayo2026.sql`
2. `seed_requirement_evaluation_mayo2026.sql`
3. `seed_control_evaluation_mayo2026.sql`
4. `seed_breaches_mayo2026.sql`
5. `seed_breaches_controls_mayo2026.sql`
6. `seed_action_plans_mayo2026.sql`
7. `seed_risks_mayo2026.sql`
8. `seed_risk_assessment_mayo2026.sql`
9. `seed_risk_treatment_mayo2026.sql`
10. `seed_control_implementation_mayo2026.sql`

Resultado esperado:
- `MAE_REQUIREMENT_EVALUATION`: 87 filas (14 en nivel 11, 73 sin cambio).
- `MAE_CONTROL_EVALUATION`: ~90 filas (6 en nivel 11, resto sin cambio).
- `MAE_BREACH`: 40 (20 que ya venían de junio + 20 nuevas).
- `MAE_ACTION_PLAN`: 48 (28 + 20).
- `MAE_RISK`: 40 (20 + 20), de los cuales 10 "En evaluación"/"En tratamiento" (los
  mismos 10 que ya estaban evaluados en junio) y 30 "Registrado".
- `MAE_RISK_ASSESSMENT`: 10. `MAE_RISK_TREATMENT`: 6. `MAE_CONTROL_IMPLEMENTATION`: 21.
