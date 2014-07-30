using System.Linq;
using System.Web.Http;
using Keywords.API.Controllers;

namespace Keywords.API.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AddRouteConfig(config);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "text/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        private static void AddRouteConfig(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("Status", "status", new { controller = "Status", action = "Status" });

            config.Routes.MapHttpRoute(
                name: "KeywordByTextAndCountry",
                routeTemplate: "keyword/{countryId}/{text}",
                constraints: new
                {
                    countryId = @"\d+"
                },
                defaults: new { controller = "Keyword", action = "GetKeywordByTextAndCountry" }
            ); 
            
            config.Routes.MapHttpRoute(
                name: "KeywordById",
                routeTemplate: "keyword/{keywordId}",
                constraints: new
                {
                    keywordId = @"\d+"
                },
                defaults: new { controller = "Keyword", action = "GetKeywordById" }
            );

            config.Routes.MapHttpRoute(
                name: "RegionById",
                routeTemplate: "region/{regionId}",
                constraints: new
                {
                    regionId = @"\d+"
                },
                defaults: new { controller = "Region", action = "GetRegionById" }
            );

            config.Routes.MapHttpRoute(
                name: "RegionByText",
                routeTemplate: "region/{countryId}/{text}",
                constraints: new
                {
                    countryId = @"\d+"
                },
                defaults: new { controller = "Region", action = "GetRegionByText" }
            ); 


            config.Routes.MapHttpRoute(
                name: "CatchAllController",
                routeTemplate: "{*url}",
                defaults: new { controller = "RouteNotFound" }
            );
        }
    }
}
