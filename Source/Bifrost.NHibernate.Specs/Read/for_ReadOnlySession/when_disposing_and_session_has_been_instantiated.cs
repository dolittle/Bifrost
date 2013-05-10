using Bifrost.NHibernate.Read;
using Machine.Specifications;

namespace Bifrost.NHibernate.Specs.Read.for_ReadOnlySession
{
    [Subject(typeof(ReadOnlySession))]
    public class when_disposing_and_session_has_been_instantiated : given.a_readonly_session
    {
        Establish context = () => read_only_session.GetCurrentSession();

        Because of = () => read_only_session.Dispose();

        It should_dispose_the_underlying_session = () => session.Verify(s => s.Dispose(), Moq.Times.Once());
    }
}