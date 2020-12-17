using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KennelCheckin.MVC.Startup))]
namespace KennelCheckin.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
