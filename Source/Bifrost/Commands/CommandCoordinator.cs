#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Linq.Expressions;
using Bifrost.Domain;
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
	    readonly IDynamicCommandFactory _dynamicCommandFactory;
		readonly ILocalizer _localizer;


		/// <summary>
		/// Initializes a new instance of the <see cref="CommandCoordinator">CommandCoordinator</see>
		/// </summary>
		/// <param name="commandHandlerManager">A <see cref="ICommandHandlerManager"/> for handling commands</param>
		/// <param name="commandContextManager">A <see cref="ICommandContextManager"/> for establishing a <see cref="CommandContext"/></param>
        /// <param name="commandSecurityManager">A <see cref="ICommandSecurityManager"/> for dealing with security and commands</param>
		/// <param name="commandValidationService">A <see cref="ICommandValidationService"/> for validating a <see cref="ICommand"/> before handling</param>
		/// <param name="dynamicCommandFactory">A <see cref="IDynamicCommandFactory"/> creating dynamic commands</param>
		/// <param name="localizer">A <see cref="ILocalizer"/> to use for controlling localization of current thread when handling commands</param>
		public CommandCoordinator(
			ICommandHandlerManager commandHandlerManager,
			ICommandContextManager commandContextManager,
            ICommandSecurityManager commandSecurityManager,
            ICommandValidationService commandValidationService,
            IDynamicCommandFactory dynamicCommandFactory,
			ILocalizer localizer)
		{
			_commandHandlerManager = commandHandlerManager;
			_commandContextManager = commandContextManager;
            _commandSecurityManager = commandSecurityManager;
		    _commandValidationService = commandValidationService;
	        _dynamicCommandFactory = dynamicCommandFactory;
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
                return commandResult;
            }            
        }

	    public CommandResult Handle<T>(ISaga saga, Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot
	    {
			var command = _dynamicCommandFactory.Create(aggregatedRootId, method);
			var result = Handle(saga, command);
			return result;
	    }

	    public CommandResult Handle<T>(Guid aggregatedRootId, Expression<Action<T>> method) where T : AggregatedRoot
	    {
	        var command = _dynamicCommandFactory.Create(aggregatedRootId, method);
	        var result = Handle(command);
	        return result;
	    }
#pragma warning restore 1591 // Xml Comments


	}
}