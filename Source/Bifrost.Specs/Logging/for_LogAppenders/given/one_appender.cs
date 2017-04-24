using Bifrost.Logging;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Logging.for_LogAppenders.given
{
    public class one_appender : all_dependencies
    {
        protected static LogAppenders appenders;
        protected static Mock<ILogAppender> appender;

        Establish context = () =>
        {
            appenders = new LogAppenders(log_appenders_configurators.Object);
            appender = new Mock<ILogAppender>();
            appenders.Add(appender.Object);
        };
    }
}
