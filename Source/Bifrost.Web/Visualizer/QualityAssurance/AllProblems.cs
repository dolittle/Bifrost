using System.Linq;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Visualizer.QualityAssurance
{


    public class AllProblems : IQueryFor<Problems>
    {
        IProblemsReporter _reporter;

        public AllProblems(IProblemsReporter reporter)
        {
            _reporter = reporter;
            reporter.Clear();
            Configure.Instance.QualityAssurance.Validate();
        }

        public IQueryable<Problems> Query
        {
            get
            {
                return _reporter.All.Select(p => new Problems
                {
                    All = p.Select(pr => new Problem
                    {
                        Type = pr.Type,
                        Data = pr.Data
                    })
                }).AsQueryable();
            }
        }
    }
}
