using System.Collections.Generic;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Visualizer.QualityAssurance
{
    public class Problems : IReadModel
    {
        public IEnumerable<Problem> All { get; set; }
    }
}
