/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
    /// <summary>
    /// Delegate that gets called when after a <see cref="ICommand"/> is handled - regardless wether or not it was successful or not
    /// </summary>
    /// <param name="command"><see cref="ICommand"/> that was handled</param>
    /// <param name="result"><see cref="CommandResult"/> for the handled <see cref="ICommand"/></param>
    public delegate void CommandHandled(ICommand command, CommandResult result);
}
