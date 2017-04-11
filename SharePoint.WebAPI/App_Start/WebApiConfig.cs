using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SharePoint.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{accountid}/{initiator_number}",
                defaults: new { accountid = RouteParameter.Optional, initiator_number = RouteParameter.Optional  }
            );

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{accountid}",
                defaults: new { accountid = RouteParameter.Optional }
            );*/

        }
    }
}
