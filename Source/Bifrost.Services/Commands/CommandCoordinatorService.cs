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
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Serialization;

namespace Bifrost.Services.Commands
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommandCoordinatorService
    {
        ICommandCoordinator _commandCoordinator;
        ISerializer _serializer;
        ITypeDiscoverer _typeDiscoverer;
        Dictionary<string, Type> _commandTypes;

        public CommandCoordinatorService(
            ICommandCoordinator commandCoordinator, 
            ISerializer serializer,
            ITypeDiscoverer typeDiscoverer)
        {
            _commandCoordinator = commandCoordinator;
            _serializer = serializer;
            _typeDiscoverer = typeDiscoverer;
            PopulateCommandTypes();
        }


        void PopulateCommandTypes()
        {
            var commands = _typeDiscoverer.FindMultiple<ICommand>();
            _commandTypes = commands.Select(c=>c).ToDictionary(c => c.Name);
        }

        [WebInvoke(
            Method="POST",
            RequestFormat=WebMessageFormat.Json,
            ResponseFormat=WebMessageFormat.Json,
            UriTemplate="handle")]
        public CommandResult Handle(CommandDescriptor commandDescriptor)
        {
            var commandName = commandDescriptor.Name;
            if (_commandTypes.ContainsKey(commandName))
            {
                var commandInstance = _serializer.FromJson(_commandTypes[commandName], commandDescriptor.Command) as ICommand;
                var result = _commandCoordinator.Handle(commandInstance);
                return result;
            }
            
            return null;
        }
    }
}
