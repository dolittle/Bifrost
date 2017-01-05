/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
    /// <summary>
    /// Defines something that can hold a <see cref="ICommand"/> instance
    /// </summary>
    public interface IHoldCommandInstance
    {
        /// <summary>
        /// Gets or sets the instance of a <see cref="ICommand"/>
        /// </summary>
        ICommand CommandInstance { get; set; }
    }
}
