using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tracker_App.Startup))]
namespace Tracker_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
