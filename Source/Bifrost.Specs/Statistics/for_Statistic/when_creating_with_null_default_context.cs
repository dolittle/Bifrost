using Bifrost.Statistics;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic
{
    public class when_creating_with_null_default_context
    {
        static IStatistic statistic;
        static Exception thrown_exception; 

        Because of = () =>
        {
            thrown_exception = Catch.Exception(() => { statistic = new Statistic(null); });
        };

        It should_throw_an_argument_null_exception = () =>
        {
            thrown_exception.ShouldBeOfType<ArgumentNullException>();
        };

    }
}
