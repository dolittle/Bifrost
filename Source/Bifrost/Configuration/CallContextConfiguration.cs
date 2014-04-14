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
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents the configuration for <see cref="ICallContext"/>
    /// </summary>
    public class CallContextConfiguration : ICallContextConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CallContextConfiguration"/>
        /// </summary>
        public CallContextConfiguration()
        {
            CallContextType = typeof(DefaultCallContext);
        }

#pragma warning disable 1591 // Xml Comments
        public Type CallContextType { get; set; }

        public void Initialize(IContainer container)
        {
            container.Bind<ICallContext>(CallContextType);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
