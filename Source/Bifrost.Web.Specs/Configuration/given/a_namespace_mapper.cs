using Bifrost.Execution;
using Bifrost.Web.Configuration;
using Machine.Specifications;
using Moq;

namespace Bifrost.Web.Specs.Configuration.given
{
    public class a_namespace_mapper
    {
        protected static Mock<ITypeDiscoverer> type_discoverer;
        protected static NamespaceMapper namespace_mapper;
        protected static string client_side_namespace_format;
        protected static string server_type_namespace_format;
        protected static string base_client_side_format;
        protected static string base_server_side_format;

        protected static string client_side_namespace;
        protected static string server_side_namespace;

        Establish context = () =>
            {
                type_discoverer = new Mock<ITypeDiscoverer>();
                type_discoverer.Setup(d => d.FindTypeByFullName(typeof (AMappedServerType).FullName))
                               .Returns(typeof (AMappedServerType));

                client_side_namespace = "DefaultClient.Configuration.given.AMappedServerType";
                server_side_namespace = typeof (AMappedServerType).Namespace;

                namespace_mapper = new NamespaceMapper(type_discoverer.Object);
                base_client_side_format = "DefaultClient";
                client_side_namespace_format = Formatize(base_client_side_format);
                base_server_side_format = "Bifrost.Web.Specs";
                server_type_namespace_format = Formatize(base_server_side_format);

                namespace_mapper.Add(client_side_namespace_format,server_type_namespace_format);
            };

        static string Formatize(string baseNamespace)
        {
            return baseNamespace + ".**.";
        }
    }
}