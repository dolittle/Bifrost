/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Web.Events
{
    public interface ICommandCoordinatorEvents
    {
        void EventsProcessed(Guid commandContext);
    }
}
