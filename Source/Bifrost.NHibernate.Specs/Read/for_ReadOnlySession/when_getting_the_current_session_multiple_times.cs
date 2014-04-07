using System;
using Bifrost.NHibernate.Read;
using Machine.Specifications;
using NHibernate;

namespace Bifrost.NHibernate.Specs.Read.for_ReadOnlySession
{
    [Subject(typeof (IReadOnlySession))]
    public class when_getting_the_current_session_multiple_times : given.a_readonly_session
    {
        static ISession first_session;
        static ISession second_session;
        static ISession third_session;

        Because of = () =>
            {
                first_session = read_only_session.GetCurrentSession();
                second_session = read_only_session.GetCurrentSession();
                third_session = read_only_session.GetCurrentSession();
            };

        It should_return_the_same_instance_of_the_session_each_time = () =>
            {
                first_session.ShouldEqual(second_session);
                second_session.ShouldEqual(third_session);
            };

        It should_only_call_open_session_on_the_session_factory_one_time =
            () => session_factory.Verify(f => f.OpenSession(Moq.It.IsAny<IInterceptor>()), Moq.Times.Once());

    }
}