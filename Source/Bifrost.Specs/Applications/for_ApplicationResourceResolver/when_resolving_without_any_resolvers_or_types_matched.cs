using System;
using Bifrost.Applications;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver
{
    public class when_resolving_without_any_resolvers_or_types_matched : given.no_resolvers
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resolver.Resolve(identifier.Object));

        It should_throw_unknown_application_resource_type = () => exception.ShouldBeOfExactType<UnknownApplicationResourceType>();
    }
}
