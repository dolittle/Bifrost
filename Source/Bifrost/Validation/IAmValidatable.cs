/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Validation
{
    /// <summary>
    /// Marker interface to indicate that a type is validatable.
    /// </summary>
    /// <remarks>Intended to be used on value objects and concepts.  Command and Queries are inherently validatable.</remarks>
    public interface IAmValidatable
    {}
}