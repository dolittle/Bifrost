using System.Linq;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Bifrost.JSON.Serialization;
using Machine.Specifications;
using Moq;

namespace Bifrost.JSON.Specs.Serialization.for_SerializerContractResolver.given
{
	public class a_serializer_contract_resolver_that_ignores_saga_properties
	{
		static readonly string[] SagaProperties = typeof(ISaga).GetProperties().Select(t => t.Name).ToArray();

		static readonly SerializationOptions serialization_options =
			new SerializationOptions
			{
				ShouldSerializeProperty = (t, p) =>
				{
					if (typeof(ISaga).IsAssignableFrom(t))
						return !SagaProperties.Any(sp => sp == p);

					return true;
				}
			};

		protected static SerializerContractResolver contract_resolver;
		protected static Mock<IContainer> container_mock;

		Establish context = () =>
		                    	{
									container_mock = new Mock<IContainer>();
		                    		contract_resolver = new SerializerContractResolver(container_mock.Object, serialization_options);
		                    	};
	}
}
