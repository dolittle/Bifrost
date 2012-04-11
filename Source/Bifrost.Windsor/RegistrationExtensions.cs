using Bifrost.Execution;
using Castle.Windsor;

namespace Bifrost.Windsor
{
	public static class RegistrationExtensions
	{
		public static ComponentRegistration<object> WithLifecycle(ComponentRegistration<object> registration, BindingLifecycle lifecycle)
		{
            switch (lifecycle)
            {
                case BindingLifecycle.Singleton:
                    return registration.LifestyleSingleton();
                case BindingLifecycle.Thread : 
					return registration.LifestylePerThread();
                case BindingLifecycle.Transient:
					return registration.LifestyleTransient();
				case BindingLifecycle.Request:
					return registration.LifestylePerWebRequest();
            }

			return registration;
		}
	}
}

