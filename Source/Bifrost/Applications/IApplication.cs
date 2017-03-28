/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines an application 
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Gets the <see cref="ApplicationName">name</see> of the <see cref="IApplication"/>
        /// </summary>
        ApplicationName Name { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationStructure"/> for the <see cref="IApplication"/>
        /// </summary>
        IApplicationStructure Structure { get; }
    }
}
