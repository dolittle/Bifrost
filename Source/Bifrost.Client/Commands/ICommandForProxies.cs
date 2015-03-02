#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a system that can deal with proxies for <see cref="ICommandFor{T}"/>
    /// </summary>
    public interface ICommandForProxies
    {
        /// <summary>
        /// Get <see cref="ICommandFor{T}"/> for specified command type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICommand"/> to get for</typeparam>
        /// <returns>Proxied instance of <see cref="ICommandFor{0}"/> proxy</returns>
        ICommandFor<T> GetFor<T>() where T : ICommand, new();
    }
}
