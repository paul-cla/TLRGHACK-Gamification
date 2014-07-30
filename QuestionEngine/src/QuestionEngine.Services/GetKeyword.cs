using System;
using Keywords.Domain;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.Services
{
    public class GetKeyword : IGetKeyword
    {
        private readonly IKeywordRepository _keywordRepository;

        public GetKeyword(IKeywordRepository keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }

        public Keyword GetKeywordByTextAndCountry(string text, int countryId)
        {
            return _keywordRepository.GetKeywordByTextAndCountry(text, countryId);
        }

        public Keyword GetKeywordById(int keywordId)
        {
            return _keywordRepository.GetKeywordById(keywordId);
        }

        public Region GetRegionByText(string text, int countryId)
        {
            return _keywordRepository.GetKeywordByText(text, countryId);
        }

        public Region GetRegionById(int regionId)
        {
            return _keywordRepository.GetRegionById(regionId);
        }

        public IStatus Status()
        {
            try
            {
                _keywordRepository.Heartbeat();
            }
            catch (Exception exception)
            {
                throw new WebApiException(5001, exception.Message);
            }
            return new GoodStatus();
        }
    }
}
