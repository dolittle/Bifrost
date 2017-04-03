/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a builder for building <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Gets the <see cref="ApplicationName">name</see> of the application
        /// </summary>
        ApplicationName Name { get; }

        /// <summary>
        /// Build <see cref="IApplicationStructureConfigurationBuilder"/> for the <see cref="IApplication"/>
        /// </summary>
        /// <param name="structureConfigurationBuilder"></param>
        IApplicationConfigurationBuilder Structure(Func<IApplicationStructureConfigurationBuilder, IApplicationStructureConfigurationBuilder> structureConfigurationBuilder);

        /// <summary>
        /// Builds the <see cref="IApplication"/>
        /// </summary>
        /// <returns>A built version of the <see cref="IApplication"/></returns>
        IApplication Build();
    }
}