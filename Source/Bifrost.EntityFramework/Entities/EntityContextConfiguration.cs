/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Entities;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Represents an <see cref="IEntityContextConfiguration"/> specific for EntityFramework
    /// </summary>
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public string ConnectionString { get; set; }

#pragma warning disable 1591 // Xml Comments
        public Type EntityContextType { get { return typeof(EntityContext<>); } }
        public IEntityContextConnection Connection { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
