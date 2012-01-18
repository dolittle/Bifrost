#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Linq;
using System.Reflection;

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
			DefaultScope = BindingLifecycle.None;
		}

#pragma warning disable 1591 // Xml Comments
		public override bool CanResolve(Type service)
		{
			var type = GetServiceInstanceType(service);
			return type != null;
		}

		public override void Resolve(IContainer container, Type service)
		{
			var serviceInstanceType = GetServiceInstanceType(service);
			if (null != serviceInstanceType)
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
				var serviceInstanceType = service.Assembly.GetType(instanceName);

                if (null != serviceInstanceType && IsAssignableFrom(service,serviceInstanceType))
				{
					if (serviceInstanceType.IsAbstract )
					{
						return null;
					}

					return serviceInstanceType;
				}
			}
			return null;
		}

        static bool IsAssignableFrom(Type service, Type serviceInstanceType)
        {
            var isAssignable = service.IsAssignableFrom(serviceInstanceType);
            if (isAssignable)
                return true;

            isAssignable = serviceInstanceType.GetInterfaces().Where(t =>
                t.Name == service.Name &&
                t.Namespace == service.Namespace).Count() == 1;

            return isAssignable;
        }
	}
}
