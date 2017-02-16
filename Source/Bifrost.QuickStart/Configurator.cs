using System.Linq;
using System.Web;
using System.Web.Routing;
using Bifrost.Application;
using Bifrost.Configuration;
using Bifrost.FluentValidation.Sagas;
using Bifrost.Sagas;
using Bifrost.Strings;
using Bifrost.Web.Services;
using Web.Domain.HumanResources.Employees;
using Web.Domain.HumanResources.Foos;

namespace Web
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var entitiesPath = HttpContext.Current.Server.MapPath("~/App_Data/Entities");
            var eventsPath = HttpContext.Current.Server.MapPath("~/App_Data/Events");
            var sagasPath = HttpContext.Current.Server.MapPath("~/App_Data/Sagas");

            configure.Container.Bind<IChapterValidationService>(typeof(ChapterValidationService));
            configure.Sagas.LibrarianType = typeof(SagaLibrarian);

            var format = Bifrost.Strings.StringFormat.Parse("[.]Web.Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*");
            var type = typeof(RegisterEmployee);
            var match = format.Match(type.Namespace);

            var d = match.AsDictionary();

            var boundedContext = new BoundedContext(d["BoundedContext"].Single());
            var module = new Module(boundedContext, d["Module"].Single());
            var feature = new Feature(module, d["Feature"].Single());

            configure
                .Application(a => a.Structure(s => s
                        .Include("Web.Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Include("Web.Events.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Include("Web.Read.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Include("Web.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                    )
                )
                .Serialization
                    .UsingJson()
                .Events
                    .Synchronous()

                .Events
                    .UsingFiles(eventsPath)

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