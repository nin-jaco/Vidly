using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidly.Startup))]
namespace Vidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            ConfigureAuth(app);
            ConfigureWebApi(httpConfig);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //app.UseWebApi(httpConfig);
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
