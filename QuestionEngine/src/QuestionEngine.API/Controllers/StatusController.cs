using System.Web.Http;
using Keywords.Domain;
using Keywords.Services;
using WebAPI.OutputCache;

namespace Keywords.API.Controllers
{
    public class StatusController : ApiController
    {
        private readonly IGetKeyword _keywordService;

        public StatusController(IGetKeyword keywordService)
        {
            _keywordService = keywordService;
        }

        [CacheOutput(NoCache = true)]
        [HttpGet]
        public IStatus Status()
        {
            return _keywordService.Status();
        }
    }
}
