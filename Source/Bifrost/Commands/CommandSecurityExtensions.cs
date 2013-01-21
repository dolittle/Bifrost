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
        /// Add a <see cref="HandleCommandSecurityActionBuilder"/> to describe the handling of <see cref="ICommand">commands</see>
        /// </summary>
        /// <param name="descriptorBuilder"><see cref="ISecurityDescriptorBuilder"/> to extend</param>
        /// <returns><see cref="HandleCommandSecurityActionBuilder"/> for describing the action</returns>
        public static HandleCommandSecurityActionBuilder Handling(this ISecurityDescriptorBuilder descriptorBuilder)
        {
            var action = new HandleCommand();
            var actionBuilder = new HandleCommandSecurityActionBuilder(action);
            descriptorBuilder.Descriptor.AddAction(action);
            return actionBuilder;
        }

        /// <summary>
        /// Add a <see cref="NamespaceSecurable"/> to the <see cref="CommandSecurityTargetBuilder"/> for a given namespace
        /// </summary>
        /// <param name="securityTargetBuilder"><see cref="CommandSecurityTargetBuilder"/> to add to</param>
        /// <param name="namespace">Namespace to secure</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="NamespaceSecurable"/> for the specific namespace</returns>
        public static CommandSecurityTargetBuilder InNamespace(this CommandSecurityTargetBuilder securityTargetBuilder, string @namespace, Action<NamespaceSecurableBuilder> continueWith)
        {
            var securable = new NamespaceSecurable(@namespace);
            var securableBuilder = new NamespaceSecurableBuilder(securable);
            securityTargetBuilder.Target.AddSecurable(securable);
            continueWith(securableBuilder);
            return securityTargetBuilder;
        }

        /// <summary>
        /// Add a <see cref="TypeSecurable"/> to the <see cref="CommandSecurityTargetBuilder"/> for a given type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ICommand"/> to secure</typeparam>
        /// <param name="securityTargetBuilder"><see cref="CommandSecurityTargetBuilder"/> to add to</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="TypeSecurable"/> for the specific type</returns>
        public static CommandSecurityTargetBuilder InstanceOf<T>(this CommandSecurityTargetBuilder securityTargetBuilder, Action<TypeSecurableBuilder> continueWith) where T : ICommand
        {
            var securable = new TypeSecurable(typeof(T));
            var securableBuilder = new TypeSecurableBuilder(securable);
            securityTargetBuilder.Target.AddSecurable(securable);
            continueWith(securableBuilder);
            return securityTargetBuilder;
        }
    }
}
