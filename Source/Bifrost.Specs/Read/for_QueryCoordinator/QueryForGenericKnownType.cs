using Bifrost.Read;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class QueryForGenericKnownType : IQuery
    {
        public GenericKnownType<object> QueryToReturn;
        public GenericKnownType<object> Query
        {
            get
            {
                return QueryToReturn;
            }
        }
    }
}
