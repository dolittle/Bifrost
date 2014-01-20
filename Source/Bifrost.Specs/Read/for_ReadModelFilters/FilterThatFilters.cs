using System.Collections.Generic;
using Bifrost.Read;

namespace Bifrost.Specs.Read.for_ReadModelFilters
{
    public class FilterThatFilters : ICanFilterReadModels
    {
        public bool FilterCalled = false;
        public IEnumerable<IReadModel> Filtered;
        public IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels)
        {
            FilterCalled = true;
            return Filtered;
        }
    }
}
