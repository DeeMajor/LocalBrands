using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalBrands.Startup))]
namespace LocalBrands
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
