using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TenantDemo.Startup))]
namespace TenantDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
