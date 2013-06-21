using Bifrost.Utils;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    public class when_resolving_a_string_with_second_mapping_matching
    {
        const string input = "something";
        static StringMapper mapper = new StringMapper();
        static Mock<IStringMapping> first_mapping_mock;
        static Mock<IStringMapping> second_mapping_mock;

        Establish context = () => 
        {
            first_mapping_mock = new Mock<IStringMapping>();
            first_mapping_mock.Setup(f => f.Matches(input)).Returns(false);
            second_mapping_mock = new Mock<IStringMapping>();
            second_mapping_mock.Setup(f => f.Matches(input)).Returns(true);
            mapper.AddMapping(first_mapping_mock.Object);
            mapper.AddMapping(second_mapping_mock.Object);
        };

        Because of = () => mapper.Resolve(input);

        It should_ask_first_mapping = () => first_mapping_mock.Verify(m => m.Matches(input), Moq.Times.Once());
        It should_ask_second_mapping = () => second_mapping_mock.Verify(m => m.Matches(input), Moq.Times.Once());
        It should_not_resolve_using_first_mapping = () => first_mapping_mock.Verify(m => m.Resolve(input), Times.Never());
        It should_resolve_using_second_mapping = () => second_mapping_mock.Verify(m => m.Resolve(input), Times.Once());
    }
}
