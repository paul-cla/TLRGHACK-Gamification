using System.Web.Http;
using Keywords.API.Support;
using Keywords.Domain;
using Keywords.Services;
using WebAPI.OutputCache;

namespace Keywords.API.Controllers
{
    public class KeywordController : ApiController
    {
        private readonly IGetKeyword _keywordService;

        public KeywordController(IGetKeyword keywordService)
        {
            _keywordService = keywordService;
        }

        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public Keyword GetKeywordByTextAndCountry(string text, int countryId)
        {
            return _keywordService.GetKeywordByTextAndCountry(StringHelper.DecodeAmpersands(text), countryId);
        }


        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public Keyword GetKeywordById(int keywordId)
        {
            return _keywordService.GetKeywordById(keywordId);
        }
    }
}
