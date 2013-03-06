using Bifrost.Commands;
using Bifrost.Statistics;
using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_command_result_with_an_exception : given.a_command_statistics_with_registered_plugins
    {
        static CommandResult command_result = new CommandResult(){Exception = new Exception()};

        Because of = () => command_statistics.Record(command_result);

        It should_add_the_command_to_the_store = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories["CommandStatistics"].Contains("HadException"))), Moq.Times.Once());
        };

        It should_be_effected_by_a_registered_plugin = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories["DummyStatisticsPlugin"].Contains("I touched a had exception statistic"))), Moq.Times.Once());
        };
    }
}
