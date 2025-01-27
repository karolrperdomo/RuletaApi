using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RuletaApi.Data;


[ApiController]
[Route("api/[controller]")]
public class ApuestasController : ControllerBase
{
    private readonly ConnectionDb _dbHelper;

    // Controlador para gestionar las apuestas realizadas por los usuarios.
    public ApuestasController(ConnectionDb dbHelper)
    {
        _dbHelper = dbHelper;
    }

    // Endpoint para realizar una apuesta en la ruleta.
    
    
    [HttpPost("apostar/{id}")] // El identificador de la regla de la ruleta sobre la que se apuesta

    // "apuesta"Objeto que contiene los detalles de la apuesta
    // "usuarioId" Identificador del usuario que realiza la apuesta 
    public IActionResult Apostar(int id, [FromBody] Apuesta apuesta, [FromHeader] int usuarioId) 
    {
        // Validación del monto máximo permitido para la apuesta.
        if (apuesta.Monto > 10000)
        {
            return BadRequest("El monto máximo permitido es de 10,000 dólares.");
        }
        // Validación del tipo de apuesta.
        if (apuesta.TipoApuesta != "Numero" && apuesta.TipoApuesta != "Color")
        {
            return BadRequest("El tipo de apuesta debe ser 'Numero' o 'Color'.");
        }
        // Validación adicional para apuestas de tipo 'Numero'
        if (apuesta.TipoApuesta == "Numero")
        {
            if (!int.TryParse(apuesta.ValorApuesta, out int valorNumerico))
            {
                return BadRequest("El valor de la apuesta debe ser un número válido.");
            }

            if (valorNumerico < 0 || valorNumerico > 36)
            {
                return BadRequest("El valor de la apuesta debe ser un número entre 0 y 36.");
            }
        }
        // Validación adicional para apuestas de tipo 'Color'.
        else if (apuesta.TipoApuesta == "Color")
        {
            if (apuesta.ValorApuesta != "Rojo" && apuesta.ValorApuesta != "Negro")
            {
                return BadRequest("El valor de la apuesta debe ser 'Rojo' o 'Negro'.");
            }
        }

        // Verificación del crédito disponible del usuario antes de aceptar la apuesta.
        string creditoQuery = "SELECT Credito FROM Usuarios WHERE Id = @UsuarioId";
        var creditoParameters = new SqlParameter[]
        {
        new SqlParameter("@UsuarioId", usuarioId)
        };

        var credito = Convert.ToDecimal(_dbHelper.ExecuteScalar(creditoQuery, creditoParameters));
        if (credito < apuesta.Monto)
        {
            return BadRequest("Crédito insuficiente para realizar la apuesta.");
        }

        // Inserción de la apuesta en la base de datos.
        string query = "INSERT INTO Apuestas (UsuarioId, ReglaId, TipoApuesta, ValorApuesta, Monto, FechaApuesta) VALUES (@UsuarioId, @ReglaId, @TipoApuesta, @ValorApuesta, @Monto, @FechaApuesta)";
        var parameters = new SqlParameter[]
        {
        new SqlParameter("@UsuarioId", usuarioId),
        new SqlParameter("@ReglaId", id),
        new SqlParameter("@TipoApuesta", apuesta.TipoApuesta),
        new SqlParameter("@ValorApuesta", apuesta.ValorApuesta),
        new SqlParameter("@Monto", apuesta.Monto),
        new SqlParameter("@FechaApuesta", DateTime.UtcNow)
        };

        _dbHelper.ExecuteNonQuery(query, parameters);

        return Ok(new { estado = "Apuesta realizada con éxito" });
    }
}