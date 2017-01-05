/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Reflection;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="System.Windows.Input.ICommand"/> that knows how
    /// to handle an invocation coming through an interceptor
    /// </summary>
    public class CommandInvocationHandler : System.Windows.Input.ICommand, INeedTargetInstance, INeedProxyInstance
    {
        ICommandCoordinator _commandCoordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandInvocationHandler"/>
        /// </summary>
        /// <param name="commandCoordinator"></param>
        public CommandInvocationHandler(ICommandCoordinator commandCoordinator)
        {
            _commandCoordinator = commandCoordinator;
        }


#pragma warning disable 1591 // Xml Comments
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = (s, e) => { };

        public void Execute(object parameter)
        {
            var command = TargetInstance.GetType().GetProperty("Instance").GetValue(TargetInstance) as ICommand;
            var result = _commandCoordinator.Handle(command);
            var process = Proxy as ICanProcessCommandProcess;
            process.Process(command, result);
        }

        public object Proxy { get; set; }

        public System.Windows.Input.ICommand TargetInstance { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
