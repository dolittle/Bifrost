using Bifrost.Execution;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Core;

namespace Bifrost.Windsor
{
	public static class RegistrationExtensions
	{
		public static ComponentRegistration<T> WithLifecycle<T>(this ComponentRegistration<T> registration, BindingLifecycle lifecycle)
		{
            switch (lifecycle)
            {
                case BindingLifecycle.Singleton: 
                    return registration.LifeStyle.Is(LifestyleType.Singleton);
				
                case BindingLifecycle.Thread : 
					return registration.LifeStyle.Is(LifestyleType.Thread);
				
                case BindingLifecycle.Transient:
					return registration.LifeStyle.Is(LifestyleType.Transient);
				
				case BindingLifecycle.Request:
					return registration.LifeStyle.Is(LifestyleType.PerWebRequest);
            }
			 
			return registration;
		}
	}
}
