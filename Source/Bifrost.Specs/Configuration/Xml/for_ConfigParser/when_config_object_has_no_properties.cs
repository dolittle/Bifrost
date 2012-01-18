using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
    public class when_config_object_has_no_properties : given.a_config_parser
    {
        static XDocument document;
        static ConfigObject config;

        Establish context = () =>
                                {
                                    document = XDocument.Parse("<ConfigObject/>");
                                };

        Because of = () => config = parser.Parse<ConfigObject>(document);

        It should_return_an_instance = () => config.ShouldNotBeNull();
    }
}
