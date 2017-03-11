using System;
using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver
{
    public class when_resolving_without_resolver_for_identifier : given.one_resolver_for_known_identifier
    {
        static Exception exception;
        protected const string resource_type_identifier = "OtherResourceType";

        protected static Mock<IApplicationResourceIdentifier> other_identifier;
        protected static Mock<IApplicationResource> other_resource;
        protected static Mock<IApplicationResourceType> other_resource_type;

        Establish context = () =>
        {
            other_resource_type = new Mock<IApplicationResourceType>();
            other_resource_type.SetupGet(r => r.Identifier).Returns(resource_type_identifier);

            other_resource = new Mock<IApplicationResource>();
            other_resource.SetupGet(r => r.Type).Returns(other_resource_type.Object);

            other_identifier = new Mock<IApplicationResourceIdentifier>();
            other_identifier.SetupGet(i => i.Resource).Returns(other_resource.Object);
        };

        Because of = () => exception = Catch.Exception(() => resolver.Resolve(other_identifier.Object));

        It should_throw_unknown_application_resource_type = () => exception.ShouldBeOfExactType<UnknownApplicationResourceType>();
    }
}
