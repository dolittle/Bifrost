/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Data.Entity.ModelConfiguration;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Represents a "null" implementation of an <see cref="EntityTypeConfiguration{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullEntityTypeConfiguration<T> : EntityTypeConfiguration<T>
        where T:class
    {
    }
}
