using MySql.Data.MySqlClient;

/*
 * This class creates connection to mysql server.
 * If you create mysql server with docker-compose in this proj,
 * you don't need to change properties below
 */
namespace supermarket.Connections
{
    public class Db
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "supermarket";
            string username = "supermarket";
            string password = "S8p3Rm0rq3d";

            string connectionString = string.Format(
                "Server={0};Database={1};port={2};User Id={3};password={4}",
                host, database, port, username, password);

            return new MySqlConnection(connectionString);
        }
    }
}