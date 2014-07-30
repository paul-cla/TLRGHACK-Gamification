using System;
using System.ServiceModel;
using FluentAssertions;
using NUnit.Framework;
using Keywords.API.Errors;
using Keywords.API.Models.Errors;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.API.Test.Errors
{
    [TestFixture]
    public class TestErrorMapper
    {
        [Test]
        public void it_should_map_an_EndpointNotFoundException_to_ServiceUnavailable()
        {
            var expected = new WebApiException(ApiErrorCodes.ServiceUnavailable);

            var ex = new EndpointNotFoundException();
            var mapped = ErrorMapper.Map(ex);
            mapped.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void it_should_map_unknown_exceptions_to_InternalServerError()
        {
            var expected = new WebApiException(ApiErrorCodes.InternalServerError);

            var ex = new Exception();
            var mapped = ErrorMapper.Map(ex);
            mapped.ShouldBeEquivalentTo(expected);
        }
    }
}
