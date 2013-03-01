﻿using Bifrost.Commands;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost;
using Bifrost.Statistics;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_adding_a_had_exception_command : given.a_command_statistics_with_registered_plugin
    {
        static Command command = new Command();

        Because of = () => command_statistics.HadException(command);

        It should_add_the_command_to_the_store = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string, string>("CommandStatistics","HadException")))), Moq.Times.Once());
        };

        It should_be_effected_by_a_registered_plugin = () =>
        {
            statistics_store
                .Verify(store =>
                    store.Add(Moq.It.Is<IStatistic>(stat => stat.Categories.Contains(
                        new KeyValuePair<string,string>("DummyStatisticsPluginContext", "ImADummy")))), Moq.Times.Once());
        };
    }
}
