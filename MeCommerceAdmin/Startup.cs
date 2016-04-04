using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeCommerceAdmin.Startup))]
namespace MeCommerceAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
