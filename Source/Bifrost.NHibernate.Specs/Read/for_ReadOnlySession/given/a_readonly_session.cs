using System;
using Bifrost.NHibernate.Read;
using Machine.Specifications;
using Moq;
using NHibernate;

namespace Bifrost.NHibernate.Specs.Read.for_ReadOnlySession.given
{
    public class a_readonly_session
    {
        protected static Mock<IConnection> connection;
        protected static Mock<ISessionFactory> session_factory;
        protected static Mock<ISession> session;

        protected static IReadOnlySession read_only_session;

        Establish context = () =>
            {
                connection = new Mock<IConnection>();
                session_factory = new Mock<ISessionFactory>();
                session = new Mock<ISession>();

                connection.Setup(c => c.SessionFactory).Returns(session_factory.Object);
                session_factory.Setup(c => c.OpenSession(Moq.It.IsAny<IInterceptor>())).Returns(session.Object);

                read_only_session = new ReadOnlySession(connection.Object);
            };
    }
}