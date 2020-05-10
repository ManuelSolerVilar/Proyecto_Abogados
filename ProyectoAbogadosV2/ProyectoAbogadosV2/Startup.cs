using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoAbogadosV2.Startup))]
namespace ProyectoAbogadosV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
