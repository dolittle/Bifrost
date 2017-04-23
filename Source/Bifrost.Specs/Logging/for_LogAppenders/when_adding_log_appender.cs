using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Logging;

namespace Bifrost.Specs.Logging.for_LogAppenders
{
    public class when_adding_log_appender : given.no_log_appenders
    {
        static ILogAppender appender;

        Establish context = () => appender = Mock.Of<ILogAppender>();

        Because of = () => appenders.Add(appender);

        It should_contain_the_appender = () => appenders.Appenders.ShouldContainOnly(appender);
    }
}
