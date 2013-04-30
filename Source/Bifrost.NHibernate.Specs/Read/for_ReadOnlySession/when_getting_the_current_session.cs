using Bifrost.NHibernate.Read;
using Machine.Specifications;
using NHibernate;

namespace Bifrost.NHibernate.Specs.Read.for_ReadOnlySession
{
    [Subject(typeof(IReadOnlySession))]
    public class when_getting_the_current_session : given.a_readonly_session
    {
        static ISession returned_session;

        Because of = () =>
            {
                returned_session = read_only_session.GetCurrentSession();
            };

        It should_return_a_read_only_proxy_instead_of_the_session_directly =
            () => returned_session.ShouldBeOfType<ReadOnlySessionProxy>();

        It should_be_set_to_flush_mode_never = () => session.VerifySet(s => s.FlushMode = FlushMode.Never, Moq.Times.Once());
    }
}