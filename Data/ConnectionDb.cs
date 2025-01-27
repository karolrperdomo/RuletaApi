using System.Data;
using Microsoft.Data.SqlClient;

namespace RuletaApi.Data;
public class ConnectionDb
{
    private readonly string _connectionString;

    // El constructor obtiene la cadena de conexión desde la configuración del sistema.
    public ConnectionDb(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("conexionmaestra")
                            ?? throw new InvalidOperationException("La cadena de conexión 'conexionmaestra' no está configurada.");
    }

    // Este método devuelve una nueva conexión a la base de datos usando la cadena de conexión.
    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    // Este método ejecuta una consulta en la base de datos que no devuelve ningún resultado, como una actualización.
    public void ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                // Si se pasan parámetros, se agregan a la consulta.
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                // Se abre la conexión a la base de datos y se ejecuta la consulta.
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    // Este método ejecuta una consulta que devuelve un único valor (como una suma o un valor específico).
    public object ExecuteScalar(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                // Si se pasan parámetros, se agregan a la consulta.
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                // Se abre la conexión a la base de datos y se ejecuta la consulta.
                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
    // Este método ejecuta una consulta que devuelve un conjunto de datos (varias filas y columnas).
    public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                // Si se pasan parámetros, se agregan a la consulta.
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                // Se abre la conexión a la base de datos y se ejecuta la consulta.
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader); // Se cargan los resultados de la consulta en una tabla de datos.
                    return dataTable;
                }
            }
        }
    }
}
