using System;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver
{
    public class when_resolving_with_resolver_for_identifier : given.one_resolver_for_known_identifier
    {
        static Type result;

        Because of = () => result = resolver.Resolve(identifier.Object);

        It should_return_the_known_type = () => result.ShouldEqual(known_type);
    }
}
