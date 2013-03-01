using Bifrost.Statistics;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic.given
{
    public class a_statistic
    {
        protected static IStatistic statistic;

        Establish context = () =>
        {
            statistic = new Statistic();
        };
    }
}
