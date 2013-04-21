using System;
using System.Web.Mvc;
using Bifrost.Serialization;
using Machine.Specifications;
using Moq;

namespace Bifrost.Web.Mvc.Specs.for_BifrostTempDataProvider.given
{
    public class a_temp_data_provider
    {
        protected static Mock<ISerializer> serializer_mock;
        protected static ITempDataProvider temp_data_provider;
        protected static string TEMP_DATA_SESSION_STATE_KEY = "__BifrostTempDataSessionStateKey";
        Establish context = () =>
                                {
                                    serializer_mock = new Mock<ISerializer>();
                                    temp_data_provider = new BifrostTempDataProvider(serializer_mock.Object);
                                };


    }
}