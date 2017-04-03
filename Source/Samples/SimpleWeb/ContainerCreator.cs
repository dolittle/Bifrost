using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.StructureMap;

namespace SimpleWeb
{
    public class ContainerCreator : ICanCreateContainer
    {
        public IContainer CreateContainer()
        {
            var structureMap = new StructureMap.Container();

            var container = new Container(structureMap);
            return container;
        }
    }
}
