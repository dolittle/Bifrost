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
                    .Synchronous()

                .Events
                    //.UsingFiles(eventsPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingMongoDB(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingRavenDB(...) line above and uncomment the line below
                    //.UsingRavenDB(e=>e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                    // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                    .UsingDocumentDB(e => e.WithUrl("https://bifrost.documents.azure.com:443/").WithDefaultDatabase("bifrost").UsingAuthorizationKey("uqd1KXaimscohEn/bPhMQS0xBd6hdtfCsgSC8t921KoTHD0WQ+9eYUZlFo3jMz9uD8k8guXEiuV2UwoVKa4HwA=="))
                    
                .DefaultStorage
                    .UsingFiles(entitiesPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingMongoDB(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingRavenDB(...) line above and uncomment the line below
                    //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                    // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                    //.UsingDocumentDB(e => e.WithUrl("https://bifrost.documents.azure.com:443/").WithDefaultDatabase("QuickStart").UsingAuthorizationKey("2NQ32KwoTGZOxiyUs7vWkq6Mvvl2Fq+HR0s5YBt7tMZwzFvUg5e5LvvLZyYUP6GLIUvN5iOqMaq7Iw6vPjseRQ=="))
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

                        w.NamespaceMapper.Add("Web.**.", "Web.Domain.**.");
                        w.NamespaceMapper.Add("Web.**.", "Web.Read.**.");
                        w.NamespaceMapper.Add("Web.**.", "Web.**.");
					})
                .WithMimir();

            RouteTable.Routes.AddService<SecuredService>();
        }
    }
}