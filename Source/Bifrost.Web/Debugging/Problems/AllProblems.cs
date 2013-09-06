using System.Linq;
using Bifrost.Commands;
using Bifrost.Commands.Diagnostics;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Debugging.Problems
{
    public class MyCommand : Command
    {
    }


    public class AllProblems : IQueryFor<Problems>
    {
        IProblemsReporter _reporter;

        public AllProblems(IProblemsReporter reporter)
        {
            _reporter = reporter;
        }

        public IQueryable<Problems> Query
        {
            get
            {
                return new Problems[] { new Problems {
                    All = new Problem[] {
                        new Problem { Type = ProblemTypes.TooManyProperties, Data = new { Name = typeof(MyCommand).Name, Namespace = typeof(MyCommand).Namespace } }
                    }
                }}.AsQueryable();
            }
        }
    }
}
