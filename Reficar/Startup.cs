using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reficar.Startup))]
namespace Reficar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
