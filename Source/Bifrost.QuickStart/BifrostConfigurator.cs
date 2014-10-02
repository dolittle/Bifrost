using System.Web;
using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.QuickStart.Domain.HumanResources.Foos;
using Bifrost.Web.Services;

namespace Bifrost.QuickStart
{
    public class BifrostConfigurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var dataPath = HttpContext.Current.Server.MapPath("~/App_Data");
            configure
                //.UsingSignalR()
                .Serialization
                    .UsingJson()
                //.Events
                    //.UsingRavenDB(e=>e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                    //.UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath).WithManagementStudio())
                //.Events
                    //.Asynchronous(e=>e.UsingSignalR())
                //.DefaultStorage
                    //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                    //.UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath))
                .Frontend
                    .Web(w=> {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();
                        w.PathsToNamespaces.Add("Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");

                        w.PathsToNamespaces.Add("Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer", "Bifrost.Visualizer");



                        w.PathsToNamespaces.Add("Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");

                        w.PathsToNamespaces.Add("Features/**/", "Bifrost.QuickStart.Features.**.");
                        w.PathsToNamespaces.Add("/Features/**/", "Bifrost.QuickStart.Features.**.");

                        w.NamespaceMapper.Add("Bifrost.QuickStart.Features.**.", "Bifrost.QuickStart.Domain.HumanResources.**.");
                        w.NamespaceMapper.Add("Bifrost.QuickStart.Features.**.", "Bifrost.QuickStart.Read.HumanResources.**.");
                        w.NamespaceMapper.Add("Bifrost.QuickStart.Features.**.", "Bifrost.QuickStart.Features.**.");
					})
                .WithMimir();

            RouteTable.Routes.AddService<SecuredService>();
        }
    }
}