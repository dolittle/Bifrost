using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bifrost.Serialization;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider
{
    [Subject(typeof(BifrostTempDataProvider))]
    public class when_saving_temp_data_with_temp_data_values : given.a_temp_data_provider
    {
        static string serialized_temp_data;
        static Dictionary<string, object> temp_data_dictionary;
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Mock<HttpSessionStateBase> http_session_mock;
        const string TEMP_DATA_KEY = "test";

        Establish context = () =>
                                {
                                    serialized_temp_data = "fake serialized data";
                                    temp_data_dictionary = new Dictionary<string, object>() { { TEMP_DATA_KEY, new object() } };
                                    http_session_mock = new Mock<HttpSessionStateBase>();
                                    http_session_mock.SetupSet(s => s[TEMP_DATA_SESSION_STATE_KEY] = serialized_temp_data);
                                    http_context_mock = new Mock<HttpContextBase>();
                                    http_context_mock.SetupGet(hc => hc.Session).Returns(http_session_mock.Object);
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
                                    serializer_mock.Setup(s => s.ToJson(temp_data_dictionary, Moq.It.IsAny<SerializationOptions>())).Returns(serialized_temp_data);
                                };

        Because of = () => temp_data_provider.SaveTempData(controller_context_mock.Object, temp_data_dictionary);

        It should_use_the_serializer_to_serialize_the_temp_data = () => serializer_mock.VerifyAll();
        It should_set_the_serialized_data_into_session = () => http_session_mock.VerifyAll();

    }
}