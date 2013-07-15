using Bifrost.Web.Configuration;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Configuration
{
    [Subject(typeof(NamespaceMapper))]
    public class when_adding_a_namespace_mapping : given.a_namespace_mapper
    {
        static string my_client_namespace;
        static string my_server_namespace;

        static bool can_resolve_to_client;
        static bool can_resolve_to_server;

        Establish context = () =>
            {
                my_client_namespace = "MyClient.**.";
                my_server_namespace = "Bifrost.Web.Specs.Configuration.**.";

                namespace_mapper.Add(my_client_namespace,my_server_namespace);
            };

        Because of = () =>
            {
                can_resolve_to_client = namespace_mapper.CanResolveToClient(my_server_namespace);
                can_resolve_to_server = namespace_mapper.CanResolveToServer(my_client_namespace);
            };

        It should_be_able_to_resolve_from_client_to_server = () => can_resolve_to_server.ShouldBeTrue();
        It should_be_able_to_resolve_from_server_to_client = () => can_resolve_to_client.ShouldBeTrue();
    }
}