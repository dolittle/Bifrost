using System.Reflection;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_TypeDiscoverer.given
{
	public class a_type_discoverer
	{
		protected static TypeDiscoverer TypeDiscoverer;

		Establish context = () =>
		                    	{
		                    	    var assemblyLocatorMock = new Mock<IAssemblyLocator>();
		                    	    assemblyLocatorMock.Setup(x => x.GetAll()).Returns(new[]
		                    	                                                           {typeof (a_type_discoverer).Assembly});
                                    TypeDiscoverer = new TypeDiscoverer(assemblyLocatorMock.Object);
		                    	};
	}
}
