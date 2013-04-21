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
    public class when_loading_temp_data_with_no_session : given.a_temp_data_provider
    {
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Exception exception;

        Establish context = () =>
                                {
                                    http_context_mock = new Mock<HttpContextBase>();
                                    http_context_mock.SetupGet(hc => hc.Session).Returns(null as HttpSessionStateBase);
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
                                };

        Because of = () => exception = Catch.Exception(() => temp_data_provider.LoadTempData(controller_context_mock.Object));

        It should_throw_an_invalid_operation_exception = () => exception.ShouldBeOfType<InvalidOperationException>();
    }

    [Subject(typeof(BifrostTempDataProvider))]
    public class when_saving_temp_data_with_no_session : given.a_temp_data_provider
    {
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Exception exception;

        Establish context = () =>
        {
            http_context_mock = new Mock<HttpContextBase>();
            http_context_mock.SetupGet(hc => hc.Session).Returns(null as HttpSessionStateBase);
            controller_context_mock = new Mock<ControllerContext>();
            controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
        };

        Because of = () => exception = Catch.Exception(() => temp_data_provider.SaveTempData(controller_context_mock.Object, new Dictionary<string, object>()));

        It should_throw_an_invalid_operation_exception = () => exception.ShouldBeOfType<InvalidOperationException>();
    }
}