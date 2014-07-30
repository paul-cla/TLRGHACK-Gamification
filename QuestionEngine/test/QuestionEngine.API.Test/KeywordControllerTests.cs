using System;
using System.Net;
using FluentAssertions;
using Keywords.API.Controllers;
using Keywords.Domain;
using Keywords.Services;
using NUnit.Framework;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.API.Test
{
    [TestFixture]
    public class KeywordControllerTests : IGetKeyword
    {
        private string _requestedText;
        private int _requestedCountryId;

        private readonly IGetKeyword _failureService = new FailureFakeService();
        private readonly IGetKeyword _successService = new SuccessFakeService();

        [Test]
        public void should_call_keyword_service_with_text_and_country_id()
        {
            var controller = new KeywordController(this);
            controller.GetKeywordByTextAndCountry("Test", 1);
            _requestedText.Should().Be("Test");
            _requestedCountryId.Should().Be(1);
        } 

        [Test]
        public void should_return_error_if_status_is_failing()
        {
            try
            {
                var controller = new StatusController(_failureService);
                controller.Status();
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<WebApiException>();
                ((WebApiException)ex).StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            }
        }

        [Test]
        public void should_return_success_if_status_is_ok()
        {
            var controller = new StatusController(_successService);
            var response = controller.Status();
            response.Message.Should().Be("OK");
            response.Code.Should().Be(HttpStatusCode.OK);
        }

        public Keyword GetKeywordByTextAndCountry(string text, int countryId)
        {
            _requestedText = text;
            _requestedCountryId = countryId;
            return null;
        }

        public Keyword GetKeywordById(int keywordId)
        {
            throw new NotImplementedException();
        }

        public Region GetRegionByText(string text, int countryId)
        {
            throw new NotImplementedException();
        }

        public Region GetRegionById(int regionId)
        {
            throw new NotImplementedException();
        }

        public IStatus Status()
        {
            throw new NotImplementedException();
        }

        class FailureFakeService : IGetKeyword
        {
            public Keyword GetKeywordByTextAndCountry(string text, int countryId)
            {
                throw new NotImplementedException();
            }

            public Keyword GetKeywordById(int keywordId)
            {
                throw new NotImplementedException();
            }

            public Region GetRegionByText(string text, int countryId)
            {
                throw new NotImplementedException();
            }

            public Region GetRegionById(int regionId)
            {
                throw new NotImplementedException();
            }

            public IStatus Status()
            {
                throw new WebApiException(500);
            }
        }

        class SuccessFakeService : IGetKeyword
        {
            public Keyword GetKeywordByTextAndCountry(string text, int countryId)
            {
                throw new NotImplementedException();
            }

            public Keyword GetKeywordById(int keywordId)
            {
                throw new NotImplementedException();
            }

            public Region GetRegionByText(string text, int countryId)
            {
                throw new NotImplementedException();
            }

            public Region GetRegionById(int regionId)
            {
                throw new NotImplementedException();
            }

            public IStatus Status()
            {
                return new GoodStatus();
            }
        }
    }
}