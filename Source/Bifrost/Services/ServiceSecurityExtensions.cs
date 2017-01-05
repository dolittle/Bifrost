/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Security;

namespace Bifrost.Services
{
    /// <summary>
    /// Extensions for building a security descriptor specific for services
    /// </summary>
    public static class ServiceSecurityExtensions
    {
        /// <summary>
        /// Add an <see cref="InvokeService">action</see> to describe the invoking of services
        /// </summary>
        /// <param name="descriptorBuilder"><see cref="ISecurityDescriptorBuilder"/> to extend</param>
        /// <returns><see cref="InvokeService"/> for describing the action</returns>
        public static InvokeService Invoking(this ISecurityDescriptorBuilder descriptorBuilder)
        {
            var action = new InvokeService();
            descriptorBuilder.Descriptor.AddAction(action);
            return action;
        }

        /// <summary>
        /// Add a <see cref="ServiceSecurityTarget"/> to the <see cref="InvokeService">action</see>
        /// </summary>
        /// <param name="action"><see cref="InvokeService">Action</see> to add to</param>
        /// <returns><see cref="ServiceSecurityTarget"/></returns>
        public static ServiceSecurityTarget Services(this InvokeService action)
        {
            var target = new ServiceSecurityTarget();
            action.AddTarget(target);
            return target;
        }

        /// <summary>
        /// Add a <see cref="NamespaceSecurable"/> to the <see cref="ServiceSecurityTarget"/> for a given namespace
        /// </summary>
        /// <param name="target"><see cref="ServiceSecurityTarget"/> to add to</param>
        /// <param name="namespace">Namespace to secure</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="NamespaceSecurable"/> for the specific namespace</returns>
        public static ServiceSecurityTarget InNamespace(this ServiceSecurityTarget target, string @namespace, Action<NamespaceSecurable> continueWith)
        {
            var securable = new NamespaceSecurable(@namespace);
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }

        /// <summary>
        /// Add a <see cref="TypeSecurable"/> to the <see cref="ServiceSecurityTarget"/> for a given type
        /// </summary>
        /// <param name="target"><see cref="ServiceSecurityTarget"/> to add to</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="TypeSecurable"/> for the specific type</returns>
        public static ServiceSecurityTarget InstanceOf<T>(this ServiceSecurityTarget target, Action<TypeSecurable> continueWith)
        {
            var securable = new TypeSecurable(typeof(T));
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }
    }
}
