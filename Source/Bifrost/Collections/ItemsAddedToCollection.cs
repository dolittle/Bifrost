/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Collections
{
    /// <summary>
    /// Represents the delegate that gets called when items are added to a collection
    /// </summary>
    /// <typeparam name="T">Type of item in the collection</typeparam>
    /// <param name="collection"><see cref="IObservableCollection{T}"/> that received the items</param>
    /// <param name="items">The items</param>
    public delegate void ItemsAddedToCollection<T>(IObservableCollection<T> collection, IEnumerable<T> items);
}
