using System;
using System.Collections.Generic;
using Infrastructure.Logging.LogToUdp;
using Infrastructure.Logging.Shared;
using Keywords.API.Support;
using TLRGrp.WebApi.ErrorHandling;
using log4net;

namespace Keywords.API.Errors
{
    public class ErrorObserver : IExceptionObserver
    {
        private readonly ILog _log;
        private readonly IMessageLogger _logToUdp;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ErrorObserver(ILog log, IMessageLogger logToUdp, IDateTimeProvider dateTimeProvider)
        {
            _log = log;
            _logToUdp = logToUdp;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Handle(Exception ex)
        {
            _log.Error(ex);

            var webApiException = ex as WebApiException ?? new WebApiException(0);

            var logInfo = new LogInformation
            {
                AdditionalLoggingInformation = new Dictionary<string, object>
                        {
                            {"ErrorCode", webApiException.JsonErrorResponse.Error.Code},
                            {"ErrorMessage", webApiException.JsonErrorResponse.Error.Message},
                            {"StackTrace", webApiException.StackTrace},
                            {"TimeStamp", _dateTimeProvider.Now},
                            {"Exception", ex},
                            {"host", Environment.MachineName}
                        }

            };

            _logToUdp.Log(LogLevel.Error, logInfo.AdditionalLoggingInformation);
        }
    }
}