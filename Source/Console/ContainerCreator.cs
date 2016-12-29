using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Ninject;
using Ninject;

namespace ConsoleApplication
{
    public class ContainerCreator : ICanCreateContainer
    {
        public static readonly IKernel Kernel = new StandardKernel();
        public IContainer CreateContainer()
        {
            var container = new Container(Kernel);
            return container;
        }
    }
}