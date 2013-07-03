using System;
using Bifrost.Web.Configuration;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Configuration
{
    [Subject(typeof(NamespaceMapper))]
    public class when_getting_the_server_type_from_the_client_namespace : given.a_namespace_mapper
    {
        static Type resolved_server_type;

        Because of = () =>
            {
                resolved_server_type = namespace_mapper.GetServerTypeFrom(client_side_namespace);
            };

        It should_be_get_the_correct_server_type = () => resolved_server_type.ShouldBeTheSameAs(typeof(given.AMappedServerType));
    }
}