/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Application
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationConfigurationBuilder"/>
    /// </summary>
    public class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        IApplicationStructureConfigurationBuilder _applicationStructure;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationConfigurationBuilder"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the application</param>
        public ApplicationConfigurationBuilder(ApplicationName name) : this(name, new NullApplicationStructureConfigurationBuilder())
        { 
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationConfigurationBuilder"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationName">Name</see> of the application</param>
        /// <param name="applicationStructure"><see cref="IApplicationStructureConfigurationBuilder"/> for the <see cref="IApplication"/></param>
        public ApplicationConfigurationBuilder(ApplicationName name, IApplicationStructureConfigurationBuilder applicationStructure)
        {
            Name = name;
            _applicationStructure = applicationStructure;
        }


        /// <inheritdoc/>
        public ApplicationName Name { get; }

        /// <inheritdoc/>
        public IApplicationConfigurationBuilder Structure(Func<IApplicationStructureConfigurationBuilder, IApplicationStructureConfigurationBuilder> callback)
        {
            IApplicationStructureConfigurationBuilder structureBuilder = new ApplicationStructureConfigurationBuilder();
            structureBuilder = callback(structureBuilder);
            var builder = new ApplicationConfigurationBuilder(Name, structureBuilder);
            return builder;
        }


        /// <inheritdoc/>
        public IApplication Build()
        {
            throw new NotImplementedException();
        }
    }
}
