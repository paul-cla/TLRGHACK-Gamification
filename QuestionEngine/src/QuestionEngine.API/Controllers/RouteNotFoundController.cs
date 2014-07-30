using System.Web.Http;
using QuestionEngine.API.Models.Errors;
using TLRGrp.WebApi.ErrorHandling;

namespace QuestionEngine.API.Controllers
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
