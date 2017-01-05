/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Collections
{
    /// <summary>
    /// Represents a delegate for when a collection is cleared
    /// </summary>
    /// <typeparam name="T">Type of element in the collection</typeparam>
    /// <param name="collection"><see cref="IObservableCollection{T}"/> that gets cleared</param>
    public delegate void CollectionCleared<T>(IObservableCollection<T> collection);
}
