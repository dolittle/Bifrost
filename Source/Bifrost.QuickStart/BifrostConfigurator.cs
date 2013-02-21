using System.Web;
using Bifrost.Configuration;

namespace Bifrost.QuickStart
{
    public class BifrostConfigurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var dataPath = HttpContext.Current.Server.MapPath("~/App_Data");
            configure
                .Events
                    .UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath))
                .DefaultStorage
                    .UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath))
                .Frontend
                    .Web(w=>w.AsSinglePageApplication())
                .WithMimir();
        }
    }
}