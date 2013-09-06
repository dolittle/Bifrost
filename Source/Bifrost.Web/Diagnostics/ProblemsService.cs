using System.Collections.Generic;
using Bifrost.Diagnostics;

namespace Bifrost.Web.Diagnostics
{
    public class ProblemsService 
    {
        IProblemsReporter _reporter;

        public ProblemsService(IProblemsReporter reporter)
        {
            _reporter = reporter;
        }

        public IEnumerable<IProblems> GetAll()
        {
            return _reporter.All;
        }
    }
}
