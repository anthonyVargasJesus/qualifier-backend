using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Qualifier.Application.Firebase
{
    public static class FirebaseManager
    {
        // La credencial de la cuenta de servicio NUNCA debe vivir en el código fuente NI en
        // launchSettings.json (ese archivo se commitea a git). El caller la resuelve vía
        // IConfiguration["FIREBASE_SERVICE_ACCOUNT_JSON"], que en local toma el valor de
        // "dotnet user-secrets" (fuera del repo) y en Railway, de la variable de entorno real
        // del servicio — misma clave, sin duplicar lógica acá.
        public static void configCredentials(string? json)
        {
            if (FirebaseApp.DefaultInstance != null) return;

            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException(
                    "Falta configurar FIREBASE_SERVICE_ACCOUNT_JSON (user-secret en local, variable de entorno en Railway) con la credencial de la cuenta de servicio de Firebase.");

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(json),
            });
        }
    }
}
