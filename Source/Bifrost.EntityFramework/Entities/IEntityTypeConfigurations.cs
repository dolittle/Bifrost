/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Defines a system that knows about all <see cref="EntityTypeConfiguration{T}"/> implementations
    /// </summary>
    public interface IEntityTypeConfigurations 
    {
        /// <summary>
        /// Get configuration for a specific type
        /// </summary>
        /// <typeparam name="T">Type to get for</typeparam>
        /// <returns><see cref="EntityTypeConfiguration{T}"/> instance - if non is found, returns a <see cref="NullEntityTypeConfiguration{T}"/></returns>
        EntityTypeConfiguration<T> GetFor<T>() where T : class;
    }
}
