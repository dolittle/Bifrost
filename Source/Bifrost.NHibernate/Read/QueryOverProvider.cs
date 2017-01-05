/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Read;
using NHibernate;
using NHibernate.Criterion;


namespace Bifrost.NHibernate.Read
{
    public class QueryOverProvider : IQueryProviderFor<IQueryOver>
    {
        public QueryProviderResult Execute(IQueryOver query, PagingInfo paging)
        {
            var result = new QueryProviderResult();
            if (paging.Enabled)
            {
                result.TotalItems = (int)query.RootCriteria.SetProjection(Projections.Count("*")).UniqueResult(); // .FutureValue<int>(); //

                query.RootCriteria.SetFirstResult(paging.Size * paging.Number);
                query.RootCriteria.SetMaxResults(paging.Size);
                
            }
            result.Items = query.RootCriteria.List();

            // For Total Items - idea
            // http://ayende.com/blog/2334/paged-data-count-with-nhibernate-the-really-easy-way
            

            return result;
        }
    }
}
