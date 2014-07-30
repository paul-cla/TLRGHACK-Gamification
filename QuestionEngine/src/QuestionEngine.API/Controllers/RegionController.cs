using System.Web.Http;
using Keywords.API.Support;
using Keywords.Domain;
using Keywords.Services;
using WebAPI.OutputCache;

namespace Keywords.API.Controllers
{
    public class RegionController : ApiController
    {
        private readonly IGetKeyword _keywordService;

        public RegionController(IGetKeyword keywordService)
        {
            _keywordService = keywordService;
        }

        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public Region GetRegionByText(string text, int countryId)
        {
            return _keywordService.GetRegionByText(StringHelper.DecodeAmpersands(text), countryId);
        }
        
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public Region GetRegionById(int regionId)
        {
            return _keywordService.GetRegionById(regionId);
        }
    }
}
