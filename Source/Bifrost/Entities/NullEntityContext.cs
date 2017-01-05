/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;

namespace Bifrost.Entities
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEntityContext"/> doing absolutely nothing
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class NullEntityContext<T> : IEntityContext<T>
    {
#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get { return new T[0].AsQueryable(); }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
        }

        public void Update(T entity)
        {
        }

        public void Delete(T entity)
        {
        }

        public void Save(T entity)
        {
        }

        public void Commit()
        {
        }

        public T GetById<TProperty>(TProperty id)
        {
            return default(T);
        }

        public void DeleteById<TProperty>(TProperty id)
        {
        }

        public void Dispose()
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}
