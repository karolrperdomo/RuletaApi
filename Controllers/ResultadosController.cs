using Microsoft.AspNetCore.Mvc;
using RuletaApi.Data;
using Microsoft.Data.SqlClient;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class ResultadosController : ControllerBase
{
    private readonly ConnectionDb _dbHelper;

    public ResultadosController(ConnectionDb dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpPost("cerrar/{id}")]
    public IActionResult CerrarApuestas(int id)
    {
        string query = "SELECT * FROM Apuestas WHERE ReglaId = @Id";
        var parameters = new SqlParameter[]
        {
        new SqlParameter("@Id", id)
        };

        var apuestas = _dbHelper.ExecuteQuery(query, parameters);

        var numeroGanador = new Random().Next(0, 37);
        string colorGanador;

        
        if (numeroGanador % 2 == 0)
        {
            colorGanador = "Rojo";
        }
        else
        {
            colorGanador = "Negro";
        }

        decimal totalMontoGanado = 0;
        var resultadosApuestas = new List<object>();

        foreach (DataRow row in apuestas.Rows)
        {
            var usuarioId = Convert.ToInt32(row["UsuarioId"]);
            var tipoApuesta = row["TipoApuesta"].ToString();
            var valorApuesta = row["ValorApuesta"].ToString();
            var monto = Convert.ToDecimal(row["Monto"]);
            decimal montoGanado = 0;

            if (tipoApuesta == "Numero" && valorApuesta == numeroGanador.ToString())
            {
                montoGanado = monto * 5; // Paga 5x para apuestas al número
            }
            else if (tipoApuesta == "Color" && valorApuesta == colorGanador)
            {
                montoGanado = monto * 1.8m; // Paga 1.8x para apuestas al color
            }

            totalMontoGanado += montoGanado;

            string updateCreditoQuery = "UPDATE Usuarios SET Credito = Credito + @MontoGanado WHERE Id = @UsuarioId";
            var updateCreditoParams = new SqlParameter[]
            {
            new SqlParameter("@MontoGanado", montoGanado),
            new SqlParameter("@UsuarioId", usuarioId)
            };
            _dbHelper.ExecuteNonQuery(updateCreditoQuery, updateCreditoParams);

            resultadosApuestas.Add(new
            {
                UsuarioId = usuarioId,
                TipoApuesta = tipoApuesta,
                ValorApuesta = valorApuesta,
                MontoApostado = monto,
                MontoGanado = montoGanado
            });
        }

        
        string resultadoQuery = "INSERT INTO Resultados (ReglaId, NumeroGanador, ColorGanador, MontoGanado) VALUES (@ReglaId, @NumeroGanador, @ColorGanador, @MontoGanado)";
        var resultadoParams = new SqlParameter[]
        {
        new SqlParameter("@ReglaId", id),
        new SqlParameter("@NumeroGanador", numeroGanador),
        new SqlParameter("@ColorGanador", colorGanador),
        new SqlParameter("@MontoGanado", totalMontoGanado)
        };
        _dbHelper.ExecuteNonQuery(resultadoQuery, resultadoParams);

        return Ok(new
        {
            NumeroGanador = numeroGanador,
            ColorGanador = colorGanador,
            TotalMontoGanado = totalMontoGanado,
            DetalleApuestas = resultadosApuestas
        });
    }
}

