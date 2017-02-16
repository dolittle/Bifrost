/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Application
{
    /// <summary>
    /// Defines a builder for building <see cref="IApplicationConfiguration"/>
    /// </summary>
    public interface IApplicationConfigurationBuilder
    {
        /// <summary>
        /// Gets the <see cref="ApplicatioName">name</see> of the application
        /// </summary>
        ApplicationName Name { get; }

        /// <summary>
        /// Build <see cref="IApplicationStructure"/> for the <see cref="IApplication"/>
        /// </summary>
        /// <param name="structureConfigurationBuilder"></param>
        IApplicationConfigurationBuilder Structure(Func<IApplicationStructureConfigurationBuilder, IApplicationStructureConfigurationBuilder> structureConfigurationBuilder);

        /// <summary>
        /// Builds the <see cref="IApplicationConfiguration"/>
        /// </summary>
        /// <returns>A built version of the <see cref="IApplicationConfigurationBuilder"/></returns>
        IApplicationConfiguration Build();
    }
}