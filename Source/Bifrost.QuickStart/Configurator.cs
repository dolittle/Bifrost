using System.Web;
using System.Web.Routing;
using Bifrost.Configuration;
using Web.Domain.HumanResources.Foos;
using Bifrost.Web.Services;
using Bifrost.Read;
using Web.Read.HumanResources.Employees;

namespace Web
{

    public class Something : IQueryFor<Employee>
    {

    }

    public class Implementation : Something
    {

    }

    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var entitiesPath = HttpContext.Current.Server.MapPath("~/App_Data/Entities");
            var eventsPath = HttpContext.Current.Server.MapPath("~/App_Data/Events");

            var queryFor = typeof(IQueryFor<>);
            var allEmployees = typeof(Implementation);
            var interfaces = allEmployees.GetInterfaces();
            


            configure
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
                    .UsingEntityFramework(e => e.WithConnectionString(@"Data Source=(LocalDB)\v11.0;AttachDbFileName=|DataDirectory|\Database.mdf;Initial Catalog=Database;Integrated Security=True"))
                    //.UsingFiles(entitiesPath)

                    // For using MongoDB - install the nuget package : install-package Bifrost.MongoDB and comment out the .UsingMongoDB(...) line above and uncomment the line below
                    //.UsingMongoDB(e => e.WithUrl("http://localhost:27017").WithDefaultDatabase("QuickStart"))

                    // For using RavenDB - install the nuget package : install-package Bifrost.RavenDB and comment out the .UsingRavenDB(...) line above and uncomment the line below
                    //.UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))

                    // For using Azure DocumentDB - install the nuget package : install-package Bifrost.DocumentDB and comment out the .UsingDocumentDB(...) line above and uncomment the line below
                    //.UsingDocumentDB(e => e.WithUrl("").WithDefaultDatabase("QuickStart").UsingAuthorizationKey(""))
                .Frontend
                    .Web(w=> {
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
                        var @namespace = string.Format("{0}.**.", baseNamespace);

                        w.PathsToNamespaces.Add("**/", @namespace);
                        w.PathsToNamespaces.Add("/**/", @namespace);
                        w.PathsToNamespaces.Add("", baseNamespace);

                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Domain.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Read.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.**.", baseNamespace));
					})
                .WithMimir();

            RouteTable.Routes.AddService<SecuredService>();
        }
    }
}