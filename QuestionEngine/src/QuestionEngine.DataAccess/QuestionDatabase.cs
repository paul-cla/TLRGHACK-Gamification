using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
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
            var connection = new SqlCeConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
