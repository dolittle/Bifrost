using Bifrost.Logging;
using Machine.Specifications;

namespace Bifrost.Specs.Logging.for_Logger
{
    public class when_logging_warning : given.a_logger_and_reusable_details
    {
        Because of = () => logger.Warning(message, file, line_number, member);

        It should_forward_to_appenders_with_level_of_warning = () => appenders.Verify(a => a.Append(file, line_number, member, LogLevel.Warning, message, null), Moq.Times.Once());
    }
}
