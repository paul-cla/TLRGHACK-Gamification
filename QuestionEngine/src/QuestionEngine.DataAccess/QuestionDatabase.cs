using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.DataAccess.Dapper;

namespace QuestionEngine.DataAccess
{
    public class QuestionDatabase : IDatabase
    {
        private readonly string _connectionString;

        public QuestionDatabase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["QuestionEngine.API.Data"].ConnectionString;
        }

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
