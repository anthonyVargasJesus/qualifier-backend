# Seed "Evaluación - Julio 2026" — orden de ejecución

Continúa el ciclo de `Análisis de Brechas ISO 27001` de ONPE (standardId=4, companyId=1).
Alcance acordado: **solo se re-evalúan los 20 ítems que quedaron NO CUMPLIDOS en
Junio 2026** (14 requisitos + 6 controles), no los 180 del Anexo A completo.

Resultado narrativo: **7 de los 20 cierran** (6 requisitos + 1 control), **13 siguen
abiertos** (7 con avance parcial documentado, 6 sin cambios desde junio).

## Verificado contra la BD real (Railway, 2026-08-09)

Todos los nombres de tabla/columna y los IDs de catálogo usados en estos 10 scripts
fueron confirmados con consultas de solo lectura contra la base de datos real, no solo
inferidos de los scripts de junio. Hallazgos relevantes:

- **Niveles de madurez** (companyId=1): 9=Cumple(5.00), 10=Parcial(3.00),
  11=No cumple(1.00), 12=No aplica(0.00). Los niveles 10 y 11 tienen
  `L_GENERATES_BREACH=true` en el catálogo (10→severidad 3 "Alta", 11→severidad 4
  "Crítica") — es decir, si alguien vuelve a **guardar esa evaluación desde la UI**,
  la app podría generar una brecha también para los ítems en nivel 10. Se decidió
  igual usar nivel 10 sin fila de brecha para "cerrar" los 7 ítems de julio, porque
  es exactamente como ya están hoy los ~85 controles y 55 requisitos "conformes" de
  Junio 2026 en esta misma BD (nivel 10, sin brecha) — el flag solo se dispara vía
  guardado desde la app, nunca por un INSERT directo por SQL como este.
- **Catálogos confirmados exactos** a lo ya usado en los scripts: `MAE_BREACH_STATUS`
  (1=Abierta), `MAE_BREACH_SEVERITY` (1=Baja,2=Media,3=Alta,4=Crítica),
  `MAE_ACTION_PLAN_STATUS` (1=Pendiente,2=En curso,4=Completado — no existe id=3),
  `MAE_ACTION_PLAN_PRIORITY` (1=Alta,2=Media,3=Baja), `MAE_RISK_STATUS`
  (1=Registrado,2=En evaluación,3=En tratamiento), `MAE_RISK_TREATMENT_METHOD`
  (1=Mitigar), `MAE_RISK_LEVEL` (BAJO 1-8, MEDIO 9-19, ALTO 20-45), `MAE_RESIDUAL_RISK`
  (1=Aceptar, rango 1-3). Responsables, amenazas, vulnerabilidades y activos usados
  también se confirmaron existentes con los mismos nombres.
- **Corregido**: la evaluación real usa `N_EVALUATION_STATE_ID_FK=2` ("Edición") y
  `N_IS_GAP_ANALYSIS=true` en ambas evaluaciones existentes (Diciembre y Junio) — el
  script de evaluación ya quedó ajustado a esos valores (antes tenía 1 y `false`).
- **Fuera de alcance a propósito**: la evaluación de Junio (id=4) tiene datos de
  prueba mezclados para los controles **2** (Políticas de seguridad) y **61**
  (Derechos de acceso privilegiado) — filas de evaluación/brecha/plan de acción con
  texto tipo `'hyfhg'`, `'no'`, `'crea backups'`, evidentemente tecleadas a mano
  probando la app, duplicadas sobre una fila "limpia" con el texto real del seed
  (`MAE_CONTROL_EVALUATION` no tiene restricción de unicidad por evaluación+control,
  así que ambas filas coexisten). El control **62** tiene el mismo patrón pero sin
  fila limpia. Julio **no toca estos 3 controles** — quedan tal cual están.

## Orden de ejecución

