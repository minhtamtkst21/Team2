using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team2.Startup))]
namespace Team2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
