/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="Securable"/> that applies to a specific <see cref="System.Type"/>
    /// </summary>
    public class TypeSecurable : Securable
    {
        const string TYPE = "OfType_{{{0}}}";

        /// <summary>
        /// Initializes an instance of <see cref="TypeSecurable"/>
        /// </summary>
        /// <param name="type"><see cref="System.Type"/> to secure</param>
        public TypeSecurable(Type type) : base(string.Format(TYPE,type.FullName))
        {
            Type = type;
        }


        /// <summary>
        /// Gets the type that is secured
        /// </summary>
        public Type Type { get; private set; }

#pragma warning disable 1591
        public override bool CanAuthorize(object actionToAuthorize)
        {
            return actionToAuthorize!= null && Type == actionToAuthorize.GetType();
        }
#pragma warning restore 1591
    }
}
