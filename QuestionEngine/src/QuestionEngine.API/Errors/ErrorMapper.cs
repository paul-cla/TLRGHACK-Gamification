using System;
using System.ServiceModel;
using QuestionEngine.API.Models.Errors;
using TLRGrp.WebApi.ErrorHandling;

namespace QuestionEngine.API.Errors
{
    public class ErrorMapper
    {
        public static WebApiException Map(Exception ex)
        {
            if (ex is EndpointNotFoundException)
                return new WebApiException(ApiErrorCodes.ServiceUnavailable);

            return new WebApiException(ApiErrorCodes.InternalServerError);
        }
    }
}