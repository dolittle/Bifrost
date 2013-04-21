using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider
{
    [Subject(typeof(BifrostTempDataProvider))]
    public class when_loading_temp_data_with_no_temp_data_in_session : given.a_temp_data_provider
    {
        static string serialized_temp_data;
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Mock<HttpSessionStateBase> http_session_mock;
        static IDictionary<string, object> temp_data;
        const string TEMP_DATA_KEY = "test";

        Establish context = () =>
                                {
                                    serialized_temp_data = null;
                                    http_session_mock = new Mock<HttpSessionStateBase>();
                                    http_session_mock.Setup(s => s[TEMP_DATA_SESSION_STATE_KEY]).Returns(serialized_temp_data);
                                    http_context_mock = new Mock<HttpContextBase>();
                                    http_context_mock.SetupGet(hc => hc.Session).Returns(http_session_mock.Object);
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
                                };

        Because of = () => temp_data = temp_data_provider.LoadTempData(controller_context_mock.Object);

        It should_retrieve_the_serialized_temp_data_from_session = () => http_session_mock.VerifyAll();
        It should_not_use_the_serializer_to_deserialize_it = () => serializer_mock.VerifyAll();
        It should_return_an_empty_temp_data_dictionar = () =>
                                                            {
                                                                temp_data.ShouldNotBeNull();
                                                                temp_data.Keys.Count.ShouldEqual(0);
                                                            };
    }
}