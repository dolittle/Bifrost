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
                .Serialization
                    .UsingJson()
                .Events
                    .UsingRavenDB(e => e.WithDefaultDatabase("Test").WithUrl("http://localhost:8080"))
                .Events
                    .Asynchronous(e=>e.UsingSignalR())
                .DefaultStorage
                    .UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath))
                .Frontend
                    .Web(w=>w.AsSinglePageApplication())
                .Statistics
                    .UsingRavenDB(e=>e.WithDefaultDatabase("Test").WithUrl("http://localhost:8080"))
                .WithMimir();
        }
    }
}