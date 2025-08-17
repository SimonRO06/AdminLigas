using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Shared.Helpers
{
    public static class DbHelper
    {
        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connStr = config.GetConnectionString("torneosdb");
            if (string.IsNullOrEmpty(connStr))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'torneosdb'.");
            }
            return connStr;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }
    }
}
