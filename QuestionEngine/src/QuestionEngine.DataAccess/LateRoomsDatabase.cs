using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.DataAccess.Dapper;

namespace Keywords.DataAccess
{
    public class LateRoomsDatabase : IDatabase
    {
        private readonly string _connectionString;

        public LateRoomsDatabase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Keywords.API.Data"].ConnectionString;
        }

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
