using Bifrost.Application;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Application.for_ApplicationResources.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;

        Establish context = () => application = new Mock<IApplication>();
    }
}
