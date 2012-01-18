using System;
using System.ServiceModel.Web;
using Bifrost.Execution;

namespace Bifrost.WCF.Execution
{
	public class ContainerWebServiceHost : WebServiceHost
	{
		IContainer _container;

		public ContainerWebServiceHost()
		{
		}

		public ContainerWebServiceHost(object singletonInstance, params Uri[] baseAddresses)
			: base(singletonInstance, baseAddresses)
		{
		}

		public ContainerWebServiceHost(IContainer container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			_container = container;
		}

		protected override void OnOpening()
		{
			if (_container == null)
				throw new ApplicationException("Container was not passed into ContainerWebServiceHost");

			Description.Behaviors.Add(new ContainerServiceBehavior(_container));
			base.OnOpening();
		}
	}
}