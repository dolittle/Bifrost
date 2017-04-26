/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Bifrost.Exceptions;
using Bifrost.Globalization;
using Bifrost.Lifecycle;
using Bifrost.Logging;

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
        readonly IExceptionPublisher _exceptionPublisher;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCoordinator">CommandCoordinator</see>
        /// </summary>
        /// <param name="commandHandlerManager">A <see cref="ICommandHandlerManager"/> for handling commands</param>
        /// <param name="commandContextManager">A <see cref="ICommandContextManager"/> for establishing a <see cref="CommandContext"/></param>
        /// <param name="commandSecurityManager">A <see cref="ICommandSecurityManager"/> for dealing with security and commands</param>
        /// <param name="commandValidators">A <see cref="ICommandValidators"/> for validating a <see cref="ICommand"/> before handling</param>
        /// <param name="localizer">A <see cref="ILocalizer"/> to use for controlling localization of current thread when handling commands</param>
        /// <param name="exceptionPublisher">An <see cref="IExceptionPublisher"/> to send exceptions to</param>
        /// <param name="logger"><see cref="ILogger"/> to log with</param>
        public CommandCoordinator(
            ICommandHandlerManager commandHandlerManager,
            ICommandContextManager commandContextManager,
            ICommandSecurityManager commandSecurityManager,
            ICommandValidators commandValidators,
            ILocalizer localizer,
            IExceptionPublisher exceptionPublisher,
            ILogger logger)
        {
            _commandHandlerManager = commandHandlerManager;
            _commandContextManager = commandContextManager;
            _commandSecurityManager = commandSecurityManager;
            _commandValidationService = commandValidators;
            _localizer = localizer;
            _exceptionPublisher = exceptionPublisher;
            _logger = logger;
        }

        /// <inheritdoc/>
        public CommandResult Handle(ICommand command)
        {
            return Handle(_commandContextManager.EstablishForCommand(command), command);
        }

        CommandResult Handle(ITransaction transaction, ICommand command)
        {
            var commandResult = new CommandResult();
            try
            {
                using (_localizer.BeginScope())
                {
                    _logger.Information("Handle command");

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
                            _logger.Error(ex, "Error handling command");
                            _exceptionPublisher.Publish(ex);
                            commandResult.Exception = ex.InnerException;
                            transaction.Rollback();
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Error handling command");
                            _exceptionPublisher.Publish(ex);
                            commandResult.Exception = ex;
                            transaction.Rollback();
                        }
                    }
                    else
                    {
                        _logger.Information("Command was not successful, rolling back");
                        transaction.Rollback();
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                _logger.Error(ex, "Error handling command");
                _exceptionPublisher.Publish(ex);
                commandResult.Exception = ex.InnerException;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error handling command");
                _exceptionPublisher.Publish(ex);
                commandResult.Exception = ex;
            }

            return commandResult;
        }
#pragma warning restore 1591 // Xml Comments
    }
}