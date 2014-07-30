using System;
using System.Collections.Generic;
using FluentAssertions;
using Infrastructure.Logging.LogToUdp;
using Infrastructure.Logging.Shared;
using NUnit.Framework;
using Keywords.API.Errors;
using Keywords.API.Models.Errors;
using Keywords.API.Support;
using Rhino.Mocks;
using TLRGrp.WebApi.ErrorHandling;
using log4net;

namespace Keywords.API.Test.Errors
{
    [TestFixture]
    public class ErrorObserverTests : IMessageLogger
    {
        ILog _mockLog;
        private Dictionary<string, object> _additionalLoggingInformation;
        IDateTimeProvider _dateTimeProvider;

        [SetUp]
        public void SetUp()
        {
            _mockLog = MockRepository.GenerateMock<ILog>();
            _dateTimeProvider = new FakeDateTimeProvider();
            _additionalLoggingInformation = null;
        }

        [Test]
        public void it_should_log_to_log4net()
        {
            var ex = new Exception("Error");
            _mockLog.Expect(l => l.Error(ex));
            var err = new ErrorObserver(_mockLog, this, _dateTimeProvider);
            err.Handle(ex);
            _mockLog.VerifyAllExpectations();
        }

        [Test]
        public void it_should_log_web_api_exceptions_to_udp()
        {
            var ex = new WebApiException(ApiErrorCodes.KeywordNotFound);

            var logInformation = new LogInformation
            {
                AdditionalLoggingInformation = new Dictionary<string, object>
                        {
                            {"ErrorCode", ApiErrorCodes.KeywordNotFound},
                            {"ErrorMessage","Keyword Not Found"},
                            {"StackTrace", ex.StackTrace},
                            {"TimeStamp", _dateTimeProvider.Now},
                            {"Exception", ex},
                            {"host", Environment.MachineName}
                        }
            };

            var err = new ErrorObserver(_mockLog, this, _dateTimeProvider);
            err.Handle(ex);
            _additionalLoggingInformation.ShouldHave().AllProperties().IncludingNestedObjects().EqualTo(logInformation.AdditionalLoggingInformation);
        }

        [Test]
        public void it_should_log_standard_exceptions_to_udp()
        {
            var ex = new Exception("message");


            var logInformation = new LogInformation
            {
                AdditionalLoggingInformation = new Dictionary<string, object>
                        {
                            {"ErrorCode", 0},
                            {"ErrorMessage","Internal Server Error"},
                            {"StackTrace", ex.StackTrace},
                            {"TimeStamp", _dateTimeProvider.Now},
                            {"Exception", ex},
                            {"host", Environment.MachineName}
                        }
            };

            var err = new ErrorObserver(_mockLog, this, _dateTimeProvider);
            err.Handle(ex);
            _additionalLoggingInformation.ShouldHave().AllProperties().IncludingNestedObjects().EqualTo(logInformation.AdditionalLoggingInformation);
        }

        public void Log(Exception exception, LogLevel loggingLevel)
        {
            throw new NotImplementedException();
        }

        public void Log(Exception exception, LogLevel loggingLevel, Dictionary<string, object> additionalLoggingInformation)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel loggingLevel, Dictionary<string, object> additionalLoggingInformation)
        {
            _additionalLoggingInformation = additionalLoggingInformation;
        }
    }

    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Today; }
        }
    }
}
