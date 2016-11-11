using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JQueryPopupModal.Startup))]
namespace JQueryPopupModal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
