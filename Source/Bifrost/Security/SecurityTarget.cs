/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a base class for any <see cref="ISecurityTarget">security targets</see>
    /// </summary>
    public class SecurityTarget : ISecurityTarget
    {
        readonly List<ISecurable> _securables = new List<ISecurable>();

        /// <summary>
        /// Instantiats an instance of <see cref="SecurityTarget"/>
        /// </summary>
        /// <param name="description">A description for this <see cref="SecurityTarget"/></param>
        public SecurityTarget(string description)
        {
            Description = description ?? string.Empty;
        }

#pragma warning disable 1591
        public void AddSecurable(ISecurable securityObject)
        {
            _securables.Add(securityObject);
        }

        public IEnumerable<ISecurable> Securables { get { return _securables; } }

        public bool CanAuthorize(object actionToAuthorize)
        {
            return _securables.Any(s => s.CanAuthorize(actionToAuthorize));
        }

        public AuthorizeTargetResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeTargetResult(this);
            foreach (var securable in Securables)
            {
                result.ProcessAuthorizeSecurableResult(securable.Authorize(actionToAuthorize));
            }
            return result;
        }

        public string Description { get; private set; }
#pragma warning restore 1591
    }
}
