using Bifrost.Read;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class QueryForKnownProvider : IQuery
    {
        public QueryType QueryToReturn;
        public bool QueryPropertyCalled;
        public QueryType Query
        {
            get
            {
                QueryPropertyCalled = true;
                return QueryToReturn;
            }
        }
    }
}
