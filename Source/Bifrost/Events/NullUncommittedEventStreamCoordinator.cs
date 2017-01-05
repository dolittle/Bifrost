/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// A null implementation for <see cref="IUncommittedEventStreamCoordinator"/>
    /// </summary>
    public class NullUncommittedEventStreamCoordinator : IUncommittedEventStreamCoordinator
    {
#pragma warning disable 1591 // Xml Comments
        public void Commit(UncommittedEventStream eventStream)
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}