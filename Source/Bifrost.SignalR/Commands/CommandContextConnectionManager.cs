#region License
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
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.SignalR.Commands
{
    [Singleton]
    public class CommandContextConnectionManager : ICommandContextConnectionManager
    {
        Dictionary<Guid, string> connectionIdsByCommandContext = new Dictionary<Guid, string>();
        Dictionary<string, List<Guid>> commandContextsByConnectionId = new Dictionary<string, List<Guid>>();

        public void Register(string connectionId, Guid commandContext)
        {
            connectionIdsByCommandContext[commandContext] = connectionId;
            var commandContexts = GetOrCreateListOfCommandContextsForConnectionId(connectionId);

            if( !commandContexts.Contains(commandContext) )
                commandContexts.Add(commandContext);
        }

        public bool HasConnectionForCommandContext(Guid commandContext)
        {
            return connectionIdsByCommandContext.ContainsKey(commandContext);
        }

        public void RemoveConnectionIfPresent(string connectionId)
        {
            var commandContexts = commandContextsByConnectionId[connectionId];
            foreach (var commandContext in commandContexts)
                connectionIdsByCommandContext.Remove(commandContext);

            commandContextsByConnectionId.Remove(connectionId);
        }

        public string GetConnectionForCommandContext(Guid commandContext)
        {
            ThrowIfUnknownCommandContext(commandContext);
            
            return connectionIdsByCommandContext[commandContext];
        }

        List<Guid> GetOrCreateListOfCommandContextsForConnectionId(string connectionId)
        {
            List<Guid> commandContexts;
            if (!commandContextsByConnectionId.ContainsKey(connectionId))
            {
                commandContexts = new List<Guid>();
                commandContextsByConnectionId[connectionId] = commandContexts;
            }
            else
                commandContexts = commandContextsByConnectionId[connectionId];
            return commandContexts;
        }

        void ThrowIfUnknownCommandContext(Guid commandContext)
        {
            if (!connectionIdsByCommandContext.ContainsKey(commandContext))
                throw new UnknownCommandContextException();
        }

    }
}
