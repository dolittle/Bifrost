/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="ISecurityDescriptor"/>
    /// </summary>
    public class BaseSecurityDescriptor : ISecurityDescriptor
    {
        List<ISecurityAction> _actions = new List<ISecurityAction>();

        /// <summary>
        /// Initializes a new instance of <see cref="BaseSecurityDescriptor"/>
        /// </summary>
        public BaseSecurityDescriptor()
        {
            When = new SecurityDescriptorBuilder(this);
        }

#pragma warning disable 1591 // Xml Comments

        public ISecurityDescriptorBuilder When { get; private set; }

        public void AddAction(ISecurityAction securityAction)
        {
            _actions.Add(securityAction);
        }

        public IEnumerable<ISecurityAction> Actions { get { return _actions; } }
        
        public bool CanAuthorize<T>(object instanceToAuthorize) where T : ISecurityAction
        {
            return _actions.Where(a => a.GetType() == typeof(T)).Any(a => a.CanAuthorize(instanceToAuthorize));
        }

        public AuthorizeDescriptorResult Authorize(object instanceToAuthorize)
        {
            var result = new AuthorizeDescriptorResult();
            foreach (var action in Actions.Where(a => a.CanAuthorize(instanceToAuthorize)))
            {
               result.ProcessAuthorizeActionResult(action.Authorize(instanceToAuthorize));
            }
            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
