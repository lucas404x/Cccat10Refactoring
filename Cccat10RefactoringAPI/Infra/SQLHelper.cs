using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace Cccat10RefactoringAPI.Infra;

public static class SQLHelper
{
    public static string ConnectionString { get; set; } = null!;

    public static async Task<DbConnection> GetConnection(bool openConnection = true)
    {
        var connection = new SqliteConnection(ConnectionString);
        if (openConnection)
        {
            await connection.OpenAsync();
        }
        return connection;
    }
}
