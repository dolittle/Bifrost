using System;
using Machine.Specifications;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider
{
    [Subject(typeof(BifrostTempDataProvider))]
    public class when_loading_temp_data_with_no_controller_context : given.a_temp_data_provider
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => temp_data_provider.LoadTempData(null));

        It should_throw_an_argument_null_exception = () => exception.ShouldBeOfType<ArgumentNullException>();
    }
}