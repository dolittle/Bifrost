/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a base for any <see cref="ISecurityAction"/>
    /// </summary>
    public class SecurityAction : ISecurityAction
    {
        readonly List<ISecurityTarget> _targets = new List<ISecurityTarget>();

#pragma warning disable 1591 // Xml Comments
        public void AddTarget(ISecurityTarget securityTarget)
        {
            _targets.Add(securityTarget);
        }

        public bool CanAuthorize(object actionToAuthorize)
        {
            return _targets.Any(s => s.CanAuthorize(actionToAuthorize));
        }

        public AuthorizeActionResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeActionResult(this);
            foreach (var target in Targets.Where(t => t.CanAuthorize(actionToAuthorize)))
            {
                result.ProcessAuthorizeTargetResult(target.Authorize(actionToAuthorize));
            }
            return result;
        }

        public virtual string ActionType { get { return string.Empty; } }

        public IEnumerable<ISecurityTarget> Targets { get { return _targets.AsEnumerable(); } }
#pragma warning restore 1591 // Xml Comments
    }
}
