/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a base implementation of<see cref="ISecurable"/>
    /// </summary>
    public class Securable : ISecurable
    {
        readonly List<ISecurityActor> _actors = new List<ISecurityActor>();

        /// <summary>
        /// Instantiates an instance of <see cref="Securable"/>
        /// </summary>
        /// <param name="securableDescription">Description of the Securable</param>
        public Securable(string securableDescription)
        {
            Description = securableDescription ?? string.Empty;
        }

#pragma warning disable 1591 // Xml Comments

        public void AddActor(ISecurityActor actor)
        {
            _actors.Add(actor);
        }

        public IEnumerable<ISecurityActor> Actors { get { return _actors;  } }

        public virtual bool CanAuthorize(object actionToAuthorize)
        {
            return false;
        }

        public virtual AuthorizeSecurableResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeSecurableResult(this);
            foreach (var actor in _actors)
            {
                result.ProcessAuthorizeActorResult(actor.IsAuthorized(actionToAuthorize));
            }
            return result;
        }

        public string Description { get; private set; }
#pragma warning restore 1591 // Xml Comments

    }
}
