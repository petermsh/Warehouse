using System.Data;
using Npgsql;

namespace TaskApp;

internal sealed class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetSection("ConnectionString").Value;
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}