using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DesafioStoneTemperatura.Startup))]

namespace DesafioStoneTemperatura
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
