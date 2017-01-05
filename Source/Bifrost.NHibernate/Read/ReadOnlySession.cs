/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using NHibernate;

namespace Bifrost.NHibernate.Read
{
    public class ReadOnlySession : IReadOnlySession
    {
        ISession _session;
        protected readonly IConnection _connection;

        public ReadOnlySession(IConnection connection)
        {
            _connection = connection;
        }

        public ISession GetCurrentSession()
        {
            return _session ?? (_session = BuildSession());
        }

        ISession BuildSession()
        {
            var session = OpenSession();
            session.FlushMode = FlushMode.Never;
            session.DefaultReadOnly = true;
            return new ReadOnlySessionProxy(session);
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }
        }

        protected virtual ISession OpenSession()
        {
            return _connection.SessionFactory.OpenSession(new UTCDateTimesInterceptor());
        }
    }
}
