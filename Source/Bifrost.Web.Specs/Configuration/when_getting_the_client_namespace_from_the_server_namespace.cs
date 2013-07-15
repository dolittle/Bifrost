using Bifrost.Web.Configuration;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Configuration
{
    [Subject(typeof(NamespaceMapper))]
    public class when_getting_the_client_namespace_from_the_server_namespace : given.a_namespace_mapper
    {
        static string resolved_client_namespace;

        Because of = () =>
            {
                resolved_client_namespace = namespace_mapper.GetClientNamespaceFrom(server_side_namespace);
            };

        It should_be_get_the_correct_client_namespace = () => resolved_client_namespace.ShouldStartWith(base_client_side_format);
    }
}