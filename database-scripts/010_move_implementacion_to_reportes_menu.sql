-- 010_move_implementacion_to_reportes_menu.sql
--
-- Mueve la opcion "Implementacion" (/gap/plan-de-accion-calendario) del menu
-- "EVALUACION" (N_MENU_ID_PK=13) al menu "REPORTES" (N_MENU_ID_PK=12). Pedido explicito
-- del usuario: es un reporte (calendario de implementacion), no parte del flujo de
-- evaluacion. Queda como la 5ta opcion de REPORTES, despues de "Resultados".
--
-- Afecta MAE_OPTION_IN_MENU (arma el menu) y MAE_OPTION_IN_MENU_IN_ROLE (visibilidad
-- por rol), que duplica N_MENU_ID/N_ORDER -- hay que actualizar ambas tablas.
--
-- IMPORTANTE: MAE_OPTION_IN_MENU_IN_ROLE tambien fija el rol (N_ROLE_ID), y el menu
-- REPORTES (12) solo esta vinculado en MAE_MENU_IN_ROLE al rol "Oficial" (12) -- el
-- menu EVALUACION (13) del que viene la opcion esta vinculado al rol "GAP" (13). Si
-- solo se mueve N_MENU_ID sin corregir N_ROLE_ID, la fila queda huerfana (no matchea
-- con el menu de NINGUN rol) y la opcion desaparece de todos los menus. Se detecto en
-- dev porque el usuario de prueba tiene ambos roles (ve REPORTES y EVALUACION juntos)
-- y "Implementacion" no aparecio en ninguno de los dos tras el primer intento.
--
-- Probado en DB de desarrollo (Railway). Pendiente de aplicar en produccion.

BEGIN;

UPDATE "MAE_OPTION_IN_MENU"
SET "N_MENU_ID" = 12, "N_ORDER" = 5
WHERE "N_OPTION_ID" = 75 AND "N_MENU_ID" = 13;

UPDATE "MAE_OPTION_IN_MENU_IN_ROLE"
SET "N_MENU_ID" = 12, "N_ROLE_ID" = 12, "N_ORDER" = 5
WHERE "N_OPTION_ID" = 75 AND "N_MENU_ID" = 13 AND "N_ROLE_ID" = 13;

COMMIT;

-- Verificacion post-ejecucion: "Implementacion" debe listar N_MENU_ID=12 y N_ROLE_ID=12 (REPORTES / Oficial)
SELECT 'MAE_OPTION_IN_MENU' as tabla, "N_MENU_ID", NULL as rol, "N_ORDER" FROM "MAE_OPTION_IN_MENU" WHERE "N_OPTION_ID" = 75
UNION ALL
SELECT 'MAE_OPTION_IN_MENU_IN_ROLE', "N_MENU_ID", "N_ROLE_ID", "N_ORDER" FROM "MAE_OPTION_IN_MENU_IN_ROLE" WHERE "N_OPTION_ID" = 75;
