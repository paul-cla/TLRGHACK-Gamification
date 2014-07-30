using System;
using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using QuestionEngine.API.Controllers;

namespace QuestionEngine.API.Test
{
    [TestFixture]
    public class RouteTests
    {
        [TestCase("keyword/1", "GetKeywordById", typeof(QuestionController))]
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

    }
}