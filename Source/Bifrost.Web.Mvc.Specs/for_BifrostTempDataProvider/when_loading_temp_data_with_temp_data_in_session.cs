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
    public class when_loading_temp_data_with_temp_data_in_session : given.a_temp_data_provider
    {
        static string serialized_temp_data;
        static Dictionary<string, object> temp_data_dictionary;
        static Mock<ControllerContext> controller_context_mock;
        static Mock<HttpContextBase> http_context_mock;
        static Mock<HttpSessionStateBase> http_session_mock;
        static IDictionary<string, object> temp_data;
        const string TEMP_DATA_KEY = "test";

        Establish context = () =>
                                {
                                    serialized_temp_data = "fake serialized data";
                                    temp_data_dictionary = new Dictionary<string, object>() { {TEMP_DATA_KEY, new object()}};
                                    http_session_mock = new Mock<HttpSessionStateBase>();
                                    http_session_mock.SetupGet(s => s[TEMP_DATA_SESSION_STATE_KEY]).Returns(serialized_temp_data);
                                    http_context_mock = new Mock<HttpContextBase>();
                                    http_context_mock.SetupGet(hc => hc.Session).Returns(http_session_mock.Object);
                                    controller_context_mock = new Mock<ControllerContext>();
                                    controller_context_mock.SetupGet(cc => cc.HttpContext).Returns(http_context_mock.Object);
                                    serializer_mock.Setup(s => s.FromJson<Dictionary<string, object>>(serialized_temp_data, Moq.It.IsAny<SerializationOptions>())).Returns(temp_data_dictionary);
                                };

        Because of = () => temp_data =  temp_data_provider.LoadTempData(controller_context_mock.Object);

        It should_retrieve_the_serialized_temp_data_from_session = () => http_session_mock.VerifyAll();
        It should_use_the_serializer_to_deserialize_it = () => serializer_mock.VerifyAll();
        It should_return_the_temp_data_as_dictionary_of_objects = () => {
                                                                            temp_data.ShouldNotBeNull();
                                                                            temp_data.ContainsKey(TEMP_DATA_KEY).ShouldBeTrue();
                                                                        };
    }
}