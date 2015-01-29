#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Serialization;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Web.Commands
{
    public class CommandCoordinator : Hub
    {
        ICommandCoordinator _commandCoordinator;
        ITypeDiscoverer _typeDiscoverer;
        ICommandContextConnectionManager _commandContextConnectionManager;
        ISerializer _serializer;

        public CommandCoordinator(
            ICommandCoordinator commandCoordinator,
            ITypeDiscoverer typeDiscoverer,
            ICommandContextConnectionManager commandContextConnectionManager,
            ISerializer serializer)
        {
            _commandCoordinator = commandCoordinator;
            _typeDiscoverer = typeDiscoverer;
            _commandContextConnectionManager = commandContextConnectionManager;
            _serializer = serializer;
        }

        public CommandResult Handle(CommandDescriptor descriptor)
        {
            try
            {
                var commandType = _typeDiscoverer.GetCommandTypeByName(descriptor.GeneratedFrom);
                var command = (ICommand)_serializer.FromJson(commandType, descriptor.Command);
                command.Id = descriptor.Id;
                _commandContextConnectionManager.Register(Context.ConnectionId, command.Id);
                var commandResult = _commandCoordinator.Handle(command);
                return commandResult;
            }
            catch (Exception ex)
            {
                return new CommandResult { 
                    Exception = ex, 
                    ExceptionMessage = string.Format("Exception occured of type '{0}' with message '{1}'. StackTrace : {2}",ex.GetType().Name,ex.Message,ex.StackTrace)
                };
            }
        }
    }
}
