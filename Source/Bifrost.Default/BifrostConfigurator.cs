using System.Web;
using Bifrost.Configuration;

namespace Bifrost
{
    public class BifrostConfigurator : ICanConfigure
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

                        w.PathsToNamespaces.Add("Features/**/", "Web.HumanResources.**.");
                        w.PathsToNamespaces.Add("/Features/**/", "Web.HumanResources.**.");

                        w.NamespaceMapper.Add("Web.**.", "Web.Domain.HumanResources.**.");
                        w.NamespaceMapper.Add("Web.**.", "Web.Read.HumanResources.**.");
                        w.NamespaceMapper.Add("Web.**.", "Web.HumanResources.**.");

                    })
                .WithMimir();
        }
    }
}