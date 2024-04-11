using Dapper;
using System.Data;
using System.Data.SQLite;

namespace VoenMedLibrary.DataAccess;

public static class SqliteDataAccess
{
    public static List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
    {
        DynamicParameters p = parameters.ToDynamicParameters();
        parameters.ToList().ForEach(x => p.Add(x.Key, x.Value));
        using IDbConnection cnn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName));
        var rows = cnn.Query<T>(sqlStatement, p);

        return rows.ToList();
    }

    public static void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
    {
        DynamicParameters p = parameters.ToDynamicParameters();
        using IDbConnection cnn = new SQLiteConnection(DataAccessHelpers.LoadConnectionString(connectionName));
        cnn.Execute(sqlStatement, p);
    }

    private static DynamicParameters ToDynamicParameters(this Dictionary<string, object> p)
    {
        DynamicParameters output = new DynamicParameters();
        p.ToList().ForEach(x => output.Add(x.Key, x.Value));

        return output;
    }
}