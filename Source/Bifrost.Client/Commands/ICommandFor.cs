/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Windows.Input;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="System.Windows.Input.ICommand"/> for a Bifrost <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="ICommand"/> to represent</typeparam>
    /// <remarks>
    /// This is a bridge interface for being able to use the build in functionality of
    /// the XAML platform without taking too many dependencies on infrastructure for 
    /// working with <see cref="ICommand">commands</see>
    /// </remarks>
    public interface ICommandFor<T> : System.Windows.Input.ICommand, ICommandProcess
        where T:Bifrost.Commands.ICommand
    {
        /// <summary>
        /// Gets or sets the instance of the <see cref="ICommand"/>
        /// </summary>
        T Instance { get; set;  }
    }
}
