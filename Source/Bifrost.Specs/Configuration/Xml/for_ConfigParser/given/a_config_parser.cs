using Bifrost.Configuration.Xml;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser.given
{
    public class a_config_parser
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static ConfigParser parser;

        Establish context = () =>
                                {
                                    type_discoverer_mock = new Mock<ITypeDiscoverer>();
                                    type_discoverer_mock.Setup(t => t.FindMultiple<IConfigElement>()).Returns(new[]
                                                                                                                  {
                                                                                                                      typeof(ConfigObject),
                                                                                                                      typeof(ConfigObjectWithStringProperty),
                                                                                                                      typeof(ConfigObjectWithConfigObjectWithString)
                                                                                                                  });

                                    parser = new ConfigParser(type_discoverer_mock.Object);
                                };
    }
}
