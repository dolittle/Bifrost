using System.Linq;
using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_ProblemsReporter
{
    public class when_reporting
    {
        static Mock<IProblems> problems_mock;
        static ProblemsReporter reporter;

        Establish context = () => 
        {
            problems_mock = new Mock<IProblems>();
            reporter = new ProblemsReporter();
        };

        Because of = () => reporter.Report(problems_mock.Object);

        It should_hold_one_problems = () => reporter.All.Count().ShouldEqual(1);
        It should_hold_the_problems_instance_reported = () => (reporter.All.First()  == problems_mock.Object).ShouldBeTrue();
    }
}
