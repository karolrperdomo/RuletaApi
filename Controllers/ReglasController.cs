using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RuletaApi.Data;

[ApiController]
[Route("api/[controller]")]
public class ReglasController : ControllerBase
{
    private readonly ConnectionDb _dbHelper;

    public ReglasController(ConnectionDb dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpPost("crear")]
    public IActionResult CrearRegla()
    {
        string query = "INSERT INTO Reglas (Activa, FechaCreacion) OUTPUT INSERTED.Id VALUES (0, @FechaCreacion)";
        var parameters = new SqlParameter[]
        {
            new SqlParameter("@FechaCreacion", DateTime.UtcNow)
        };

        var id = _dbHelper.ExecuteScalar(query, parameters);

        return Ok(new { id });
    }

    [HttpPost("abrir/{id}")]
    public IActionResult AbrirRegla(int id)
    {
        string query = "UPDATE Reglas SET Activa = 1 WHERE Id = @Id";
        var parameters = new SqlParameter[]
        {
            new SqlParameter("@Id", id)
        };

        _dbHelper.ExecuteNonQuery(query, parameters);

        return Ok(new { estado = "Ruleta abierta con éxito" });
    }

    [HttpGet("color/{numero}")]
    public IActionResult ObtenerColorGanador(int numero)
    {
        
        string color = (numero % 2 == 0) ? "Rojo" : "Negro";

        return Ok(new { numero, color });
    }
}
