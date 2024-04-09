using System.Configuration;
namespace VoenMedLibrary.DataAccess;

public static class DataAccessHelpers
{
    internal static string LoadConnectionString(string id = "Default")
    {
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }
}