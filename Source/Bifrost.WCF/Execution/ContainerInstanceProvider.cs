using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Bifrost.Execution;

namespace Bifrost.WCF.Execution
{
	public class ContainerInstanceProvider : IInstanceProvider
	{
		IContainer _container;
		Type _serviceType;

		public ContainerInstanceProvider(IContainer container, Type serviceType)
		{
			_container = container;
			_serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return _container.Get(_serviceType);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
		}
	}
}