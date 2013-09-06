using Bifrost.Diagnostics;
using Machine.Specifications;

namespace Bifrost.Specs.Diagnostics.for_Problems
{
    public class when_asking_if_has_any_problems_with_one_problem
    {
        static Problems  problems;
        static bool result;

        Establish context = () =>
        {
            problems = new Problems();
            problems.Report(null, null);
        };

        Because of = () => result = problems.Any;

        It should_have = () => result.ShouldBeTrue();
    }
}
