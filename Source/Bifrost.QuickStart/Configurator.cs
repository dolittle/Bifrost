using System.Web;
using System.Web.Routing;
using Bifrost.Configuration;
using Web.Domain.HumanResources.Foos;
using Bifrost.Web.Services;

namespace Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var entitiesPath = HttpContext.Current.Server.MapPath("~/App_Data/Entities");
            var eventsPath = HttpContext.Current.Server.MapPath("~/App_Data/Events");

            configure
                .Serialization
                    .UsingJson()
                .Events
                    .UsingFiles(eventsPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingRavenDB(e=>e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                .DefaultStorage
                    .UsingFiles(entitiesPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                .Frontend
                    .Web(w=> {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();
                        w.PathsToNamespaces.Add("Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");

                        w.PathsToNamespaces.Add("Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer", "Bifrost.Visualizer");

                        w.PathsToNamespaces.Add("**/", "Web.**.");
                        w.PathsToNamespaces.Add("/**/", "Web.**.");
                        w.PathsToNamespaces.Add("", "Web");

                        w.NamespaceMapper.Add("Web.HumanResources.**.", "Web.Domain.HumanResources.**.");
                        w.NamespaceMapper.Add("Web.HumanResources.**.", "Web.Read.HumanResources.**.");
                        w.NamespaceMapper.Add("Web.HumanResources.**.", "Web.HumanResources.**.");
					})
                .WithMimir();

            RouteTable.Routes.AddService<SecuredService>();
        }
    }
}