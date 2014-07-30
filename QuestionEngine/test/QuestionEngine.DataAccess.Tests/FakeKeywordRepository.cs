using System;
using Keywords.Domain;

namespace Keywords.DataAccess.Tests
{
    public class FakeKeywordRepository : IKeywordRepository
    {
        private Keyword _keyword;
        private Region _region;
        private bool _broken;

        public void AddFakeKeyword(Keyword keyword)
        {
            _keyword = keyword;
        }
        
        public Keyword GetKeywordByTextAndCountry(string text, int countryId)
        {
            return _keyword;
        }

        public Keyword GetKeywordById(int keywordId)
        {
            return _keyword;
        }

        public Region GetKeywordByText(string text, int countryId)
        {
            return _region;
        }

        public Region GetRegionById(int regionId)
        {
            return _region;
        }

        public void Heartbeat()
        {
            if (_broken)
            {
                throw new Exception("Repository is broken");
            }
        }

        public int GetKeywordIdByText(string text)
        {
            return _keyword.Id;
        }

        public void ConfigureRepositoryToFail()
        {
            _broken = true;
        }

        public void AddFakeRegion(Region region)
        {
            _region = region;
        }
    }
}