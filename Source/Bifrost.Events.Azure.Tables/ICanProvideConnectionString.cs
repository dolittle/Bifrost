/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.Azure.Tables
{
    /// <summary>
    /// Delegate for providing connection string for <see cref="EventStore"/>
    /// </summary>
    /// <returns></returns>
    public delegate string ICanProvideConnectionString();
}
