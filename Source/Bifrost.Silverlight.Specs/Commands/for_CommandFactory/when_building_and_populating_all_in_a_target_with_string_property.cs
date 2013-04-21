using Machine.Specifications;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_and_populating_all_in_a_target_with_string_property : given.a_command_factory
    {
        static ViewModelWithStringProperty view_model;

        static Exception exception;

        Establish context = () => 
        {
            view_model = new ViewModelWithStringProperty();
        };

        Because of = () => exception = Catch.Exception(() => command_factory.BuildAndPopulateAll(view_model));

        It should_not_throw_an_exception = () => exception.ShouldBeNull();
    }
}
