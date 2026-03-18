using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Qualifier.Api;
using Qualifier.Api.Infrastructure.Qualifier.Persistence;
using Qualifier.Application;
using Qualifier.Common;
using Qualifier.Domain;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("CONNECTION_STRING");

// Añade una fuente de configuración en memoria con alta prioridad.
// Esto sobrescribirá lo que venga de appsettings.json
// y lo que vendría de ConnectionStrings__DefaultConnection si existiera.

if (connectionString != null)
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
        { "ConnectionStrings:DefaultConnection", connectionString }
    });

builder.Services
    .AddWebAPI()
    .AddCommon()
    .AddApplication(builder.Configuration)
    .AddDomain()
    .AddPersistence(builder.Configuration);

builder.Logging.ClearProviders(); // Limpia proveedores por defecto si es necesario
builder.Logging.AddConsole(); // Añade el proveedor de consola

builder.Services.AddControllers();

// Configuración de JWT
var issuer = builder.Configuration.GetValue<string>("JWT_ISSUER");
var audience = builder.Configuration.GetValue<string>("JWT_AUDIENCE");
var secretKey = builder.Configuration.GetValue<string>("JWT_SECRET_KEY");

if (issuer != null)
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
        { "Authentication:issuer", issuer }
    });

if (audience != null)
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
        { "Authentication:audience", audience }
    });

if (secretKey != null)
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
        { "Authentication:secretKey", secretKey }
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    string? bytes = "";

    if (builder.Configuration["Authentication:secretKey"] != null)
        bytes = builder.Configuration["Authentication:secretKey"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:issuer"],
        ValidAudience = builder.Configuration["Authentication:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bytes == null ? "" : bytes)),
        RequireSignedTokens = true,
    };

    // 🔹 habilitar auth vía query string (SignalR lo usa en websockets)
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // Si la petición es a tu Hub de SignalR
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/hubs/cloth"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };

});

// Configuración de CORS
var frontendUrl = builder.Configuration.GetValue<string>("FRONTEND_URL");
if (frontendUrl == null)
    frontendUrl = "http://localhost:4200";

builder.Services.AddCors(op =>
{
    op.AddPolicy("angularApp", builder =>
    {
        builder.WithOrigins([frontendUrl])
         .SetIsOriginAllowedToAllowWildcardSubdomains()
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials();
    });
});


// Agregar SignalR
builder.Services.AddSignalR(options =>
{
    // ⏱ cuánto tiempo espera el cliente antes de marcar timeout
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);

    // 📡 cada cuánto el servidor envía un "ping" automático
    options.KeepAliveInterval = TimeSpan.FromSeconds(10);

    // (opcional) tamaño máximo de mensajes si envías payloads grandes
    options.MaximumReceiveMessageSize = 1024 * 1024; // 1 MB
})
.AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Solo para desarrollo, muestra detalles al cliente
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    // En producción, usa un manejador de excepciones más seguro
    app.UseExceptionHandler("/Error"); // Configura una ruta o middleware para manejar excepciones
    // Puedes añadir un middleware personalizado aquí antes o después de UseExceptionHandler
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("angularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
