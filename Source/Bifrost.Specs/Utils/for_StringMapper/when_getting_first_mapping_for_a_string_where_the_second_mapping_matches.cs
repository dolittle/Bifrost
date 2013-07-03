using Bifrost.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    [Subject(typeof(StringMapper))]
    public class when_getting_first_mapping_for_a_string_where_the_second_mapping_matches
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;
        static IStringMapping result;

        Establish context = () => 
        {
            first_mapping_mock = new Mock<IStringMapping>();
            first_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            second_mapping_mock = new Mock<IStringMapping>();
            second_mapping_mock.Setup(f => f.Matches(input)).Returns(true);
            mapper.AddMapping(first_mapping_mock.Object);
            mapper.AddMapping(second_mapping_mock.Object);
        };

        Because of = () => result = mapper.GetFirstMatchingMappingFor(input);

        It should_return_correct_mapping = () => result.ShouldEqual(second_mapping_mock.Object);
    }
}
