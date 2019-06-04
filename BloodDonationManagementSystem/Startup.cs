using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodDonationManagementSystem.Startup))]
namespace BloodDonationManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
