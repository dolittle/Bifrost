using Bifrost.Logging;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Logging.for_LogAppenders
{
    public class when_constructing_with_two_instances_of_log_appenders_configurators : given.no_log_appenders
    {
        static Mock<ICanConfigureLogAppenders> first_log_appenders_configurator;
        static Mock<ICanConfigureLogAppenders> second_log_appenders_configurator;

        Establish context = () =>
        {
            first_log_appenders_configurator = new Mock<ICanConfigureLogAppenders>();
            second_log_appenders_configurator = new Mock<ICanConfigureLogAppenders>();

            actual_log_appenders_configurators.Add(first_log_appenders_configurator.Object);
            actual_log_appenders_configurators.Add(second_log_appenders_configurator.Object);
        };

        Because of = () => appenders = new LogAppenders(log_appenders_configurators.Object);

        It should_call_configure_on_the_first_configurator = () => first_log_appenders_configurator.Verify(l => l.Configure(appenders), Times.Once());
        It should_call_configure_on_the_second_configurator = () => second_log_appenders_configurator.Verify(l => l.Configure(appenders), Times.Once());
    }
}
