using Machine.Specifications;

namespace Bifrost.Specs.Logging.for_LogAppenders
{
    public class when_clearing_log_appenders : given.one_appender
    {
        Because of = () => appenders.Clear();

        It should_not_have_any_appenders = () => appenders.Appenders.ShouldBeEmpty();
    }
}
