using Bifrost.Statistics;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic
{
    public class when_creating
    {
        static IStatistic statistic;

        Because of = () =>
            {
                statistic = new Statistic();
            };

        It should_have_an_empty_category_list = () =>
        {
            statistic.Categories.ShouldBeEmpty();
        };

    }
}
