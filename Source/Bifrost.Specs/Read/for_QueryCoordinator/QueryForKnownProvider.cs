using Bifrost.Read;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class QueryForKnownProvider : IQuery
    {
        public QueryType QueryToReturn;
        public QueryType Query
        {
            get
            {
                return QueryToReturn;
            }
        }
    }
}
