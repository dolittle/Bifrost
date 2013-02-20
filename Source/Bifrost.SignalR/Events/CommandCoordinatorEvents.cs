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
using Bifrost.SignalR.Commands;
using Microsoft.AspNet.SignalR;

namespace Bifrost.SignalR.Events
{
    public class CommandCoordinatorEvents : ICommandCoordinatorEvents
    {
        ICommandContextConnectionManager _commandContextConnectionManager;

        public CommandCoordinatorEvents(ICommandContextConnectionManager commandContextConnectionManager)
        {
            _commandContextConnectionManager = commandContextConnectionManager;
        }

        public void EventsProcessed(Guid commandContext)
        {
            if (_commandContextConnectionManager.HasConnectionForCommandContext(commandContext))
            {
                var connectionId = _commandContextConnectionManager.GetConnectionForCommandContext(commandContext);
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<CommandCoordinator>();
                hubContext.Clients.Client(connectionId).EventsProcessed(commandContext);
            }
        }
    }
}
