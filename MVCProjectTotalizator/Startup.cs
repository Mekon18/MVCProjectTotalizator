using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCProjectTotalizator.Startup))]
namespace MVCProjectTotalizator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
