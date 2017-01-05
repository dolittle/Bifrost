/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Web.Commands;
using Microsoft.AspNet.SignalR;

namespace Bifrost.Web.Events
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
