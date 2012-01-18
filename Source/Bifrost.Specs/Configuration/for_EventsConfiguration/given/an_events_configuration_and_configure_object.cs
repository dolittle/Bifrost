using Bifrost.Configuration;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_EventsConfiguration.given
{
	public class an_events_configuration_and_configure_object : an_events_configuration
	{
		protected static Mock<IConfigure> configure_mock;
		protected static Mock<IContainer> container_mock;

		Establish context = () =>
		                    	{
		                    		configure_mock = new Mock<IConfigure>();
		                    		container_mock = new Mock<IContainer>();
		                    		configure_mock.Setup(c => c.Container).Returns(container_mock.Object);
		                    	};
	}
}