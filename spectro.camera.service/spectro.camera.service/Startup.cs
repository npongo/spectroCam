using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(spectro.camera.service.Startup))]

namespace spectro.camera.service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}