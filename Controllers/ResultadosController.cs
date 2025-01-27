using Microsoft.AspNetCore.Mvc;
using RuletaApi.Data;
using Microsoft.Data.SqlClient;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class ResultadosController : ControllerBase
{
    private readonly ConnectionDb _dbHelper;

    // Este constructor se usa para conectar con la base de datos.
    public ResultadosController(ConnectionDb dbHelper)
    {
        _dbHelper = dbHelper;
    }

    [HttpPost("cerrar/{id}")]
    public IActionResult CerrarApuestas(int id)
    {
        // Este método cierra las apuestas para una ruleta específica (identificada por 'id').
        // Primero, obtiene todas las apuestas asociadas a esa ruleta desde la base de datos.
        string query = "SELECT * FROM Apuestas WHERE ReglaId = @Id";
        var parameters = new SqlParameter[]
        {
        new SqlParameter("@Id", id)
        };

        var apuestas = _dbHelper.ExecuteQuery(query, parameters);

        // Luego, el sistema genera un número ganador aleatorio entre 0 y 36.
        var numeroGanador = new Random().Next(0, 37);

        // El color ganador se determina en función del número: par = "Rojo", impar = "Negro".
        string colorGanador;

        
        if (numeroGanador % 2 == 0)
        {
            colorGanador = "Rojo";
        }
        else
        {
            colorGanador = "Negro";
        }

        // Aquí se sumarán los montos ganados por los usuarios
        decimal totalMontoGanado = 0;
        var resultadosApuestas = new List<object>();

        // Para cada apuesta realizada, se evalúa si fue ganadora o no.
        foreach (DataRow row in apuestas.Rows)
        {
            var usuarioId = Convert.ToInt32(row["UsuarioId"]);
            var tipoApuesta = row["TipoApuesta"].ToString();
            var valorApuesta = row["ValorApuesta"].ToString();
            var monto = Convert.ToDecimal(row["Monto"]);
            decimal montoGanado = 0;


            if (tipoApuesta == "Numero" && valorApuesta == numeroGanador.ToString())
            {
                // Si la apuesta es al número y coincide con el número ganador, se multiplica el monto por 5.
                montoGanado = monto * 5; // Paga 5x para apuestas al número
            }
            // Si la apuesta es al color y coincide con el color ganador, se multiplica el monto por 1.8.
            else if (tipoApuesta == "Color" && valorApuesta == colorGanador)
            {
                montoGanado = monto * 1.8m; // Paga 1.8x para apuestas al color
            }

            // Se suma el monto ganado al total.
            totalMontoGanado += montoGanado;

            // Se actualiza el crédito del usuario con el monto ganado.
            string updateCreditoQuery = "UPDATE Usuarios SET Credito = Credito + @MontoGanado WHERE Id = @UsuarioId";
            var updateCreditoParams = new SqlParameter[]
            {
            new SqlParameter("@MontoGanado", montoGanado),
            new SqlParameter("@UsuarioId", usuarioId)
            };
            _dbHelper.ExecuteNonQuery(updateCreditoQuery, updateCreditoParams);

            // Se guarda un detalle de la apuesta para mostrarlo al final.
            resultadosApuestas.Add(new
            {
                UsuarioId = usuarioId,
                TipoApuesta = tipoApuesta,
                ValorApuesta = valorApuesta,
                MontoApostado = monto,
                MontoGanado = montoGanado
            });
        }

        // Finalmente, se guarda el resultado de la ruleta con el número ganador, el color ganador y el total ganado.
        string resultadoQuery = "INSERT INTO Resultados (ReglaId, NumeroGanador, ColorGanador, MontoGanado) VALUES (@ReglaId, @NumeroGanador, @ColorGanador, @MontoGanado)";
        var resultadoParams = new SqlParameter[]
        {
        new SqlParameter("@ReglaId", id),
        new SqlParameter("@NumeroGanador", numeroGanador),
        new SqlParameter("@ColorGanador", colorGanador),
        new SqlParameter("@MontoGanado", totalMontoGanado)
        };
        _dbHelper.ExecuteNonQuery(resultadoQuery, resultadoParams);

        // Al final, se devuelve el número ganador, el color ganador, el total ganado y los detalles de las apuestas.
        return Ok(new
        {
            NumeroGanador = numeroGanador,
            ColorGanador = colorGanador,
            TotalMontoGanado = totalMontoGanado,
            DetalleApuestas = resultadosApuestas
        });
    }
}

