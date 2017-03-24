using System.Web;
using System.Web.Routing;
using Bifrost.Applications;
using Bifrost.Configuration;
using Bifrost.Events;
using Bifrost.Web.Services;
using Web.Domain.HumanResources.Foos;

namespace Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var entitiesPath = HttpContext.Current.Server.MapPath("~/App_Data/Entities");
            var eventsPath = HttpContext.Current.Server.MapPath("~/App_Data/Events");
            var eventSequenceNumbersPath = HttpContext.Current.Server.MapPath("~/App_Data/EventSequenceNumbers");
            var eventProcessorsStatePath = HttpContext.Current.Server.MapPath("~/App_Data/EventProcessors");

            configure
                .Application("QuickStart", a => a.Structure(s => s
                        .Domain("Web.Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Events("Web.Events.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Read("Web.Read.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Frontend("Web.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                ))

                .Events(e =>
                    {
                        //e.EventStore.UsingFiles(eventsPath);
                        //e.EventSequenceNumbers.UsingFiles(eventSequenceNumbersPath);
                        e.EventProcessorStates.UsingFiles(eventProcessorsStatePath);
                        e.EventSequenceNumbers.UsingRedis("dolittle.redis.cache.windows.net:6380,password=yGQibET0Re058gvkGz0VaObJzcY4rKFitMy1PWCfFd4=,ssl=True,abortConnect=False");
                    })
                    
                .Serialization
                    .UsingJson()

                // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingMongoDB(...) line above and uncomment the line below
                //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingRavenDB(...) line above and uncomment the line below
                //.UsingRavenDB(e=>e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                //.UsingDocumentDB(e => e.WithUrl("").WithDefaultDatabase("QuickStart").UsingAuthorizationKey(""))


                .DefaultStorage
                    //.UsingEntityFramework(e => e.WithConnectionString(@"Data Source=(LocalDB)\v11.0;AttachDbFileName=|DataDirectory|\Database.mdf;Initial Catalog=Database;Integrated Security=True"))
                    .UsingFiles(entitiesPath)

                // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingMongoDB(...) line above and uncomment the line below
                //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingRavenDB(...) line above and uncomment the line below
                //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                //.UsingDocumentDB(e => e.WithUrl("").WithDefaultDatabase("QuickStart").UsingAuthorizationKey(""))
                .Frontend
                    .Web(w =>
                    {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();

                        #region Temporary Configuration for the Bifrost Visualizer - work in progress
                        w.PathsToNamespaces.Add("Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");

                        w.PathsToNamespaces.Add("Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer", "Bifrost.Visualizer");

                        w.NamespaceMapper.Add("Bifrost.Visualizer.**.", "Bifrost.Web.Visualizer.**.");
                        #endregion

                        var baseNamespace = global::Bifrost.Configuration.Configure.Instance.EntryAssembly.GetName().Name;

                        // Normally you would use the base namespace from the assembly - but since the demo code is written for a specific namespace
                        // all the conventions in Bifrost won't work.
                        // Recommend reading up on the namespacing and conventions related to it:
                        // https://dolittle.github.io/bifrost/Frontend/JavaScript/namespacing.html
                        baseNamespace = "Web";

                        var @namespace = string.Format("{0}.**.", baseNamespace);

                        w.PathsToNamespaces.Add("**/", @namespace);
                        w.PathsToNamespaces.Add("/**/", @namespace);
                        w.PathsToNamespaces.Add("", baseNamespace);

                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Domain.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Read.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.**.", baseNamespace));
                    });

            RouteTable.Routes.AddService<SecuredService>();
        }
    }
}