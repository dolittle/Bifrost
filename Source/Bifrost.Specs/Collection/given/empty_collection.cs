using Bifrost.Collections;
using Machine.Specifications;

namespace Bifrost.Specs.Collection.given
{
    public class empty_collection
    {
        protected static ObservableCollection<object> collection;

        Establish context = () => collection = new ObservableCollection<object>();
    }
}
