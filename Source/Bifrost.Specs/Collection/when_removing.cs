using System.Collections.Generic;
using Bifrost.Collections;
using Machine.Specifications;

namespace Bifrost.Specs.Collection
{
    public class when_removing : given.empty_collection
    {
        static object object_to_add_and_remove;
        static IObservableCollection<object> collection_removed_from;
        static IEnumerable<object>  items_removed;

        Establish context = () => 
        {
            object_to_add_and_remove = new object();
            collection.Add(object_to_add_and_remove);

            collection.Removed += (c, i) => 
            {
                collection_removed_from = c;
                items_removed = i;
            };

        };

        Because of = () => collection.Remove(object_to_add_and_remove);

        It should_pass_the_right_collection_to_the_removed_event_handler = () => collection_removed_from.ShouldEqual(collection);
        It should_pass_only_the_item_as_items_to_the_removed_event_handler = () => items_removed.ShouldContainOnly(object_to_add_and_remove);
        It should_not_hold_only_the_item_in_the_collection = () => collection.ShouldNotContain(object_to_add_and_remove);
    }
}
