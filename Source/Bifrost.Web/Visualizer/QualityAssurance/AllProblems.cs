using System.Linq;
using Bifrost.Commands;
using Bifrost.Configuration;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Visualizer.QualityAssurance
{
    public class MyCommand : Command
    {
        public int Property0 { get; set; }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public int Property3 { get; set; }
        public int Property4 { get; set; }
        public int Property5 { get; set; }
        public int Property6 { get; set; }
        public int Property7 { get; set; }
        public int Property8 { get; set; }
        public int Property9 { get; set; }
        public int Property10 { get; set; }
        public int Property11 { get; set; }
        public int Property12 { get; set; }
    }


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
