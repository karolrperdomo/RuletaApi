# Prueba-Ruleta
El sistema gestiona ruletas con endpoints para crear nuevas ruletas, abrirlas para permitir apuestas, realizar apuestas a números (0-36) o colores (rojo/negro) con validación de saldo y cierre de apuestas. Al cerrar, se selecciona un número ganador, pagando 5x en apuestas numéricas acertadas y 1.8x en color, perdiendo las demás.

## Tecnologías usadas
Lenguaje: C#

Framework: ASP.NET Core

Base de Datos: SQL Server

Sin uso de migraciones (consultas SQL directas)

Gestor de dependencias: NuGet

## Endpoints Principales
1. Creación de nuevas ruletas
   https://localhost:7113/api/reglas/crear
   
3. Apertura de ruleta
   https://localhost:7113/api/reglas/abrir/{id}
   
5. Realizar apuesta
   https://localhost:7113/api/apuestas/apostar/{id}
   
   Headers
   
   Key usuarioId
   
   Value 1
   
7. Cierre de ruleta
   https://localhost:7113/api/resultados/cerrar/{id}

## Instrucciones de Uso 

1. Instalación y configuración: 
Clona el repositorio: https://github.com/karolrperdomo/RuletaApi.git
cd RuletaApi

2. Configurar la base de datos

- Crear una base de datos SQL Server utilizando el script proporcionado en el archivo schema.sql.
  
- Actualizar la cadena de conexión en el archivo appsettings.json si es con usuario y contraseña:
  
  "ConnectionStrings": {
    "conexionmaestra": "Server=<tu_servidor>;Database=Ruleta;User Id=<tu_usuario>;Password=<tu_contraseña>;" 
  }
- Actualizar la cadena de conexión en el archivo appsettings.json sin usuario y contraseña:
  
  "ConnectionStrings": {
  "conexionmaestra": "Server=<tu_servidor>;Database=Ruleta;Integrated Security=True;TrustServerCertificate=True;"
},

3. Ejecutar la API:
   
  dotnet run por defecto se corre en el puerto 5200 y con el protocolo HTTP

  dotnet run --launch-profile https Corre en el protocolo HTTPS puerto 7113
  

## Pruebas Manuales

Usa herramientas como Postman o Swagger
 
