/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModel{T}"/> for dealing with fetching of single <see cref="IReadModel"/> instances
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IReadModel"/></typeparam>
    public class ReadModelOf<T> : IReadModelOf<T> where T:IReadModel
    {
        IReadModelRepositoryFor<T> _repository;

        /// <summary>
        /// Initializes an instance of <see cref="ReadModelOf{T}"/>
        /// </summary>
        /// <param name="repository">Repository to use getting instances</param>
        public ReadModelOf(IReadModelRepositoryFor<T> repository)
        {
            _repository = repository;
        }


#pragma warning disable 1591 // Xml Comments
        public T InstanceMatching(params Expression<Func<T, bool>>[] propertyExpressions)
        {
            var query = _repository.Query;

            foreach( var expression in propertyExpressions )
                query = query.Where(expression);

            return query.SingleOrDefault();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
