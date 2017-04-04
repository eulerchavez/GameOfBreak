using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameOfBreak.Startup))]
namespace GameOfBreak
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);

            // CreateRoleAndUserAdminAsync();

        }

    }

}
