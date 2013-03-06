using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic.given
{
    public class when_recording_without_context : given.a_statistic
    {
        static Exception thrown_exception = null;

        Because of = () =>
        {
            thrown_exception = Catch.Exception(() => { statistic.Record("A Category"); });
            thrown_exception = Catch.Exception(() => { statistic.Record("Another Category"); });
        };

        It should_not_throw_an_exception = () => { thrown_exception.ShouldBeNull(); };

        It should_contain_the_default_context = () =>
        {
            statistic.Categories.ShouldContain(c => c.Key == "Default Context");
        };

        It should_contain_the_categories = () =>
        {
            statistic.Categories.ShouldContain(c => c.Value.Contains("A Category") && c.Value.Contains("Another Category"));
        };
    }
}
