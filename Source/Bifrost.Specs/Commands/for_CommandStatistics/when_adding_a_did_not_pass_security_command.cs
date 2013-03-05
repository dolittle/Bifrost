using Bifrost.Commands;
using Bifrost.Statistics;
using Machine.Specifications;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_adding_a_did_not_pass_security_command : given.a_command_statistics_with_registered_plugins
    {
        static CommandResult commandResult;
        Because of = () =>
        {
            commandResult = new CommandResult()
            {
                SecurityMessages = new[] { "Security Error" }
            };

            command_statistics.Record(commandResult);

        };

        It should_add_the_command_to_the_store = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string, string>("CommandStatistics","DidNotPassSecurity")))), Moq.Times.Once());
        };

        It should_be_effected_by_a_registered_plugin = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string, string>("DummyStatisticsPluginContext", "I touched a did not pass security statistic")))), Moq.Times.Once());
        };
    }
}
