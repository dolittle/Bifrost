/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implemenation of <see cref="IApplicationResource"/>
    /// </summary>
    public class ApplicationResource : IApplicationResource
    { 
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResource"/>
        /// </summary>
        /// <param name="name"><see cref="ApplicationResourceName">Name</see> of the <see cref="IApplicationResource">application resource</see></param>
        /// <param name="type"><see cref="IApplicationResourceType">Type</see> of the <see cref="IApplicationResource">application resource</see></param>
        public ApplicationResource(ApplicationResourceName name, IApplicationResourceType type)
        {
            Name = name;
            Type = type;
        }

        /// <inheritdoc/>
        public ApplicationResourceName Name { get; }

        /// <inheritdoc/>
        public IApplicationResourceType Type { get; }
    }
}