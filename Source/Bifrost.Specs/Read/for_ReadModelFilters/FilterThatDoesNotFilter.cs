using System.Collections.Generic;
using Bifrost.Read;

namespace Bifrost.Specs.Read.for_ReadModelFilters
{
    public class FilterThatDoesNotFilter : ICanFilterReadModels
    {
        public bool FilterCalled = false;
        public IEnumerable<IReadModel> ReadModelsPassedForFilter = null;
        public IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels)
        {
            FilterCalled = true;
            ReadModelsPassedForFilter = readModels;
            return readModels;
        }
    }
}
