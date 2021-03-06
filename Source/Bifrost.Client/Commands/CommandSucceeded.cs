﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
    /// <summary>
    /// Delegate that gets called when a <see cref="ICommand"/> is successfully handled
    /// </summary>
    /// <param name="command"><see cref="ICommand"/> that succeeded</param>
    /// <param name="result"><see cref="CommandResult"/> for the handled <see cref="ICommand"/></param>
    public delegate void CommandSucceeded(ICommand command, CommandResult result);
}
