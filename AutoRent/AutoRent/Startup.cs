using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoRent.Startup))]
namespace AutoRent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
