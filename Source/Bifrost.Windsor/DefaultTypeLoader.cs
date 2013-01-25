using System;
using System.Collections;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.Registration;

namespace Bifrost.Windsor
{
	public class DefaultTypeLoader : ILazyComponentLoader
	{
		public IRegistration Load (string key, Type service, IDictionary arguments)
		{         
			if( !service.IsInterface && !service.IsAbstract ) {
				return Component.For (service);
			}
			return null;
		}
	}
}

