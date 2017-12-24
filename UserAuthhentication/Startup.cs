using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserAuthhentication.Startup))]
namespace UserAuthhentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
