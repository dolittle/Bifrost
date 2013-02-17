#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using System.Collections.Generic;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Serialization;

namespace Bifrost.Web.Commands
{
    public class CommandCoordinatorService
    {
        ICommandCoordinator _commandCoordinator;
        ICommandTypeManager _commandTypeManager;
        ISerializer _serializer;
        ITypeDiscoverer _typeDiscoverer;
        ISagaLibrarian _sagaLibrarian;


        public CommandCoordinatorService(
            ICommandCoordinator commandCoordinator, 
            ICommandTypeManager commandTypeManager,
            ISerializer serializer,
            ITypeDiscoverer typeDiscoverer,
            ISagaLibrarian sagaLibrarian)
        {
            _commandCoordinator = commandCoordinator;
            _commandTypeManager = commandTypeManager;
            _serializer = serializer;
            _typeDiscoverer = typeDiscoverer;
            _sagaLibrarian = sagaLibrarian;
        }

        public CommandResult Handle(CommandDescriptor commandDescriptor)
        {
            var commandInstance = GetCommandFromDescriptor(commandDescriptor);
            if (commandInstance == null)
                return new CommandResult { Exception = new UnknownCommandException(commandDescriptor.Name) };

            var result = _commandCoordinator.Handle(commandInstance);
            return result;
        }

        public IEnumerable<CommandResult> HandleForSaga(Guid sagaId, CommandDescriptor[] commandDescriptors)
        {
            var results = new List<CommandResult>();
            var saga = _sagaLibrarian.Get(sagaId);

            // Todo : IMPORTANT : We need to treat this as a unit of work with rollbacks if one or more commands fail and some succeed!!!!!!!!!!! 
            foreach (var commandDescriptor in commandDescriptors)
            {
                ICommand commandInstance = null;
                try
                {
                    commandInstance = GetCommandFromDescriptor(commandDescriptor);
                    results.Add(_commandCoordinator.Handle(saga, commandInstance));
                }
                catch (Exception ex)
                {
                    var commandResult = CommandResult.ForCommand(commandInstance);
                    commandResult.Exception = ex;
                    return new[] { commandResult };
                }
            }

            return results.ToArray();
        }

        ICommand GetCommandFromDescriptor(CommandDescriptor commandDescriptor)
        {
            var commandName = commandDescriptor.Name;
            var commandType = _commandTypeManager.GetFromName(commandName);
            var commandInstance = _serializer.FromJson(commandType, commandDescriptor.Command) as ICommand;
            return commandInstance;
        }
    }
}
