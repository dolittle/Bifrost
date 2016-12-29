using System;
using System.Runtime.InteropServices;
using Bifrost.Execution;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer.given
{
    public class a_type_discoverer : dependency_injection
    {
        protected static TypeDiscoverer type_discoverer;
        protected static Type[] types;

        Establish context = () =>
        {
            types = new[]
            {
                typeof (ISingle),
                typeof (Single),
                typeof (IMultiple),
                typeof (FirstMultiple),
                typeof (SecondMultiple)
            };

            GetMock<_Assembly>().Setup(a => a.GetTypes()).Returns(types);
            GetMock<_Assembly>().Setup(a => a.FullName).Returns("A.Full.Name");

            GetMock<IAssemblies>().Setup(x => x.GetAll()).Returns(new[] {Get<_Assembly>()});

            type_discoverer = Get<TypeDiscoverer>();
        };
    }
}