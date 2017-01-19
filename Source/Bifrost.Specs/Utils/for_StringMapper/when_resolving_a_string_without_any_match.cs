using Bifrost.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    public class when_resolving_a_string_without_any_match
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;
        static string result;

        Establish context = () => 
        {
            first_mapping_mock = new Mock<IStringMapping>();
            first_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            second_mapping_mock = new Mock<IStringMapping>();
            second_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            mapper.AddMapping(first_mapping_mock.Object);
            mapper.AddMapping(second_mapping_mock.Object);
        };

        Because of = () => result = mapper.Resolve(input);

        It should_return_empty = () => result.ShouldEqual(string.Empty);
    }
}
