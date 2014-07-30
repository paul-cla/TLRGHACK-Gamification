using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Infrastructure.DataAccess.Dapper;
using QuestionEngine.Domain;

namespace QuestionEngine.DataAccess
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDatabase _database;
        private readonly ObjectCache _cache = MemoryCache.Default;

        public QuestionRepository(IDatabase database)
        {
            _database = database;
        }

        public Question GetQuestion(int questionId)
        {
            var cacheKey = "GetQuestion" + questionId;
            return _cache.GetOrAdd(cacheKey, () => GetQuestionFromRepo(questionId), new CacheItemPolicy());
        }


//        private Keyword GetKeywordOrDefaultByTextAndCountry(string text, int countryId)
//        {
//            const string query = @"SELECT TOP 1 k.id AS KeywordID,  k.[text] AS KeywordText, a.name AS AreaName, a2.Name AS Country, k.AreaID, k.FriendlyText FROM laterooms.Area a ( NOLOCK )
//                                    INNER JOIN laterooms.area a2 ( NOLOCK ) ON a.CountryID = a2.id
//                                    INNER JOIN laterooms.Keyword k ( NOLOCK ) ON K.AreaID = a.ID 
//                                    WHERE k.[Text] = @Text
//                                    AND (a.CountryID = @CountryId OR a2.ParentID = @CountryId)
//                                    ORDER BY k.SearchCount DESC, k.ID ASC";

//            using (var connection = _database.OpenConnection())
//            {
//                var data = connection.Query<Keyword>(query, new { Text = text, CountryId = countryId });
//                var keywordData = data as Keyword[] ?? data.ToArray();
//                return keywordData.Any() ? keywordData.First() : new Keyword(0, string.Empty, string.Empty, string.Empty, 0, string.Empty);
//            }
//        }

//        private Keyword GetKeywordOrDefaultById(int keywordId)
//        {
//            const string query = @"SELECT TOP 1 k.id AS KeywordID,  k.[text] AS KeywordText, a.name AS AreaName, a2.Name AS Country, k.AreaID, k.FriendlyText FROM laterooms.Area a ( NOLOCK )
//                                    INNER JOIN laterooms.area a2 ( NOLOCK ) ON a.CountryID = a2.id
//                                    INNER JOIN laterooms.Keyword k ( NOLOCK ) ON K.AreaID = a.ID 
//                                    WHERE k.id = @KeywordId
//                                    ORDER BY k.SearchCount DESC, k.ID ASC";

//            using (var connection = _database.OpenConnection())
//            {
//                var data = connection.Query<Keyword>(query, new { KeywordId = keywordId });
//                var keywordData = data as Keyword[] ?? data.ToArray();
//                return keywordData.Any() ? keywordData.First() : new Keyword(0, string.Empty, string.Empty, string.Empty, 0, string.Empty);
//            }
//        }
        
//        private Region GetRegionOrDefaultByText(string text, int countryId)
//        {
//            const string query = @"SELECT TOP 1 id as RegionId, Name AS RegionText, FriendlyName AS FriendlyText FROM laterooms.Area ( NOLOCK )
//                                    WHERE [Name] = @Text and ([CountryID] = @CountryId or [ID] = @CountryId)
//                                    ORDER BY ID";

//            using (var connection = _database.OpenConnection())
//            {
//                var data = connection.Query<Region>(query, new { Text = text, CountryId = countryId });
//                var regionData = data as Region[] ?? data.ToArray();
//                return regionData.Any() ? regionData.First() : new Region(0, string.Empty, string.Empty);
//            }
//        }

//        private Region GetRegionOrDefaultById(int regionId)
//        {
//            const string query = @"SELECT TOP 1 id as RegionId, Name AS RegionText, FriendlyName AS FriendlyText FROM laterooms.Area ( NOLOCK )
//                                    WHERE [id] = @RegionId";

//            using (var connection = _database.OpenConnection())
//            {
//                var data = connection.Query<Region>(query, new { RegionId = regionId });
//                var regionData = data as Region[] ?? data.ToArray();
//                return regionData.Any() ? regionData.First() : new Region(0, string.Empty, string.Empty);
//            }
//        }

        public Question GetQuestionFromRepo(int questionId)
        {
//            const string query = @"SELECT TOP 1 id as RegionId, Name AS RegionText, FriendlyName AS FriendlyText FROM laterooms.Area ( NOLOCK )
//                                    WHERE [id] = @QuestionId";

//            using (var connection = _database.OpenConnection())
//            {
//                var data = connection.Query<Question>(query, new { QuestionId = questionId });
//                var regionData = data as Question[] ?? data.ToArray();
//                return regionData.Any() ? regionData.First() : new Question(0, string.Empty, null);
//            }

            return new Question(1, "Question 1", new List<Answer>());
        }
    }
}
