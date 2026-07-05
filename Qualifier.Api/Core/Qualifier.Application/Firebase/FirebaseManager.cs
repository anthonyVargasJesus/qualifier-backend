using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Qualifier.Application.Firebase
{
    public static class FirebaseManager
    {
        // La credencial de la cuenta de servicio NUNCA debe vivir en el código fuente.
        //
        // Configura la variable de entorno FIREBASE_SERVICE_ACCOUNT_JSON con el contenido
        // completo del JSON de la cuenta de servicio (en local, vía launchSettings.json/tu
        // shell; en Railway, como variable de entorno del servicio).
        public static void configCredentials()
        {
            if (FirebaseApp.DefaultInstance != null) return;

            var json = Environment.GetEnvironmentVariable("FIREBASE_SERVICE_ACCOUNT_JSON");
            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException(
                    "Falta la variable de entorno FIREBASE_SERVICE_ACCOUNT_JSON con la credencial de la cuenta de servicio de Firebase.");

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(json),
            });
        }
    }
}
