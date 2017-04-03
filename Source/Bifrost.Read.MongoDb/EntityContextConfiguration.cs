/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.Read.MongoDB
{
    /// <summary>
    /// Represents a MongoDB implementation of <see cref="IEntityContextConfiguration"/>
    /// </summary>
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        /// <summary>
        /// Gets or sets the URL to the database
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets wether or not to use SSL for the connection
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Gets or sets the default database
        /// </summary>
        public string DefaultDatabase { get; set; }

        /// <inheritdoc/>
        public Type EntityContextType { get { return typeof(EntityContext<>); } }

        /// <inheritdoc/>
        public IEntityContextConnection Connection { get; set; }
    }
}
