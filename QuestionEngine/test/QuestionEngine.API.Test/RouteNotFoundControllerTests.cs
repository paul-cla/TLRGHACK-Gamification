using System;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Keywords.API.Controllers;
using TLRGrp.WebApi.ErrorHandling;

namespace Keywords.API.Test
{
    [TestFixture]
    public class RouteNotFoundControllerTests
    {
        [Test]
        public void should_throw_exception_on_get()
        {
            var controller = new RouteNotFoundController();
            Action act = () => controller.Get();
            act.ShouldThrow<WebApiException>();
        }
        
        [Test]
        public void should_throw_exception_on_post()
        {
            var controller = new RouteNotFoundController();
            Action act = () => controller.Post();
            act.ShouldThrow<WebApiException>();
        }
    }
}
