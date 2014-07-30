using System;
using System.Net;
using FluentAssertions;
using Keywords.API.Controllers;
using Keywords.Domain;
using StructureMap;
using TLRGrp.WebApi.ErrorHandling;
using TechTalk.SpecFlow;

namespace Keywords.API.AcceptanceTests.Steps
{
    [Binding]
    public class StatusCheckSteps
    {
        [Given(@"the service is responding")]
        public void GivenTheServiceIsResponding()
        {
            //no-op
        }

        [When(@"I request the status of the service")]
        public void WhenIRequestTheStatusOfTheService()
        {
            var statusController = ObjectFactory.GetInstance<StatusController>();
            try
            {
                ScenarioContext.Current["Status"] = statusController.Status();
            }
            catch (Exception exception)
            {
                ScenarioContext.Current["Exception"] = exception;
            }
        }

        [Then(@"the service reports it is responding")]
        public void ThenTheServiceReportsItIsResponding()
        {
            var status = ScenarioContext.Current["Status"] as IStatus;
            status.GetType().Should().Be(typeof (GoodStatus));
        }

        [Given(@"the service is not responding")]
        public void GivenTheServiceIsNotResponding()
        {
            CommonSteps.FakeKeywordRepository.ConfigureRepositoryToFail();
        }

        [Then(@"the service reports it is not responding")]
        public void ThenTheServiceReportsItIsNotResponding()
        {
            var exception = ScenarioContext.Current["Exception"] as WebApiException;

            exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
