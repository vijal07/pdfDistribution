using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamPaperDistributionSystem.Startup))]
namespace ExamPaperDistributionSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
