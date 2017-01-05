/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Read;
using NHibernate;
using NHibernate.Linq;

namespace Bifrost.NHibernate.Read
{
    public class ReadModelRepositoryFor<T> : IReadModelRepositoryFor<T> where T:IReadModel
    {
        ISessionFactory _sessionFactory;

        public ReadModelRepositoryFor(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        ISession Session 
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public T GetById(object id)
        {
            return Session.Get<T>(id);
        }

        public IQueryable<T> Query
        {
            get { return Session.Query<T>();  }
        }

        public void Insert(T readModel)
        {
            Session.Save(readModel);
        }

        public void Update(T readModel)
        {
            Session.Update(readModel);
        }

        public void Delete(T readModel)
        {
            Session.Delete(readModel);
        }
    }
}
