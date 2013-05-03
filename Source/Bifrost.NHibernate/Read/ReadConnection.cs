using System;
using NHibernate;

namespace Bifrost.NHibernate.Read
{
    public class ReadConnection : IConnection
    {
        readonly Func<ISessionFactory> _getSessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return _getSessionFactory.Invoke(); }
        }

        public ReadConnection(Func<ISessionFactory> getSessionFactory)
        {
            _getSessionFactory = getSessionFactory;
        }
    }
}