using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoDealer_Service.Startup))]
namespace AutoDealer_Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
