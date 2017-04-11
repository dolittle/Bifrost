using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Tenancy;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration.given
{
    public class no_tenant_id_resolvers
    {
        protected static Mock<IContainer> container;
        protected static Mock<ITypeDiscoverer> type_discoverer;
        protected static TenancyConfiguration configuration;
        protected static List<Type> resolvers;

        Establish context = () =>
        {
            resolvers = new List<Type>();
            type_discoverer = new Mock<ITypeDiscoverer>();
            type_discoverer.Setup(t => t.FindMultiple<ICanResolveTenantId>()).Returns(resolvers);
            container = new Mock<IContainer>();
            container.Setup(c => c.Get<ITypeDiscoverer>()).Returns(type_discoverer.Object);
            configuration = new TenancyConfiguration();
        };
    }
}
