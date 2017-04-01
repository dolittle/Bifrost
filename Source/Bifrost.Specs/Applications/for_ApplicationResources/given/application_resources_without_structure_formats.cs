using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResources.given
{
    public class application_resources_without_structure_formats : all_dependencies
    {
        protected static ApplicationResources resources;
        protected static Mock<IApplicationStructure> application_structure;
        protected static Mock<IApplicationResourceType> application_resource_type;


        Establish context = () =>
        {
            application_structure = new Mock<IApplicationStructure>();
            application_structure.SetupGet(a => a.AllStructureFormats).Returns(new IStringFormat[0]);

            application.SetupGet(a => a.Structure).Returns(application_structure.Object);

            application_resource_type = new Mock<IApplicationResourceType>();
            application_resource_types.Setup(a => a.GetFor(typeof(string))).Returns(application_resource_type.Object);

            resources = new ApplicationResources(application.Object, application_resource_types.Object);
        };
    }
}
