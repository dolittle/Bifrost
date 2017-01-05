/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Globalization;
using Bifrost.Lifecycle;
using Bifrost.Sagas;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandCoordinator">ICommandCoordinator</see>
    /// </summary>
    public class CommandCoordinator : ICommandCoordinator
	{
		readonly ICommandHandlerManager _commandHandlerManager;
		readonly ICommandContextManager _commandContextManager;
	    readonly ICommandValidators _commandValidationService;
        readonly ICommandSecurityManager _commandSecurityManager;
		readonly ILocalizer _localizer;


		/// <summary>
		/// Initializes a new instance of the <see cref="CommandCoordinator">CommandCoordinator</see>
		/// </summary>
		/// <param name="commandHandlerManager">A <see cref="ICommandHandlerManager"/> for handling commands</param>
		/// <param name="commandContextManager">A <see cref="ICommandContextManager"/> for establishing a <see cref="CommandContext"/></param>
        /// <param name="commandSecurityManager">A <see cref="ICommandSecurityManager"/> for dealing with security and commands</param>
		/// <param name="commandValidators">A <see cref="ICommandValidators"/> for validating a <see cref="ICommand"/> before handling</param>
		/// <param name="localizer">A <see cref="ILocalizer"/> to use for controlling localization of current thread when handling commands</param>
		public CommandCoordinator(
			ICommandHandlerManager commandHandlerManager,
			ICommandContextManager commandContextManager,
            ICommandSecurityManager commandSecurityManager,
            ICommandValidators commandValidators,
			ILocalizer localizer)
		{
			_commandHandlerManager = commandHandlerManager;
			_commandContextManager = commandContextManager;
            _commandSecurityManager = commandSecurityManager;
		    _commandValidationService = commandValidators;
	    	_localizer = localizer;
		}

#pragma warning disable 1591 // Xml Comments
		public CommandResult Handle(ISaga saga, ICommand command)
		{
            return Handle(_commandContextManager.EstablishForSaga(saga,command), command);
		}

		public CommandResult Handle(ICommand command)
		{
		    return Handle( _commandContextManager.EstablishForCommand(command),command);
		}

        CommandResult Handle(ITransaction transaction, ICommand command)
        {
            var commandResult = new CommandResult();
            try
            {
                using (_localizer.BeginScope())
                {
                    commandResult = CommandResult.ForCommand(command);

                    var authorizationResult = _commandSecurityManager.Authorize(command);
                    if (!authorizationResult.IsAuthorized)
                    {
                        commandResult.SecurityMessages = authorizationResult.BuildFailedAuthorizationMessages();
                        transaction.Rollback();
                        return commandResult;
                    }

                    var validationResult = _commandValidationService.Validate(command);
                    commandResult.ValidationResults = validationResult.ValidationResults;
                    commandResult.CommandValidationMessages = validationResult.CommandErrorMessages;

                    if (commandResult.Success)
                    {
                        try
                        {
                            _commandHandlerManager.Handle(command);
                            transaction.Commit();
                        }
                        catch (TargetInvocationException ex)
                        {
                            commandResult.Exception = ex.InnerException;
                            transaction.Rollback();
                        }
                        catch (Exception exception)
                        {
                            commandResult.Exception = exception;
                            transaction.Rollback();
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                commandResult.Exception = ex.InnerException;
            }
            catch (Exception ex)
            {
                commandResult.Exception = ex;
            }

            return commandResult;            
        }
#pragma warning restore 1591 // Xml Comments
	}
}