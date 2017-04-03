using System;
using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceTypes.given
{
    public class one_resource_type
    {
        protected static ApplicationResourceTypes resource_types;
        protected static Mock<IApplicationResourceType> resource_type;
        protected const string identifier = "ResourceIdentifier";
        protected static Type base_type = typeof(IInterface);

        Establish context = () =>
        {
            resource_type = new Mock<IApplicationResourceType>();
            resource_type.SetupGet(r => r.Identifier).Returns(identifier);
            resource_type.SetupGet(r => r.Type).Returns(base_type);

            var instances = new Mock<IInstancesOf<IApplicationResourceType>>();
            instances.Setup(i => i.GetEnumerator()).Returns(new List<IApplicationResourceType>(
                new[] { resource_type.Object }).GetEnumerator());

            resource_types = new ApplicationResourceTypes(instances.Object);
        };
    }
}
