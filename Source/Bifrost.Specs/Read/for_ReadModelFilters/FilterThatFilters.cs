using System.Collections.Generic;
using Bifrost.Read;

namespace Bifrost.Specs.Read.for_ReadModelFilters
{
    public class FilterThatFilters : ICanFilterReadModels
    {
        public IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels)
        {
            throw new System.NotImplementedException();
        }
    }
}
