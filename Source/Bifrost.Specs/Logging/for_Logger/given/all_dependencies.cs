using Bifrost.Logging;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Logging.for_Logger.given
{
    public class all_dependencies
    {
        protected static Mock<ILogAppenders> appenders;

        Establish context = () => appenders = new Mock<ILogAppenders>();
    }
}
