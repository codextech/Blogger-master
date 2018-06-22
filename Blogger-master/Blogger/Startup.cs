using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blogger.Startup))]
namespace Blogger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
