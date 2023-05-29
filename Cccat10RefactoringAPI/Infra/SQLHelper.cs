using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace Cccat10RefactoringAPI.Infra;

public static class SQLHelper
{
    public static string ConnectionString { get; set; } = null!;

    public static DbConnection GetConnection()
    {
        return new SqliteConnection(ConnectionString);
    }
}
