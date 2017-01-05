/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.DocumentDB.Entities
{
    /// <summary>
    /// Implements the <see cref="IEntityContextConfiguration"/> specific for the DocumentDB support
    /// </summary>
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        /// <summary>
        /// Gets or sets the url endpoint for the database server
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the database id 
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the authorization key
        /// </summary>
        public string AuthorizationKey { get; set; }


#pragma warning disable 1591 // Xml Comments
        public Type EntityContextType { get { return typeof(EntityContext<>); } }

        public IEntityContextConnection Connection { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
