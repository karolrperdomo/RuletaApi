using System.Data;
using Microsoft.Data.SqlClient;

namespace RuletaApi.Data;
public class ConnectionDb
{
    private readonly string _connectionString;

    public ConnectionDb(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("conexionmaestra")
                            ?? throw new InvalidOperationException("La cadena de conexión 'conexionmaestra' no está configurada.");
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public void ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public object ExecuteScalar(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }

    public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
    {
        using (var connection = GetConnection())
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
    }
}
