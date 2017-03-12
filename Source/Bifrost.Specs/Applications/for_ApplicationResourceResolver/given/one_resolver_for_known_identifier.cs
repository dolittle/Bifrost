using System;
using System.Collections.Generic;
using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class one_resolver_for_known_identifier : an_identifier
    {
        protected static ApplicationResourceResolver resolver;

        protected static Mock<ICanResolveApplicationResources> resource_resolver;

        protected static Type known_type = typeof(an_identifier);

        Establish context = () =>
        {
            resource_resolver = new Mock<ICanResolveApplicationResources>();
            resource_resolver.SetupGet(r => r.ApplicationResourceType).Returns(resource_type.Object);
            resource_resolver.Setup(r => r.Resolve(identifier.Object)).Returns(known_type);

            resolvers.Setup(r => r.GetEnumerator()).Returns(
                new List<ICanResolveApplicationResources>(
                    new[] { resource_resolver.Object }).GetEnumerator()
                );

            resolver = new ApplicationResourceResolver(
                application.Object,
                application_resource_types.Object, 
                resolvers.Object,
                type_discoverer.Object);
        };
    }
}
