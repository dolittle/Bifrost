/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Collections
{
    /// <summary>
    /// Defines a collection that enables observing 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        /// Event that gets fired when elements are added to the collection
        /// </summary>
        event ItemsAddedToCollection<T> Added;

        /// <summary>
        /// Event that gets fired when elements are removed from the collection
        /// </summary>
        event ItemsRemovedFromCollection<T> Removed;

        /// <summary>
        /// Event that gets fired when the collection is cleared
        /// </summary>
        event CollectionCleared<T> Cleared;
    }
}
