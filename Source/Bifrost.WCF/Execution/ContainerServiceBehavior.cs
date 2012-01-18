using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Bifrost.Execution;

namespace Bifrost.WCF.Execution
{
	public class ContainerServiceBehavior : IServiceBehavior
	{
		IContainer _container;

		public ContainerServiceBehavior(IContainer container)
		{
			_container = container;
		}


		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (var endpointDispatcher in
				serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>().SelectMany(
					channelDispatcher => channelDispatcher.Endpoints))
			{
				endpointDispatcher.DispatchRuntime.InstanceProvider = new ContainerInstanceProvider(_container, serviceDescription.ServiceType);
			}
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}