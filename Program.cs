using Microsoft.Data.SqlClient;
using RuletaApi.Data;


var builder = WebApplication.CreateBuilder(args);

// Obtiene la cadena de conexi�n 'conexionmaestra' desde el archivo de configuraci�n (appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("conexionmaestra");

// Registra una instancia �nica (singleton) de SqlConnection en los servicios de la aplicaci�n
builder.Services.AddSingleton(new SqlConnection(connectionString));

// Registra la clase 'ConnectionDb' para ser inyectada en los controladores
builder.Services.AddScoped<ConnectionDb>();

// A�ade soporte para controladores (API)
builder.Services.AddControllers();

// Habilita la generaci�n de documentaci�n de la API y la exploraci�n de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Si la aplicaci�n est� en desarrollo, habilita Swagger para documentaci�n interactiva
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige todas las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

// Mapea los controladores definidos en la aplicaci�n a las rutas de la API
app.MapControllers();

// Ejecuta la aplicaci�n
app.Run();
