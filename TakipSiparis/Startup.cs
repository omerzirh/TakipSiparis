using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TakipSiparis.StartupOwin))]

namespace TakipSiparis
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
