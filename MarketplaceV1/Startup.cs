using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketplaceV1.Startup))]
namespace MarketplaceV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
