using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BandEngine.Startup))]
namespace BandEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
