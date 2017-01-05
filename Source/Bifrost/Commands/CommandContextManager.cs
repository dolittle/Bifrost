/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Sagas;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandContextManager">Command context manager</see>
    /// </summary>
    public class CommandContextManager : ICommandContextManager
    {
        readonly ICommandContextFactory _factory;

        [ThreadStatic] static ICommandContext _currentContext;


        static ICommandContext CurrentContext
        {
            get { return _currentContext;  }
            set { _currentContext = value; }
        }

        /// <summary>
        /// Reset context
        /// </summary>
        public static void ResetContext()
        {
            CurrentContext = null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CommandContextManager">CommandContextManager</see>
        /// </summary>
        /// <param name="factory">A <see cref="ICommandContextFactory"/> to use for building an <see cref="ICommandContext"/></param>
        public CommandContextManager(ICommandContextFactory factory)
        {
            _factory = factory;
        }

        private static bool IsInContext(ICommand command)
        {
            var inContext = null != CurrentContext && CurrentContext.Command.Equals(command);
            return inContext;
        }

#pragma warning disable 1591 // Xml Comments
        public bool HasCurrent
        {
            get { return CurrentContext != null; }
        }

        public ICommandContext GetCurrent()
        {
            if (!HasCurrent)
            {
                throw new InvalidOperationException("Command not established");
            }
            return CurrentContext;
        }

        public ICommandContext EstablishForCommand(ICommand command)
        {
            if (!IsInContext(command))
            {
                var commandContext = _factory.Build(command);
                CurrentContext = commandContext;
            }
            return CurrentContext;
        }

        public ICommandContext EstablishForSaga(ISaga saga, ICommand command)
        {
            if (!IsInContext(command))
            {
                var commandContext = _factory.Build(saga,command);

                CurrentContext = commandContext;
            }
            return CurrentContext;
        }
#pragma warning restore 1591 // Xml Comments
    }
}