/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Mapping;

namespace Bifrost.Entities.Files.Mapping
{
    /// <summary>
    /// Represents the base mapping for mapping any type to a <see cref="File"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DocumentMapFor<T> : Map<T, Document>
    {
    }
}
