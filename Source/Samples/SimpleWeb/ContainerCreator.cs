using System;
using System.Linq;
using System.Reflection;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.StructureMap;
using Ninject;
using Ninject.Selection.Heuristics;

namespace SimpleWeb
{
    /*
    public class SelfBindingConvention : BaseConvention
    {
        public override bool CanResolve(IContainer container, Type service)
        {
            var typeInfo = service.GetTypeInfo();
            return !typeInfo.GetInterfaces().Any(i => i.Name.Equals($"I{service.Name}")) &&
                !typeInfo.IsInterface &&
                !typeInfo.IsAbstract &&
                !container.HasBindingFor(service);
        }

        public override void Resolve(IContainer container, Type service)
        {
            container.Bind(service, service);
        }
    }
    */

    public class Allah
    {

    }

    public class Something
    {
        public Something(Allah a)
        {

        }

    }

    public class ContainerCreator : ICanCreateContainer
    {
        public Bifrost.Execution.IContainer CreateContainer()
        {
            var kernel = new StandardKernel();
            var components = kernel.Components;

            var structureMap = new StructureMap.Container();

            var container = new Container(structureMap);

            var s = container.Get<Something>();
            return container;
        }
    }
}
