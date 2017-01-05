/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Web.Commands
{
    public interface ICommandContextConnectionManager
    {
        void Register(string connectionId, Guid commandContext);
        void RemoveConnectionIfPresent(string connectionId);
        bool HasConnectionForCommandContext(Guid commandContext);
        string GetConnectionForCommandContext(Guid commandContext);
    }
}
