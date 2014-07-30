using System;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using Infrastructure.DataAccess.Dapper;
using Keywords.Domain;

namespace Keywords.DataAccess
{
    public class KeywordRepository : IKeywordRepository
    {
        private readonly IDatabase _database;
        private readonly ObjectCache _cache = MemoryCache.Default;

        public KeywordRepository(IDatabase database)
        {
            _database = database;
        }

        public Keyword GetKeywordByTextAndCountry(string text, int countryId)
        {
            var cacheKey = "GetKeywordOrDefaultByTextAndCountry" + text + countryId;
            return _cache.GetOrAdd(cacheKey, () => GetKeywordOrDefaultByTextAndCountry(text, countryId), new CacheItemPolicy());
        }

        public Keyword GetKeywordById(int keywordId)
        {
            var cacheKey = "GetKeywordOrDefaultById" + keywordId;
            return _cache.GetOrAdd(cacheKey, () => GetKeywordOrDefaultById(keywordId), new CacheItemPolicy());
        }

        public Region GetKeywordByText(string text, int countryId)
        {
            var cacheKey = "GetRegionOrDefaultByTextAndCountry" + text;
            return _cache.GetOrAdd(cacheKey, () => GetRegionOrDefaultByText(text, countryId), new CacheItemPolicy());
        }

        public Region GetRegionById(int regionId)
        {
            var cacheKey = "GetRegionOrDefaultById" + regionId;
            return _cache.GetOrAdd(cacheKey, () => GetRegionOrDefaultById(regionId), new CacheItemPolicy());
        }

        public void Heartbeat()
        {
            var result = _database.QueryStoredProc<int>("laterooms.dbo.AmAlive", IsolationLevel.ReadUncommitted);
            var enumerable = result as int[] ?? result.ToArray();
            if (!enumerable.Any() || enumerable.First() != 1)
            {
                throw new Exception("Database not alive");
            }
        }

        private Keyword GetKeywordOrDefaultByTextAndCountry(string text, int countryId)
        {
            const string query = @"SELECT TOP 1 k.id AS KeywordID,  k.[text] AS KeywordText, a.name AS AreaName, a2.Name AS Country, k.AreaID, k.FriendlyText FROM laterooms.Area a ( NOLOCK )
                                    INNER JOIN laterooms.area a2 ( NOLOCK ) ON a.CountryID = a2.id
                                    INNER JOIN laterooms.Keyword k ( NOLOCK ) ON K.AreaID = a.ID 
                                    WHERE k.[Text] = @Text
                                    AND (a.CountryID = @CountryId OR a2.ParentID = @CountryId)
                                    ORDER BY k.SearchCount DESC, k.ID ASC";

            using (var connection = _database.OpenConnection())
            {
                var data = connection.Query<Keyword>(query, new { Text = text, CountryId = countryId });
                var keywordData = data as Keyword[] ?? data.ToArray();
                return keywordData.Any() ? keywordData.First() : new Keyword(0, string.Empty, string.Empty, string.Empty, 0, string.Empty);
            }
        }

        private Keyword GetKeywordOrDefaultById(int keywordId)
        {
            const string query = @"SELECT TOP 1 k.id AS KeywordID,  k.[text] AS KeywordText, a.name AS AreaName, a2.Name AS Country, k.AreaID, k.FriendlyText FROM laterooms.Area a ( NOLOCK )
                                    INNER JOIN laterooms.area a2 ( NOLOCK ) ON a.CountryID = a2.id
                                    INNER JOIN laterooms.Keyword k ( NOLOCK ) ON K.AreaID = a.ID 
                                    WHERE k.id = @KeywordId
                                    ORDER BY k.SearchCount DESC, k.ID ASC";

            using (var connection = _database.OpenConnection())
            {
                var data = connection.Query<Keyword>(query, new { KeywordId = keywordId });
                var keywordData = data as Keyword[] ?? data.ToArray();
                return keywordData.Any() ? keywordData.First() : new Keyword(0, string.Empty, string.Empty, string.Empty, 0, string.Empty);
            }
        }
        
        private Region GetRegionOrDefaultByText(string text, int countryId)
        {
            const string query = @"SELECT TOP 1 id as RegionId, Name AS RegionText, FriendlyName AS FriendlyText FROM laterooms.Area ( NOLOCK )
                                    WHERE [Name] = @Text and ([CountryID] = @CountryId or [ID] = @CountryId)
                                    ORDER BY ID";

            using (var connection = _database.OpenConnection())
            {
                var data = connection.Query<Region>(query, new { Text = text, CountryId = countryId });
                var regionData = data as Region[] ?? data.ToArray();
                return regionData.Any() ? regionData.First() : new Region(0, string.Empty, string.Empty);
            }
        }

        private Region GetRegionOrDefaultById(int regionId)
        {
            const string query = @"SELECT TOP 1 id as RegionId, Name AS RegionText, FriendlyName AS FriendlyText FROM laterooms.Area ( NOLOCK )
                                    WHERE [id] = @RegionId";

            using (var connection = _database.OpenConnection())
            {
                var data = connection.Query<Region>(query, new { RegionId = regionId });
                var regionData = data as Region[] ?? data.ToArray();
                return regionData.Any() ? regionData.First() : new Region(0, string.Empty, string.Empty);
            }
        }
    }
}
