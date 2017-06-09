/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Defines something that can provide a connection string for Redis
    /// </summary>
    public delegate string ICanProvideConnectionStringToSender();
}