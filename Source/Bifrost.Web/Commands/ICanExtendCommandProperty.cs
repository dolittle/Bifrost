/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.CodeGeneration.JavaScript;

namespace Bifrost.Web.Commands
{
    /// <summary>
    /// Defines the extender for properties on commands - a visitor that can take part of the proxy generation of properties on commands
    /// </summary>
    public interface ICanExtendCommandProperty
    {
        /// <summary>
        /// Extend a given property
        /// </summary>
        /// <param name="commandType">Type of command property belongs to</param>
        /// <param name="propertyName">Name of the property that can be extended</param>
        /// <param name="observable">The observable that can be extended</param>
        void Extend(Type commandType, string propertyName, Observable observable);
    }
}
