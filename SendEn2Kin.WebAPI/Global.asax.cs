using System.Web.Http;
using SendEn2Kin.WebAPI.App_Start;

namespace SendEn2Kin.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalConfiguration.Configure(WebApiConfig.Register);

			AutoMapperConfiguration.Configure();
        }
    }
}