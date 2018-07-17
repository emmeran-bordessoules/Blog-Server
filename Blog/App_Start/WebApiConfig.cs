using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace Blog
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
