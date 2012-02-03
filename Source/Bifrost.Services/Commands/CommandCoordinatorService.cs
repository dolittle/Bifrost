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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Serialization;

namespace Bifrost.Services.Commands
{
    public class CommandCoordinatorService
    {
        ICommandCoordinator _commandCoordinator;
        ISerializer _serializer;
        ITypeDiscoverer _typeDiscoverer;
        ISagaLibrarian _sagaLibrarian;
        Dictionary<string, Type> _commandTypes;


        public CommandCoordinatorService(
            ICommandCoordinator commandCoordinator, 
            ISerializer serializer,
            ITypeDiscoverer typeDiscoverer,
            ISagaLibrarian sagaLibrarian)
        {
            _commandCoordinator = commandCoordinator;
            _serializer = serializer;
            _typeDiscoverer = typeDiscoverer;
            _sagaLibrarian = sagaLibrarian;
            PopulateCommandTypes();
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
                var commandInstance = GetCommandFromDescriptor(commandDescriptor);
                if (commandInstance == null) {
                    var commandResult = CommandResult.ForCommand(commandInstance);
                    commandResult.Exception = new UnknownCommandException(commandDescriptor.Name);
                    return new[] { commandResult };
                }

                results.Add(_commandCoordinator.Handle(saga, commandInstance));
            }

            return results.ToArray();
        }

        void PopulateCommandTypes()
        {
            var commands = _typeDiscoverer.FindMultiple<ICommand>();
            _commandTypes = commands.Select(c => c).ToDictionary(c => c.Name);
        }

        ICommand GetCommandFromDescriptor(CommandDescriptor commandDescriptor)
        {
            var commandName = commandDescriptor.Name;
            if (_commandTypes.ContainsKey(commandName))
            {
                var commandInstance = _serializer.FromJson(_commandTypes[commandName], commandDescriptor.Command) as ICommand;
                return commandInstance;
            }
            return null;
        }
    }
}
