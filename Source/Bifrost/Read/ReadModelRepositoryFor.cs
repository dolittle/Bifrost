/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Entities;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModelRepositoryFor{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of ReadModel the repository represents</typeparam>
    public class ReadModelRepositoryFor<T> : IReadModelRepositoryFor<T> where T:IReadModel
    {
        IEntityContext<T> _entityContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ReadModelRepositoryFor{T}"/>
        /// </summary>
        /// <param name="entityContext"><see cref="IEntityContext{T}"/> used by the repository</param>
        public ReadModelRepositoryFor(IEntityContext<T> entityContext)
        {
            _entityContext = entityContext;
        }

#pragma warning disable 1591
        public IQueryable<T> Query { get { return _entityContext.Entities; } }

        public void Insert(T readModel)
        {
            _entityContext.Insert(readModel);
            _entityContext.Commit();
        }

        public void Update(T readModel)
        {
            _entityContext.Update(readModel);
            _entityContext.Commit();
        }

        public void Delete(T readModel)
        {
            _entityContext.Delete(readModel);
            _entityContext.Commit();
        }

        public T GetById(object id)
        {
            return _entityContext.GetById(id);
        }
#pragma warning restore 1591
    }
}
