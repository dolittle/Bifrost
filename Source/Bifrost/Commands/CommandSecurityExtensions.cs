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
using Bifrost.Security;
using System;

namespace Bifrost.Commands
{
    /// <summary>
    /// Extensions for building a security descriptor specific for <see cref="ICommand">commands</see>
    /// </summary>
    public static class CommandSecurityExtensions
    {
        /// <summary>
        /// Add an <see cref="HandleCommand">action</see> to describe the handling of <see cref="ICommand">commands</see>
        /// </summary>
        /// <param name="descriptorBuilder"><see cref="ISecurityDescriptorBuilder"/> to extend</param>
        /// <returns><see cref="HandleCommandSecurityAction"/> for describing the action</returns>
        public static HandleCommand Handling(this ISecurityDescriptorBuilder descriptorBuilder)
        {
            var action = new HandleCommand();
            descriptorBuilder.Descriptor.AddAction(action);
            return action;
        }

        /// <summary>
        /// Add a <see cref="CommandSecurityTarget"/> to the <see cref="HandleCommand">action</see>
        /// </summary>
        /// <param name="action"><see cref="HandleCommand">Action</see> to add to</param>
        /// <returns><see cref="CommandSecurityTarget"/></returns>
        public static CommandSecurityTarget Commands(this HandleCommand action)
        {
            var target = new CommandSecurityTarget();
            action.AddTarget(target);
            return target;
        }

        /// <summary>
        /// Add a <see cref="NamespaceSecurable"/> to the <see cref="CommandSecurityTarget"/> for a given namespace
        /// </summary>
        /// <param name="target"><see cref="CommandSecurityTarget"/> to add to</param>
        /// <param name="namespace">Namespace to secure</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="NamespaceSecurable"/> for the specific namespace</returns>
        public static CommandSecurityTarget InNamespace(this CommandSecurityTarget target, string @namespace, Action<NamespaceSecurable> continueWith)
        {
            var securable = new NamespaceSecurable(@namespace);
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }

        /// <summary>
        /// Add a <see cref="TypeSecurable"/> to the <see cref="CommandSecurityTarget"/> for a given type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICommand"/> to secure</typeparam>
        /// <param name="target"><see cref="CommandSecurityTarget"/> to add to</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="TypeSecurable"/> for the specific type</returns>
        public static CommandSecurityTarget InstanceOf<T>(this CommandSecurityTarget target, Action<TypeSecurable> continueWith) where T : ICommand
        {
            var securable = new TypeSecurable(typeof(T));
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }
    }
}
