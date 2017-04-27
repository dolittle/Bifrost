using System.Web;
using System.Web.Routing;
using Bifrost.Applications;
using Bifrost.Configuration;
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
            var eventSourceVersionsPath = HttpContext.Current.Server.MapPath("~/App_Data/EventSourceVersions");

            configure
                .Application("QuickStart", a => a.Structure(s => s
                        .Domain("Web.Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Events("Web.Events.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Read("Web.Read.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                        .Frontend("Web.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                ))

                .Events(e =>
                    {
                        e.EventStore.UsingFiles(eventsPath);
                        e.EventSequenceNumbers.UsingFiles(eventSequenceNumbersPath);
                        e.EventProcessorStates.UsingFiles(eventProcessorsStatePath);
                        e.EventSourceVersions.UsingFiles(eventSourceVersionsPath);
                    })
                    
                .Serialization
                    .UsingJson()


                .DefaultStorage
                    .UsingFiles(entitiesPath)

                .Frontend
                    .Web(w =>
                    {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();

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