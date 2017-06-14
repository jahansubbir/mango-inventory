using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MangoInventory.Startup))]
namespace MangoInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
