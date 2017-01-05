/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Read
{
    /// <summary>
    /// Defines a query for a specified type, typically a ReadModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryFor<T> : IQuery
        where T:IReadModel
    {
    }
}
