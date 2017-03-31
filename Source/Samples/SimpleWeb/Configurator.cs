using Bifrost.Applications;
using Bifrost.Configuration;

namespace SimpleWeb
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var entitiesPath = "./App_Data/Entities";
            var eventsPath = "./App_Data/Events";
            var eventSequenceNumbersPath = "./App_Data/EventSequenceNumbers";
            var eventProcessorsStatePath = "./App_Data/EventProcessors";

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
                    
                    //e.EventSourceVersions.UsingRedis(redis);
                    //e.EventSequenceNumbers.UsingRedis(redis);
                    //e.EventStore.UsingTables("DefaultEndpointsProtocol=https;AccountName=dolittle;AccountKey=XcfKv4RV5Hd3My4PbXlBATvLhvI0TpZmP5jwcCFbiILM/kESPr6pibI8hdD3+qPpe+UZ5OlmWUI7Z7qSKlRwuQ==;EndpointSuffix=core.windows.net");
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

        }
    }
}
