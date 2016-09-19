using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SILI.Startup))]
namespace SILI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
