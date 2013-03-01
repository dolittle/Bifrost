using Bifrost.Commands;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost;
using Bifrost.Statistics;
using Bifrost.Execution;

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
            type_discoverer.Verify(t => t.FindMultiple<ICommandStatistics>(), Moq.Times.Once());
        };

        It should_not_throw_an_exception = () => thrown_exception.ShouldBeNull();
    }
}
