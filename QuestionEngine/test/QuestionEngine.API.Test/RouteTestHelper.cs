using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using NSubstitute;
using QuestionEngine.API.App_Start;

namespace QuestionEngine.API.Test
{
    public static class RouteTestHelper
    {
        public static RouteInfo RouteRequest(HttpConfiguration config, HttpRequestMessage request)
        {
            // create context
            var controllerContext = new HttpControllerContext(config, Substitute.For<IHttpRouteData>(), request);

            // get route data
            var routeData = config.Routes.GetRouteData(request);
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            controllerContext.RouteData = routeData;

            // get controller type
            var controllerDescriptor = new DefaultHttpControllerSelector(config).SelectController(request);
            controllerContext.ControllerDescriptor = controllerDescriptor;

            // get action name
            var actionMapping = new ApiControllerActionSelector().SelectAction(controllerContext);

            return new RouteInfo
                {
                    Controller = controllerDescriptor.ControllerType,
                    Action = actionMapping.ActionName,
                    RouteData =  routeData
                };
        }

        public static RouteInfo RouteThis(string url)
        {
            return RouteThis(new Uri(url));
        }

        public static RouteInfo RouteThis(Uri url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            return RouteRequest(config, request);
        }

        public class RouteInfo
        {
            public Type Controller { get; set; }
            public string Action { get; set; }
            public IHttpRouteData RouteData { get; set; }
        }
    }
}