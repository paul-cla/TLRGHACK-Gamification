using System;
using System.ServiceModel;
using Keywords.API.Models.Errors;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.API.Errors
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