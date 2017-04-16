/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events.Files
{
    /// <summary>
    /// Delegate providing path to where to store <see cref="EventSourceVersion"/> for each <see cref="EventSource"/>
    /// </summary>
    public delegate string ICanProvideEventSourceVersionsPath();
}