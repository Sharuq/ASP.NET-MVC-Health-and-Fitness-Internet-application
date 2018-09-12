using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StayFit.Startup))]
namespace StayFit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
