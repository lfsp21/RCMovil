using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clock.Backend.Startup))]
namespace Clock.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
