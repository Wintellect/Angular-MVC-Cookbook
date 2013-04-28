using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MvcAngular.Web
{
    public static class WebApiConfig
    {
        public static JsonSerializerSettings JsonSerializerSettings { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Set camelCase JSON serialization as default.
            WebApiConfig.JsonSerializerSettings =
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
#if DEBUG
                        Formatting = Formatting.Indented,
#endif
                    };
            var index = config.Formatters.IndexOf(config.Formatters.JsonFormatter);
            config.Formatters[index] = new JsonMediaTypeFormatter
                {
                    SerializerSettings = WebApiConfig.JsonSerializerSettings
                };
        }
    }
}
