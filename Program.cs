using Microsoft.Data.SqlClient;
using RuletaApi.Data;


var builder = WebApplication.CreateBuilder(args);

// Obtiene la cadena de conexión 'conexionmaestra' desde el archivo de configuración (appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("conexionmaestra");

// Registra una instancia única (singleton) de SqlConnection en los servicios de la aplicación
builder.Services.AddSingleton(new SqlConnection(connectionString));

// Registra la clase 'ConnectionDb' para ser inyectada en los controladores
builder.Services.AddScoped<ConnectionDb>();

// Añade soporte para controladores (API)
builder.Services.AddControllers();

// Habilita la generación de documentación de la API y la exploración de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Si la aplicación está en desarrollo, habilita Swagger para documentación interactiva
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige todas las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

// Mapea los controladores definidos en la aplicación a las rutas de la API
app.MapControllers();

// Ejecuta la aplicación
app.Run();
