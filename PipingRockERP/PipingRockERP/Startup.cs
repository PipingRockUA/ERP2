using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PipingRockERP.Startup))]
namespace PipingRockERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
