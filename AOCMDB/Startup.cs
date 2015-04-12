using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AOCMDB.Startup))]
namespace AOCMDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
