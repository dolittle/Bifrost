/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{

    /// <summary>
    /// Defines an event which is the subsequent generation of the <see cref="IEvent">Event</see>
    /// </summary>
    /// <typeparam name="T">The previous generation of this event which this event supercedes</typeparam>
    public interface IAmNextGenerationOf<T> where T : IEvent
    {}
}