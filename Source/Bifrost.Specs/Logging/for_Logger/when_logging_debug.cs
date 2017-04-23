using Bifrost.Logging;
using Machine.Specifications;

namespace Bifrost.Specs.Logging.for_Logger
{
    public class when_logging_debug : given.a_logger_and_reusable_details
    {
        Because of = () => logger.Debug(message, file, line_number, member);

        It should_forward_to_appenders_with_level_of_debug = () => appenders.Verify(a => a.Append(file, line_number, member, LogLevel.Debug, message, null), Moq.Times.Once());
    }
}
