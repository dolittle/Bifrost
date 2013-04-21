using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider
{
    [Subject(typeof(BifrostTempDataProvider))]
    public class when_saving_temp_data_with_no_temp_data_values_and_there_are_existing_values : given.a_temp_data_provider
    {
        static Dictionary<string, object> temp_data_dictionary;
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Mock<HttpSessionStateBase> http_session_mock;
        const string serialized_temp_data = "existing serialized temp data";

        Establish context = () =>
                                {
                                    temp_data_dictionary = new Dictionary<string, object>();
                                    http_session_mock = new Mock<HttpSessionStateBase>();
                                    http_context_mock = new Mock<HttpContextBase>();
                                    http_context_mock.SetupGet(hc => hc.Session).Returns(http_session_mock.Object);
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
                                    http_session_mock.SetupGet(s => s[TEMP_DATA_SESSION_STATE_KEY]).Returns(serialized_temp_data);
                                    http_session_mock.Setup(s => s.Remove(TEMP_DATA_SESSION_STATE_KEY));
                                };

        Because of = () => temp_data_provider.SaveTempData(controller_context_mock.Object, temp_data_dictionary);

        It should_not_use_the_serializer_to_serialize_the_temp_data = () => serializer_mock.VerifyAll();
        It should_remove_the_temp_data_key_from_the_session = () => http_session_mock.VerifyAll();

    }
}