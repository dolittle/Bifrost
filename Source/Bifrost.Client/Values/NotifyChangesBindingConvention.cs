#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.ComponentModel;
using Bifrost.Execution;
using IContainer = Bifrost.Execution.IContainer;

namespace Bifrost.Values
{
    /// <summary>
    /// Represents a <see cref="IBindingConvention"/> that will associate any
    /// type adorned with the <see cref="NotifyChangesAttribute"/> with a 
    /// proxy type implementing <see cref="INotifyPropertyChanged"/> using
    /// the <see cref="NotifyingProxyWeaver"/>
    /// </summary>
    public class NotifyChangesBindingConvention : IBindingConvention
    {
        NotifyingObjectWeaver _weaver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyChangesBindingConvention"/>
        /// </summary>
        public NotifyChangesBindingConvention()
        {
            _weaver = new NotifyingObjectWeaver();
        }


#pragma warning disable 1591 // Xml Comments
        public bool CanResolve(IContainer container, Type service)
        {
            if (service.Name.Contains("ViewModel"))
            {
                var i = 0;
                i++;
            }
            return service.GetCustomAttributes(typeof(NotifyChangesAttribute), false).Length == 1;
        }

        public void Resolve(IContainer container, Type service)
        {
            container.Bind(service, _weaver.GetProxyType(service));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
