using System;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver
{
    public class when_resolving_without_resolver_for_identifier_or_types_matched : given.one_resolver_for_known_identifier
    {
        static Exception exception;
        const string other_resource_type_identifier = "OtherResourceType";
        const string other_resource_type_area = "OtherArea";
        const string other_resource_name = "OtherName";

        static Mock<IApplicationResourceIdentifier> other_identifier;
        static Mock<IApplicationResource> other_resource;
        static Mock<IApplicationResourceType> other_resource_type;

        Establish context = () =>
        {
            other_resource_type = new Mock<IApplicationResourceType>();
            other_resource_type.SetupGet(r => r.Identifier).Returns(other_resource_type_identifier);
            other_resource_type.SetupGet(r => r.Area).Returns(other_resource_type_area);

            other_resource = new Mock<IApplicationResource>();
            other_resource.SetupGet(r => r.Name).Returns(other_resource_name);
            other_resource.SetupGet(r => r.Type).Returns(other_resource_type.Object);
            
            other_identifier = new Mock<IApplicationResourceIdentifier>();
            other_identifier.SetupGet(i => i.Resource).Returns(other_resource.Object);

            application_structure.Setup(a => a.GetStructureFormatsForArea(other_resource_type_area)).Returns(new IStringFormat[0]);

            application_resource_types.Setup(a => a.GetFor(other_resource_type_identifier)).Returns(other_resource_type.Object);
        };

        Because of = () => exception = Catch.Exception(() => resolver.Resolve(other_identifier.Object));

        It should_throw_unknown_application_resource_type = () => exception.ShouldBeOfExactType<UnknownApplicationResourceType>();
    }
}
