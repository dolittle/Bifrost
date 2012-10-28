using Bifrost.Execution;
using Bifrost.Content.Resources;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Resources.for_ResourceConvention.given
{
	public class a_resource_convention
	{
		protected static ResourceConvention convention;
		protected static ResourceInterceptorStub interceptor;
	    protected static Mock<IContainer> container_mock;

		Establish context = () =>
		                    	{
                                    container_mock = new Mock<IContainer>();
		                    	    var resolverMock = new Mock<IResourceResolver>();
		                    		interceptor = new ResourceInterceptorStub(resolverMock.Object);
		                    	    container_mock.Setup(c => c.Get<ResourceInterceptor>()).Returns(interceptor);
		                    		convention = new ResourceConvention();
		                    	};
	}
}
