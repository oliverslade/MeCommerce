using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeCommerce.Startup))]

namespace MeCommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}