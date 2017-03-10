using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifierConverter.given
{
    public class an_application_resource_identifier_converter : all_dependencies
    {
        protected static ApplicationResourceIdentifierConverter converter;

        Establish context = () => converter = new ApplicationResourceIdentifierConverter(application.Object);
    }
}
