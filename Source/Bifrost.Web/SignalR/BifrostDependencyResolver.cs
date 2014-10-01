#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
using System.Linq;
using Bifrost.Execution;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Web.SignalR
{
    public class BifrostDependencyResolver : DefaultDependencyResolver
    {
        IContainer _container;

        public BifrostDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            var service = base.GetService(serviceType);
            if (service == null)
                try { service = _container.Get(serviceType); }
                catch { }

            return service;

            /*
            if (!IsSignalRInternalType(serviceType))
            //if( _container.HasBindingFor(serviceType) )
                return _container.Get(serviceType);

            return base.GetService(serviceType);*/
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            //if (!IsSignalRInternalType(serviceType) )
            if (_container.HasBindingFor(serviceType))
                return _container.GetAll(serviceType).Concat(base.GetServices(serviceType));

            return base.GetServices(serviceType);
        }

        
        bool IsSignalRInternalType(Type serviceType)
        {
            return serviceType.Namespace.StartsWith("Microsoft") || serviceType.Namespace.StartsWith("System") || serviceType.Namespace.StartsWith("Owin");
        }

    }
}
