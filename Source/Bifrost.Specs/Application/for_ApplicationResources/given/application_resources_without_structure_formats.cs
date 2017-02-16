using Bifrost.Application;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Application.for_ApplicationResources.given
{
    public class application_resources_without_structure_formats : all_dependencies
    {
        protected static ApplicationResources resources;
        protected static Mock<IApplicationStructure> application_structure;

        Establish context = () =>
        {
            application_structure = new Mock<IApplicationStructure>();
            application_structure.SetupGet(a => a.StructureFormats).Returns(new IStringFormat[0]);

            application.SetupGet(a => a.Structure).Returns(application_structure.Object);

            resources = new ApplicationResources(application.Object);
        };
    }
}
