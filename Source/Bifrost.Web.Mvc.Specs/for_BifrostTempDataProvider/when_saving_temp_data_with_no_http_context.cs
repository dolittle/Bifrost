using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider
{
    [Subject(typeof(BifrostTempDataProvider))]
    public class when_saving_temp_data_with_no_http_context : given.a_temp_data_provider
    {
        static Mock<ControllerContext> controller_context_mock;
        static Exception exception;

        Establish context = () =>
                                {
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(null as HttpContextBase);
                                };

        Because of = () => exception = Catch.Exception(() => temp_data_provider.SaveTempData(controller_context_mock.Object, new Dictionary<string, object>()));

        It should_throw_an_invalid_operation_exception = () => exception.ShouldBeOfType<InvalidOperationException>();
    }
}