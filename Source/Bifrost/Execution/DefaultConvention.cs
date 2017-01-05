/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
	/// <summary>
	/// Represents a <see cref="IBindingConvention">IBindingConvention</see>
	/// that will apply default conventions
	/// </summary>
	/// <remarks>
	/// Any interface being resolved and is prefixed with I and have an implementation
	/// with the same name in the same namespace but without the prefix I, will automatically
	/// be resolved with this convention.
	/// </remarks>
	public class DefaultConvention : BaseConvention
	{
		/// <summary>
		/// Initializes a new instance of <see creF="DefaultConvention">DefaultConvention</see>
		/// </summary>
		public DefaultConvention()
		{
			DefaultScope = BindingLifecycle.Transient;
		}

#pragma warning disable 1591 // Xml Comments
		public override bool CanResolve(IContainer container, Type service)
		{
			var type = GetServiceInstanceType(service) ;
			return type != null && !container.HasBindingFor(type);
		}

		public override void Resolve(IContainer container, Type service)
		{
			var serviceInstanceType = GetServiceInstanceType(service);
			if (serviceInstanceType != null )
			{
                var scope = GetScopeForTarget(serviceInstanceType);
				container.Bind(service,serviceInstanceType, scope);
			}
		}
#pragma warning restore 1591 // Xml Comments


		static Type GetServiceInstanceType(Type service)
		{
			var serviceName = service.Name;
			if (serviceName.StartsWith("I"))
			{
				var instanceName = string.Format("{0}.{1}", service.Namespace, serviceName.Substring(1));
                var serviceInstanceType = service.GetTypeInfo().Assembly.GetType(instanceName);
                if (null != serviceInstanceType &&
                    IsAssignableFrom(service,serviceInstanceType) &&
                    !HasMultipleImplementationInSameNamespace(service) &&
                    !serviceInstanceType.HasAttribute<IgnoreDefaultConventionAttribute>())
				{
                    if (serviceInstanceType.GetTypeInfo().IsAbstract) return null;

					return serviceInstanceType;
				}
			}
			return null;
		}

        static bool HasMultipleImplementationInSameNamespace(Type service)
        {
            var implementationsCount = service
                .GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType())
                .Where(t => !string.IsNullOrEmpty(t.Namespace) && t.Namespace.Equals(service.Namespace))
                .Where(t => t.HasInterface(service)).Count();
            return implementationsCount > 1;
        }

        static bool IsAssignableFrom(Type service, Type serviceInstanceType)
        {
            var isAssignable = service
                .GetTypeInfo().IsAssignableFrom(serviceInstanceType.GetTypeInfo());
            if (isAssignable)
                return true;

            isAssignable = serviceInstanceType
                                    .GetTypeInfo().ImplementedInterfaces
                .Where(t =>
                t.Name == service.Name &&
                t.Namespace == service.Namespace).Count() == 1;

            return isAssignable;
        }
	}
}