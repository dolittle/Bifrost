using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_Application
{
    public class when_initializing
    {
        const string application_name = "My Application";
        static Mock<IApplicationStructure> application_structure;
        static Application result;

        Establish context = () => application_structure = new Mock<IApplicationStructure>();

        Because of = () => result = new Application(application_name, application_structure.Object);

        It should_hold_name = () => ((string) result.Name).ShouldEqual(application_name);
        It should_hold_application_structure = () => result.Structure.ShouldEqual(application_structure.Object);
    }
}
