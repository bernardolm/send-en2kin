using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SendEn2Kin.WebAPI
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
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			// Json by default
			config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
