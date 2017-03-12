/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines the type of an <see cref="IApplicationResource"/>
    /// </summary>
    public interface IApplicationResourceType
    {
        /// <summary>
        /// Gets the identifier of the type
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Gets the CLR type the <see cref="IApplicationResourceType"/> represents
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationArea"/> the <see cref="IApplicationResourceType">resource type</see> belongs to
        /// </summary>
        ApplicationArea Area { get; }
    }
}