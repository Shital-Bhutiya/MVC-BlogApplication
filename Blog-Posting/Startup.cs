using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog_Posting.Startup))]
namespace Blog_Posting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
