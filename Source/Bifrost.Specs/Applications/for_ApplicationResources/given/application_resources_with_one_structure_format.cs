using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResources.given
{
    public class application_resources_with_one_structure_format : application_resources_without_structure_formats
    {
        protected static ApplicationResources resources;
        protected static Mock<IStringFormat> string_format;

        Establish context = () =>
        {
            string_format = new Mock<IStringFormat>();
            application_structure.SetupGet(a => a.AllStructureFormats).Returns(new[] { string_format.Object });
            resources = new ApplicationResources(application.Object);
        };
    }
}
