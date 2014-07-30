using System.Web.Http;
using Keywords.API.Models.Errors;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.API.Controllers
{
    public class RouteNotFoundController : ApiController
    {
        public object Get()
        {
            throw new WebApiException(ApiErrorCodes.ResourceNotFound);
        }

        public object Post()
        {
            throw new WebApiException(ApiErrorCodes.ResourceNotFound);
        }
    }
}
