using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Specs.Statistics.for_Statistic.given
{
    public class when_recording_a_category : given.a_statistic
    {
        Because of = () =>
            {
                statistic.Record("The Context", "A Category");
            };

        It should_record_the_category = () =>
            {
                statistic.Categories.ShouldContain(new KeyValuePair<string, string>("The Context", "A Category"));
            };
    }
}
