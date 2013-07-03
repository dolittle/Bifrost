using System.Collections.Generic;
using System.Linq;
using Bifrost.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    [Subject(typeof(StringMapper))]
    public class when_getting_all_mappings_for_a_string_with_two_mappings_matching
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;
        static IEnumerable<IStringMapping> result;

        Establish context = () =>
            {
                first_mapping_mock = new Mock<IStringMapping>();
                first_mapping_mock.Setup(f => f.Matches(input)).Returns(true);
                second_mapping_mock = new Mock<IStringMapping>();
                second_mapping_mock.Setup(f => f.Matches(input)).Returns(true);
                mapper.AddMapping(first_mapping_mock.Object);
                mapper.AddMapping(second_mapping_mock.Object);
            };

        Because of = () => result = mapper.GetAllMatchingMappingsFor(input);

        It should_return_two_mappings= () => result.Count().ShouldEqual(2);
        It should_contain_the_first_mapping = () => result.First().ShouldEqual(first_mapping_mock.Object);
        It should_contain_the_second_mapping = () => result.Last().ShouldEqual(second_mapping_mock.Object);
    }
}