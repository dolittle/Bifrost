using Bifrost.Commands;
using Bifrost.Statistics;
using Machine.Specifications;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_command_result_that_was_handled : given.a_command_statistics_with_registered_plugins
    {
        static CommandResult command_result = new CommandResult();

        Because of = () => command_statistics.Record(command_result);

        It should_add_the_command_to_the_store = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string, string>("CommandStatistics","WasHandled")))), Moq.Times.Once());
        };

        It should_be_effected_by_a_registered_plugin = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string, string>("DummyStatisticsPlugin", "I touched a was handled statistic")))), Moq.Times.Once());
        };
    }
}
