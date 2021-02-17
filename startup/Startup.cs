using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(startup.Startup))]
namespace startup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
