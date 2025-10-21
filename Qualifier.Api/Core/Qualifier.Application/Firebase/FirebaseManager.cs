using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Qualifier.Application.Firebase
{
    public static class FirebaseManager
    {
        public static void configCredentials()
        {
            var firebaseConfig = new
            {
                type = "service_account",
                project_id = "qualifier-a9a5c",
                private_key_id = "e05ca0f57a49abcf57f4e00a49944d3e40b26d0a",
                private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQC3mXPmUazTZ5w/\ny+kUDz3JESZ4XhOj9hB6aGPglZcK30ujozA222uxnMeyXC9ITAl4xncEyOqzR6SW\nRmrHvJzE/7Hth/tXHock8Ax4yp1MUDy+Y/5Dae8sGqUVMjxajKAGraT7OeJSULoG\nuAWKr7HmNk7SCLw1pw+pY0pHbYx4dmp72DoVVJtbZHCHZuwe31japbnhmmdRSqHU\nn8XPe0JalkPOMMlS64fUN/kn1kEnghReuHzobB8JRNqbUs7+n+PVrYAEMoyuIJ+I\n0lWyCInwFORwUiYo/p7XQUL1QKCHpi71jKiKK7r/jiWeW2jbtBmt7vw1oLkI6Ggm\nAPJVDYG7AgMBAAECggEABfmQwhvS77VeUKmU1w4+uMv1QD3g2yYXRrlRS9a1nROq\nF1QGg2uTEQCLQFp0Ybo7vvGdKGlnt4LilnnFk+TJW8/+0D4v8A8YtL11qvpkwqz7\nhiW1pDjbj/LXO539IuROvUn3B9B/ZC5SKZ6L0gXTPvmUNDeMfYOyICZuezqo7y7/\nox5tCWS8OUeTQ8f+tKUx96ekEwMj06rFkIfLNa+zkQl3VcH5fkSyoKgL8lSHiJVw\nxb6g76Zlra6ZtVx2RSNfvq4KWY4fbdyBhT0wOzXJZXMIfQrMCvCqCUw8ud0n3/dZ\nvGkcFq1blSHWyRVey4Zs3wBY6FX4l6SbOSKCQYLHcQKBgQDfNguHucYKgibjfgtB\nKyoFfmEDdhI2IZyVL+YhTOa7jA/XgFVhLnS9SeLJBima7lUm3fmHWi4xSyDnxuFw\n+7H1hF3oktZBNdUY0f92urT8Fz7kphVGeQVEEJK52sJeyJU+U8clhqjtK0/SB9hL\nQLg815ju1f03NGNnCdV3I2LXsQKBgQDSkcrON/2mNV9+nzf969N77uX63B7FfSHz\nzwBbSPaqk1yIAztqCKZUCtmZTFpFk3qFiHfFRfuTXX9OU6b1LPKBlRALCsO6aMZS\nLsxrukm7EA0SwgucSf5+qoukt/w2sFPD9OuN0XK2a4l9BS7Q2oOnuVSJ7LkFf9ty\nhfI0ADB3KwKBgQDLSIc7tv8b9ui/6r0JPuxoUG6+Hjv7vKTEYfkufsDsMWUEGMap\nWQvMkIvQFKKzjTdTzHlCweir0AZJ0CDlKvUp6sEz3PbwMLzAfBAy61uE8w6+Yluh\n8PnQwV8/kHHQrNDvEJGgJYGXbgil+asM7gZcsuV/LrgMHNQitRwKAto0IQKBgQCO\noyfILXFq53i6o0wge2EU8K1lEzrc+HjJWQ2ayIAe6BekItxaZWWWRItGJsx3mQFD\n9z96qH9UQd98xXmxVzMmyDzH3hDb8yF0hjL/Pn+3cZYJgNywkAcO8qQJfoAg46y6\nbuwpcxnLrGjEAYDHBV5tfUNg8rQan5OdrlJ1GzO5ZQKBgQCkO/8KqiZf64HlJqj3\nq0XGfACl2MvxVLef6L08eMy+hOjXunN3CltURQ8dkRRP8awah5FbuxEnGWWh9rDg\nU2Oqhh/R2ubZbwIocJSsEIXVZIXstsjdKlWx8Q/mSoBzR+kjsCFFwFs0hhhjIcwF\ncZGrrU9X20ia/VuDBJ7Da1ugaw==\n-----END PRIVATE KEY-----\n",
                client_email = "firebase-adminsdk-fbsvc@qualifier-a9a5c.iam.gserviceaccount.com",
                client_id = "108614327707346936584",
                auth_uri = "https://accounts.google.com/o/oauth2/auth",
                token_uri = "https://oauth2.googleapis.com/token",
                auth_provider_x509_cert_url = "https://www.googleapis.com/oauth2/v1/certs",
                client_x509_cert_url = "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-fbsvc%40qualifier-a9a5c.iam.gserviceaccount.com",
                universe_domain = "googleapis.com"
            };

            var options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonString = JsonConvert.SerializeObject(firebaseConfig, options);

            var app = FirebaseApp.DefaultInstance;
            if (FirebaseApp.DefaultInstance == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonString),
                });
            }

        }
    }
}
