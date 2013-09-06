using System;
using System.Linq;
using Bifrost.Diagnostics;
using Machine.Specifications;

namespace Bifrost.Specs.Diagnostics.for_Problems
{
    public class when_reporting
    {
        const string data = "Some data";
        static ProblemType type = ProblemType.Create(Guid.NewGuid().ToString(), "Something", ProblemSeverity.Warning);
        static Problems problems;
        

        Establish context = () => problems = new Problems();

        Because of = () => problems.Report(type, data);

        It should_have_one_problem_registered = () => problems.Count().ShouldEqual(1);
        It should_have_the_problem_type_on_the_problem = () => problems.First().Type.ShouldEqual(type);
        It should_have_the_data_on_the_problem = () => problems.First().Data.ShouldEqual(data);
    }
}
