using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic.given
{
    public class when_recording_two_categories_for_a_single_context : given.a_statistic
    {
        static Exception thrown_exception = null;

        Because of = () =>
        {
            thrown_exception = Catch.Exception(() => { statistic.Record("The Context", "A Category"); });
            thrown_exception = Catch.Exception(() => { statistic.Record("The Context", "Another Category"); });
        };

        It should_not_throw_an_exception = () => { thrown_exception.ShouldBeNull(); };

        It should_contain_the_context = () =>
        {
            statistic.Categories.ShouldContain(c => c.Key == "The Context");
        };

        It should_contain_the_categories = () =>
        {
            statistic.Categories.ShouldContain(c => c.Value.Contains("A Category") && c.Value.Contains("Another Category"));
        };
    }
}