1. `seed_evaluation_julio2026.sql` — crea la evaluación y desmarca Junio como "actual".
2. `seed_requirement_evaluation_julio2026.sql` — re-evalúa los 14 requisitos en brecha.
3. `seed_control_evaluation_julio2026.sql` — re-evalúa los 6 controles en brecha.
4. `seed_breaches_julio2026.sql` — brecha para los 8 requisitos que siguen sin cumplir.
5. `seed_breaches_controls_julio2026.sql` — brecha para los 5 controles que siguen sin cumplir.
6. `seed_action_plans_julio2026.sql` — 17 planes de acción para las 13 brechas abiertas.
7. `seed_risks_julio2026.sql` — 1 riesgo por cada una de las 13 brechas abiertas.
8. `seed_risk_assessment_julio2026.sql` — evalúa 6 de esos 13 riesgos.
9. `seed_risk_treatment_julio2026.sql` — pasa 3 de esos 6 a tratamiento (los ALTO).
10. `seed_control_implementation_julio2026.sql` — controles de implementación para esos 3.

Cada script (salvo el primero) resuelve el id de la evaluación y de las brechas/riesgos
dinámicamente vía subquery (por `C_DESCRIPTION` / por la combinación
evaluación+tipo+requisito o control), en vez de asumir un ID numérico fijo.

## Qué se cerró y qué sigue abierto

| Ítem | Junio | Julio | Nota |
|---|---|---|---|
| Req. 4.2.1 (partes interesadas) | No cumple | **Cumple (Parcial)** | Registro aprobado |
| Req. 4.2.2 (requisitos de partes interesadas) | No cumple | No cumple | — |
| Req. 4.2.3 (alcance de requisitos) | No cumple | No cumple | — |
| Req. 6.2.1 (consistencia objetivos-política) | No cumple | No cumple | En progreso |
| Req. 6.2.2 (medición de objetivos) | No cumple | No cumple | — |
| Req. 6.2.3 (objetivos según riesgos) | No cumple | No cumple | En progreso |
| Req. 6.2.4 (comunicar objetivos) | No cumple | No cumple | En progreso |
| Req. 6.2.5 (control de versiones) | No cumple | No cumple | — |
| Req. 6.2.6 (actividades por objetivo) | No cumple | **Cumple (Parcial)** | — |
| Req. 6.2.7 (recursos) | No cumple | **Cumple (Parcial)** | — |
| Req. 6.2.8 (responsables) | No cumple | **Cumple (Parcial)** | — |
| Req. 6.2.9 (plazos) | No cumple | No cumple | En progreso |
| Req. 6.2.10 (criterio de evaluación) | No cumple | **Cumple (Parcial)** | — |
| Req. 7.4.1 (comunicación del SGSI) | No cumple | **Cumple (Parcial)** | Procedimiento aprobado |
| Control 5.8 (proyectos) | No cumple | No cumple | En progreso |
| Control 5.12 (clasificación) | No cumple | **Cumple (Parcial)** | Esquema aprobado |
| Control 5.13 (etiquetado) | No cumple | No cumple | Depende de 5.12 |
| Control 5.14 (transferencia) | No cumple | No cumple | En progreso, en tratamiento |
| Control 8.18 (utilidades privilegiadas) | No cumple | No cumple | En progreso, en tratamiento |
| Control 8.19 (instalación de software) | No cumple | No cumple | — |

*(No incluye los controles 5.1 y 8.2/8.3 — ver "fuera de alcance" arriba.)*

## Antes de ejecutar

Revisar cada script manualmente. En particular, correr primero el paso 1 solo y
confirmar con un `SELECT` que:
- La evaluación 'Evaluación - Julio 2026' quedó creada con un solo `N_EVALUATION_ID_PK`.
- Junio (id=4) quedó con `N_IS_CURRENT=false` (el UPDATE debe afectar exactamente 1 fila).

Luego seguir con los pasos 2-10 en orden, ya que cada uno depende de que el anterior
haya corrido (las brechas dependen de la evaluación, los planes de acción y riesgos
dependen de las brechas, el tratamiento depende de la evaluación de riesgos, etc.).
