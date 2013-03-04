using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Statistics;
using Machine.Specifications;
using System;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_creating
    {
        static ICommandStatistics command_statistics = null;
        static Exception thrown_exception;
        static Moq.Mock<ITypeDiscoverer> type_discoverer = new Moq.Mock<ITypeDiscoverer>(); 

        Because of = () => thrown_exception = Catch.Exception(() =>
        {
            command_statistics = new CommandStatistics(new Moq.Mock<IStatisticsStore>().Object, type_discoverer.Object);
        });

        It should_discover_statistics_plugins = () =>
        {
            type_discoverer.Verify(t => t.FindMultiple<IStatisticsPlugin>(), Moq.Times.Once());
        };

        It should_not_throw_an_exception = () => thrown_exception.ShouldBeNull();
    }
}
