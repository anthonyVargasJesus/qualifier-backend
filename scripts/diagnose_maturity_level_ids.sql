-- Diagnóstico de solo lectura: revisa si "No cumple" tiene el maturityLevelId
-- que el backend espera (5 o 6) para generar la brecha automáticamente.
SELECT "N_MATURITY_LEVEL_ID_PK", "C_NAME", "C_ABBREVIATION", "N_VALUE", "C_COLOR"
FROM "MAE_MATURITY_LEVEL"
ORDER BY "N_VALUE" DESC;

-- El control 8.1 recién marcado "No cumple": revisa qué maturityLevelId quedó guardado
SELECT ce."N_CONTROL_EVALUATION_ID_PK", ce."N_CONTROL_ID_FK", ce."N_MATURITY_LEVEL_ID_FK", ce."N_EVALUATION_ID_FK"
FROM "MAE_CONTROL_EVALUATION" ce
JOIN "MAE_CONTROL" c ON c."N_CONTROL_ID_PK" = ce."N_CONTROL_ID_FK"
WHERE c."C_NAME" LIKE '%Dispositivos de punto final%';
