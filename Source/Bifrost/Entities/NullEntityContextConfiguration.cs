/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;

namespace Bifrost.Entities
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEntityContextConfiguration"/>
    /// </summary>
    public class NullEntityContextConfiguration : IEntityContextConfiguration
    {

        /// <summary>
        /// Initializes a new instance of <see cref="NullEntityContextConfiguration"/>
        /// </summary>
        public NullEntityContextConfiguration()
        {
            Connection = new NullEntityContextConnection();
        }


#pragma warning disable 1591 // Xml Comments
        public Type EntityContextType
        {
            get { return typeof(NullEntityContext<>); }
        }

        public IEntityContextConnection Connection { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
