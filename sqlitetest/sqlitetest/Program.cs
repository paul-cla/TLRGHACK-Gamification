using System;
using System.Data.SQLite;
using System.IO;

namespace sqlitetest
{
    internal class Program
    {
        private const string DATABASE_NAME = "questions.db";

        private static void Main(string[] args)
        {
            if (!File.Exists(DATABASE_NAME))
            {
                CreateDatabase();
            }

            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Questions", connection))
                {
                    Console.WriteLine(command.ExecuteScalar());
                }
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Answers", connection))
                {
                    Console.WriteLine(command.ExecuteScalar());
                }
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Answers WHERE QuestionID = 0", connection))
                {
                    Console.WriteLine(command.ExecuteScalar());
                }
            }
            Console.ReadLine();
        }

        private static string GetConnectionString()
        {
            return string.Format("Data Source={0};Version=3;", DATABASE_NAME);
        }

        private static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(DATABASE_NAME);

            CreateQuestionTable();
            CreateAnswerTable();

            PopulateQuestionTable();
            PopulateAnswerTable();
        }

        private static void CreateQuestionTable()
        {
            const string CREATE_QUESTION_TABLE = "CREATE TABLE Questions (ID INT, Text VARCHAR(1000))";

            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SQLiteCommand(CREATE_QUESTION_TABLE, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void PopulateQuestionTable()
        {
            const string POPULATE_QUESTION_TABLE_FILE = "PopulateQuestionTable.sql";

            string data = File.ReadAllText(POPULATE_QUESTION_TABLE_FILE);

            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SQLiteCommand(data, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void CreateAnswerTable()
        {
            const string CREATE_ANSWER_TABLE =
                "CREATE TABLE Answers (ID INT, QuestionID INT, Text VARCHAR(1000), IsCorrect BIT, NextUrl VARCHAR(1000))";

            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SQLiteCommand(CREATE_ANSWER_TABLE, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void PopulateAnswerTable()
        {
            const string POPULATE_ANSWER_TABLE_FILE = "PopulateAnswerTable.sql";

            string data = File.ReadAllText(POPULATE_ANSWER_TABLE_FILE);

            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                connection.Open();
                using (var command = new SQLiteCommand(data, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}