using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RuletaApi.Data;

[ApiController]
[Route("api/[controller]")]
public class ReglasController : ControllerBase
{
    private readonly ConnectionDb _dbHelper;

    // El constructor se usa para conectar con la base de datos.
    public ReglasController(ConnectionDb dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpPost("crear")]
    public IActionResult CrearRegla()
    {
        // Este método crea una nueva regla en el sistema y la marca como no activa al principio.
        string query = "INSERT INTO Reglas (Activa, FechaCreacion) OUTPUT INSERTED.Id VALUES (0, @FechaCreacion)";
        var parameters = new SqlParameter[]
        {
            new SqlParameter("@FechaCreacion", DateTime.UtcNow)
        };

        // Se ejecuta la acción de crear la regla y se obtiene el ID de la nueva regla.
        var id = _dbHelper.ExecuteScalar(query, parameters);

        // Se devuelve el ID de la nueva regla para que el usuario lo vea.
        return Ok(new { id });
    }

    [HttpPost("abrir/{id}")]
    public IActionResult AbrirRegla(int id)
    {
        // Este método cambia el estado de la regla para activarla (es decir, hacerla "abierta").
        string query = "UPDATE Reglas SET Activa = 1 WHERE Id = @Id";
        var parameters = new SqlParameter[]
        {
            new SqlParameter("@Id", id)
        };

        // Se actualiza la regla en la base de datos para marcarla como abierta.
        _dbHelper.ExecuteNonQuery(query, parameters);

        // Se envía un mensaje diciendo que la ruleta está abierta y lista para usarse.
        return Ok(new { estado = "Ruleta abierta con éxito" });
    }

    [HttpGet("color/{numero}")]
    public IActionResult ObtenerColorGanador(int numero)
    {

        // Este método determina el color ganador dependiendo del número que sale:
        // Si el número es par, el color es rojo, y si es impar, el color es negro.

        string color = (numero % 2 == 0) ? "Rojo" : "Negro";

        // Se devuelve el número y el color correspondiente para mostrar al usuario.
        return Ok(new { numero, color });
    }
}
