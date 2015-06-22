using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
	[Subject(typeof(TypeImporter))]
	public class when_importing_single_types_and_multiple_are_available : given.a_type_importer
	{
		static Exception Exception;

		Because of = () => Exception = Catch.Exception(() =>
		{
            GetMock<ITypeDiscoverer>().Setup(t => t.FindSingle<IMultipleInterface>()).Throws(new ArgumentException());
		    type_importer.Import<IMultipleInterface>();
		});

        It should_throw_an_ArgumentException = () => Exception.ShouldBeOfExactType<ArgumentException>();
	}
}