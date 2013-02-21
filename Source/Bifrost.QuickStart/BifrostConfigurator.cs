using System.Web;
using Bifrost.Configuration;

namespace Bifrost.QuickStart
{
    public class BifrostConfigurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            
            configure
                .Events
                    .UsingRavenDBEmbedded(e=>e.LocatedAt(HttpContext.Current.Server.MapPath("~/App_Data")))
                .AsSinglePageApplication()
                .WithMimir();
        }
    }
}