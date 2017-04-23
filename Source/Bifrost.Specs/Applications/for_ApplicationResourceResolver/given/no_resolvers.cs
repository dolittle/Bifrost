using System.Collections.Generic;
using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class no_resolvers : an_identifier
    {
        protected static ApplicationResourceResolver resolver;

        Establish context = () =>
        {
            resolvers.Setup(r => r.GetEnumerator()).Returns(new List<ICanResolveApplicationResources>().GetEnumerator());
            resolver = new ApplicationResourceResolver(
                application.Object,
                application_resource_types.Object, 
                resolvers.Object,
                type_discoverer.Object,
                logger);
        };
    }
}
