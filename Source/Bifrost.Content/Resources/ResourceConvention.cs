﻿#region License
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
using Bifrost.Execution;
using Bifrost.Extensions;
using Castle.DynamicProxy;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents a <see cref="IBindingConvention"/> that resolves anything implementing <see cref="IHaveResources"/>
    /// </summary>
	public class ResourceConvention : BaseConvention
	{
        readonly ProxyGenerator _proxyGenerator;

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceConvention"/>
        /// </summary>
        public ResourceConvention()
        {
            _proxyGenerator = new ProxyGenerator();
        }

#pragma warning disable 1591 // Xml Comments
        public override bool CanResolve(IContainer container, Type service)
        {
            var hasIStrings = service.HasInterface<IHaveResources>();
                
			return hasIStrings;
		}

		public override void Resolve(IContainer container, Type service)
		{
			var interceptor = container.Get<ResourceInterceptor>();
			var proxy = _proxyGenerator.CreateClassProxy(service, interceptor);
			container.Bind(service,proxy);
        }
#pragma warning restore 1591 // Xml Comments
    }
}