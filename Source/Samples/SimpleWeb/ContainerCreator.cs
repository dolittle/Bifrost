using Bifrost.Configuration;
using Bifrost.Execution;
using Ninject;
//using Bifrost.StructureMap;
using Bifrost.Ninject;

namespace SimpleWeb
{
    public class ContainerCreator : ICanCreateContainer
    {
        public IContainer CreateContainer()
        {
            //var structureMap = new StructureMap.Container();
            //var container = new Container(structureMap);

            var kernel = new StandardKernel();
            var container = new Container(kernel);

            

            return container;
        }
    }
}
