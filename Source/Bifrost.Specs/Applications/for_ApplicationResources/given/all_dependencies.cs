using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResources.given
{
    public class all_dependencies
    {
        protected const string application_name = "MyApplication";
        protected static Mock<IApplication> application;

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns(application_name);
        };
    }
}
