using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ApplicationManager
{
    public class when_getting_an_application_twice : given.an_application_manager
    {
        static IApplication application;
        static IApplication second_application;
        static Mock<IApplication> application_mock;

        Establish context = () =>
                                {
                                    application_mock = new Mock<IApplication>();
                                    type_discoverer_mock.Setup(t => t.FindSingle<IApplication>()).Returns(application_mock.GetType());
                                    container_mock.Setup(c => c.Get(application_mock.GetType())).Returns(application_mock.Object);
                                };

        Because of = () =>
                         {
                             application = application_manager.Get();
                             second_application = application_manager.Get();
                         };

        It should_return_same_instance = () => application.ShouldEqual(second_application);

        It should_call_service_locator_once = () => container_mock.Verify(c => c.Get(application_mock.GetType()), Times.Once());
    }
}