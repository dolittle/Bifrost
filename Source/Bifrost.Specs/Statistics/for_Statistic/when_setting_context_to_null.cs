using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic.given
{
    public class when_setting_context_to_null : given.a_statistic
    {
        static Exception thrown_exception = null;

        Because of = () =>
        {
            thrown_exception = Catch.Exception(() => { statistic.SetContext(null); });
        };

        It should_throw_an_exception = () => { thrown_exception.ShouldBeOfType<ArgumentNullException>(); };
    }
}
