﻿#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Globalization;
using Bifrost.Lifecycle;
using Bifrost.Sagas;
using Bifrost.Validation;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandCoordinator">ICommandCoordinator</see>
	/// </summary>
	public class CommandCoordinator : ICommandCoordinator
	{
		readonly ICommandHandlerManager _commandHandlerManager;
		readonly ICommandContextManager _commandContextManager;
	    readonly ICommandValidationService _commandValidationService;
        readonly ICommandSecurityManager _commandSecurityManager;
        readonly ICommandStatistics _commandStatistics;
		readonly ILocalizer _localizer;


		/// <summary>
		/// Initializes a new instance of the <see cref="CommandCoordinator">CommandCoordinator</see>
		/// </summary>
		/// <param name="commandHandlerManager">A <see cref="ICommandHandlerManager"/> for handling commands</param>
		/// <param name="commandContextManager">A <see cref="ICommandContextManager"/> for establishing a <see cref="CommandContext"/></param>
        /// <param name="commandSecurityManager">A <see cref="ICommandSecurityManager"/> for dealing with security and commands</param>
		/// <param name="commandValidationService">A <see cref="ICommandValidationService"/> for validating a <see cref="ICommand"/> before handling</param>
        /// <param name="commandStatistics">A <see cref="ICommandStatistics"/> for generating command statistics</param>
		/// <param name="localizer">A <see cref="ILocalizer"/> to use for controlling localization of current thread when handling commands</param>
		public CommandCoordinator(
			ICommandHandlerManager commandHandlerManager,
			ICommandContextManager commandContextManager,
            ICommandSecurityManager commandSecurityManager,
            ICommandValidationService commandValidationService,
            ICommandStatistics commandStatistics,
			ILocalizer localizer)
		{
			_commandHandlerManager = commandHandlerManager;
			_commandContextManager = commandContextManager;
            _commandSecurityManager = commandSecurityManager;
		    _commandValidationService = commandValidationService;
            _commandStatistics = commandStatistics;
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

        CommandResult Handle(IUnitOfWork unitOfWork, ICommand command)
        {
            using (_localizer.BeginScope())
            {
                var commandResult = CommandResult.ForCommand(command);

                var authorizationResult = _commandSecurityManager.Authorize(command);
                if (!authorizationResult.IsAuthorized)
                {
                    commandResult.SecurityMessages = authorizationResult.BuildFailedAuthorizationMessages();
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
                        unitOfWork.Commit();
                    }
                    catch (Exception exception)
                    {
                        commandResult.Exception = exception;
                        unitOfWork.Rollback();
                    }
                }
                _commandStatistics.Record(commandResult);
                return commandResult;
            }            
        }
#pragma warning restore 1591 // Xml Comments
	}
}