using System;
using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using Keywords.API.Controllers;

namespace Keywords.API.Test
{
    [TestFixture]
    public class RouteTests
    {
        [TestCase("keyword/1/Test", "GetKeywordByTextAndCountry", typeof(KeywordController))]
        [TestCase("keyword/1", "GetKeywordById", typeof(KeywordController))]
        [TestCase("region/1/Test", "GetRegionByText", typeof(RegionController))]
        [TestCase("region/1", "GetRegionById", typeof(RegionController))]
        public void TestValidRoutes(string url, string controllerAction, Type controller)
        {
            var host = ConfigurationManager.AppSettings["hostname"];

            var route = RouteTestHelper.RouteThis(new Uri(string.Format("http://{0}/{1}", host, url)));
           
            route.Controller.Should().Be(controller);
            route.Action.Should().Be(controllerAction);
        }
        
        [TestCase("foo/1")]
        public void TestInvalidRoutes(string url)
        {
            var host = ConfigurationManager.AppSettings["hostname"];

            var route = RouteTestHelper.RouteThis(new Uri(string.Format("http://{0}/{1}", host, url)));

            route.Controller.Should().Be(typeof(RouteNotFoundController));
            route.Action.Should().Be("Get");
        }

        [Test]
        public void TestStatusRoute()
        {
            var host = ConfigurationManager.AppSettings["hostname"];

            const string url = "status/";

            var route = RouteTestHelper.RouteThis(new Uri(string.Format("http://{0}/{1}", host, url)));

            route.Controller.Should().Be(typeof(StatusController));
            route.Action.Should().Be("Status");        
        }
    }
}