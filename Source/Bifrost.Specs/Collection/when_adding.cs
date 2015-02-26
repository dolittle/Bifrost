using System.Collections.Generic;
using Bifrost.Collections;
using Machine.Specifications;
namespace Bifrost.Specs.Collection
{
    public class when_adding : given.empty_collection
    {
        static object object_to_add;
        static IObservableCollection<object> collection_added_to;
        static IEnumerable<object>  items_added;

        Establish context = () => 
        {
            object_to_add = new object();

            collection.Added += (c, i) => 
            {
                collection_added_to = c;
                items_added = i;
            };

        };

        Because of = () => collection.Add(object_to_add);

        It should_pass_the_right_collection_to_the_added_event_handler = () => collection_added_to.ShouldEqual(collection);
        It should_pass_only_the_item_as_items_to_the_added_event_handler = () => items_added.ShouldContainOnly(object_to_add);
        It should_hold_only_the_item_in_the_collection = () => collection.ShouldContainOnly(object_to_add);
    }
}
