using Bifrost.NHibernate.Read;
using Machine.Specifications;

namespace Bifrost.NHibernate.Specs.Read.for_ReadOnlySession
{
    [Subject(typeof(ReadOnlySession))]
    public class when_disposing_and_session_has_not_been_instantiated : given.a_readonly_session
    {
        Because of = () => read_only_session.Dispose();

        It should_not_dispose_the_underlying_session = () => session.Verify(s => s.Dispose(), Moq.Times.Never());
    }
}