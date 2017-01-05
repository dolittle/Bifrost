/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityActor"/>
    /// </summary>
    public class SecurityActor : ISecurityActor
    {
        List<ISecurityRule> _rules = new List<ISecurityRule>();

        /// <summary>
        /// Instantiates an instance of <see cref="SecurityActor"/>
        /// </summary>
        /// <param name="description">String that describes this actor type</param>
        public SecurityActor(string description)
        {
            Description = description ?? string.Empty;
        }

#pragma warning disable 1591 // Xml Comments
        public void AddRule(ISecurityRule rule)
        {
            _rules.Add(rule);
        }

        public IEnumerable<ISecurityRule> Rules { get { return _rules; } }

        public AuthorizeActorResult IsAuthorized(object actionToAuthorize)
        {
            var result = new AuthorizeActorResult(this);
            foreach (var rule in _rules)
            {
                try
                {
                    if(!rule.IsAuthorized(actionToAuthorize))
                        result.AddBrokenRule(rule);
                }
                catch (Exception ex)
                {
                    result.AddErrorRule(rule,ex);
                }
            }
            return result;
        }

        public string Description { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
