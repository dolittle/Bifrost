using System.Web;
using Bifrost.Configuration;

namespace Web
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

                    // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                    //.UsingDocumentDB(e => e.WithUrl("").WithDefaultDatabase("QuickStart").UsingAuthorizationKey(""))

                .DefaultStorage
                    .UsingFiles(entitiesPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingFiles(...) line above and uncomment the line below
                    //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                    // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                    //.UsingDocumentDB(e => e.WithUrl("").WithDefaultDatabase("QuickStart").UsingAuthorizationKey(""))

                .Frontend
					.Web(w=> {
                        w.AsSinglePageApplication();

                        var baseNamespace = global::Bifrost.Configuration.Configure.Instance.EntryAssembly.GetName().Name;
                        var @namespace = string.Format("{0}.**.", baseNamespace);

                        w.PathsToNamespaces.Add("**/", @namespace);
                        w.PathsToNamespaces.Add("/**/", @namespace);
                        w.PathsToNamespaces.Add("", baseNamespace);

                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Domain.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Read.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.**.", baseNamespace));
                    })
                .WithMimir();
        }
    }
}