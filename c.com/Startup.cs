using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(c.com.Startup))]
namespace c.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
