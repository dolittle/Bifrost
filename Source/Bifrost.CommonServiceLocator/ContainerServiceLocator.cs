#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System.Collections.Generic;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.CommonServiceLocator
{
	/// <summary>
	/// Represents a <see cref="IServiceLocator"/> that works with the generic <see cref="IContainer"/>
	/// </summary>
    public class ContainerServiceLocator : IServiceLocator
    {
        readonly IContainer _container;

		/// <summary>
		/// Initializes an instance of <see cref="ContainerServiceLocator"/>
		/// </summary>
		/// <param name="container"><see cref="IContainer"/> to use</param>
        public ContainerServiceLocator(IContainer container)
        {
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
		public object GetService(Type serviceType)
        {
            return _container.Get(serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.Get(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.Get(serviceType);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        public T GetInstance<T>()
        {
            return _container.Get<T>();
        }

        public T GetInstance<T>(string key)
        {
            return _container.Get<T>();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAll<TService>();
		}
#pragma warning restore 1591 // Xml Comments

	}
}