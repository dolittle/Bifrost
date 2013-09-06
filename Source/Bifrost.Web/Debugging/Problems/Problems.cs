using System.Collections.Generic;
using Bifrost.Diagnostics;
using Bifrost.Read;

namespace Bifrost.Web.Debugging.Problems
{
    public class Problems : IReadModel
    {
        public IEnumerable<Problem> All { get; set; }
    }
}
