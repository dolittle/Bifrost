using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;
using Bifrost.Configuration;

namespace Bifrost.WCF.Execution
{
	public class ContainerServiceHostFactory : WebServiceHostFactory
	{
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
            var container = Configure.Instance.Container;
			return new ContainerWebServiceHost(container, serviceType, baseAddresses);
		}
	}
}