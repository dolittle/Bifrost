/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events.MongoDB
{
    /// <summary>
    /// Delegate for providing connection string for <see cref="EventStore"/>
    /// </summary>
    /// <returns></returns>
    public delegate Tuple<string,string> ICanProvideConnectionDetails();
}
